using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Employee
    {
        public Employee()
        {
            this.Orders = new HashSet<Order>();
        }

        public Employee(string name, int age, Position position) : this()
        {
            this.Name = name;
            this.Age = age;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30), MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Range(15, 80)]
        public int Age { get; set; }

        [Required]
        public int PositionId { get; set; }

        [Required]
        public Position Position { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}