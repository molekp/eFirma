using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.SupplyRepositories
{
    
    public interface ISupplyRepository
    {
        IDataBaseContext DataBaseContext { get; set; }
        bool AddSupply(Supply a_supplyEntity);
        IEnumerable<Supply> GetAllSupplies();
        Supply GetSupply(int a_idSupply);
        bool SaveSupply(Supply a_mapDtoToEntity);
        bool RemoveSupply(Supply a_getSupply);
        bool SendSupply(int a_idSupply);
        int AddProduct(ProductItem a_productItem);
        bool AddProductToSupply(int a_idSupply, int a_idProduct);
        bool ReadyToSendSupply(int a_idSupply);
        ProductItem GetProduct(int a_idProduct);
        int StoreProduct(ProductItem a_entity);
        bool RemoveProductFromSupply(Supply a_getSupply, ProductItem a_getProduct);
        bool RemoveProduct(int a_idProduct);
        void setState(int state, Supply supply);
    }
}