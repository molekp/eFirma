using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BussinessLogic.DTOs.Factures;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.FactureRepositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.Currencies;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Factures;
using BussinessLogic.Mappers;
using BussinessLogic.Mappers.Factures;
using Database.Entities.Customers;
using Database.Entities.Factures;
using Database.Entities.WarehouseEntities.Currencies;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Factures
{
    public class FactureLogic : IFactureLogic
    {
        public IFactureRepository FactureRepository { get; set; }
        public FactureViewMapper FactureViewMapper { get; set; }
        public FactureMapper FactureMapper { get; set; }
        public FactureItemMapper FactureItemMapper { get; set; }
        public FirmMapper FirmMapper { get; set; }
        public ExchangeMapper ExchangeMapper { get; set; }
        public FirmAddMapper FirmAddMapper { get; set; }
        public FirmViewMapper FirmViewMapper { get; set; }
        public ICurrencyRepository CurrencyRepository { get; set; }


        public List<FactureViewDto> GetAllFactures()
        {
            var factureDtos = new List<FactureViewDto>();
            FactureRepository.GetAllFactures().ToList().ForEach(i => factureDtos.Add(FactureViewMapper.MapEntityToDto(i)));
            return factureDtos;
        }

        public FactureDto ViewFacture(int a_idFacture)
        {
            var facture = FactureRepository.GetFacture(a_idFacture);
            if(facture == null)
            {
                return null;
            }
            return FactureMapper.MapEntityToDto(facture);
        }

        public List<FirmViewDto> GetAllFirms()
        {
            var firmDtos = new List<FirmViewDto>();
            FactureRepository.GetAllFirms().ToList().ForEach(i => firmDtos.Add(FirmViewMapper.MapEntityToDto(i)));
            return firmDtos;
        }

        public FirmDto ViewFirm(int a_idFirm)
        {
            var firm = FactureRepository.GetCustomer(a_idFirm);
            if(firm == null)
            {
                return null;
            }
            return FirmMapper.MapEntityToDto(firm);
        }

         public int idFacture(int a_idDistribution)
         {
            return FactureRepository.idFacture(a_idDistribution);
         }

        public bool RemoveFirm(int a_idFirm)
        {
            return FactureRepository.RemoveFirm(FactureRepository.GetFirm(a_idFirm));
        }

        public FactureAddDto AddFacture(int a_idDistribution)
        {
            var distributionEntity = FactureRepository.GetDistribution(a_idDistribution);
            if(distributionEntity == null)
            {
                return null;
            }
            if (!(distributionEntity.DistributionCustomer is Firm) || !(distributionEntity.DistributionProvider is Firm))
            {
                return null;
            }
            var factureItemDtos = new List<FactureItemDto>();
            foreach (var product in distributionEntity.ProductItems)
            {
                var factureItemDto = FactureItemMapper.MapItemEntityToDto(product);
                var factureItemId = FactureRepository.SaveFactureItem(FactureItemMapper.MapDtoToEntity(factureItemDto));
                if (factureItemId == 0)
                {
                    return null;
                }
                factureItemDto.IdItem = factureItemId;
                factureItemDtos.Add(factureItemDto);
            }
            foreach (var service in distributionEntity.ServiceItems)
            {
                var factureItemDto = FactureItemMapper.MapItemEntityToDto(service);
                var factureItemId = FactureRepository.SaveFactureItem(FactureItemMapper.MapDtoToEntity(factureItemDto));
                if(factureItemId == 0)
                {
                    return null;
                }
                factureItemDto.IdItem = factureItemId;
                factureItemDtos.Add(factureItemDto);
            }
            return FactureMapper.MapDistributionEntityToDto(distributionEntity, factureItemDtos);
        }

        public int SaveFacture(FactureAddDto a_factureAddDto)
        {
            var factureItemEntites = new List<FactureItem>();
            if(a_factureAddDto == null || a_factureAddDto.ItemIds == null)
            {
                return 0;
            }
            foreach (var factureItemId in a_factureAddDto.ItemIds)
            {
                factureItemEntites.Add(FactureRepository.GetFactureItem(factureItemId));
            }
            var factureEntity = FactureMapper.MapDtoToEntity(a_factureAddDto, factureItemEntites);
            factureEntity.Exchanges = CurrencyRepository.GetLatestList().ToList();
            var idFacture = FactureRepository.SaveFacture(factureEntity);
            if(FactureRepository.SetDistributionFacture(a_factureAddDto.IdDistribution, idFacture))
            {
                return idFacture;
            }
            return 0;
        }

        public List<FactureItemDto> getItems(List<int> a_itemIds)
        {
            var factureItemDtos = new List<FactureItemDto>();
            if (a_itemIds != null)
            {
                foreach (var itemId in a_itemIds)
                {
                    factureItemDtos.Add(FactureItemMapper.MapEntityToDto(FactureRepository.GetFactureItem(itemId)));
                }
            }
            return factureItemDtos;
        }

        public FactureAddDto getSum(FactureAddDto a_factureAddDto)
        {
            a_factureAddDto.SumOfTax = new decimal(0);
            a_factureAddDto.SumWithTax = new decimal(0);
            foreach (var factureItemDto in a_factureAddDto.Items){
                a_factureAddDto.SumOfTax += factureItemDto.PriceTax;
                a_factureAddDto.SumWithTax += factureItemDto.PriceWithTax;
            }
            a_factureAddDto.SumWithoutTax = a_factureAddDto.SumWithTax - a_factureAddDto.SumOfTax;
            return a_factureAddDto;
        }

        public List<SelectListItem> getCurrencies()
        {
            var list = CurrencyRepository.GetAllCurrencies().ToList();
            var selectList = new List<SelectListItem>();
            foreach (var currency in list)
            {
                selectList.Add(new SelectListItem(){Selected = false, Text = currency.Code, Value = currency.CurrencyId.ToString()});
            }
            return selectList;
        }

        public ExchangeDto getExchange(int a_idFacture, int a_idCurrency)
        {
            var exchangeCurrency = FactureRepository.getFactureExchange(a_idFacture, a_idCurrency);
            var currency = CurrencyRepository.GetCurrency(a_idCurrency);
            return ExchangeMapper.MapEntitiesToDto(currency, exchangeCurrency);
        }

        public int AddFirm(FirmAddDto a_addFirmDto)
        {
            return FactureRepository.AddFirm(FirmAddMapper.MapDtoToEntity(a_addFirmDto));
        }

        public FirmAddDto GetFirm(int a_idFirm)
        {
            return FirmAddMapper.MapEntityToDto(FactureRepository.GetFirm(a_idFirm));
        }

        public bool SaveFirm(FirmAddDto a_firmAddDto)
        {
            return FactureRepository.SaveFirm(FirmAddMapper.MapDtoToEntity(a_firmAddDto));
        }
    }

}