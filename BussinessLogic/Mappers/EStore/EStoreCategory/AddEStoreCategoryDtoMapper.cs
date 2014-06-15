using System.Collections.Generic;
using AutoMapper;
using BussinessLogic.DTOs.EStore.EStoreCategoryDto;
using Entit = Database.Entities.EStore;

namespace BussinessLogic.Mappers.EStore.EStoreCategory
{
    public class AddEStoreCategoryDtoMapper
    {
        public AddEStoreCategoryDtoMapper()
        {
            Mapper.CreateMap<AddEStoreCategoryDto, Entit.Category.EStoreCategory>()
                .ForMember(desc => desc.Name, s => s.MapFrom(src => src.Name))
                .ForMember(desc => desc.SortOrder, s => s.MapFrom(src => src.SortOrder));

        }

        public Entit.Category.EStoreCategory MapDtoToEntity(AddEStoreCategoryDto a_category, Entit.EStore a_eStore)
        {
            var tmp = Mapper.Map<AddEStoreCategoryDto, Entit.Category.EStoreCategory>(a_category);
            tmp.ProductItems = new List<Database.Entities.WarehouseEntities.Product.ProductItem>();
            tmp.ServiceItems = new List<Database.Entities.WarehouseEntities.Service.ServiceItem>();
            tmp.EStore = a_eStore;
            return tmp;
        } 
    }
}