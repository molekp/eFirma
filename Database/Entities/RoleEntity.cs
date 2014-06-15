using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class RoleEntity
    {
        [Key]
        public int IdRole { get; set; }

        public string NameRole { get; set; }
    }
}