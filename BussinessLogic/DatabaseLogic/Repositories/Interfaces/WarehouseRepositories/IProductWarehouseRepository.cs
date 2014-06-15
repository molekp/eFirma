using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories
{
    public interface IProductWarehouseRepository
    {
        IDataBaseContext DataBaseContext { get; set; }

        ProductWarehouse Get(int a_idProductWarehouse);
        IEnumerable<ProductWarehouse> GetAll();
        IEnumerable<ProductWarehouse> GetAllFreeProductWarehouses();
        bool Edit(ProductWarehouse a_productWarehouse);
        bool Add(ProductWarehouse a_productWarehouse, ProductItem a_productItem);
        bool RemoveProductItemFromWarehouse(ProductWarehouse a_productWarehouse, ProductItem a_productItem);
    }
}