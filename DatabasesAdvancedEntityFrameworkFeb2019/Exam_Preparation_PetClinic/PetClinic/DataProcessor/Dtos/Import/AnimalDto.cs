using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetClinic.DataProcessor.Dtos.Import
{
    public class AnimalDto
    {
        [Required]
        [StringLength(20), MinLength(3)]
        public string Name { get; set; }

        [Required]
        [StringLength(20), MinLength(3)]
        public string Type { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Age { get; set; }

        [Required]
        public PassportDto Passport { get; set; }
    }
}
