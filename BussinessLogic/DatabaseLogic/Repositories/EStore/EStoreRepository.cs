using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.EStore;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;
using Entit = Database.Entities.EStore;

namespace BussinessLogic.DatabaseLogic.Repositories.EStore
{
    public class EStoreRepository : IEStoreRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public IEnumerable<Entit.EStore> GetAll()
        {
            return DataBaseContext.EStore.ToList();
        }

        public bool Add(Entit.EStore a_eStore)
        {
            try
            {
                DataBaseContext.EStore.Add(a_eStore);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Remove(Entit.EStore a_eStore)
        {
            try
            {
                DataBaseContext.EStore.Remove(a_eStore);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Entit.EStore Get(int a_eStore)
        {
            return DataBaseContext.EStore.FirstOrDefault(c => c.IdEStore == a_eStore);
        }

        public IEnumerable<Warehouse> GetWarehouseActive(int a_eStore)
        {

            return DataBaseContext.EStore.FirstOrDefault(c => c.IdEStore == a_eStore).Warehouses;
        }

        public IEnumerable<Warehouse> GetWarehouseOffline(int a_eStore)
        {
            var list = new List<Warehouse>();
            var warehouse = DataBaseContext.EStore.FirstOrDefault(c => c.IdEStore == a_eStore).Warehouses;
            var idWarehpuses = warehouse.Select(c => c.IdWarehouse);

            list = DataBaseContext.Warehouses.Where(c => !idWarehpuses.Contains(c.IdWarehouse)).ToList();
            
            return list;
        }

        public bool AddWarehouse(Entit.EStore a_eStore, Warehouse a_warehouse)
        {
            try
            {
                a_eStore.Warehouses.Add(a_warehouse);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool RemoveWarehouse(Entit.EStore a_eStore, Warehouse a_warehouse)
        {
            try
            {
                a_eStore.Warehouses.Remove(a_warehouse);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
