using System.Collections.Generic;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.Inventary;
using BussinessLogic.DTOs.ViewModelsOnly;
using BussinessLogic.DatabaseLogic.Repositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.InventaryRepositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.TaxRepositories;
using BussinessLogic.Mappers;
using BussinessLogic.Mappers.Inventary;
using BussinessLogic.Mappers.Warehouse.Supplies;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces
{
    public interface IItemManagementLogic
    {
        IItemRepository ItemRepository { get; set; }
        IWarehouseRepository WarehouseRepository { get; set; }
        IProductWarehouseRepository ProductWarehouseRepository { get; set; }
        IServiceWarehouseRepository ServiceWarehouseRepository { get; set; }
        IProductTypeRepository ProductTypeRepository { get; set; }
        ITaxRepository TaxRepository { get; set; }
        ISaleTypeRepository SaleTypeRepository { get; set; }
        IAttributeTypeRepository AttributeTypeRepository { get; set; }
        IAttributeRepository AttributeRepository { get; set; }

        SearchItemDtoMapper SearchSearchItemDtoDtoMapper { get; set; }
        DisplayProductDtoMapper DisplayProductDtoMapper { get; set; }
        DisplayProductAttributeDtoMapper DisplayProductAttributeDtoMapper { get; set; }
        ManageProductDtoMapper ManageProductDtoMapper { get; set; }
        ManageProductAttributeDtoMapper ManageProductAttributeDtoMapper { get; set; }
        SearchItemByAttributeDtoMapper SearchItemByAttributeDtoMapper { get; set; }

        List<SearchItemDto> GetAllItemsForSearch();
        string GetTypeOfItem(int a_itemId);
        DisplayProductDto GetDisplayProductDto(int a_idProduct);
        DisplayServiceDto GetDisplayServiceDto(int a_serviceId);
        List<SearchItemByAttributeDto> GetAllItemsForSearchByAttribute();
        ManageProductDto GetManageProductDto(int a_idProduct);
        bool EditProductItem(ManageProductDto a_productDto);
        bool RemoveAttributeFromProduct(RemoteAttributeModel a_removeAttributeModel);
        bool RemoveItemFromProductWarehouse(RemoveItemFromSpecyficWarehouse a_removeItem);
    }
}
