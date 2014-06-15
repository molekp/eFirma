using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinessLogic.DTOs.WarehouseDtos;
using BussinessLogic.DTOs.WarehouseDtos.Distributions;
using BussinessLogic.DTOs.WarehouseDtos.Items;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse
{
    public interface IDistributionLogic
    {
        IEnumerable<SearchDistributionDto> GetAllDistributionQueue();
        DisplayDistributionDto GetDisplayDistribution(int a_distributionId);
        PerformDistributionDto GetPerformDistribution(int a_distributionId);
        bool PerformDistribution(int a_idDistribution);
        EditDistributionDto GetEditDistributionDto(int a_idDistribution);
        bool RemoveProductItemFrom(int a_distributionId, int a_itemId, ItemTypeEnum a_itemType);
        bool EditDistributionDto(EditDistributionDto a_distributionDto);
        AddDistributionDto GetAddDistributionDto();
        /// <summary>
        /// returns -1 if not added. Otherwise it returns id of added distribution
        /// </summary>
        int AddDistribution(AddDistributionDto a_distributionDto, string a_loggedUserName);
    }
}
