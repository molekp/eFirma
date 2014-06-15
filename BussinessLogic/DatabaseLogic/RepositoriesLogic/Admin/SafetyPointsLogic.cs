using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BussinessLogic.DTOs.Admin;
using BussinessLogic.DTOs.ViewModelsOnly;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Safety;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Admin;
using BussinessLogic.Mappers.Admin;
using BussinessLogic.Mappers.Safety;
using Database.Entities.Safety;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Admin
{
    public class SafetyPointsLogic : ISafetyPointsLogic
    {
        public ITypesOfSafetyPointsRepository TypesOfSafetyPointsRepository { get; set; }
        public IWarehouseRepository WarehouseRepository { get; set; }
        public ISafetyPointRepository SafetyPointRepository { get; set; }
        public ISafetyPointGroupsRepository SafetyPointGroupsRepository { get; set; }
        public AddNewSafetyPointDtoMapper SafetyPointMapper { get; set; }
        public DispalySafetyPointDtoMapper DispalySafetyPointDtoMapper { get; set; }
        public DispalySafetyPointGroupDtoMapper DispalySafetyPointGroupDtoMapper { get; set; }
        public AddSafetyPointGroupMapper AddSafetyPointGroupMapper { get; set; }
        public EditSafetyPointGroupDtoMapper EditSafetyPointGroupDtoMapper { get; set; }

        public IEnumerable<SelectListItem> GetAllTypesOfSafetyPoints()
        {
            var safetyPoints = TypesOfSafetyPointsRepository.GetAll();

            return new SelectList(safetyPoints, "IdTypeOfSafetyPoint", "Name",null);
        }

        public List<SelectListItem> GetRecordsForType(int a_idTypeOfSafetyPoint)
        {
            var type=TypesOfSafetyPointsRepository.Get(a_idTypeOfSafetyPoint);
            var result = new List<SelectListItem>();

            if (null == type) return result;

            switch (type.Name)
            {
                case "Warehouse":
                    var warehouses = WarehouseRepository.GetAll();
                    foreach (var warehouse in warehouses)
                    {
                        result.Add(new SelectListItem{ Selected = false, Text = warehouse.Name,Value = warehouse.IdWarehouse.ToString()});
                    }
                    break;
            }

            return result;
        }

        public bool CreateSafetyPoint(AddNewSafetyPointDto a_newSafetyPointDto)
        {
            var typeOfSafetyPoint = TypesOfSafetyPointsRepository.Get(a_newSafetyPointDto.IdTypeOfSafetyPoint);
            if (typeOfSafetyPoint == null) return false;

            var safetyPoint = SafetyPointMapper.MapDtoToEntity(a_newSafetyPointDto,typeOfSafetyPoint);

            return SafetyPointRepository.CreateSafetyPoint(safetyPoint);
        }

        public IEnumerable<DisplaySafetyPointDto> GetAllDisplaySafetyPoints()//TODO poprawic testy partial mock i stub na NameForRecordInTable
        {
            var safetyPoints = SafetyPointRepository.GetAll();

            var displaySp = new List<DisplaySafetyPointDto>();
            foreach (var safetyPoint in safetyPoints)
            {
                displaySp.Add(DispalySafetyPointDtoMapper.MapEntityToDto(safetyPoint, NameForRecordInTable(safetyPoint)));
            }

            return displaySp;
        }

        public int CreateSafetyPointGroup(AddSafetyPointGroup a_addSafetyPointGroup)
        {
            if (a_addSafetyPointGroup.GroupName==null || a_addSafetyPointGroup.GroupName.Equals("")) return 0;
            var newSafetyPointGroup = AddSafetyPointGroupMapper.MapDtoToEntity(a_addSafetyPointGroup);

            return SafetyPointGroupsRepository.CreateSafetyPointGroup(newSafetyPointGroup);
        }

        public IEnumerable<DisplaySafetyPointGroupDto> GetAllDisplaySafetyPointsGroups()//TODO zrobic wyliczanie uzytkownikow w grupie
        {
            var groups = SafetyPointGroupsRepository.GetAll();
            var returns = new List<DisplaySafetyPointGroupDto>();
            foreach (var safetyPointGroup in groups)
            {
                returns.Add( DispalySafetyPointGroupDtoMapper.MapEntityToDto(safetyPointGroup,0));
            }

            return returns;
        }

        public bool RemoveSafetyPointGroup(int a_idGroup)
        {
            return SafetyPointGroupsRepository.RemoveSafetyPointGroup(a_idGroup);
        }

        public EditSafetyPointGroupDto GetEditSafetyPointGroupDto(int a_idSafetyPointGroup)
        {
            var group = SafetyPointGroupsRepository.Get(a_idSafetyPointGroup);
            if (group == null) return null;

            var safetyPointsChoices = SafetyPointRepository.GetAll().ToList();
            var displaySafetyPoints = new List<DisplaySafetyPointDto>();
            foreach (var safetyPoint in group.SafetyPoints)
            {
                displaySafetyPoints.Add(DispalySafetyPointDtoMapper.MapEntityToDto(safetyPoint, NameForRecordInTable(safetyPoint)));
                safetyPointsChoices.Remove(safetyPoint);
            }

            var selectListChoicesToAdd = new List<SelectListItem>();
            selectListChoicesToAdd.Add(new SelectListItem() { Selected = false, Text = "---wybierz---", Value = "0" });
            foreach (var safetyPoint in safetyPointsChoices)
            {
                selectListChoicesToAdd.Add(new SelectListItem() { Selected = false, Text = safetyPoint.NameOfsafetyPoint,Value = safetyPoint.IdSafetyPoint.ToString() });
            }


            return EditSafetyPointGroupDtoMapper.MapEntityToDto(group, displaySafetyPoints, selectListChoicesToAdd);
        }

        public virtual string NameForRecordInTable(SafetyPoint a_safetyPoint)//TODO test, zamiast switch jak poznac ktora to tabela ? jakis interfejs pewnie z metoda GetAllRows i rows musza miec property name
        {
            string nameRecordInTable = "";
            switch (a_safetyPoint.TypeOfSafetyPoint.Name)
            {
                case "Warehouse":
                    var tmp = WarehouseRepository.Get(a_safetyPoint.IdOfPointInTable);
                    if (null == tmp)
                    {
                        return null;
                    }
                    nameRecordInTable = tmp.Name;
                    break;
            }
            return nameRecordInTable;
        }

        public bool AddSafetyPointToGroup(int a_idSafetyPointGroup, int a_idSafetyPoint)
        {
            var group = SafetyPointGroupsRepository.Get(a_idSafetyPointGroup);
            if (group == null) return false;

            if (group.SafetyPoints.FirstOrDefault(x => x.IdSafetyPoint == a_idSafetyPoint) != null) return false;

            var point = SafetyPointRepository.Get(a_idSafetyPoint);
            if (point == null) return false;

            return SafetyPointGroupsRepository.AddSafetyPointToGroup(group, point);
        }

        public bool RenameSafetyPointGroup(int a_idSafetyPointGroup, string a_nameOfsafetyPointGroup)
        {
            var group = SafetyPointGroupsRepository.Get(a_idSafetyPointGroup);

            if (null == group) return false;

            return SafetyPointGroupsRepository.RenameSafetyPointGroup(group, a_nameOfsafetyPointGroup);
        }

        public bool RemoveSafetyPointFromGroup(RemoveSafetyPointFromGroup a_removeSafetyPointFromGroup)
        {
            var group = SafetyPointGroupsRepository.Get(a_removeSafetyPointFromGroup.IdSafetyPointGroup);

            var safetyPoint = SafetyPointRepository.Get(a_removeSafetyPointFromGroup.IdSafetyPoint);

            if (null == group || null == safetyPoint || null == group.SafetyPoints.FirstOrDefault(x=>x.IdSafetyPoint == safetyPoint.IdSafetyPoint))
            {
                return false;
            }

            return SafetyPointGroupsRepository.RemoveSafetyPointFromGroup(group,safetyPoint);
        }
    }
}
