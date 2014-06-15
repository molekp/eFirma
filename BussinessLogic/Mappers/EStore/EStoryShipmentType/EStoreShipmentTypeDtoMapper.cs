using System.Collections.Generic;
using AutoMapper;
using BussinessLogic.DTOs.EStore.EStoreShipmentType;
using Entit = Database.Entities.EStore;

namespace BussinessLogic.Mappers.EStore.EStoryShipmentType
{
    public class EStoreShipmentTypeDtoMapper
    {
        public EStoreShipmentTypeDtoMapper()
        {
            Mapper.CreateMap<Entit.EStoreShipmentType, EStoreShipmentTypeDto>()          
                  .ForMember(desc => desc.IdEStoreShipmentType, s => s.MapFrom(src => src.IdEStoreShipmentType))
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(desc => desc.SortOrder, s => s.MapFrom(src => src.SortOrder))
                  .ForMember(desc => desc.PriceShipment, s => s.MapFrom(src => src.PriceShipment))
                  .ForMember(desc => desc.Info, s => s.MapFrom(src => src.Info));

        }

        public EStoreShipmentTypeDto MapEntityToDto(Entit.EStoreShipmentType a_shipmentType)
        {
            return Mapper.Map<Entit.EStoreShipmentType, EStoreShipmentTypeDto>(a_shipmentType);
        }

        public IEnumerable<EStoreShipmentTypeDto> MapCollectionOfEntityToDto(IEnumerable<Entit.EStoreShipmentType> a_shipmentType)
        {
            var list = new List<EStoreShipmentTypeDto>();
            if (a_shipmentType != null)
            {
                foreach (var item in a_shipmentType)
                {
                    list.Add(MapEntityToDto(item));
                }
            }

            return list;
        }
    }
}