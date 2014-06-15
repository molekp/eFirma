using System.Collections.Generic;
using BussinessLogic.DTOs.EStore.EStoreDisplayDto;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.EStore;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.EStore.EStoreDisplay;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.EStore;
using BussinessLogic.Mappers.EStore.EStoreDisplay;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.EStore
{
    public class EStoreDisplayLogic : IEStoreDisplayLogic 
    {
        private int idTypeOfProduct = 1;

        public IEStoreDisplayRepository EStoreDisplayRepository { get; set; }
        public EStoreDisplayItemDtoMapper EStoreDisplayItemDtoMapper { get; set; }
        public EStoreDisplayItemTypeDtoMapper EStoreDisplayItemTypeDtoMapper { get; set; }
        public EStoreDisplayDetailItemDtoMapper EStoreDisplayDetailItemDtoMapper { get; set; }


        public EStoreDisplayIndexDto GetEStoreDisplayIndexDto(int a_idEStore, int a_typeItems)
        {
            var result = new EStoreDisplayIndexDto();

            result.IdEStore = a_idEStore;

            if (a_typeItems == idTypeOfProduct)
            {
                result.EStoreDisplayItemsDto = new EStoreDisplayItemsDto()
                                                   {
                                                       IdEStore = a_idEStore,
                                                       Items =
                                                           EStoreDisplayItemDtoMapper.MapCollectionOfEntityToDto(
                                                               EStoreDisplayRepository.GetProducts(a_idEStore)),
                                                       TypeOfItem = a_typeItems
                                                   };
            }
            else
            {
                result.EStoreDisplayItemsDto = new EStoreDisplayItemsDto()
                                                   {
                                                       IdEStore = a_idEStore,
                                                       Items =
                                                           EStoreDisplayItemDtoMapper.MapCollectionOfEntityToDto(
                                                               EStoreDisplayRepository.GetServices(a_idEStore)),
                                                       TypeOfItem = a_typeItems
                                                   };
            }

            result.EStoreDisplayMenu = new EStoreDisplayMenuDto()
            {
                IdEStore = a_idEStore,
                Type = a_typeItems
            };

            if (a_typeItems == idTypeOfProduct)
            {
                result.EStoreDisplayMenu.ItemTypes =
                    (List<EStoreDisplayItemTypeDto>) EStoreDisplayItemTypeDtoMapper.MapCollectionOfEntityItemTypeToDto(EStoreDisplayRepository.GetCollectionOfProductType(a_idEStore) );
            }
            else
            {
                result.EStoreDisplayMenu.ItemTypes =
                    (List<EStoreDisplayItemTypeDto>)EStoreDisplayItemTypeDtoMapper.MapCollectionOfEntityItemTypeToDto(EStoreDisplayRepository.GetCollectionOfServiceType(a_idEStore));
            }


            return result;
        }

        public EStoreDisplayDetailDto GetEStoreDisplayDetailDto(int a_idEStore, int a_typeItems, int a_item)
        {
            var result = new EStoreDisplayDetailDto();

            result.EStoreDisplayMenu = new EStoreDisplayMenuDto()
            {
                IdEStore = a_idEStore,
                Type = a_typeItems
            };

            if (a_typeItems == idTypeOfProduct)
            {
                result.EStoreDisplayMenu.ItemTypes =
                    (List<EStoreDisplayItemTypeDto>)EStoreDisplayItemTypeDtoMapper.MapCollectionOfEntityItemTypeToDto(EStoreDisplayRepository.GetCollectionOfProductType(a_idEStore));
            }
            else
            {
                result.EStoreDisplayMenu.ItemTypes =
                    (List<EStoreDisplayItemTypeDto>)EStoreDisplayItemTypeDtoMapper.MapCollectionOfEntityItemTypeToDto(EStoreDisplayRepository.GetCollectionOfServiceType(a_idEStore));
            }

            if (a_typeItems == idTypeOfProduct)
            {
                result.EStoreDisplayDetailItemDto = EStoreDisplayDetailItemDtoMapper.MapEntityToDto(EStoreDisplayRepository.GetProduct(a_item));
                
            }
            else
            {
                result.EStoreDisplayDetailItemDto =
                    EStoreDisplayDetailItemDtoMapper.MapEntityToDto(EStoreDisplayRepository.GetService(a_item));
            }
            
            


            return result;
        }
    }
}