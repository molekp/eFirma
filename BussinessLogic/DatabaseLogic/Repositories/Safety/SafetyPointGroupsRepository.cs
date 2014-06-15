using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.Safety;
using Database.Core.Interfaces;
using Database.Entities.Safety;

namespace BussinessLogic.DatabaseLogic.Repositories.Safety
{
    public class SafetyPointGroupsRepository : ISafetyPointGroupsRepository
    {
        public IDataBaseContext DataBaseContext { get; set; }

        public int CreateSafetyPointGroup(SafetyPointGroup a_newSafetyPointGroup)
        {
            try
            {
                a_newSafetyPointGroup=DataBaseContext.SafetyPointGroups.Add(a_newSafetyPointGroup);
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }

            return a_newSafetyPointGroup.IdSafetyPointGroup;
        }

        public IEnumerable<SafetyPointGroup> GetAll()
        {
            return DataBaseContext.SafetyPointGroups.ToList();
        }

        public bool RemoveSafetyPointGroup(int a_idGroup)
        {
            var group = DataBaseContext.SafetyPointGroups.FirstOrDefault(x => x.IdSafetyPointGroup == a_idGroup);
            if (null == group) return false;

            try
            {
                DataBaseContext.SafetyPointGroups.Remove(group);
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public SafetyPointGroup Get(int a_idSafetyPointGroup)
        {
            return DataBaseContext.SafetyPointGroups.FirstOrDefault(x => x.IdSafetyPointGroup == a_idSafetyPointGroup);
        }

        public bool AddSafetyPointToGroup(SafetyPointGroup a_safetyPointGroup, SafetyPoint a_safetyPoint)
        {
            try
            {
                a_safetyPointGroup.SafetyPoints.Add(a_safetyPoint);
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RenameSafetyPointGroup(SafetyPointGroup a_idSafetyPointGroup, string a_nameOfsafetyPointGroup)
        {
            try
            {
                a_idSafetyPointGroup.GroupName = a_nameOfsafetyPointGroup;
                DataBaseContext.SaveChanges();
            }catch(Exception)
            {
                return false;
            }
            return true;
        }

        public bool RemoveSafetyPointFromGroup(SafetyPointGroup a_group, SafetyPoint a_safetyPoint)
        {
            try
            {
                a_group.SafetyPoints.Remove(a_safetyPoint);
                DataBaseContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
