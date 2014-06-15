using System;
using System.Collections.Generic;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities.Product;
using System.Linq;

namespace BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories
{
    public class ProductWarehouseRepository : IProductWarehouseRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }


        public ProductWarehouse Get(int a_idProductWarehouse)
        {
            return DataBaseContext.ProductWarehouses.FirstOrDefault(x => x.IdProductWarehouse == a_idProductWarehouse);
        }

        public IEnumerable<ProductWarehouse> GetAll()
        {
            return DataBaseContext.ProductWarehouses.ToList();
        }

        

        public IEnumerable<ProductWarehouse> GetAllFreeProductWarehouses()
        {
            var productWarehouses = DataBaseContext.ProductWarehouses.ToList();
            foreach (var warehouse in DataBaseContext.Warehouses.ToList())
            {
                foreach (var productWarehouse in warehouse.ProductWarehouses)
                {
                    productWarehouses.Remove(productWarehouse);
                }
            }
            return productWarehouses;
        }

        public bool Edit(ProductWarehouse a_productWarehouse)
        {
            try
            {
                DataBaseContext.SetModified(a_productWarehouse);
                DataBaseContext.SetModified(a_productWarehouse);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Add(ProductWarehouse a_productWarehouse, ProductItem a_productItem)
        {
            try
            {
                a_productWarehouse.ProductItems.Add(a_productItem);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveProductItemFromWarehouse(ProductWarehouse a_productWarehouse, ProductItem a_productItem)
        {
            try
            {
                if (a_productWarehouse.ProductItems.Remove(a_productItem))
                {
                    DataBaseContext.SetModified(a_productWarehouse);
                    DataBaseContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
