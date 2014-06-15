using System.Collections.Generic;
using BussinessLogic.DTOs.WarehouseDtos.Taxes;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.TaxRepositories;
using BussinessLogic.Mappers.Warehouse.Taxes;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse.Taxes
{
    public interface ITaxManagementLogic
    {
        ITaxRepository TaxRepository { get; set; }
        TaxMapper TaxMapper { get; set; }
        TaxAddMapper TaxAddMapper { get; set; }
        TaxEditMapper TaxEditMapper { get; set; }

        List<TaxDto> GetAllTaxes();

        bool AddTax(TaxAddDto a_taxAddDto);

        TaxEditDto GetTax(int a_idTax);

        bool SaveTax(TaxEditDto a_taxEditDto);

        bool RemoveTax(int a_idTax);

    }
}
