using System.Collections.Generic;
using BussinessLogic.DTOs.Admin;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Safety;
using BussinessLogic.Mappers.Admin;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Admin
{
    public interface IUserManagementAdminLogic
    {
        IUserDataBaseRepository UserDataBaseRepository { get; set; }
        ISafetyPointGroupsRepository SafetyPointGroupsRepository { get; set; }
        AdminDisplayUserDtoMapper AdminDisplayUserDtoMapper { get; set; }
        UserForManageSafetyPointGroupsMapper UserForManageSafetyPointGroupsMapper { get; set; }
        SafetyPointGroupForUserManageMapper SafetyPointGroupForUserManageMapper { get; set; }



        IEnumerable<AdminDisplayUserDto> GetAllUsersForDiplay   ();
        UserForManageSafetyPointGroups GetUserForManageSafetyPointGroups(int a_idUser);
        bool AddUserToSafetyPointGroup(int a_idUser, int a_idAddToGroup);
        bool RemoveUserFromSafetyPointGroup(int a_idUser, int a_idSafetyPointGroup);
    }
}