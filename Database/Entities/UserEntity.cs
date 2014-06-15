using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.Entities.Safety;

namespace Database.Entities
{
    public class UserEntity
    {
        [Key]
        public int IdUser { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string EMail { get; set; }

        public virtual RoleEntity Role { get; set; }

        public virtual List<SafetyPointGroup> UserSafetyPointGroups { get; set; }
    }
}