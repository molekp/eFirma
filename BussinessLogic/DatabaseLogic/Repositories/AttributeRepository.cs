using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.DatabaseLogic.Repositories
{
    public class AttributeRepository : IAttributeRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        /// <summary>
        /// Zwraca wszystkie Atrybuty
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ValueAttributeType> GetAllAttributes()
        {
            return DataBaseContext.Attributes.ToList();
        }

        public bool RemoveAttribute(ValueAttributeType a_valueAttributeType)
        {
            try
            {
                DataBaseContext.Attributes.Remove(a_valueAttributeType);
                DataBaseContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}