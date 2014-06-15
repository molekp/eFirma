using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BussinessLogic.DTOs.Store;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Customer;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Store;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Store;
using BussinessLogic.Mappers.Store;
using Database.Entities.Stores;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Store
{
    public class StoreLogic : IStoreLogic
    {
        public IStoreRepository StoreRepository { get; set; }
        public DisplayStoreDtoMapper DisplayStoreDtoMapper { get; set; }
        public SimpleDisplayStoreDtoMapper SimpleDisplayStoreDtoMapper { get; set; }
        public StoreSellDtoMapper StoreSellDtoMapper { get; set; }
        public IDistributionRepository DistributionRepository { get; set; }
        public SoldItemDtoMapper SoldItemDtoMapper { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public IComplaintRepository ComplaintRepository { get; set; }
        public IItemRepository ItemRepository { get; set; }
        public IProductWarehouseRepository ProductWarehouseRepository { get; set; }
        public IServiceWarehouseRepository ServiceWarehouseRepository { get; set; }

        public SimpleDisplayStoreDto GetDisplayStoreDto(int a_storeId)
        {
            var store = StoreRepository.Get(a_storeId);
            if (store == null) return null;
            return SimpleDisplayStoreDtoMapper.MapEntityToDto(store);
        }

        public StoreSellDto GetStoreSellDto(int a_storeId, int a_distributionId)
        {
            var store = StoreRepository.Get(a_storeId);
            if (store == null) return null;
            var distribution = DistributionRepository.Get(a_distributionId);
            if (distribution == null) return null;
            var items= DistributionRepository.GetDistributionItems(a_distributionId);
            return StoreSellDtoMapper.MapEntityToDto(store, distribution.IdDistribution, items);
        }

        public IEnumerable<SoldItemDto> GetAllSoldItemDto(int a_storeId)
        {
            var store = StoreRepository.Get(a_storeId);
            if (store == null) return null;
            var items =new List<SoldItemDto>();
            foreach (var distribution in store.Distributions)
            {
                items.AddRange(distribution.ProductItems.Select(x => SoldItemDtoMapper.MapEntityToDto(x, distribution)).ToList());
                items.AddRange(distribution.ServiceItems.Select(x => SoldItemDtoMapper.MapEntityToDto(x, distribution)).ToList());
            }
            return items;
        }

        public ComplaintDto GetComplaintDto(int a_distributionId, int a_itemId, ItemTypeEnum a_itemType)
        {
            var distribution = DistributionRepository.Get(a_distributionId);
            if (distribution == null) return null;
            if (ComplaintRepository.DoesExists(a_itemId, a_itemType)) return null;

            var result = new ComplaintDto();
            switch (a_itemType)
            {
                case ItemTypeEnum.Product:
                    if (distribution.ProductItems.FirstOrDefault(x => x.IdItem == a_itemId) == null) return null;
                        result.ItemId = a_itemId;
                        result.ItemType = a_itemType;
                        result.DistributionId = a_distributionId;
                        if (distribution.DistributionCustomer != null)
                        {
                            result.CustomerName = distribution.DistributionCustomer.Name;
                        }
                        else
                        {
                            result.ChoicesCustomer = GetChoicesCustomer();
                        }
                    break;
                case ItemTypeEnum.Service:
                    if (distribution.ServiceItems.FirstOrDefault(x => x.IdItem == a_itemId) == null) return null;
                        result.ItemId = a_itemId;
                        result.ItemType = a_itemType;
                        result.DistributionId = a_distributionId;
                        if (distribution.DistributionCustomer != null)
                        {
                            result.CustomerName = distribution.DistributionCustomer.Name;
                        }
                        else
                        {
                            result.ChoicesCustomer = GetChoicesCustomer();
                        }
                    break;

            }
            return result;
        }

        public bool AddComplaint(ComplaintDto a_complaintDto)
        {
            var distribution = DistributionRepository.Get(a_complaintDto.DistributionId);
            if (a_complaintDto.ComplaintDescription == null || a_complaintDto.ComplaintDescription.Equals(""))
                return false;
            var complaint = new Complaint();
            complaint.ComplaintDate = DateTime.Now;
            complaint.IsResolved = false;
            complaint.Description = a_complaintDto.ComplaintDescription;
            if (distribution.DistributionCustomer != null)
            {
                complaint.Customer = distribution.DistributionCustomer;
            }
            else
            {
                complaint.Customer = CustomerRepository.Get(a_complaintDto.SelectedCustomer);
            }
            switch (a_complaintDto.ItemType)
            {
                case ItemTypeEnum.Product:
                    complaint.ProductItem = ItemRepository.GetProduct(a_complaintDto.ItemId);
                    break;
                case ItemTypeEnum.Service:
                    complaint.ServiceItem = ItemRepository.GetService(a_complaintDto.ItemId);
                    break;
                default:
                    return false;
            }
            return ComplaintRepository.AddComplaint(complaint);
        }

        public ReturnDto GetReturnDto(int a_distributionId, int a_itemId, ItemTypeEnum a_itemType)
        {
            var dto = new ReturnDto
                {
                        ItemId = a_itemId,
                        ItemType = a_itemType,
                        DistributionId = a_distributionId
                };
            switch (a_itemType)
            {
                case ItemTypeEnum.Product:
                    dto.ChoicesWarehouses = ProductWarehouseRepository.GetAll().Select(x => new SelectListItem
                        {
                                Selected = false,
                                Text = x.Name,
                                Value = x.IdProductWarehouse.ToString()
                        }).ToList();
                    break;
                case ItemTypeEnum.Service:
                    dto.ChoicesWarehouses = ServiceWarehouseRepository.GetAll().Select(x => new SelectListItem
                        {
                                Selected = false,
                                Text = x.Name,
                                Value = x.IdServiceWarehouse.ToString()
                        }).ToList();
                    break;
            }
            return dto;
        }

        public bool ReturnItem(ReturnDto a_returnDto)
        {
            var distribution = DistributionRepository.Get(a_returnDto.DistributionId);
            if (distribution == null) return false;
            switch (a_returnDto.ItemType)
            {
                case ItemTypeEnum.Product:
                    var productWarehouse = ProductWarehouseRepository.Get(a_returnDto.SelectedWarehouse);
                    if (productWarehouse == null) return false;
                    var productItem = ItemRepository.GetProduct(a_returnDto.ItemId);
                    if (productItem == null) return false;
                    if (ProductWarehouseRepository.Add(productWarehouse, productItem))
                    {
                        productItem.ItemState = ItemState.InWarehouse;
                        ItemRepository.EditProductItem(productItem);
                        return DistributionRepository.RemoveProductItem(distribution, productItem);
                    }
                    return false;
                case ItemTypeEnum.Service:
                    var serviceWarehouse = ServiceWarehouseRepository.Get(a_returnDto.SelectedWarehouse);
                    if (serviceWarehouse == null) return false;
                    var serviceItem = ItemRepository.GetService(a_returnDto.ItemId);
                    if (serviceItem == null) return false;
                    if (ServiceWarehouseRepository.Add(serviceWarehouse, serviceItem))
                    {
                        serviceItem.ItemState = ItemState.InWarehouse;
                        ItemRepository.EditServiceItem(serviceItem);
                        return DistributionRepository.RemoveServiceItem(distribution, serviceItem);
                    }
                    return false;
                default:
                    return false;
            }
        }

        public bool StoreExists(int a_storeId)
        {
            return StoreRepository.Get(a_storeId) != null;
        }

        public IEnumerable<DisplayStoreDto> GetAllDisplayStoreDto()
        {
            var stores = StoreRepository.GetAll();

            return stores.Select(x => DisplayStoreDtoMapper.MapEntityToDto(x));
        }

        private IEnumerable<SelectListItem> GetChoicesCustomer()
        {
            return CustomerRepository.GetAll().Select(x => new SelectListItem
                {
                        Selected = false,
                        Text = x.Name + @" | " + x.Address,
                        Value = x.IdCustomer.ToString()
                }).ToList();
        } 
    }
}
