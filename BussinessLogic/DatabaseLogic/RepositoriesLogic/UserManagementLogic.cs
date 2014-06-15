using BussinessLogic.DTOs.Account;
using BussinessLogic.DatabaseLogic.Repositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces;
using BussinessLogic.Helpers;
using BussinessLogic.Mappers;
using BussinessLogic.Mappers.Account;
using Database.Entities;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic
{
    public class UserManagementLogic : IUserManagementLogic
    {
        public IUserDataBaseRepository UserDataBaseRepository { get; set; }
        public IRoleDataBaseRepository RoleDataBaseRepository { get; set; }
        public LogOnMapper LogOnMapper { private get; set; }
        public RegisterUserDtoMapper RegisterUserDtoMapper { get; set; }
        public EditUserToDtoMapper EditUserToDtoMapper { get; set; }
        public DetailsUserToDtoMapper DetailsUserToDtoMapper { get; set; }

        
        /// <summary>
        /// Tworzy uzytkownika i dodaje go do bazy danych
        /// </summary>
        /// <param name="a_createUserDto">Dto zawierające informacje o kandydacie oraz jego dane logowania</param>
        public bool CreateUser(RegisterUserDto a_createUserDto)
        {
            RoleEntity roleEntity = RoleDataBaseRepository.GetRoleForRoleName(ConstantsHelper.CUSTOMER_ROLE);
            if (roleEntity == null) return false;
            var userEntity = RegisterUserDtoMapper.MapDtoToEntity(a_createUserDto, roleEntity);
            return UserDataBaseRepository.CreateUser(userEntity);
        }

       /// <summary>
        /// Wyszukuje uzytkownika do widoku edycji uzytkownika
        /// </summary>
        /// <param name="a_userName"></param>
        /// <returns></returns>
        public EditUserDto GetUserForEdit(string a_userName)
        {
            var userEntity = UserDataBaseRepository.GetUser(a_userName);

            return EditUserToDtoMapper.MapEntityToDto(userEntity);
        }

        /// <summary>
        /// Wyszukuje uzytkownika do widoku detali uzytkownika
        /// </summary>
        /// <param name="a_userName"></param>
        /// <returns></returns>
        public DetailsUserDto GetUserForDetails(string a_userName)
        {
            UserEntity userEntity = UserDataBaseRepository.GetUser(a_userName);
            
            return DetailsUserToDtoMapper.MapEntityToDto(userEntity);
        }
        public string GetRoleNameForUserName(string a_userName)
        {
            return UserDataBaseRepository.GetRoleNameForUserName(a_userName);
        }

        public bool DoesUserExists(string a_userName)
        {
            return UserDataBaseRepository.DoesUserExists(a_userName);
        }

        public bool EditUser(EditUserDto a_editUserDto)
        {
            if (string.IsNullOrEmpty(a_editUserDto.NewPassword) || string.IsNullOrEmpty(a_editUserDto.ConfirmPassword))
                return false;
            if (!a_editUserDto.NewPassword.Equals(a_editUserDto.ConfirmPassword))
                return false;
            var userEntity = UserDataBaseRepository.GetUser(a_editUserDto.IdUser);
            userEntity.EMail = a_editUserDto.Email;
            userEntity.Password = Hasher.HashPassword(a_editUserDto.NewPassword);
            return UserDataBaseRepository.EditUser(userEntity);
        }
    }
}
