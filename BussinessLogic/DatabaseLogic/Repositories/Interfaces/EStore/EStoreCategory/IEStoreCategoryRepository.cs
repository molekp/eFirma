using System.Collections.Generic;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;
using Entit = Database.Entities.EStore;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.EStore.EStoreCategory
{
    public interface IEStoreCategoryRepository
    {
        IEnumerable<Entit.Category.EStoreCategory> GetAll();
        IEnumerable<Entit.Category.EStoreCategory> GetAllOfEStore(Entit.EStore a_eStore);

        bool Add(Entit.Category.EStoreCategory a_category);
        bool Remove(Entit.Category.EStoreCategory a_category);
        bool Edit(Database.Entities.EStore.Category.EStoreCategory a_category);
        Entit.Category.EStoreCategory Get(int a_category);

        bool AddItem(Entit.Category.EStoreCategory a_category, IItem a_item);
        bool RemoveItem(Entit.Category.EStoreCategory a_category, IItem a_item);
        ProductItem GetProductItem(int a_idProductItem);
        ServiceItem GetServiceItem(int a_idServiceItem);
    }
}