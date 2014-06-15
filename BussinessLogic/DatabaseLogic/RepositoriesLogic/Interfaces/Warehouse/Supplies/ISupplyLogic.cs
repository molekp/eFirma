using System.Collections.Generic;
using System.Web.Mvc;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos.Supplies;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.SupplyRepositories;
using BussinessLogic.Mappers;
using BussinessLogic.Mappers.Warehouse;
using BussinessLogic.Mappers.Warehouse.Product;
using BussinessLogic.Mappers.Warehouse.Supplies;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse.Supplies
{
    public interface ISupplyLogic
    {
        IProductTypeRepository ProductTypesRepository { get; set; }
        IWarehouseRepository WarehouseRepository { get; set; }
        ISaleTypeRepository SaleTypesRepository { get; set; }
        IAttributeRepository AttributesRepository { get; set; }
        ISupplyRepository SupplyRepository { get; set; }
        IItemRepository ItemRepository { get; set; }
        AttributeTypeMapper AttributeTypeMapper { get; set; }
        AttributeMapper AttributeMapper { get; set; }
        SupplyAddMapper SupplyAddMapper { get; set; }
        SupplyViewMapper SupplyViewMapper { get; set; }
        ProductAddMapper ProductAddMapper { get; set; }
        ProductMapper ProductMapper { get; set; }
        SupplyMapper SupplyMapper { get; set; }
        SupplyEditMapper SupplyEditMapper { get; set; }
        ProductWarehousesMapper ProductWarehousesMapper { get; set; }
        WarehousesMapper WarehousesMapper { get; set; }

        List<SelectListItem> GetProductTypes(int a_idProductType);
        List<SelectListItem> GetSaleTypes();
        List<AttributeTypeDto> GetAttributeTypes(int a_idProductType);
        bool AddSupply(SupplyAddDto a_addSupplyDto);
        List<SupplyDto> GetAllSupplies();
        SupplyEditDto GetSupply(int a_idSupply);
        bool SaveSupply(SupplyEditDto a_supplyEditDto);
        bool RemoveSupply(int a_idSupply);
        bool SendSupply(int a_idSupply);
        int AddProduct(ProductAddDto a_productAddDto);
        bool AddProductToSupply(int a_idSupply, int a_idProduct);
        List<AttributeDto> GetAttributes(int a_idProductType);
        ProductDto StoreProduct(int a_idProduct);
        int StoreProduct(ProductDto a_productDto);
        SupplyViewDto ViewSupply(int a_idSupply);
        List<SelectListItem> GetWarehousesWithProducts(int a_idWarehouse);
        List<SelectListItem> GetProductWarehouses(int a_idWarehouse, int a_idProductWarehouse);
        bool RemoveProduct(int a_idProduct, int a_idSupply);
        bool RemoveProductFromSupply(int a_idProduct, int a_idSupply);
    }
}