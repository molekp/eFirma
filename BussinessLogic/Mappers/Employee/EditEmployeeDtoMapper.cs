using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using BussinessLogic.DTOs.Employee;
using Database.Entities;

namespace BussinessLogic.Mappers.Employee
{
    public class EditEmployeeDtoMapper
    {
        public EditEmployeeDtoMapper()
        {

            Mapper.CreateMap<EmployeeEntity, EditEmployeeDto>()
                  .ForMember(desc => desc.FirstName, s => s.MapFrom(src => src.FirstName))
                  .ForMember(desc => desc.LastName, s => s.MapFrom(src => src.LastName))
                  .ForMember(desc => desc.Phone, s => s.MapFrom(src => src.Phone))
                  .ForMember(desc => desc.Salary, s => s.MapFrom(src => src.Salary))
                  .ForMember(desc => desc.Address, s => s.MapFrom(src => src.Address))
                  .ForMember(desc => desc.IdEmployee, s => s.MapFrom(src => src.IdEmployee))
                  .ForMember(desc => desc.EMail, s => s.MapFrom(src => src.UserEntity.EMail))
                  .ForMember(desc => desc.BankAccountNumber, s => s.MapFrom(src => src.BankAccountNumber));

        }

        public EditEmployeeDto MapEntityToDto(EmployeeEntity a_employeeEntity, IEnumerable<SelectListItem> a_choicesRoles)
        {
            var tmp= Mapper.Map<EmployeeEntity, EditEmployeeDto>(a_employeeEntity);
            tmp.ChoicesRoles = a_choicesRoles;
            return tmp;
        }
    }
}
