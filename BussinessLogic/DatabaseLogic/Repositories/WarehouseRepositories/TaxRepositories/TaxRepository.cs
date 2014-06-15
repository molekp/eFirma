using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.TaxRepositories;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories.TaxRepositories
{
    public class TaxRepository : ITaxRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        /// <summary>
        /// Zwraca listę wszystkich tax
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TaxEntity> GetAllTaxes()
        {
            return DataBaseContext.Taxes.OrderBy(t => t.TaxName).ToList();
        }

        /// <summary>
        /// Dodaje tax
        /// </summary>
        /// <returns></returns>
        public bool AddTax(TaxEntity a_taxEntity)
        {
            try
            {
                DataBaseContext.Taxes.Add(a_taxEntity);
                DataBaseContext.SaveChanges();
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// Wyszukuje tax
        /// </summary>
        /// <returns>Czy operacja się powiodła</returns>
        public TaxEntity GetTax(int a_idTax)
        {
            return DataBaseContext.Taxes.FirstOrDefault(t => t.IdTax == a_idTax);
        }

        /// <summary>
        /// Zapisuje tax
        /// </summary>
        /// <returns>Czy operacja się powiodła</returns>
        public bool SaveTax(TaxEntity a_taxEntity)
        {
            try
            {
                var taxEntity = GetTax(a_taxEntity.IdTax);
                DataBaseContext.SetModified(taxEntity);
                taxEntity.TaxName = a_taxEntity.TaxName;
                taxEntity.TaxValue = a_taxEntity.TaxValue;
                DataBaseContext.SaveChanges();
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Usuwa tax
        /// </summary>
        /// <returns>Czy operacja się powiodła</returns>
        public bool RemoveTax(TaxEntity a_taxEntity)
        {
            try
            {
                DataBaseContext.Taxes.Remove(a_taxEntity);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}