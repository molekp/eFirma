using System.Collections.Generic;
using Database.Core.Interfaces;
using Database.Entities.Safety;

namespace BussinessLogic.DatabaseLogic.Repositories.Interfaces.Safety
{
    public interface ISafetyPointGroupsRepository
    {
        IDataBaseContext DataBaseContext { get; set; }
        int CreateSafetyPointGroup(SafetyPointGroup a_newSafetyPointGroup);
        IEnumerable<SafetyPointGroup> GetAll();
        bool RemoveSafetyPointGroup(int a_idGroup);
        SafetyPointGroup Get(int a_idSafetyPointGroup);
        bool AddSafetyPointToGroup(SafetyPointGroup a_safetyPointGroup, SafetyPoint a_safetyPoint);
        bool RenameSafetyPointGroup(SafetyPointGroup a_idSafetyPointGroup, string a_nameOfsafetyPointGroup);
        bool RemoveSafetyPointFromGroup(SafetyPointGroup a_group, SafetyPoint a_safetyPoint);
    }
}