using AutoMapper;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.Factures;
using BussinessLogic.Mappers.Warehouse.Taxes;
using Database.Entities.Factures;
using Database.Entities.WarehouseEntities;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;
using BussinessLogic.Helpers;

namespace BussinessLogic.Mappers.Factures
{
    public class FactureItemMapper
    {
        public TaxMapper TaxMapper { get; set; }
        public FactureItemMapper()
        {
            Mapper.CreateMap<FactureItem, FactureItemDto>()
                    .ForMember(i => i.IdItem, s => s.MapFrom(src => src.IdItem))
                    .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                    .ForMember(i => i.PricePerUnit, s => s.MapFrom(src => src.Price))
                    .ForMember(i => i.Quantity, s => s.MapFrom(src => src.Quantity))
                    .ForMember(i => i.SaleTypeName, s => s.MapFrom(src => src.SaleTypeName))
                    .ForMember(i => i.PKWiU, s => s.MapFrom(src => src.PKWiU))
                    .ForMember(i => i.TaxValue, s => s.MapFrom(src => src.TaxValue));

            Mapper.CreateMap<FactureItemDto, FactureItem>()
                    .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                    .ForMember(i => i.Price, s => s.MapFrom(src => src.PricePerUnit))
                    .ForMember(i => i.Quantity, s => s.MapFrom(src => src.Quantity))
                    .ForMember(i => i.PKWiU, s => s.MapFrom(src => src.PKWiU))
                    .ForMember(i => i.TaxValue, s => s.MapFrom(src => src.TaxValue))
                    .ForMember(i => i.SaleTypeName, s => s.MapFrom(src => src.SaleTypeName))
                    ;

            Mapper.CreateMap<IItem, FactureItemDto>()
                    .ForMember(i => i.IdItem, s => s.MapFrom(src => src.IdItem))
                    .ForMember(i => i.Name, s => s.MapFrom(src => src.Name))
                    .ForMember(i => i.PricePerUnit, s => s.MapFrom(src => src.Price))
                    .ForMember(i => i.Quantity, s => s.MapFrom(src => src.Quantity))
                    .ForMember(i => i.SaleTypeName, s => s.MapFrom(src => src.SaleType.Name))
                    .ForMember(i => i.PKWiU, s => s.MapFrom(src => src.PKWiU));
        }

        public FactureItemDto MapEntityToDto(FactureItem a_FactureItemEntity)
        {
            var dto = Mapper.Map<FactureItem, FactureItemDto>(a_FactureItemEntity);
            dto.PriceWithTax = dto.PricePerUnit * new decimal(dto.Quantity);
            dto.PriceTax = dto.PriceWithTax * new decimal(dto.TaxValue);
            dto.PriceWithoutTax = dto.PriceWithTax - dto.PriceTax;
            return dto;
        }
        
        public FactureItem MapDtoToEntity(FactureItemDto a_factureItemDto)
        {
            return Mapper.Map<FactureItemDto, FactureItem>(a_factureItemDto);
        }

        public FactureItemDto MapItemEntityToDto(IItem a_item)
        {
            var dto = Mapper.Map<IItem, FactureItemDto>(a_item);
            if (a_item is ProductItem)
            {
                dto.TaxValue = TaxMapper.MapEntityToDto(((ProductItem)a_item).ItemType.ItemTax).TaxValue;
            }
            if (a_item is ServiceItem)
            {
                dto.TaxValue = TaxMapper.MapEntityToDto(((ServiceItem)a_item).ItemType.ItemTax).TaxValue;
            }
            dto.PriceWithTax = dto.PricePerUnit * new decimal(dto.Quantity);
            dto.PriceTax = dto.PriceWithTax * new decimal(dto.TaxValue);
            dto.PriceWithoutTax = dto.PriceWithTax - dto.PriceTax;
            return dto;
        }

        
    }
}
