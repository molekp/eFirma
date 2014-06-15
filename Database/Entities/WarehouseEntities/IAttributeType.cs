using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities.WarehouseEntities
{
    public interface IAttributeType
    {
        int IdAttributeType { get; set; }

        string Name { get; set; }
    }
}