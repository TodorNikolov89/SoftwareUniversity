using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.Data.Models
{
    public class Cell
    {
        public Cell()
        {
            this.Prisoners = new HashSet<Prisoner>();
        }


        public Cell(int number, bool hasWindow)
        {
            this.CellNumber = number;
            this.HasWindow = hasWindow;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 1000)]
        public int CellNumber { get; set; }

        [Required]
        public bool HasWindow { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public Department Department { get; set; }

        public ICollection<Prisoner> Prisoners { get; set; }
    }
}
