using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities.Safety;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.Safety
{
    public interface ITypesOfSafetyPointsRepository 
    {
        IDataBaseContext DataBaseContext { get; set; }
        IEnumerable<TypeOfSafetyPoint> GetAll();
        TypeOfSafetyPoint Get(int a_idTypeOfSafetyPoint);
    }
}