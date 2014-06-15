using System.Collections.Generic;
using AutoMapper;
using BussinessLogic.DTOs.EStore.EStoreCategoryDto;
using Entit = Database.Entities.EStore.Category;

namespace BussinessLogic.Mappers.EStore.EStoreCategory
{
    public class EStoreCategoryDtoMapper
    {
        public EStoreCategoryDtoMapper()
        {
            Mapper.CreateMap<Entit.EStoreCategory, EStoreCategoryDto>()          
                  .ForMember(desc => desc.IdEStoreCategory, s => s.MapFrom(src => src.IdEStoreCategory))
                  .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                  .ForMember(desc => desc.SortOrder, s => s.MapFrom(src => src.SortOrder))
                  .ForMember(desc => desc.EStore, s => s.MapFrom(src => src.EStore))
                  .ForMember(desc => desc.ProductItems, s => s.MapFrom(src => src.ProductItems))
                  .ForMember(desc => desc.ServiceItems, s => s.MapFrom(src => src.ServiceItems));

        }

        public EStoreCategoryDto MapEntityToDto(Entit.EStoreCategory a_category)
        {
            return Mapper.Map<Entit.EStoreCategory, EStoreCategoryDto>(a_category);
        }

        public IEnumerable<EStoreCategoryDto> MapCollectionOfEntityToDto(IEnumerable<Entit.EStoreCategory> a_category)
        {
            var list = new List<EStoreCategoryDto>();
            if (a_category != null)
            {
                foreach (var item in a_category)
                {
                    list.Add(MapEntityToDto(item));
                }
            }

            return list;
        }
    }
}