using System.Collections.Generic;
using System.Web.Mvc;
using BussinessLogic.DTOs.Admin;
using BussinessLogic.DTOs.ViewModelsOnly;
using BussinessLogic.DatabaseLogic.Repositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Safety;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using BussinessLogic.Mappers.Safety;
using Database.Entities.Safety;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Admin
{
    public interface ISafetyPointsLogic
    {
        ITypesOfSafetyPointsRepository TypesOfSafetyPointsRepository { get; set; }
        IWarehouseRepository WarehouseRepository { get; set; }
        ISafetyPointRepository SafetyPointRepository { get; set; }
        ISafetyPointGroupsRepository SafetyPointGroupsRepository { get; set; }
        AddNewSafetyPointDtoMapper SafetyPointMapper { get; set; }
        DispalySafetyPointDtoMapper DispalySafetyPointDtoMapper { get; set; }
        AddSafetyPointGroupMapper AddSafetyPointGroupMapper { get; set; }
        EditSafetyPointGroupDtoMapper EditSafetyPointGroupDtoMapper { get; set; }

        IEnumerable<SelectListItem> GetAllTypesOfSafetyPoints();
        List<SelectListItem> GetRecordsForType(int a_idTypeOfSafetyPoint);
        bool CreateSafetyPoint(AddNewSafetyPointDto a_newSafetyPointDto);
        IEnumerable<DisplaySafetyPointDto> GetAllDisplaySafetyPoints();
        int CreateSafetyPointGroup(AddSafetyPointGroup a_addSafetyPointGroup);
        IEnumerable<DisplaySafetyPointGroupDto> GetAllDisplaySafetyPointsGroups();
        bool RemoveSafetyPointGroup(int a_idGroup);
        EditSafetyPointGroupDto GetEditSafetyPointGroupDto(int a_idSafetyPointGroup);
        string NameForRecordInTable(SafetyPoint a_safetyPoint);
        bool AddSafetyPointToGroup(int a_idSafetyPointGroup, int a_idSafetyPoint);
        bool RenameSafetyPointGroup(int a_idSafetyPointGroup, string a_nameOfsafetyPointGroup);
        bool RemoveSafetyPointFromGroup(RemoveSafetyPointFromGroup a_removeSafetyPointFromGroup);
    }
}