using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;
using BussinessLogic.Helpers;

namespace BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories
{
    public class WarehouseRepository : IWarehouseRepository
            //nie ma testow bo to byla przykladowa klasa do tworzenia safety points
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public IEnumerable<Warehouse> GetAll()
        {
            List<Warehouse> tmp = CacheLayer.Get<List<Warehouse>>("WarehouseRepository_GetAll");
            if (tmp == null)
            {
                tmp = DataBaseContext.Warehouses.ToList();
                CacheLayer.Add(tmp, "WarehouseRepository_GetAll", 5);
            }
            return tmp;
        }

        public Warehouse Get(int a_idWarehouse)
        {
            return DataBaseContext.Warehouses.FirstOrDefault(x => x.IdWarehouse == a_idWarehouse);
        }

        public bool AddWarehouse(Warehouse a_newWarehouse)
        {
            try
            {
                DataBaseContext.Warehouses.Add(a_newWarehouse);
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool EditWarehouse(Warehouse a_warehouse)
        {
            try
            {
                DataBaseContext.SetModified(a_warehouse);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveProductWarehouseFromWarehouse(Warehouse a_warehouse, ProductWarehouse a_productWarehouse)
        {
            try
            {
                a_warehouse.ProductWarehouses.Remove(a_productWarehouse);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveServiceWarehouseFromWarehouse(Warehouse a_warehouse, ServiceWarehouse a_serviceWarehouse)
        {
            try
            {
                a_warehouse.ServiceWarehouses.Remove(a_serviceWarehouse);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddProductToProductWarehouse(ProductItem a_entity, int a_idProductWarehouse)
        {
            try
            {
                var productWarehouse = GetProductWarehouse(a_idProductWarehouse);
                DataBaseContext.SetModified(a_entity);
                DataBaseContext.SetModified(productWarehouse);
                a_entity.ItemState = ItemState.InWarehouse;
                productWarehouse.ProductItems.Add(a_entity);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ProductWarehouse GetProductWarehouse(int a_idProductWarehouse)
        {
            return DataBaseContext.ProductWarehouses.FirstOrDefault(x => x.IdProductWarehouse == a_idProductWarehouse);
        }

        public Warehouse FindWarehouseForItem(IItem a_item)
        {
            if (a_item is ProductItem)
            {
                foreach (var warehouse in DataBaseContext.Warehouses.ToList())
                {
                    foreach (var productWarehouse in warehouse.ProductWarehouses)
                    {
                        if (productWarehouse.ProductItems.Contains(a_item))
                        {
                            return warehouse;
                        }
                    }
                }
            }
            else
            {
                foreach (var warehouse in DataBaseContext.Warehouses.ToList())
                {
                    foreach (var serviceWarehouse in warehouse.ServiceWarehouses)
                    {
                        if (serviceWarehouse.ServiceItems.Contains(a_item))
                        {
                            return warehouse;
                        }
                    }
                }
            }
            return null;
        }

        public string FindItemWarehouseName(IItem a_item)
        {
            if (a_item is ProductItem)
            {
                foreach (var productWarehouse in DataBaseContext.ProductWarehouses)
                {
                    if (productWarehouse.ProductItems.Contains(a_item))
                    {
                        return productWarehouse.Name;
                    }
                }
            }
            else
            {
                foreach (var serviceWarehouse in DataBaseContext.ServiceWarehouses)
                {
                    if (serviceWarehouse.ServiceItems.Contains(a_item))
                    {
                        return serviceWarehouse.Name;
                    }
                }
            }
            return null;
        }

        public ProductWarehouse GetProductWarehouseForItem(ProductItem a_productItem)
        {
            var warehouses = GetAll();
            return (from warehouse in warehouses
                    from productWarehouse in warehouse.ProductWarehouses
                    where productWarehouse.ProductItems.Contains(a_productItem)
                    select productWarehouse).FirstOrDefault();
        }

        public ServiceWarehouse GetServiceWarehouseForItem(ServiceItem a_serviceItem)
        {
            var warehouses = GetAll();
            return (from warehouse in warehouses
                    from serviceWarehouse in warehouse.ServiceWarehouses
                    where serviceWarehouse.ServiceItems.Contains(a_serviceItem)
                    select serviceWarehouse).FirstOrDefault();
        }
    }
}