using System.ComponentModel.DataAnnotations;

namespace Database.Entities.Safety
{
    public class SafetyPoint
    {
        [Key]
        public int IdSafetyPoint { get; set; }

        [Required]
        public string NameOfsafetyPoint { get; set; }

        [Required]
        public virtual TypeOfSafetyPoint TypeOfSafetyPoint { get; set; }

        [Required]
        public int IdOfPointInTable { get; set; }//the table is specificated by typeOdSafetyPoint

        public bool Read { get; set; }
        public bool Write { get; set; }
    }
}