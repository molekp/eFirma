using System.Collections.Generic;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.EStore.EStoreDisplay
{
    public interface IEStoreDisplayRepository
    {
        IEnumerable<ProductItem> GetProducts(int a_idEStore);
        IEnumerable<ServiceItem> GetServices(int a_idEStore);

        IEnumerable<ProductType> GetCollectionOfProductType(int a_idEStore);
        IEnumerable<ServiceType> GetCollectionOfServiceType(int a_idEStore);

        ProductItem GetProduct(int a_item);
        ServiceItem GetService(int a_item);
    }
}