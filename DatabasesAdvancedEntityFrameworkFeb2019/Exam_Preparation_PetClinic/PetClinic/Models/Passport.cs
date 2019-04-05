using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetClinic.Models
{
    public class Passport
    {
        public Passport(string serialNumber, string ownerName, string ownerPhoneNumber, DateTime registrationDate)
        {
            this.SerialNumber = serialNumber;
            this.OwnerName = ownerName;
            this.OwnerPhoneNumber = ownerPhoneNumber;
            this.RegistrationDate = registrationDate;
        }

        [Key]
        [StringLength(10)]
        [RegularExpression("[A-Za-z]{7}[0-9]{3}")]
        public string SerialNumber { get; set; }

        [Required]
        public Animal Animal { get; set; }

        [Required]
        [RegularExpression(@"(\+359+[0-9]{9})|(0+[0-9]{9})")]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        [StringLength(30),MinLength(3)]
        public string OwnerName { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

    }
}
