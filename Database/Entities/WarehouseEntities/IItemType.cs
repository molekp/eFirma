using System.Collections.Generic;

namespace Database.Entities.WarehouseEntities
{

    public interface IItemType
    {
        int IdItemType { get; set; }

        string Name { get; set; }

        TaxEntity ItemTax { get; set; }

        List<SimpleAttributeType> AttributeTypes { get; set; }

    }
}
