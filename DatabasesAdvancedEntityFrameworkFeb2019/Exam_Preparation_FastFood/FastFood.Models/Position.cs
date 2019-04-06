using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFood.Models
{
    public class Position
    {
        public Position()
        {
            this.Employees = new HashSet<Employee>();
        }

        public Position(string name) : this()
        {
            this.Name = name;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30), MinLength(3)]
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
