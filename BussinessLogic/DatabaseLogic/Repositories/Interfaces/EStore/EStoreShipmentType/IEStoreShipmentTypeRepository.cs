using System.Collections.Generic;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.EStore.EStoreShipmentType
{
    public interface IEStoreShipmentTypeRepository
    {
        IEnumerable<Database.Entities.EStore.EStoreShipmentType> GetAll();
        List<Database.Entities.EStore.EStoreShipmentType> GetAllOfEStore(Database.Entities.EStore.EStore a_eStore);
        bool Add(Database.Entities.EStore.EStoreShipmentType a_shipmentType, Database.Entities.EStore.EStore a_eStore);
        bool Remove(Database.Entities.EStore.EStoreShipmentType a_shipmentType, Database.Entities.EStore.EStore a_eStore);
        bool Edit(Database.Entities.EStore.EStoreShipmentType a_shipmentType);
        Database.Entities.EStore.EStoreShipmentType Get(int a_idShipmentType);

    }
}