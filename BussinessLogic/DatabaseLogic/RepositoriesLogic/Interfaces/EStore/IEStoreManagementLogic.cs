using System.Collections.Generic;
using BussinessLogic.DTOs.EStore;
using BussinessLogic.DTOs.EStore.EStoreCategoryDto;
using BussinessLogic.DTOs.EStore.EStoreShipmentType;
using BussinessLogic.DTOs.EStore.EStoreWarehouseDto;
using BussinessLogic.DTOs.WarehouseDtos;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.EStore
{
    public interface IEStoreManagementLogic
    {
        IEnumerable<EStoreDto> GetAllEStores();
        bool AddEStore(AddEStoreDto a_eStore);
        bool DeleteEStore(int a_idEStore);
        EStoreDto GetEStore(int a_idEStore);
        EStoreManageWarehousesDto GetWarehouses(int a_idEStore);
        EStoreCategoryManageDto GetCategory(int a_idEStore);
        EditEStoreCategoryDto GetOneOfCategoryEdit(int a_category);

        bool AddEStoreWarehouse(int a_idEStore, int a_idWarehouse);
        bool DeleteEStoreWarehouse(int a_idEStore, int a_idWarehouse);

        bool AddEStoreCategory(AddEStoreCategoryDto a_category);
        bool DeleteEStoreCategory(int a_category);
        bool EditEStoreCategory(EditEStoreCategoryDto a_category);

        bool AddEStoreCategoryProductItem(int a_category, int a_idItem);
        bool DeleteEStoreCategoryProductItem(int a_category, int a_idItem);
        bool AddEStoreCategoryServiceItem(int a_category, int a_idItem);
        bool DeleteEStoreCategoryServiceItem(int a_category, int a_idItem);

        bool AddEStoreShipmentType(AddOrEditEStoreShipmentTypeDto a_shipmentType);
        bool DeleteEStoreShipmentType(int a_shipmentType, int a_idEStore);
        bool EditEStoreShipmentType(AddOrEditEStoreShipmentTypeDto a_shipmentType);

        EStoreShipmentTypeManageDto GetShipmentType(int a_idEStore);
        AddOrEditEStoreShipmentTypeDto GetOneOfShipmentTypeEdit(int a_shipmentType);
    }
}