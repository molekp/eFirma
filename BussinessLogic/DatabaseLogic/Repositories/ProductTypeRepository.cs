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
    public class ProductTypeRepository : IProductTypeRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        /// <summary>
        /// Zwraca listę wszystkich ProductType
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductType> GetAll()
        {
            return DataBaseContext.ProductTypes.OrderBy(t => t.Name).ToList();
        }

        public ProductType GetProductType(int a_idProductType)
        {
            return DataBaseContext.ProductTypes.FirstOrDefault(t => t.IdItemType == a_idProductType);
        }

        public IEnumerable<ProductType> GetAllBut(int a_idProductType)
        {
            return DataBaseContext.ProductTypes.Where(x=>x.IdItemType != a_idProductType).OrderBy(t => t.Name).ToList();
        }
    }
}