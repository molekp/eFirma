using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinessLogic.DTOs.EStore;
using BussinessLogic.DTOs.EStore.EStoreCategoryDto;
using BussinessLogic.DTOs.EStore.EStoreShipmentType;
using BussinessLogic.DTOs.EStore.EStoreWarehouseDto;
using BussinessLogic.DTOs.WarehouseDtos;
using BussinessLogic.DatabaseLogic.Repositories.EStore;
using BussinessLogic.DatabaseLogic.Repositories.EStore.EStoreCategory;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.EStore;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.EStore.EStoreCategory;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.EStore.EStoreShipmentType;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.EStore;
using BussinessLogic.Mappers.EStore;
using BussinessLogic.Mappers.EStore.EStoreCategory;
using BussinessLogic.Mappers.EStore.EStoreWarehouse;
using BussinessLogic.Mappers.EStore.EStoryShipmentType;
using Entit = Database.Entities.EStore;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.EStore
{
    class EStoreManagementLogic : IEStoreManagementLogic
    {
        public IEStoreRepository EStoreRepository { get; set; }
        public IEStoreCategoryRepository EStoreCategoryRepository { get; set; }
        public IEStoreShipmentTypeRepository EStoreShipmentTypeRepository { get; set; }
        public EStoreMapper EStoreMapper { get; set; }
        public AddEStoreDtoMapper AddEStoreDtoMapper { get; set; }
        public EStoreWarehouseDtoMapper EStoreWarehouseDtoMapper { get; set; }
        public EStoreCategoryDtoMapper EStoreCategoryDtoMapper { get; set; }
        public AddEStoreCategoryDtoMapper AddEStoreCategoryDtoMapper { get; set; }
        public EditEStoreCategoryDtoMapper EditEStoreCategoryDtoMapper { get; set; }
        public AddOrEditEStoreShipmentTypeDtoMapper AddOrEditEStoreShipmentTypeDtoMapper { get; set; }
        public EStoreShipmentTypeDtoMapper EStoreShipmentTypeDtoMapper { get; set; }

        public IEnumerable<EStoreDto> GetAllEStores()
        {
            var stores = EStoreRepository.GetAll();
            return stores.Select(store => EStoreMapper.MapEntityToDto(store)).ToList();
        }

        public bool AddEStore(AddEStoreDto a_eStore)
        {
            var store = AddEStoreDtoMapper.MapDtoToEntity(a_eStore);

            store.UniqHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(DateTime.Now.ToString()));

            return EStoreRepository.Add(store);
        }

        public bool DeleteEStore(int a_idEStore)
        {
            var store = EStoreRepository.Get(a_idEStore);

            return EStoreRepository.Remove(store);
        }

        public EStoreDto GetEStore(int a_idEStore)
        {
            var store = EStoreRepository.Get(a_idEStore);
            return EStoreMapper.MapEntityToDto(store);
        }

        public EStoreManageWarehousesDto GetWarehouses(int a_idEStore)
        {
            var warehouseActive = EStoreRepository.GetWarehouseActive(a_idEStore);
            var warehouseNotActive = EStoreRepository.GetWarehouseOffline(a_idEStore);

            var objectFirst = EStoreWarehouseDtoMapper.MapCollectionOfEntityToDto(warehouseActive);
            var objectSecond = EStoreWarehouseDtoMapper.MapCollectionOfEntityToDto(warehouseNotActive);

            var result = new EStoreManageWarehousesDto()
                             {
                                 WarehousesWhichIsActive = objectFirst,
                                 WarehousesWhichIsNotActive = objectSecond
                             };

            return result;
        }

        public EStoreCategoryManageDto GetCategory(int a_idEStore)
        {
            var store = EStoreRepository.Get(a_idEStore);
            var category = EStoreCategoryRepository.GetAllOfEStore(store);

            return new EStoreCategoryManageDto()
                       {
                           IdEStore = a_idEStore,
                           CategoryOfEStore = EStoreCategoryDtoMapper.MapCollectionOfEntityToDto(category).OrderBy(c=>c.SortOrder)
                       };

        }

        public EditEStoreCategoryDto GetOneOfCategoryEdit(int a_category)
        {
            var category = EStoreCategoryRepository.Get(a_category);
            return EditEStoreCategoryDtoMapper.MapEntityToDto(category);
        }

        public bool AddEStoreWarehouse(int a_idEStore, int a_idWarehouse)
        {
            var store = EStoreRepository.Get(a_idEStore);

            var warehouse = EStoreRepository.GetWarehouseOffline(a_idEStore).SingleOrDefault(c => c.IdWarehouse == a_idWarehouse);

            return warehouse != null && EStoreRepository.AddWarehouse(store, warehouse);
        }

        public bool DeleteEStoreWarehouse(int a_idEStore, int a_idWarehouse)
        {
            var store = EStoreRepository.Get(a_idEStore);

            var warehouse = store.Warehouses.SingleOrDefault(c => c.IdWarehouse == a_idWarehouse);

            return warehouse != null && EStoreRepository.RemoveWarehouse(store, warehouse);
        }

        public bool AddEStoreCategory(AddEStoreCategoryDto a_category)
        {
            var store = EStoreRepository.Get(a_category.IdEStore);

            var category = AddEStoreCategoryDtoMapper.MapDtoToEntity(a_category, store);

            return EStoreCategoryRepository.Add(category);
        }

        public bool DeleteEStoreCategory(int a_category)
        {
            var category = EStoreCategoryRepository.Get(a_category);

            return EStoreCategoryRepository.Remove(category);
        }


        public bool EditEStoreCategory(EditEStoreCategoryDto a_category)
        {
            var category = EStoreCategoryRepository.Get(a_category.IdEStoreCategory);

            category.Name = a_category.Name;
            category.SortOrder = a_category.SortOrder;

            return EStoreCategoryRepository.Edit(category);
        }

        public bool AddEStoreCategoryProductItem(int a_category, int a_idItem)
        {
            var category = EStoreCategoryRepository.Get(a_category);

            var product = EStoreCategoryRepository.GetProductItem(a_idItem);

            return EStoreCategoryRepository.AddItem(category, product);
        }

        public bool DeleteEStoreCategoryProductItem(int a_category, int a_idItem)
        {
            var category = EStoreCategoryRepository.Get(a_category);

            var product = EStoreCategoryRepository.GetProductItem(a_idItem);

            return EStoreCategoryRepository.RemoveItem(category, product);
        }

        public bool AddEStoreCategoryServiceItem(int a_category, int a_idItem)
        {
            var category = EStoreCategoryRepository.Get(a_category);

            var product = EStoreCategoryRepository.GetServiceItem(a_idItem);

            return EStoreCategoryRepository.AddItem(category, product);
        }

        public bool DeleteEStoreCategoryServiceItem(int a_category, int a_idItem)
        {
            var category = EStoreCategoryRepository.Get(a_category);

            var product = EStoreCategoryRepository.GetServiceItem(a_idItem);

            return EStoreCategoryRepository.RemoveItem(category, product);
        }

        public bool AddEStoreShipmentType(AddOrEditEStoreShipmentTypeDto a_shipmentType)
        {
            var store = EStoreRepository.Get(a_shipmentType.IdEStore);

            var shipmentType = AddOrEditEStoreShipmentTypeDtoMapper.MapDtoToEntity(a_shipmentType);

            return EStoreShipmentTypeRepository.Add(shipmentType, store);
        }


        public bool DeleteEStoreShipmentType(int a_shipmentType, int a_idEStore)
        {
            var shipmentType = EStoreShipmentTypeRepository.Get(a_shipmentType);
            var store = EStoreRepository.Get(a_idEStore);
            return EStoreShipmentTypeRepository.Remove(shipmentType,store);
        }

        public bool EditEStoreShipmentType(AddOrEditEStoreShipmentTypeDto a_shipmentType)
        {
            var shipmentType = EStoreShipmentTypeRepository.Get(a_shipmentType.IdEStoreShipmentType);

            shipmentType.Name = a_shipmentType.Name;
            shipmentType.SortOrder = a_shipmentType.Sortorder;
            shipmentType.Info = a_shipmentType.Info;
            shipmentType.PriceShipment = a_shipmentType.PriceShipment;

            return EStoreShipmentTypeRepository.Edit(shipmentType);
        }

        public EStoreShipmentTypeManageDto GetShipmentType(int a_idEStore)
        {
            var store = EStoreRepository.Get(a_idEStore);
            var category = EStoreShipmentTypeRepository.GetAllOfEStore(store);

            return new EStoreShipmentTypeManageDto()
            {
                IdEStore = a_idEStore,
                EStoreShipmentTypeDto = EStoreShipmentTypeDtoMapper.MapCollectionOfEntityToDto(category).OrderBy(c => c.SortOrder)
            };

        }

        public AddOrEditEStoreShipmentTypeDto GetOneOfShipmentTypeEdit(int a_shipmentType)
        {
            var shipmentType = EStoreShipmentTypeRepository.Get(a_shipmentType);
            return AddOrEditEStoreShipmentTypeDtoMapper.MapEntityToDto(shipmentType);
        }
    }
}
