using System.Collections.Generic;
using AutoMapper;
using BussinessLogic.DTOs.EStore.EStoreCategoryDto;
using Entit = Database.Entities.EStore;

namespace BussinessLogic.Mappers.EStore.EStoreCategory
{
    public class EditEStoreCategoryDtoMapper
    {
        public EditEStoreCategoryDtoMapper()
        {
            Mapper.CreateMap<EditEStoreCategoryDto, Entit.Category.EStoreCategory>()
                .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                .ForMember(desc => desc.IdEStoreCategory, s => s.MapFrom(src => src.IdEStoreCategory))
                .ForMember(desc => desc.SortOrder, s => s.MapFrom(src => src.SortOrder));

            Mapper.CreateMap< Entit.Category.EStoreCategory,EditEStoreCategoryDto>()
                .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                .ForMember(desc => desc.IdEStoreCategory, s => s.MapFrom(src => src.IdEStoreCategory))
                .ForMember(desc => desc.IdEStore, s => s.MapFrom(src => src.EStore.IdEStore))
                .ForMember(desc => desc.SortOrder, s => s.MapFrom(src => src.SortOrder));

        }

        public Entit.Category.EStoreCategory MapDtoToEntity(EditEStoreCategoryDto a_category, Entit.EStore a_eStore)
        {
            var tmp = Mapper.Map<EditEStoreCategoryDto, Entit.Category.EStoreCategory>(a_category);
            tmp.ProductItems = new List<Database.Entities.WarehouseEntities.Product.ProductItem>();
            tmp.ServiceItems = new List<Database.Entities.WarehouseEntities.Service.ServiceItem>();
            tmp.EStore = a_eStore;
            return tmp;
        }

        public EditEStoreCategoryDto MapEntityToDto(Entit.Category.EStoreCategory a_category)
        {
            return Mapper.Map<Entit.Category.EStoreCategory, EditEStoreCategoryDto>(a_category);
        }
    }
}