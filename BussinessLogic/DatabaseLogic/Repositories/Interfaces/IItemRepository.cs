using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces
{
    public interface IItemRepository
    {
        IDataBaseContext DataBaseContext { get; set; }

        /// <summary>
        /// Zwraca listę wszystkich item
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductItem> GetAllProductItems();
        IEnumerable<ServiceItem> GetAllServiceItems();
        ProductItem GetProduct(int a_itemId);
        ServiceItem GetService(int a_itemId);
        bool EditProductItem(ProductItem a_product);
        bool Remove(ProductItem a_product);
        IEnumerable<IItem> GetAll();
        bool EditServiceItem(ServiceItem a_service);
        IEnumerable<IItem> GetAll(ItemState a_inWarehouse);
        bool CreateProductItem(ProductItem a_productItem);
        bool CreateServiceItem(ServiceItem a_serviceItem);
        bool StoreProduct(ProductItem a_oldProductEntity, ProductItem a_newProductEntity);
    }
}
