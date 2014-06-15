using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Store;
using Database.Core.Interfaces;
using Database.Entities.Stores;

namespace BussinessLogic.DatabaseLogic.Repositories.Store
{
    public class StoreRepository : IStoreRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public IEnumerable<StoreEntity> GetAll()
        {
            return DataBaseContext.Stores.ToList();
        }

        public StoreEntity Get(int a_storeId)
        {
            return DataBaseContext.Stores.FirstOrDefault(x => x.IdStore == a_storeId);
        }

        public bool AddDistribution(int a_storeId, int a_distributionId)
        {
            try
            {
                var distribution=DataBaseContext.Distributions.First(x => x.IdDistribution == a_distributionId);
                var store = DataBaseContext.Stores.First(x => x.IdStore == a_storeId);
                if (store.Distributions.FirstOrDefault(x => x.IdDistribution == a_distributionId) != null) return true;
                store.Distributions.Add(distribution);
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
