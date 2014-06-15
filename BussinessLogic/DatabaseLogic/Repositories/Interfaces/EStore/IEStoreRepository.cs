using System.Collections.Generic;
using Database.Entities.WarehouseEntities;
using Entit = Database.Entities.EStore;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.EStore
{
    public interface IEStoreRepository
    {
        IEnumerable<Entit.EStore> GetAll();
        bool Add(Entit.EStore a_eStore);
        bool Remove(Entit.EStore a_eStore);
        Entit.EStore Get(int a_eStore);

        bool AddWarehouse(Entit.EStore a_eStore, Warehouse a_warehouse);
        bool RemoveWarehouse(Entit.EStore a_eStore, Warehouse a_warehouse);

        IEnumerable<Warehouse> GetWarehouseActive(int a_eStore);
        IEnumerable<Warehouse> GetWarehouseOffline(int a_eStore);

    }
}