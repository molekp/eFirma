using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces
{
    public interface ISaleTypeRepository
    {
        IDataBaseContext DataBaseContext { get; set; }

        /// <summary>
        /// Zwraca listê wszystkich ProductType
        /// </summary>
        /// <returns></returns>
        IEnumerable<SaleType> GetAllSaleTypes();

        IEnumerable<SaleType> GetAllSaleTypesBut(int a_idSaleType);
        SaleType Get(int a_idSaleType);
    }
}