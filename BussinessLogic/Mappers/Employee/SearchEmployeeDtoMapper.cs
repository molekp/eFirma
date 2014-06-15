using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BussinessLogic.DTOs.Employee;
using Database.Entities;

namespace BussinessLogic.Mappers.Employee
{
    public class SearchEmployeeDtoMapper
    {
        public SearchEmployeeDtoMapper()
        {

            Mapper.CreateMap<EmployeeEntity, SearchEmployeeDto>()
                  .ForMember(desc => desc.IdEmployee, s => s.MapFrom(src => src.IdEmployee))
                  .ForMember(desc => desc.UserName, s => s.MapFrom(src => src.UserEntity.UserName))
                  .ForMember(desc => desc.EMail, s => s.MapFrom(src => src.UserEntity.EMail))
                  .ForMember(desc => desc.Adress, s => s.MapFrom(src => src.Address))
                  .ForMember(desc => desc.Phone, s => s.MapFrom(src => src.Phone))
                  .ForMember(desc => desc.LastName, s => s.MapFrom(src => src.LastName))
                  .ForMember(desc => desc.FirstName, s => s.MapFrom(src => src.FirstName))
                  .ForMember(desc => desc.NameRole, s => s.MapFrom(src => src.UserEntity.Role.NameRole));

        }

        public SearchEmployeeDto MapEntityToDto(EmployeeEntity a_employee)
        {
            return Mapper.Map<EmployeeEntity, SearchEmployeeDto>(a_employee);
        }
    }
}
