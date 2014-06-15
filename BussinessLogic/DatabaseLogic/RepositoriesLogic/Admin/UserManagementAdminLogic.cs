using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BussinessLogic.DTOs.Admin;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Safety;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Admin;
using BussinessLogic.Mappers.Admin;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Admin
{
    public class UserManagementAdminLogic : IUserManagementAdminLogic
    {
        public IUserDataBaseRepository UserDataBaseRepository { get; set; }
        public ISafetyPointGroupsRepository SafetyPointGroupsRepository { get; set; }
        public AdminDisplayUserDtoMapper AdminDisplayUserDtoMapper { get; set; }
        public UserForManageSafetyPointGroupsMapper UserForManageSafetyPointGroupsMapper { get; set; }
        public SafetyPointGroupForUserManageMapper SafetyPointGroupForUserManageMapper { get; set; }


        public IEnumerable<AdminDisplayUserDto> GetAllUsersForDiplay()
        {
            var users = UserDataBaseRepository.GetAllUsers();

            return users.Select(userEntity => AdminDisplayUserDtoMapper.MapEntityToDto(userEntity)).ToList();
        }

        public UserForManageSafetyPointGroups GetUserForManageSafetyPointGroups(int a_idUser)
        {
            var userEntity = UserDataBaseRepository.GetUser(a_idUser);
            var currentGroups = new List<SafetyPointGroupForUserManage>();
            var choicesGroupDtos = new List<SelectListItem>(){new SelectListItem{Selected = true,Text = "---wybierz---",Value = "0"}};

            var choicesSafetyPointGroups = SafetyPointGroupsRepository.GetAll().ToList();

            foreach (var safetyPointGroup in userEntity.UserSafetyPointGroups)
            {
                choicesSafetyPointGroups.Remove(safetyPointGroup);
                currentGroups.Add(SafetyPointGroupForUserManageMapper.MapEntityToDto(safetyPointGroup));
            }

            foreach (var choiceSafetyPointGroup in choicesSafetyPointGroups)
            {
                choicesGroupDtos.Add(new SelectListItem{Selected = false,Text = choiceSafetyPointGroup.GroupName,Value = choiceSafetyPointGroup.IdSafetyPointGroup.ToString()});
            }

            return UserForManageSafetyPointGroupsMapper.MapEntityToDto(userEntity,currentGroups,choicesGroupDtos);
        }

        public bool AddUserToSafetyPointGroup(int a_idUser, int a_idAddToGroup)
        {
            var user = UserDataBaseRepository.GetUser(a_idUser);
            var group = SafetyPointGroupsRepository.Get(a_idAddToGroup);

            if (null == user || null == group || user.UserSafetyPointGroups.Contains(group))
                return false;

            return UserDataBaseRepository.AddUserToSafetyPointGroup(user, group);
        }

        public bool RemoveUserFromSafetyPointGroup(int a_idUser, int a_idSafetyPointGroup)//TODO tests
        {
            var user = UserDataBaseRepository.GetUser(a_idUser);
            var group = SafetyPointGroupsRepository.Get(a_idSafetyPointGroup);

            if (null == user || null == group || false == user.UserSafetyPointGroups.Contains(group))
                return false;

            return UserDataBaseRepository.RemoveUserFromSafetyPointGroup(user,group);
        }
    }
}
