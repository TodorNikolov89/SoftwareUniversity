using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFood.DataProcessor.Dto.Import
{
    public class PositionDto
    {
        [Required]
        [StringLength(30), MinLength(3)]
        public string Name { get; set; }
    }
}
