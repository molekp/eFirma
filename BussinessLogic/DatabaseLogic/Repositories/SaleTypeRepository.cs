using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using Database.Core.Interfaces;
using Database.Entities;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.DatabaseLogic.Repositories
{
    public class SaleTypeRepository : ISaleTypeRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        /// <summary>
        /// Zwraca listę wszystkich ProductType
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SaleType> GetAllSaleTypes()
        {
            return DataBaseContext.SaleTypes.OrderBy(t => t.Name).ToList();
        }

        public IEnumerable<SaleType> GetAllSaleTypesBut(int a_idSaleType)
        {
            return DataBaseContext.SaleTypes.Where(x => x.IdSaleType != a_idSaleType).OrderBy(t => t.Name).ToList();
        }

        public SaleType Get(int a_idSaleType)
        {
            return DataBaseContext.SaleTypes.FirstOrDefault(x => x.IdSaleType == a_idSaleType);
        }
    }
}