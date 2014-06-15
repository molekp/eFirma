using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.InventaryRepositories
{
    public interface IAttributeTypeRepository
    {
        IDataBaseContext DataBaseContext { get; set; }
        IEnumerable<IAttributeType> GetAll();
        IAttributeType Get(int a_idAttributeType);
    }
}