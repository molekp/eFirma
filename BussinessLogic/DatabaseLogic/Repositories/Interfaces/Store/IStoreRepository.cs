using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database.Core.Interfaces;
using Database.Entities.Stores;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.Store
{
    public interface IStoreRepository
    {
        IDataBaseContext DataBaseContext { get; set; }
        IEnumerable<StoreEntity> GetAll();
        StoreEntity Get(int a_storeId);
        bool AddDistribution(int a_storeId, int a_distributionId);
    }
}
