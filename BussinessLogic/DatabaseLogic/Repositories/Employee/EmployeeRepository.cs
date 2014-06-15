using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Employee;
using Database.Core.Interfaces;
using Database.Entities;

namespace BussinessLogic.DatabaseLogic.Repositories.Employee
{
    public class EmployeeRepository :IEmployeeRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }
        public IEnumerable<EmployeeEntity> GetAll()
        {
            return DataBaseContext.Employees.ToList();
        }

        public EmployeeEntity Get(int a_employeeId)
        {
            return DataBaseContext.Employees.FirstOrDefault(x => x.IdEmployee == a_employeeId);
        }

        public bool CreateEmployee(EmployeeEntity a_employee)
        {
            try
            {
                DataBaseContext.Employees.Add(a_employee);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool EditEmployee(EmployeeEntity a_employee)
        {
            try
            {
                DataBaseContext.SetModified(a_employee);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
