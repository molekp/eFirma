using System.Collections.Generic;
using System.Web.Mvc;
using BussinessLogic.DTOs.WarehouseDtos;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using BussinessLogic.Mappers.Warehouse;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse
{
    public interface IWarehouseLogic
    {
        IWarehouseRepository WarehouseRepository { get; set; }
        IProductWarehouseRepository ProductWarehouseRepository { get; set; }
        IServiceWarehouseRepository ServiceWarehouseRepository { get; set; }
        AddWarehouseDtoMapper AddWarehouseDtoMapper { get; set; }
        DisplayWarehouseMapper DisplayWarehouseMapper { get; set; }
        EditWarehouseDtoMapper EditWarehouseDtoMapper { get; set; }
        DisplaySpecyficWarehouseMapper DisplaySpecyficWarehouseMapper { get; set; }

        bool AddWarehouse(AddWarehouseDto a_addWarehouse);
        IEnumerable<DisplayWarehouseDto> GetAllWarehouses();
        EditWarehouseDto GetWarehouseForManage(int a_idWarehouse);
        bool EditWarehouse(EditWarehouseDto a_editWarehouse);
        bool RemoveProductWarehouseFromWarehouse(int a_idWarehouse, int a_idProductWarehouse);
        bool RemoveServiceWarehouseFromWarehouse    (int a_idWarehouse, int a_idServiceWarehouse);
        ManageProductWarehouseDto GetProductWarehouseForManage(int a_idProductWarehouse, int a_idWarehouse);
        bool EditProductWarehouse(ManageProductWarehouseDto a_manageProductWarehouse);
        bool RemoveProductItemFromProductWarehouse(int a_itemid, int a_idProductWarehouse);
    }
}