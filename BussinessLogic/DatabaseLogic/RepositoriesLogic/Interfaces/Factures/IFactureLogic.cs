using System.Collections.Generic;
using System.Web.Mvc;
using BussinessLogic.DTOs.Factures;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.FactureRepositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.Currencies;
using BussinessLogic.Mappers;
using BussinessLogic.Mappers.Factures;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Factures
{
    public interface IFactureLogic
    {
        IFactureRepository FactureRepository { get; set; }
        FactureViewMapper FactureViewMapper { get; set; }
        FactureMapper FactureMapper { get; set; }
        FactureItemMapper FactureItemMapper { get; set; }
        ICurrencyRepository CurrencyRepository { get; set; }
        ExchangeMapper ExchangeMapper { get; set; }
        FirmAddMapper FirmAddMapper { get; set; }

        List<FactureViewDto> GetAllFactures();
        FactureDto ViewFacture(int a_idFacture);
        List<FirmViewDto> GetAllFirms();
        FirmDto ViewFirm(int a_idFirm);
        FactureAddDto AddFacture(int a_idDistribution);
        int SaveFacture(FactureAddDto a_factureAddDto);
        List<FactureItemDto> getItems(List<int> a_itemIds);
        FactureAddDto getSum(FactureAddDto a_factureAddDto);
        List<SelectListItem> getCurrencies();
        ExchangeDto getExchange(int a_idFacture, int a_idCurrency);
        int AddFirm(FirmAddDto a_addFirmDto);
        FirmAddDto GetFirm(int a_idFirm);
        bool SaveFirm(FirmAddDto a_firmAddDto);
        int idFacture(int a_idDistribution);
        bool RemoveFirm(int a_idFirm);
    }
}