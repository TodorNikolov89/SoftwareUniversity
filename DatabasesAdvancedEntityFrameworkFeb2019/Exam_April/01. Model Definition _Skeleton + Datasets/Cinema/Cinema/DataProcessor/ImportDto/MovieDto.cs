using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.DataProcessor.ImportDto
{
    public class MovieDto
    {
        [Required]
        [StringLength(20), MinLength(3)]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public  string Duration { get; set; }

        [Required]
        [Range(typeof(double), "1.00", "10.00")]
        public double Rating { get; set; }

        [Required]
        [StringLength(20), MinLength(3)]
        public string Director { get; set; }
    }
}
