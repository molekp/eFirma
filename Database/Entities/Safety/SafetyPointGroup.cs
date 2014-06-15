using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities.Safety
{
    public class SafetyPointGroup
    {
        [Key]
        public int IdSafetyPointGroup { get; set; }

        [Required]
        public string GroupName { get; set; }

        public virtual List<SafetyPoint> SafetyPoints { get; set; }
    }
}