using System.ComponentModel.DataAnnotations;

namespace Database.Entities.WarehouseEntities
{
    public class ValueAttributeType : IAttributeType
    {
        [Key]
        public string Value { get; set; }
        public int IdAttributeType { get; set; }
        public string Name { get; set; }
    }
}