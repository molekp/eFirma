using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BussinessLogic.AuthorizationLogic;
using BussinessLogic.DTOs.WarehouseDtos.Distributions;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using BussinessLogic.DatabaseLogic.Repositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Customer;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse;
using BussinessLogic.Helpers;
using BussinessLogic.Mappers.Warehouse;
using BussinessLogic.Mappers.Warehouse.Distributions;
using Database.Entities.Customers;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;
using ResourceLibrary;
using WebGrease.Css.Extensions;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Warehouse
{
    public class DistributionLogic : IDistributionLogic
    {
        public IDistributionRepository DistributionRepository { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public IItemRepository ItemRepository { get; set; }
        public IUserDataBaseRepository UserDataBaseRepository { get; set; }
        public SearchDistributionDtoMapper SearchDistributionDtoMapper { get; set; }
        public DisplayDistributionDtoMapper DisplayDistributionDtoMapper { get; set; }
        public PerformDistributionDtoMapper PerformDistributionDtoMapper { get; set; }
        public EditDistributionDtoMapper EditDistributionDtoMapper { get; set; }
        public DisplayItemDtoMapper DisplayItemDtoMapper { get; set; }
        public SimpleItemDtoMapper SimpleItemDtoMapper { get; set; }
        public IWarehouseRepository WarehouseRepository { get; set; }
        public IProductWarehouseRepository ProductWarehouseRepository { get; set; }
        public IServiceWarehouseRepository ServiceWarehouseRepository { get; set; }

        public IEnumerable<SearchDistributionDto> GetAllDistributionQueue()
        {
            var distributions = DistributionRepository.GetAll();

            return distributions.Select(x => SearchDistributionDtoMapper.MapEntityToDto(x)).ToList();
        }

        public DisplayDistributionDto GetDisplayDistribution(int a_distributionId)
        {
            var distribution = DistributionRepository.Get(a_distributionId);

            return DisplayDistributionDtoMapper.MapEntityToDto(distribution);
        }

        public PerformDistributionDto GetPerformDistribution(int a_distributionId)
        {
            var distribution = DistributionRepository.Get(a_distributionId);

            return PerformDistributionDtoMapper.MapEntityToDto(distribution);
        }

        public bool PerformDistribution(int a_idDistribution)
        {
            var distribution= DistributionRepository.Get(a_idDistribution);
            if (distribution == null) return false;
            distribution.State = DistributionState.Performed;
            bool edit= DistributionRepository.Edit(distribution);
            if (edit)
            {
                ChangeDistributionItemState(distribution.ProductItems, ItemState.Distributed);
                ChangeDistributionItemState(distribution.ServiceItems, ItemState.Distributed);
            }
            return edit;
        }

        public EditDistributionDto GetEditDistributionDto(int a_idDistribution)
        {
            var distribution = DistributionRepository.Get(a_idDistribution);
            if (distribution == null) return null;
            var customers = CustomerRepository.GetAll();
           var choicesCustomer = new List<SelectListItem>
                {
                        new SelectListItem {Selected = true, Text = Resource.choiceCustomer, Value = "0"}
                };
           choicesCustomer.AddRange(customers.Select(x =>
                {
                    var tmp =new SelectListItem
                        {
                                Selected = false,
                                Text = x.Name,
                                Value = x.IdCustomer.ToString()
                        };
                    if (distribution.DistributionCustomer!= null && x.IdCustomer == distribution.DistributionCustomer.IdCustomer) tmp.Selected = true;
                    return tmp;
                }).ToList());
            return EditDistributionDtoMapper.MapEntityToDto(distribution, choicesCustomer);
        }

        public bool RemoveProductItemFrom(int a_distributionId, int a_itemId, ItemTypeEnum a_itemType)
        {
            var distribution = DistributionRepository.Get(a_distributionId);
            if (distribution == null) return false;
            switch (a_itemType)
            {
                  case ItemTypeEnum.Product:
                    var product = distribution.ProductItems.FirstOrDefault(x => x.IdItem == a_itemId);
                    if (product == null) return false;
                    product.ItemState = ItemState.InWarehouse;
                    ItemRepository.EditProductItem(product);
                    try
                    {
                        distribution.ProductItems.Remove(product);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    break;
                 case ItemTypeEnum.Service:
                    var service = distribution.ServiceItems.FirstOrDefault(x => x.IdItem == a_itemId);
                    if (service == null) return false;
                    service.ItemState = ItemState.InWarehouse;
                    ItemRepository.EditServiceItem(service);
                    try{
                        distribution.ServiceItems.Remove(service);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    break;
                default:
                    return false;
            }
            
            return DistributionRepository.Edit(distribution);
        }

        public bool EditDistributionDto(EditDistributionDto a_distributionDto)
        {
            var distribution = DistributionRepository.Get(a_distributionDto.IdDistribution);
            if (distribution == null) return false;
            if (distribution.State != a_distributionDto.SelectedChoiceState)
            {
                switch (a_distributionDto.SelectedChoiceState)
                {
                    case DistributionState.Performed:
                        ChangeDistributionItemState(distribution.ProductItems, ItemState.Distributed);
                        break;
                    case DistributionState.Prepared:
                        ChangeDistributionItemState(distribution.ServiceItems, ItemState.PreDistributed);
                        break;
                }
            }
            distribution.State = a_distributionDto.SelectedChoiceState;
            distribution.DistributionCreateTime = a_distributionDto.DistributionCreateTime;
            distribution.DistributionTime = a_distributionDto.DistributionTime;
            var customer = CustomerRepository.Get(a_distributionDto.SelectedChoiceCustomer);
                distribution.DistributionCustomer = customer;
            return DistributionRepository.Edit(distribution);
        }

        public AddDistributionDto GetAddDistributionDto()
        {
            var distribution = new AddDistributionDto {DistributionTime = DateTime.Now, DistributionDate = DateTime.Now};
            var customers = CustomerRepository.GetAll();
            distribution.ChoicesCustomer = new List<SelectListItem>
                {
                        new SelectListItem {Selected = true, Text = Resource.choiceCustomer, Value = "0"}
                };
            distribution.ChoicesCustomer.AddRange(customers.Select(x => new SelectListItem
                {
                        Selected = false,
                        Text = x.Name + @" | "+x.Address,
                        Value = x.IdCustomer.ToString()
                }).ToList());
            distribution.ChoicesProvider = customers.Where(x => x is Firm).Select(x => new SelectListItem
                {
                        Selected = false,
                        Text = x.Name + @" | " + x.Address,
                        Value = x.IdCustomer.ToString()
                }).ToList();


            distribution.Items = ItemRepository.GetAll(ItemState.InWarehouse).ToList().Select(x =>
                {
                    var warehouse = WarehouseRepository.FindWarehouseForItem(x);
                    var itemWarehouseName = WarehouseRepository.FindItemWarehouseName(x);
                    return SimpleItemDtoMapper.MapEntityToDto(x, warehouse, itemWarehouseName);
                });
           return distribution;
        }

        public int AddDistribution(AddDistributionDto a_distributionDto, string a_loggedUserName)
        {
            var user = UserDataBaseRepository.GetUser(a_loggedUserName);
            if (user == null || a_distributionDto.SelectedItems == null || !a_distributionDto.SelectedItems.Any() ||
                a_distributionDto.SelectedProvider == 0)
                return -1;
            var productItems = new List<ProductItem>();
            var serviceItems = new List<ServiceItem>();
            foreach (var selectedItem in a_distributionDto.SelectedItems)
            {
                switch (selectedItem.ItemTypeEnum)
                {
                    case ItemTypeEnum.Product:
                        {
                            var product =ItemRepository.GetProduct(selectedItem.ItemId);
                            if (product == null || product.Quantity <= 0.0 || selectedItem.ItemQuantity<=0.0) return -1;
                            if (product.Quantity < selectedItem.ItemQuantity || selectedItem.ItemQuantity<=0) return 0;
                            if (Math.Abs(product.Quantity - selectedItem.ItemQuantity) < 0.001)
                            {
                                productItems.Add(product);
                            }
                            else
                            {
                                product.Quantity = product.Quantity - selectedItem.ItemQuantity;
                                ItemRepository.EditProductItem(product);
                                var newProduct = CreateProductItemCopy(product, selectedItem.ItemQuantity);
                                if(!ItemRepository.CreateProductItem(newProduct)) return -1;
                                productItems.Add(newProduct);
                            }
                        }
                        break;
                    case ItemTypeEnum.Service:
                        {
                            var service = ItemRepository.GetService(selectedItem.ItemId);
                            if (service == null || service.Quantity <= 0.0 || selectedItem.ItemQuantity<=0.0) return -1;
                            if (service.Quantity < selectedItem.ItemQuantity || selectedItem.ItemQuantity <= 0) return -1;
                            if (Math.Abs(service.Quantity - selectedItem.ItemQuantity) < 0.001)
                            {
                                serviceItems.Add(service);
                            }
                            else
                            {
                                service.Quantity = service.Quantity - selectedItem.ItemQuantity;
                                ItemRepository.EditServiceItem(service);
                                var newService = CreateServiceItemCopy(service, selectedItem.ItemQuantity);
                                if (!ItemRepository.CreateServiceItem(newService)) return-1;
                                serviceItems.Add(service);
                            }
                        }
                        break;
                }
            }
            ChangeDistributionItemState(productItems, ItemState.PreDistributed);
            ChangeDistributionItemState(serviceItems, ItemState.PreDistributed);
            var customer = CustomerRepository.Get(a_distributionDto.SelectedCustomer);
            var provider = CustomerRepository.GetFirm(a_distributionDto.SelectedProvider);
            var distribution = new Distribution
                {
                        DistributionCreator = user,
                        DistributionProvider = provider,
                        ProductItems = productItems,
                        ServiceItems = serviceItems,
                        State = DistributionState.Prepared,
                        DistributionCustomer = customer,
                        DistributionCreateTime = DateTime.Now,
                        DistributionTime = makeDate(a_distributionDto.DistributionDate, a_distributionDto.DistributionTime)
                };
            return DistributionRepository.Add(distribution);
        }

        

        private bool ChangeDistributionItemState(IEnumerable<IItem> items, ItemState a_state)
        {
            try
            {
                items.ForEach(x =>
                    {
                        x.ItemState = a_state;
                        if (x is ProductItem)
                        {
                            ItemRepository.EditProductItem((ProductItem) x);
                            var productWarehouse = WarehouseRepository.GetProductWarehouseForItem((ProductItem)x);
                            ProductWarehouseRepository.RemoveProductItemFromWarehouse(productWarehouse, (ProductItem)x);
                        }
                        else if (x is ServiceItem)
                        {
                            ItemRepository.EditServiceItem((ServiceItem) x);
                            var serviceWarehouse = WarehouseRepository.GetServiceWarehouseForItem((ServiceItem)x);
                            ServiceWarehouseRepository.RemoveServiceItemFromWarehouse(serviceWarehouse, (ServiceItem) x);
                        }
                    });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private DateTime makeDate(DateTime date, DateTime time)
        {
           return  new DateTime(date.Year,date.Month,date.Day,time.Hour,time.Minute,time.Second,time.Millisecond);
        }

        private ProductItem CreateProductItemCopy(ProductItem a_product, double a_itemQuantity)
        {
            return new ProductItem
            {
                Name = a_product.Name,
                Attributes = a_product.Attributes,
                ItemState = ItemState.PreDistributed,
                ExpirationTime = a_product.ExpirationTime,
                ItemType = a_product.ItemType,
                Price = a_product.Price,
                Quantity = a_itemQuantity,
                SaleType = a_product.SaleType,
                Vin = a_product.Vin
            };
        }

        private ServiceItem CreateServiceItemCopy(ServiceItem serviceItem, double a_itemQuantity)
        {
            return new ServiceItem
            {
                Name = serviceItem.Name,
                Attributes = serviceItem.Attributes,
                ItemState = ItemState.PreDistributed,
                ExpirationTime = serviceItem.ExpirationTime,
                ItemType = serviceItem.ItemType,
                Price = serviceItem.Price,
                Quantity = a_itemQuantity,
                SaleType = serviceItem.SaleType,
                Vin = serviceItem.Vin
            };
        }
    }
}
