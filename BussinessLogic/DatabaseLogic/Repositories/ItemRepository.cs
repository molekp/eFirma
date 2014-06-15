using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using Database.Core.Interfaces;
using Database.Entities;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DatabaseLogic.Repositories
{
    public class ItemRepository : IItemRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        /// <summary>
        /// Zwraca listę wszystkich itemów
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductItem> GetAllProductItems()
        {
            return DataBaseContext.ProductItems.OrderBy(r => r.Name).ToList();
        }

        public IEnumerable<ServiceItem> GetAllServiceItems()
        {
            return DataBaseContext.ServiceItems.OrderBy(r => r.Name).ToList();
        }


        public ProductItem GetProduct(int a_itemId)
        {
            return DataBaseContext.ProductItems.FirstOrDefault(x => x.IdItem == a_itemId);
        }

        public ServiceItem GetService(int a_itemId)
        {
            return DataBaseContext.ServiceItems.FirstOrDefault(x => x.IdItem == a_itemId);
        }

        public bool EditProductItem(ProductItem a_product)
        {
            try
            {
                DataBaseContext.SetModified(a_product);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(ProductItem a_product)
        {
            try
            {
                DataBaseContext.ProductItems.Remove(a_product);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<IItem> GetAll()
        {
            var tmp = new List<IItem>(DataBaseContext.ProductItems.ToList());
            tmp.AddRange(DataBaseContext.ServiceItems.ToList());
            return tmp;
        }

        public bool EditServiceItem(ServiceItem a_service)
        {
            try
            {
                DataBaseContext.SetModified(a_service);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<IItem> GetAll(ItemState a_inWarehouse)
        {
            var tmp = new List<IItem>(DataBaseContext.ProductItems.Where(x=>x.DbState == (int)a_inWarehouse).ToList());
            tmp.AddRange(DataBaseContext.ServiceItems.Where(x => x.DbState == (int)a_inWarehouse).ToList());
            return tmp;
        }

        public bool CreateProductItem(ProductItem a_productItem)
        {
            try
            {
                DataBaseContext.ProductItems.Add(a_productItem);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateServiceItem(ServiceItem a_serviceItem)
        {
            try
            {
                DataBaseContext.ServiceItems.Add(a_serviceItem);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool StoreProduct(ProductItem a_oldProductEntity, ProductItem a_newProductEntity)
        {
            try
            {
                DataBaseContext.SetModified(a_newProductEntity);
                a_newProductEntity.Name = a_oldProductEntity.Name;
                a_newProductEntity.Price = a_oldProductEntity.Price;
                a_newProductEntity.Quantity = a_oldProductEntity.Quantity;
                a_newProductEntity.Attributes = a_oldProductEntity.Attributes;
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}