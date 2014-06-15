using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.EStore.EStoreCategory;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DatabaseLogic.Repositories.EStore.EStoreCategory
{
    public class EStoreCategoryRepository : IEStoreCategoryRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public IEnumerable<Database.Entities.EStore.Category.EStoreCategory> GetAll()
        {
            return DataBaseContext.EStoreCategory.ToList();
        }

        public IEnumerable<Database.Entities.EStore.Category.EStoreCategory> GetAllOfEStore(Database.Entities.EStore.EStore a_eStore)
        {
            return DataBaseContext.EStoreCategory.Where(c=>c.EStore.IdEStore == a_eStore.IdEStore).ToList();
        }

        public bool Add(Database.Entities.EStore.Category.EStoreCategory a_category)
        {
            try
            {
                DataBaseContext.EStoreCategory.Add(a_category);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Remove( Database.Entities.EStore.Category.EStoreCategory a_category)
        {
            try
            {
                DataBaseContext.EStoreCategory.Remove(a_category);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Edit(Database.Entities.EStore.Category.EStoreCategory a_category)
        {
            try
            {
                DataBaseContext.SetModified(a_category);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Database.Entities.EStore.Category.EStoreCategory Get(int a_category)
        {
            return DataBaseContext.EStoreCategory.FirstOrDefault(c => c.IdEStoreCategory == a_category);
        }

        public bool AddItem(Database.Entities.EStore.Category.EStoreCategory a_category, IItem a_item)
        {
            try
            {
                if(a_item is ProductItem)
                {
                    a_category.ProductItems.Add(a_item as ProductItem);
                }
                else if (a_item is ServiceItem)
                {
                    a_category.ServiceItems.Add(a_item as ServiceItem);
                }
                
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool RemoveItem(Database.Entities.EStore.Category.EStoreCategory a_category, IItem a_item)
        {
            try
            {
                if (a_item is ProductItem)
                {
                    a_category.ProductItems.Remove(a_item as ProductItem);
                }
                else if (a_item is ServiceItem)
                {
                    a_category.ServiceItems.Remove(a_item as ServiceItem);
                }

                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ProductItem GetProductItem(int a_idProductItem)
        {
            return DataBaseContext.ProductItems.SingleOrDefault(c => c.IdItem == a_idProductItem);
        }

        public ServiceItem GetServiceItem(int a_idServiceItem)
        {
            return DataBaseContext.ServiceItems.SingleOrDefault(c => c.IdItem == a_idServiceItem);
        }
    }
}