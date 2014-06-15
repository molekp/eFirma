using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.Employee
{
    public interface IEmployeeRepository
    {
        IDataBaseContext DataBaseContext { get; set; }
        IEnumerable<EmployeeEntity> GetAll();
        EmployeeEntity Get(int a_employeeId);
        bool CreateEmployee(EmployeeEntity a_employee);
        bool EditEmployee(EmployeeEntity a_employee);
    }
}
