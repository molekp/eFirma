using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces
{
    public interface IProductTypeRepository
    {
        IDataBaseContext DataBaseContext { get; set; }

        /// <summary>
        /// Zwraca listê wszystkich ProductType
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductType> GetAll();

        ProductType GetProductType(int a_idProductType);
        IEnumerable<ProductType> GetAllBut(int a_idProductType);
    }
}