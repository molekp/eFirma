using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Safety;
using Database.Core.Interfaces;
using Database.Entities.Safety;

namespace BussinessLogic.DatabaseLogic.Repositories.Safety
{
    public class TypesOfSafetyPointsRepository : ITypesOfSafetyPointsRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public IEnumerable<TypeOfSafetyPoint> GetAll()
        {
            return DataBaseContext.TypesOfSafetyPoints.ToList();
        }

        public TypeOfSafetyPoint Get(int a_idTypeOfSafetyPoint)
        {
            return DataBaseContext.TypesOfSafetyPoints.FirstOrDefault(x => x.IdTypeOfSafetyPoint == a_idTypeOfSafetyPoint);
        }
    }
}
