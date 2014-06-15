using System.Collections.Generic;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.InventaryRepositories;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;
using System.Linq;

namespace BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories.InventaryRepositories
{
    public class AttributeTypeRepository : IAttributeTypeRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public IEnumerable<IAttributeType> GetAll()
        {
            return DataBaseContext.AttributeTypes.ToList();
        }

        public IAttributeType Get(int a_idAttributeType)
        {
            return DataBaseContext.AttributeTypes.FirstOrDefault(x => x.IdAttributeType == a_idAttributeType);
        }
    }
}
