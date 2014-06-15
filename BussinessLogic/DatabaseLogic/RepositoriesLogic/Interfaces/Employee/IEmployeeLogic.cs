using System.Collections.Generic;
using BussinessLogic.DTOs.Employee;
using Database.Core.Interfaces;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Employee
{
    public interface IEmployeeLogic
    {
        IEnumerable<SearchEmployeeDto> GetAllDisplayEmployeesDto();
        DisplayEmployeeDto GetDisplayEmployeeDto(int a_employeeId, string a_currentUserName);
        AddEmployeeDto GetAddEmployeeDto();
        EditEmployeeDto GetEditEmployee(int a_employeeId);
        bool DoesEmployeeExists(string a_userName);
        bool CreateEmployee(AddEmployeeDto a_employeeDto);
        bool EditEmployee(EditEmployeeDto a_editEmployeeDto);
    }
}
