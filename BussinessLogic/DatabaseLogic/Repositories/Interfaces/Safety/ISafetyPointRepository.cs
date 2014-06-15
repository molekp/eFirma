using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities.Safety;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.Safety
{
    public interface ISafetyPointRepository
    {
        IDataBaseContext DataBaseContext { get; set; }
        bool CreateSafetyPoint(SafetyPoint a_safetyPoint);
        IEnumerable<SafetyPoint> GetAll();
        SafetyPoint Get(int a_idSafetyPoint);
    }
}