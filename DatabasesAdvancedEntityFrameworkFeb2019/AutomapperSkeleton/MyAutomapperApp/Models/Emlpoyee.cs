namespace MyAutomapperApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Emlpoyee
    {
        public Emlpoyee()
        {
            this.ManagedEmployees = new List<Emlpoyee>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Range(typeof(decimal), "0.00", "79228162514264337593543950335")]
        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        public int?  ManagerId { get; set; }
        public Emlpoyee Manager { get; set; }

        public List<Emlpoyee> ManagedEmployees { get; set; }
    }
}
