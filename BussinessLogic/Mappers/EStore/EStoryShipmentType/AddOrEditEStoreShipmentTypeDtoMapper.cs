using System.Collections.Generic;
using AutoMapper;
using BussinessLogic.DTOs.EStore.EStoreCategoryDto;
using BussinessLogic.DTOs.EStore.EStoreShipmentType;
using Entit = Database.Entities.EStore;

namespace BussinessLogic.Mappers.EStore.EStoryShipmentType
{
    public class AddOrEditEStoreShipmentTypeDtoMapper
    {
        public AddOrEditEStoreShipmentTypeDtoMapper()
        {
            Mapper.CreateMap<AddOrEditEStoreShipmentTypeDto, Entit.EStoreShipmentType>()
                .ForMember(desc => desc.IdEStoreShipmentType, s => s.MapFrom(src => src.IdEStoreShipmentType))
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(desc => desc.SortOrder, s => s.MapFrom(src => src.Sortorder))
                  .ForMember(desc => desc.PriceShipment, s => s.MapFrom(src => src.PriceShipment))
                  .ForMember(desc => desc.Info, s => s.MapFrom(src => src.Info));

            Mapper.CreateMap< Entit.EStoreShipmentType,AddOrEditEStoreShipmentTypeDto>()
                .ForMember(desc => desc.IdEStoreShipmentType, s => s.MapFrom(src => src.IdEStoreShipmentType))
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(desc => desc.Sortorder, s => s.MapFrom(src => src.SortOrder))
                  .ForMember(desc => desc.PriceShipment, s => s.MapFrom(src => src.PriceShipment))
                  .ForMember(desc => desc.Info, s => s.MapFrom(src => src.Info));

        }

        public Entit.EStoreShipmentType MapDtoToEntity(AddOrEditEStoreShipmentTypeDto a_shipmentType)
        {
            var tmp = Mapper.Map<AddOrEditEStoreShipmentTypeDto, Entit.EStoreShipmentType>(a_shipmentType);
            return tmp;
        }

        public AddOrEditEStoreShipmentTypeDto MapEntityToDto(Entit.EStoreShipmentType a_shipmentType)
        {
            var tmp = Mapper.Map<Entit.EStoreShipmentType, AddOrEditEStoreShipmentTypeDto>(a_shipmentType);
            return tmp;
        }
    }
}