using System.Collections.Generic;
using BussinessLogic.DTOs;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces
{
    public interface IAttributeRepository
    {
        IEnumerable<ValueAttributeType> GetAllAttributes();

        bool RemoveAttribute(ValueAttributeType a_valueAttributeType);
    }
}