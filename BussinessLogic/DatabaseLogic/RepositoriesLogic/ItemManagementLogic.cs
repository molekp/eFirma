using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BussinessLogic.DTOs.Inventary;
using BussinessLogic.DTOs.ViewModelsOnly;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DTOs;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.InventaryRepositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.TaxRepositories;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces;
using BussinessLogic.Helpers;
using BussinessLogic.Mappers;
using BussinessLogic.Mappers.Inventary;
using System.Linq;
using BussinessLogic.Mappers.Warehouse.Supplies;
using Database.Entities.WarehouseEntities;
using ResourceLibrary;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic
{
    public class ItemManagementLogic : IItemManagementLogic
    {
        public IItemRepository ItemRepository { get; set; }
        public IWarehouseRepository WarehouseRepository { get; set; }
        public IProductWarehouseRepository ProductWarehouseRepository { get; set; }
        public IServiceWarehouseRepository ServiceWarehouseRepository { get; set; }
        public IProductTypeRepository ProductTypeRepository { get; set; }
        public ITaxRepository TaxRepository { get; set; }
        public ISaleTypeRepository SaleTypeRepository { get; set; }
        public IAttributeTypeRepository AttributeTypeRepository { get; set; }
        public IAttributeRepository AttributeRepository { get; set; }
        public SearchItemDtoMapper SearchSearchItemDtoDtoMapper { get; set; }
        public DisplayProductDtoMapper DisplayProductDtoMapper { get; set; }
        public DisplayProductAttributeDtoMapper DisplayProductAttributeDtoMapper { get; set; }
        public ManageProductDtoMapper ManageProductDtoMapper { get; set; }
        public ManageProductAttributeDtoMapper ManageProductAttributeDtoMapper { get; set; }
        public SearchItemByAttributeDtoMapper SearchItemByAttributeDtoMapper { get; set; }

        public List<SearchItemDto> GetAllItemsForSearch()
        {
            var warehouses = WarehouseRepository.GetAll();

            return (from warehouse in warehouses
                    from productWarehouse in warehouse.ProductWarehouses
                    from productItem in productWarehouse.ProductItems
                    select SearchSearchItemDtoDtoMapper.MapEntityToDto(productItem, warehouse.Name, ConstantsHelper.PRODUCT_WAREHOUSE,productWarehouse.IdProductWarehouse, productWarehouse.Name)).ToList();
        }

        public List<SearchItemByAttributeDto> GetAllItemsForSearchByAttribute()
        {
            var productItems = ItemRepository.GetAllProductItems();
            var servicesItems = ItemRepository.GetAllServiceItems();

            var searchItems = (from productItem in productItems
                               from attribute in productItem.Attributes
                               select SearchItemByAttributeDtoMapper.MapEntityToDto(productItem, attribute)).ToList();

            searchItems.AddRange((from serviceItem in servicesItems
                                  from attribute in serviceItem.Attributes
                                  select SearchItemByAttributeDtoMapper.MapEntityToDto(serviceItem, attribute)).ToList());

            return searchItems;
        }

        public string GetTypeOfItem(int a_itemId)
        {
            var product = ItemRepository.GetProduct(a_itemId);
            if (product != null) return ConstantsHelper.PRODUCT_ITEM;

            var service = ItemRepository.GetService(a_itemId);
            if (service != null) return ConstantsHelper.SERVICE_ITEM;

            return null;
        }

        public DisplayProductDto GetDisplayProductDto(int a_idProduct)
        {
            var product = ItemRepository.GetProduct(a_idProduct);

            var displayAttributes =
                    product.Attributes.Select(x => DisplayProductAttributeDtoMapper.MapEntityToDto(x)).ToList();

            return DisplayProductDtoMapper.MapEntityToDto(product, displayAttributes);
        }

        public DisplayServiceDto GetDisplayServiceDto(int a_serviceId)
        {
            var service = ItemRepository.GetService(a_serviceId);

            return null;
        }

        

        public ManageProductDto GetManageProductDto(int a_idProduct)
        {
            var product = ItemRepository.GetProduct(a_idProduct);

            var manageAttributes =
                    product.Attributes.Select(x => ManageProductAttributeDtoMapper.MapEntityToDto(x)).ToList();

            var choicesProductTypes = new List<SelectListItem>{new SelectListItem{Selected = true,Text = Resource.SelectListChooseItem,Value = "0"}};
            var productTypes = ProductTypeRepository.GetAllBut(product.ItemType.IdItemType);
            choicesProductTypes.AddRange(productTypes.Select(x=>new SelectListItem
                {
                        Selected = false,Text = x.Name,Value = x.IdItemType.ToString()
                }).ToList());

            //var choicesTaxes = new List<SelectListItem> { new SelectListItem { Selected = true, Text = Resource.SelectListChooseItem, Value = "0" } };
            //var taxes = TaxRepository.GetAllTaxes().ToList().Remove(product.);
            //choicesTaxes.AddRange(taxes.Select(x => new SelectListItem
            //{
            //    Selected = false,
            //    Text = x.TaxName + @" | value="+x.TaxValue,
            //    Value = x.IdTax.ToString()
            //}).ToList());

            var choicesSaleTypes = new List<SelectListItem> { new SelectListItem { Selected = true, Text = Resource.SelectListChooseItem, Value = "0" } };
            var saleTypes = SaleTypeRepository.GetAllSaleTypesBut(product.SaleType.IdSaleType);
            choicesSaleTypes.AddRange(saleTypes.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.Name,
                Value = x.IdSaleType.ToString()
            }).ToList());

            var choicesAttributeTypes = new List<SelectListItem> { new SelectListItem { Selected = true, Text = Resource.SelectListChooseItem, Value = "0" } };
            var attributeTypes = AttributeTypeRepository.GetAll();
            choicesAttributeTypes.AddRange(attributeTypes.Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.Name,
                Value = x.IdAttributeType.ToString()
            }).ToList());

            return ManageProductDtoMapper.MapEntityToDto(product, manageAttributes,choicesProductTypes,choicesSaleTypes,choicesAttributeTypes);
        }

        public bool EditProductItem(ManageProductDto a_productDto)
        {
            var product =ItemRepository.GetProduct(a_productDto.IdProduct);

            product.Name = a_productDto.Name;
            product.Price = a_productDto.Price;
            product.Quantity = a_productDto.Quantity;

            var productType = ProductTypeRepository.GetProductType(a_productDto.SelectedChoiceIdProductType);
            if (null != productType )
                product.ItemType =productType;

            var saleType = SaleTypeRepository.Get(a_productDto.SelectedChoiceIdSaleType);
            if (null != saleType)
                product.SaleType = saleType;

            var attributeType = AttributeTypeRepository.Get(a_productDto.AddAttributeDto.SelectedChoiceIdAttributeType);
            if(null != attributeType && false == a_productDto.AddAttributeDto.Value.Equals(""))
                product.Attributes.Add(new ValueAttributeType
                    {
                            Name = attributeType.Name,
                            Value = a_productDto.AddAttributeDto.Value
                    });

            DateTime expiration = a_productDto.ExpirationDate;
            TimeSpan ts = new TimeSpan(a_productDto.ExpirationTime.Hour, a_productDto.ExpirationTime.Minute, 0);
            expiration = expiration.Date + ts;
            product.ExpirationTime = expiration;
            

            return ItemRepository.EditProductItem(product); 
        }

        public bool RemoveAttributeFromProduct(RemoteAttributeModel a_removeAttributeModel)
        {
            var product = ItemRepository.GetProduct(a_removeAttributeModel.IdItem);
            if (null == product) return false;

            var attribute = product.Attributes.FirstOrDefault(x => x.IdAttributeType == a_removeAttributeModel.IdAttribute);
            if (null == attribute) return false;

            product.Attributes.Remove(attribute);
            if (false == ItemRepository.EditProductItem(product)) return false;

            return AttributeRepository.RemoveAttribute(attribute);
        }

        public bool RemoveItemFromProductWarehouse(RemoveItemFromSpecyficWarehouse a_removeItem)
        {
            var productWarehouse = ProductWarehouseRepository.Get(a_removeItem.IdSpecyficWarehouse);
            if (null == productWarehouse) return false;

            var product = productWarehouse.ProductItems.FirstOrDefault(x => x.IdItem == a_removeItem.IdItem);
            if (null == product) return false;

            if (false == productWarehouse.ProductItems.Remove(product)) return false;

            if (false == ItemRepository.Remove(product)) return false;

            return ProductWarehouseRepository.Edit(productWarehouse);
        }
    }
}
