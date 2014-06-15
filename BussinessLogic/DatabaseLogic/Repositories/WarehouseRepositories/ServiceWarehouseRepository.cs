using System;
using System.Collections.Generic;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities.Product;
using System.Linq;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories
{
    public class ServiceWarehouseRepository : IServiceWarehouseRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }


        public ServiceWarehouse Get(int a_idServiceWarehouse)
        {
            return DataBaseContext.ServiceWarehouses.FirstOrDefault(x => x.IdServiceWarehouse == a_idServiceWarehouse);
        }

        public IEnumerable<ServiceWarehouse> GetAll()
        {
            return DataBaseContext.ServiceWarehouses.ToList();
        }

        

        public IEnumerable<ServiceWarehouse> GetAllFreeServiceWarehouses()
        {
            var serviceWarehouses = DataBaseContext.ServiceWarehouses.ToList();
            foreach (var warehouse in DataBaseContext.Warehouses.ToList())
            {
                foreach (var serviceWarehouse in warehouse.ServiceWarehouses)
                {
                    serviceWarehouses.Remove(serviceWarehouse);
                }
            }
            return serviceWarehouses;
        }

        public bool Add(ServiceWarehouse a_serviceWarehouse, ServiceItem a_serviceItem)
        {
            try
            {
                a_serviceWarehouse.ServiceItems.Add(a_serviceItem);
                DataBaseContext.SetModified(a_serviceWarehouse);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveServiceItemFromWarehouse(ServiceWarehouse a_serviceWarehouse, ServiceItem a_serviceItem)
        {
            try
            {
                if (a_serviceWarehouse.ServiceItems.Remove(a_serviceItem))
                {
                    DataBaseContext.SetModified(a_serviceWarehouse);
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
