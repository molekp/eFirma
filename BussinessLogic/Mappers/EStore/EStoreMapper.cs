using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs.EStore;
using Entit = Database.Entities.EStore;

namespace BussinessLogic.Mappers.EStore
{
    
    class EStoreMapper
    {
        public EStoreMapper()
        {
            Mapper.CreateMap<Entit.EStore, EStoreDto>()          
                  .ForMember(desc => desc.IdEStore, s => s.MapFrom(src => src.IdEStore))
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(desc => desc.Warehouses, s => s.MapFrom(src => src.Warehouses));

        }

        public EStoreDto MapEntityToDto(Entit.EStore a_eStore)
        {
            return Mapper.Map<Entit.EStore, EStoreDto>(a_eStore);
        }
    }
}
