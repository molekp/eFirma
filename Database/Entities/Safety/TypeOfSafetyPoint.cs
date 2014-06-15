using System.ComponentModel.DataAnnotations;

namespace Database.Entities.Safety
{
    public class TypeOfSafetyPoint
    {
        [Key]
        public int IdTypeOfSafetyPoint{ get; set; }

        [Required]
        public string Name { get; set; }
    }
}