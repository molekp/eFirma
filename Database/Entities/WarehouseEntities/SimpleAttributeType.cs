using System.ComponentModel.DataAnnotations;

namespace Database.Entities.WarehouseEntities
{
    public class SimpleAttributeType : IAttributeType
    {
        [Key]
        public int IdAttributeType { get; set; }
        public string Name { get; set; }
    }
}
