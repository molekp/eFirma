using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DTOs.WarehouseDtos.Taxes;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.TaxRepositories;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse.Taxes;
using BussinessLogic.Mappers.Warehouse.Taxes;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Warehouse.Taxes
{
    public class TaxManagementLogic : ITaxManagementLogic
    {
        public ITaxRepository TaxRepository { get; set; }
        public TaxMapper TaxMapper { get; set; }
        public TaxAddMapper TaxAddMapper { get; set; }
        public TaxEditMapper TaxEditMapper { get; set; }

        public List<TaxDto> GetAllTaxes()
        {
            var taxDtos = new List<TaxDto>();
            TaxRepository.GetAllTaxes().ToList().ForEach(i => taxDtos.Add(TaxMapper.MapEntityToDto(i)));
            return taxDtos;
        }

        public bool AddTax(TaxAddDto a_taxAddDto)
        {
            return TaxRepository.AddTax(TaxAddMapper.MapDtoToEntity(a_taxAddDto));
        }

        public TaxEditDto GetTax(int a_idTax)
        {
            return TaxEditMapper.MapEntityToDto(TaxRepository.GetTax(a_idTax));
        }

        public bool SaveTax(TaxEditDto a_taxEditDto)
        {
            return TaxRepository.SaveTax(TaxEditMapper.MapDtoToEntity(a_taxEditDto));
        }

        public bool RemoveTax(int a_idTax)
        {
            return TaxRepository.RemoveTax(TaxRepository.GetTax(a_idTax));
        }

    }
}
