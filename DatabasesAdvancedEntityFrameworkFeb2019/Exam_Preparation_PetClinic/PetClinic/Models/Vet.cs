﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetClinic.Models
{
    public class Vet
    {
        public Vet()
        {
            this.Procedures = new HashSet<Procedure>();
        }

        public Vet(string name, string profession, int age, string phoneNumber) : this()
        {
            this.Name = name;
            this.Profession = profession;
            this.Age = age;
            this.PhoneNumber = phoneNumber;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40), MinLength(3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50), MinLength(3)]
        public string Profession { get; set; }

        [Required]
        [Range(22, 65)]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"(\+359+[0-9]{9})|(0+[0-9]{9})")]
        public string PhoneNumber { get; set; }

        public ICollection<Procedure> Procedures { get; set; }
    }
}
