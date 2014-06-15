using System.Collections.Generic;
using System.Linq;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories
{
    public interface IWarehouseRepository
    {
        IDataBaseContext DataBaseContext { get; set; }
        IEnumerable<Warehouse> GetAll();
        Warehouse Get(int a_idWarehouse);
        bool AddWarehouse(Warehouse a_newWarehouse);
        bool EditWarehouse(Warehouse a_warehouse);
        bool RemoveProductWarehouseFromWarehouse(Warehouse a_warehouse, ProductWarehouse a_productWarehouse);
        bool RemoveServiceWarehouseFromWarehouse(Warehouse a_warehouse, ServiceWarehouse a_serviceWarehouse);
        bool AddProductToProductWarehouse(ProductItem a_entity, int a_idProductWarehouse);
        ProductWarehouse GetProductWarehouse(int a_idProductWarehouse);
        Warehouse FindWarehouseForItem(IItem a_item);
        string FindItemWarehouseName(IItem a_item);
        ProductWarehouse GetProductWarehouseForItem(ProductItem a_productItem);
        ServiceWarehouse GetServiceWarehouseForItem(ServiceItem a_serviceItem);
    }
}