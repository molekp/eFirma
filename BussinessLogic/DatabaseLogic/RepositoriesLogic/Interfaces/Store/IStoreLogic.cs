using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinessLogic.DTOs.Store;
using BussinessLogic.DTOs.WarehouseDtos.Items;
using BussinessLogic.Mappers.Store;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Store
{
    public interface IStoreLogic
    {
        IEnumerable<DisplayStoreDto> GetAllDisplayStoreDto();
        DisplayStoreDtoMapper DisplayStoreDtoMapper { get; set; }
        SimpleDisplayStoreDto GetDisplayStoreDto(int a_storeId);
        StoreSellDto GetStoreSellDto(int a_storeId, int a_distributionId);
        IEnumerable<SoldItemDto> GetAllSoldItemDto(int a_storeId);
        ComplaintDto GetComplaintDto(int a_distributionId, int a_itemId, ItemTypeEnum a_itemType);
        bool AddComplaint(ComplaintDto a_complaintDto);
        ReturnDto GetReturnDto(int a_distributionId, int a_itemId, ItemTypeEnum a_itemType);
        bool ReturnItem(ReturnDto a_returnDto);
    }
}
