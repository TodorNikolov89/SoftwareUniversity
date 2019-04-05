using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetClinic.DataProcessor.Dtos.Import
{
    public class PassportDto
    {
        [StringLength(10)]
        [RegularExpression("[A-Za-z]{7}[0-9]{3}")]
        public string SerialNumber { get; set; }

        [Required]
        [StringLength(30), MinLength(3)]
        public string OwnerName { get; set; }

        [Required]
        [RegularExpression(@"(\+359+[0-9]{9})|(0+[0-9]{9})")]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        public string RegistrationDate { get; set; }
    }
}
