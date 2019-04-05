﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Models
{
    public class Animal
    {
        public Animal()
        {
            this.Procedures = new HashSet<Procedure>();
        }


        public Animal(string name, string type, int age, Passport passport) : this()
        {
            this.Name = name;
            this.Type = type;
            this.Age = age;
            this.Passport = passport;
        }


        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20), MinLength(3)]
        public string Name { get; set; }

        [Required]
        [StringLength(20), MinLength(3)]
        public string Type { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Age { get; set; }

        [ForeignKey("Passport")]
        public string PassportSerialNumber { get; set; }
        [Required]
        public Passport Passport { get; set; }

        public ICollection<Procedure> Procedures { get; set; }
    }
}
