﻿using System.ComponentModel.DataAnnotations;
using ResourceLibrary;

namespace BussinessLogic.DTOs.Inventary
{
    public class DisplayProductAttributeDto
    {
        [Display(Name = "attributeTypeName", ResourceType = typeof(Resource))]
        public string AttributeTypeName { get; set; }

        [Display(Name = "attributeValue", ResourceType = typeof(Resource))]
        public string AttributeValue { get; set; }
    }
}