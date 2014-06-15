using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Safety;
using Database.Core.Interfaces;
using Database.Entities.Safety;

namespace BussinessLogic.DatabaseLogic.Repositories.Safety
{
    public class SafetyPointRepository : ISafetyPointRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public bool CreateSafetyPoint(SafetyPoint a_safetyPoint)
        {
            try
            {
                DataBaseContext.SafetyPoints.Add(a_safetyPoint);
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<SafetyPoint> GetAll()
        {
            return DataBaseContext.SafetyPoints.ToList();
        }

        public SafetyPoint Get(int a_idSafetyPoint)//TODO tests
        {
            return DataBaseContext.SafetyPoints.FirstOrDefault(x => x.IdSafetyPoint == a_idSafetyPoint);
        }
    }
}
