namespace BussinessLogic.DTOs.Inventary
{
    public class SearchItemByAttributeDto
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public decimal ItemPrice { get; set; }

        public string ItemTypeName { get; set; }

        public string AttributeTypeName { get; set; }

        public string AttributeValue { get; set; }

        public string ItemState { get; set; }
    }
}