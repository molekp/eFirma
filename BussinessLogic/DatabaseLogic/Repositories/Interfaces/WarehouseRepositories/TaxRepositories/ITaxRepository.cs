using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.TaxRepositories
{
    public interface ITaxRepository
    {
        IDataBaseContext DataBaseContext { get; set; }

        /// <summary>
        /// Zwraca listę wszystkich tax
        /// </summary>
        /// <returns></returns>
        IEnumerable<TaxEntity> GetAllTaxes();

        /// <summary>
        /// Dodaje tax
        /// </summary>
        /// <returns></returns>
        bool AddTax(TaxEntity a_taxEntity);

        /// <summary>
        /// Wyszukuje tax
        /// </summary>
        /// <returns>Czy operacja się powiodła</returns>
        TaxEntity GetTax(int a_idTax);

        /// <summary>
        /// Zapisuje tax
        /// </summary>
        /// <returns>Czy operacja się powiodła</returns>
        bool SaveTax(TaxEntity a_taxEntity);

        /// <summary>
        /// Usuwa tax
        /// </summary>
        /// <returns>Czy operacja się powiodła</returns>
        bool RemoveTax(TaxEntity a_taxEntity);

    }
}
