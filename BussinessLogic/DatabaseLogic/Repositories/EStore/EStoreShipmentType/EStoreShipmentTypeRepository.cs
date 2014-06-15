using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.EStore.EStoreCategory;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.EStore.EStoreShipmentType;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DatabaseLogic.Repositories.EStore.EStoreShipmentType
{
    public class EStoreShipmentTypeRepository : IEStoreShipmentTypeRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public IEnumerable<Database.Entities.EStore.EStoreShipmentType> GetAll()
        {
            return DataBaseContext.EStoreShipmentTypes.ToList();
        }

        public List<Database.Entities.EStore.EStoreShipmentType> GetAllOfEStore(Database.Entities.EStore.EStore a_eStore)
        {
            var tmp = DataBaseContext.EStore.FirstOrDefault(p => p.IdEStore == a_eStore.IdEStore);
            return tmp.EStoreShipmentType;
        }

        public bool Add(Database.Entities.EStore.EStoreShipmentType a_shipmentType, Database.Entities.EStore.EStore a_eStore)
        {
            try
            {
                a_eStore.EStoreShipmentType.Add(a_shipmentType);
                DataBaseContext.SetModified(a_eStore);

                DataBaseContext.EStoreShipmentTypes.Add(a_shipmentType);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Remove(Database.Entities.EStore.EStoreShipmentType a_shipmentType, Database.Entities.EStore.EStore a_eStore)
        {
            try
            {
                a_eStore.EStoreShipmentType.Remove(a_shipmentType);
                DataBaseContext.SetModified(a_eStore);

                DataBaseContext.EStoreShipmentTypes.Remove(a_shipmentType);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Edit(Database.Entities.EStore.EStoreShipmentType a_shipmentType)
        {
            try
            {
                DataBaseContext.SetModified(a_shipmentType);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Database.Entities.EStore.EStoreShipmentType Get(int a_idShipmentType)
        {
            return DataBaseContext.EStoreShipmentTypes.FirstOrDefault(c => c.IdEStoreShipmentType == a_idShipmentType);
        }

    }
}