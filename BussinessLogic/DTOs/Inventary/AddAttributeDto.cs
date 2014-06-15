using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Inventary
{
    public class AddAttributeDto
    {
        [Display(Name = "attributeValue", ResourceType = typeof(Resource))]
        public string Value { get; set; }

        public int SelectedChoiceIdAttributeType { get; set; }

        public List<SelectListItem> AttributeTypes { get; set; }
    }
}