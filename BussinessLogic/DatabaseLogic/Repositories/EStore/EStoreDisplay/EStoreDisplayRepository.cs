using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.EStore.EStoreDisplay;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DatabaseLogic.Repositories.EStore.EStoreDisplay
{
    public class EStoreDisplayRepository : IEStoreDisplayRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public IEnumerable<ProductItem> GetProducts(int a_idEStore)
        {
            var result = new List<ProductItem>();

            var warehose = DataBaseContext.EStore.FirstOrDefault(c => c.IdEStore == a_idEStore).Warehouses;

            foreach (var item in warehose)
            {
                foreach (var subItem in item.ProductWarehouses)
                {
                    result.AddRange(subItem.ProductItems);
                }
            }

            return result;
        }

        public IEnumerable<ServiceItem> GetServices(int a_idEStore)
        {
            var result = new List<ServiceItem>();

            var warehose = DataBaseContext.EStore.FirstOrDefault(c => c.IdEStore == a_idEStore).Warehouses;

            foreach (var item in warehose)
            {
                foreach (var subItem in item.ServiceWarehouses)
                {
                    result.AddRange(subItem.ServiceItems);
                }
            }

            return result;
        }

        public IEnumerable<ProductType> GetCollectionOfProductType(int a_idEStore)
        {
            var items = this.GetProducts(a_idEStore);
            var itemsType = items.Select(c => c.ItemType).ToList();
            var result = itemsType.Distinct().ToList();

            return result;
        }

        public IEnumerable<ServiceType> GetCollectionOfServiceType(int a_idEStore)
        {
            var items = this.GetServices(a_idEStore);
            var itemsType = items.Select(c => c.ItemType).ToList();
            var result = itemsType.Distinct().ToList();

            return result;
        }

        public ProductItem GetProduct(int a_item)
        {
            var result =  DataBaseContext.ProductItems.FirstOrDefault(c => c.IdItem == a_item);
            return result;
        }

        public ServiceItem GetService(int a_item)
        {
            return DataBaseContext.ServiceItems.FirstOrDefault(c => c.IdItem == a_item);
        }
    }
}