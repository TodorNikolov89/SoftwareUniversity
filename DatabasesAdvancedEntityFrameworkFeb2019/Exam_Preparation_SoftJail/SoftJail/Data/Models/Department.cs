using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.Data.Models
{
    public class Department
    {
        public Department()
        {
            this.Cells = new HashSet<Cell>();
        }

        public Department(string name, List<Cell> cells)
        {
            this.Name = name;
            this.Cells = cells;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25), MinLength(3)]
        public string Name { get; set; }

        public ICollection<Cell> Cells { get; set; }
    }
}
