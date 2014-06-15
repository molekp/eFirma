using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BussinessLogic.DTOs.Employee;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Employee;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Employee;
using BussinessLogic.Helpers;
using BussinessLogic.Mappers.Employee;
using Database.Entities;
using Database.Entities.Safety;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Employee
{
    public class EmployeeLogic :IEmployeeLogic
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IUserDataBaseRepository UserDataBaseRepository { get; set; }
        public SearchEmployeeDtoMapper SearchEmployeeDtoMapper { get; set; }
        public DisplayEmployeeDtoMapper DisplayEmployeeDtoMapper { get; set; }
        public IRoleDataBaseRepository RoleDataBaseRepository { get; set; }
        public AddEmployeeDtoMapper AddEmployeeDtoMapper { get; set; }
        public EditEmployeeDtoMapper EditEmployeeDtoMapper { get; set; }

        public IEnumerable<SearchEmployeeDto> GetAllDisplayEmployeesDto()
        {
            var employees = EmployeeRepository.GetAll();

            return employees.Select(x => SearchEmployeeDtoMapper.MapEntityToDto(x));
        }

        public DisplayEmployeeDto GetDisplayEmployeeDto(int a_employeeId, string a_currentUserName)
        {
            var employee = EmployeeRepository.Get(a_employeeId);

            var employeeDto = DisplayEmployeeDtoMapper.MapEntityToDto(employee);
            if (!employeeDto.UserName.Equals(a_currentUserName))
            {
                employeeDto.Salary = 0;
            }
            return employeeDto;
        }

        public AddEmployeeDto GetAddEmployeeDto()
        {
            var roles = RoleDataBaseRepository.GetAll();
            var employeeDto = new AddEmployeeDto();
            employeeDto.ChoicesRoles =
                    roles.Select(x => new SelectListItem {Selected = false, Text = x.NameRole, Value = x.IdRole.ToString()});
            return employeeDto;
        }

        public EditEmployeeDto GetEditEmployee(int a_employeeId)
        {
            var employee = EmployeeRepository.Get(a_employeeId);
            var roles = RoleDataBaseRepository.GetAll();
            var choicesRoles =
                    roles.Select(x =>
                        {
                            if(x.IdRole == employee.UserEntity.Role.IdRole)
                              return new SelectListItem {Selected = true, Text = x.NameRole, Value = x.IdRole.ToString()};
                            return new SelectListItem {Selected = false, Text = x.NameRole, Value = x.IdRole.ToString()};
                        });

            return EditEmployeeDtoMapper.MapEntityToDto(employee,choicesRoles);
        }

        public bool DoesEmployeeExists(string a_userName)
        {
             return UserDataBaseRepository.DoesUserExists(a_userName);
        }

        public bool CreateEmployee(AddEmployeeDto a_employeeDto)
        {
            var role = RoleDataBaseRepository.GetRole(a_employeeDto.SelectedRole);
            if (role == null) return false;
            var user = new UserEntity()
                {
                        UserName = a_employeeDto.UserName,
                        EMail = a_employeeDto.EMail,
                        Password = Hasher.HashPassword(a_employeeDto.Password),
                        Role = role, UserSafetyPointGroups = new List<SafetyPointGroup>()
                };

            if (!UserDataBaseRepository.CreateUser(user)) return false;

            user = UserDataBaseRepository.GetUser(user.IdUser);
            var employee = AddEmployeeDtoMapper.MapDtoToEntity(a_employeeDto, user);
            return EmployeeRepository.CreateEmployee(employee);
        }

        public bool EditEmployee(EditEmployeeDto a_editEmployeeDto)
        {
            var employee = EmployeeRepository.Get(a_editEmployeeDto.IdEmployee);
            var role = RoleDataBaseRepository.GetRole(a_editEmployeeDto.SelectedRole);
            employee.UserEntity.Role = role;
            employee.FirstName = a_editEmployeeDto.FirstName;
            employee.Address = a_editEmployeeDto.Address;
            employee.BankAccountNumber = a_editEmployeeDto.BankAccountNumber;
            employee.LastName = a_editEmployeeDto.LastName;
            employee.Phone = a_editEmployeeDto.Phone;
            employee.Salary = a_editEmployeeDto.Salary;
            if (!string.IsNullOrEmpty(a_editEmployeeDto.Password))
                employee.UserEntity.Password = Hasher.HashPassword(a_editEmployeeDto.Password);

            return EmployeeRepository.EditEmployee(employee);
        }
    }
}
