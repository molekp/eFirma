using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs.Employee;
using Database.Entities;

namespace BussinessLogic.Mappers.Employee
{
    public class AddEmployeeDtoMapper
    {
        public AddEmployeeDtoMapper()
        {

            Mapper.CreateMap<AddEmployeeDto, EmployeeEntity>()
                  .ForMember(desc => desc.FirstName, s => s.MapFrom(src => src.FirstName))
                  .ForMember(desc => desc.LastName, s => s.MapFrom(src => src.LastName))
                  .ForMember(desc => desc.Phone, s => s.MapFrom(src => src.Phone))
                  .ForMember(desc => desc.Salary, s => s.MapFrom(src => src.Salary))
                  .ForMember(desc => desc.Address, s => s.MapFrom(src => src.Address))
                  .ForMember(desc => desc.BankAccountNumber, s => s.MapFrom(src => src.BankAccountNumber));

        }

        public EmployeeEntity MapDtoToEntity(AddEmployeeDto a_employeeDto, UserEntity a_userEntity)
        {
            var employee= Mapper.Map<AddEmployeeDto, EmployeeEntity>(a_employeeDto);
            employee.UserEntity = a_userEntity;
            return employee;
        }
    }
}
