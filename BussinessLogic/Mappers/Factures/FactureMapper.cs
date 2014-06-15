using System.Collections.Generic;
using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.Factures;
using BussinessLogic.Helpers;
using BussinessLogic.Mappers.Factures;
using Database.Entities;
using Database.Entities.Customers;
using Database.Entities.Factures;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;

namespace BussinessLogic.Mappers
{
    public class FactureMapper
    {
        public FactureItemMapper FactureItemMapper { get; set; }
        public FactureMapper()
        {
            Mapper.CreateMap<Facture, FactureDto>()
                    .ForMember(i => i.IdFacture, s => s.MapFrom(src => src.IdFacture))
                    .ForMember(i => i.CreationPlace, s => s.MapFrom(src => src.CreationPlace))
                    .ForMember(i => i.CreationTime, s => s.MapFrom(src => src.CreationTime))
                    .ForMember(i => i.RealizationTime, s => s.MapFrom(src => src.RealizationTime))
                    .ForMember(i => i.ExpirationTime, s => s.MapFrom(src => src.ExpirationTime))
                    .ForMember(i => i.ClientName, s => s.MapFrom(src => src.ClientName))
                    .ForMember(i => i.ClientAddress, s => s.MapFrom(src => src.ClientAddress))
                    .ForMember(i => i.ClientInfo, s => s.MapFrom(src => src.ClientInfo))
                    .ForMember(i => i.ClientNIP, s => s.MapFrom(src => src.ClientNIP))
                    .ForMember(i => i.ProviderName, s => s.MapFrom(src => src.ProviderName))
                    .ForMember(i => i.ProviderAddress, s => s.MapFrom(src => src.ProviderAddress))
                    .ForMember(i => i.ProviderInfo, s => s.MapFrom(src => src.ProviderInfo))
                    .ForMember(i => i.ProviderNIP, s => s.MapFrom(src => src.ProviderNIP))
                    .ForMember(i => i.ProviderBankAccountNumber,
                               s => s.MapFrom(src => src.ProviderBankAccountNumber))
                    .ForMember(i => i.ProviderBankInfo, s => s.MapFrom(src => src.ProviderBankInfo))
                    .ForMember(i => i.Issuer, s => s.MapFrom(src => src.Issuer))
                    .ForMember(i => i.Reciver, s => s.MapFrom(src => src.Reciver))
                    .ForMember(i => i.SumString, s => s.MapFrom(src => src.SumString))
                    .ForMember(i => i.Payment, s => s.MapFrom(src => src.Payment))
/*****************************************************************************/
                    //.ForMember(i => i.SumWithTax, s => s.MapFrom(src => src.Sum))
/*****************************************************************************/
                    ;

            Mapper.CreateMap<FactureAddDto, Facture>()
                    .ForMember(i => i.IdFacture, s => s.MapFrom(src => src.IdFacture))
                    .ForMember(i => i.CreationPlace, s => s.MapFrom(src => src.CreationPlace))
                    .ForMember(i => i.CreationTime, s => s.MapFrom(src => src.CreationTime))
                    .ForMember(i => i.RealizationTime, s => s.MapFrom(src => src.RealizationTime))
                    .ForMember(i => i.ExpirationTime, s => s.MapFrom(src => src.ExpirationTime))
                    .ForMember(i => i.ClientName, s => s.MapFrom(src => src.ClientName))
                    .ForMember(i => i.ClientAddress, s => s.MapFrom(src => src.ClientAddress))
                    .ForMember(i => i.ClientInfo, s => s.MapFrom(src => src.ClientInfo))
                    .ForMember(i => i.ClientNIP, s => s.MapFrom(src => src.ClientNIP))
                    .ForMember(i => i.ProviderName, s => s.MapFrom(src => src.ProviderName))
                    .ForMember(i => i.ProviderAddress, s => s.MapFrom(src => src.ProviderAddress))
                    .ForMember(i => i.ProviderInfo, s => s.MapFrom(src => src.ProviderInfo))
                    .ForMember(i => i.ProviderNIP, s => s.MapFrom(src => src.ProviderNIP))
                    .ForMember(i => i.ProviderBankAccountNumber,
                               s => s.MapFrom(src => src.ProviderBankAccountNumber))
                    .ForMember(i => i.ProviderBankInfo, s => s.MapFrom(src => src.ProviderBankInfo))
                    .ForMember(i => i.Issuer, s => s.MapFrom(src => src.Issuer))
                    .ForMember(i => i.Reciver, s => s.MapFrom(src => src.Reciver))
                    .ForMember(i => i.Sum, s => s.MapFrom(src => src.SumWithTax))
                    .ForMember(i => i.SumString, s => s.MapFrom(src => src.SumString))
                    .ForMember(i => i.Payment, s => s.MapFrom(src => src.Payment))
                    ;

            Mapper.CreateMap<Distribution, FactureAddDto>()
                    .ForMember(i => i.IdDistribution, s => s.MapFrom(src => src.IdDistribution))
                    .ForMember(i => i.CreationTime, s => s.MapFrom(src => src.DistributionCreateTime))
                    .ForMember(i => i.RealizationTime, s => s.MapFrom(src => src.DistributionTime))
                    .ForMember(i => i.Issuer, s => s.MapFrom(src => src.DistributionCreator.UserName))
                    .ForMember(i => i.ClientName, s => s.MapFrom(src => src.DistributionCustomer.Name))
                    .ForMember(i => i.ClientAddress, s => s.MapFrom(src => src.DistributionCustomer.Address))
                    .ForMember(i => i.ClientInfo, s => s.MapFrom(src => ((Firm)src.DistributionCustomer).Info))
                    .ForMember(i => i.ClientNIP, s => s.MapFrom(src => ((Firm)src.DistributionCustomer).NIP))
                    .ForMember(i => i.ProviderName, s => s.MapFrom(src => src.DistributionProvider.Name))
                    .ForMember(i => i.ProviderAddress, s => s.MapFrom(src => src.DistributionProvider.Address))
                    .ForMember(i => i.ProviderInfo, s => s.MapFrom(src => ((Firm)src.DistributionProvider).Info))
                    .ForMember(i => i.ProviderNIP, s => s.MapFrom(src => ((Firm)src.DistributionProvider).NIP))
                    .ForMember(i => i.ProviderBankAccountNumber, s => s.MapFrom(src => ((Firm)src.DistributionProvider).BankAccountNumber))
                    .ForMember(i => i.ProviderBankInfo, s => s.MapFrom(src => ((Firm)src.DistributionProvider).BankInfo))
                    ;
        }

        public FactureDto MapEntityToDto(Facture a_FactureEntity)
        {
            var dto = Mapper.Map<Facture, FactureDto>(a_FactureEntity);
            dto.FactureNr = a_FactureEntity.IdFacture.ToString() + "/" + dto.CreationTime.Year.ToString();
            dto.Items = new List<FactureItemDto>();
            dto.SumOfTax = new decimal(0);
/*****************************************************************************/
            dto.SumWithTax = new decimal(0);
/*****************************************************************************/
            foreach (var item in a_FactureEntity.Items)
            {
                var itemDto = FactureItemMapper.MapEntityToDto(item);
                dto.Items.Add(itemDto);
                dto.SumOfTax += itemDto.PriceTax;
/*****************************************************************************/
                dto.SumWithTax += itemDto.PriceWithTax;
/*****************************************************************************/
            }
            dto.Remain = dto.SumWithTax - dto.Paid;
            dto.SumWithoutTax = dto.SumWithTax - dto.SumOfTax;
            return dto;
        }

        public FactureAddDto MapDistributionEntityToDto(Distribution a_distributionEntity, List<FactureItemDto> factureItems)
        {
            var dto = Mapper.Map<Distribution, FactureAddDto>(a_distributionEntity);
            dto.ItemIds = new int[factureItems.Count];
            dto.Items = factureItems;
            var index = 0;
            foreach (var factureItemDto in factureItems)
            {
                dto.ItemIds[index++] = (factureItemDto.IdItem);
            }
            dto.ExpirationTime = dto.CreationTime.AddDays(14);
            dto.SumOfTax = new decimal(0);
            dto.SumWithTax = new decimal(0);
            foreach (var itemDto in factureItems)
            {
                dto.SumOfTax += itemDto.PriceTax;
                dto.SumWithTax += itemDto.PriceWithTax;
            }
            dto.SumWithoutTax = dto.SumWithTax - dto.SumOfTax;
            return dto;
        }

        public Facture MapDtoToEntity(FactureAddDto a_factureAddDto, List<FactureItem> a_factureItemEntites)
        {
            var entity = Mapper.Map<FactureAddDto, Facture>(a_factureAddDto);
            entity.Items = a_factureItemEntites;
            return entity;
        }
    }
}
