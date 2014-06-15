using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs.EStore;
using Entit = Database.Entities.EStore;

namespace BussinessLogic.Mappers.EStore
{
    
    public class AddEStoreDtoMapper
    {
        public AddEStoreDtoMapper()
        {
            Mapper.CreateMap<AddEStoreDto,Entit.EStore>()
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name));

        }

        public Entit.EStore MapDtoToEntity(AddEStoreDto a_eStore)
        {
            var tmp = Mapper.Map<AddEStoreDto,Entit.EStore>(a_eStore);
            tmp.Warehouses = new List<Database.Entities.WarehouseEntities.Warehouse>();
            return tmp;
        }
    }
}
