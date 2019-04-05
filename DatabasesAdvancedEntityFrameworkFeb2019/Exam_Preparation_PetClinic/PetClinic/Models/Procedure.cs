using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace PetClinic.Models
{
    public class Procedure
    {
        public Procedure()
        {
            this.ProcedureAnimalAids = new HashSet<ProcedureAnimalAid>();
        }

        public Procedure(Vet vet, Animal animal, DateTime date, List<ProcedureAnimalAid> procedureAnimalAids) : this()
        {
            this.Vet = vet;
            this.Animal = animal;
            this.Date = date;
            this.ProcedureAnimalAids = procedureAnimalAids;
        }

        [Key]
        public int Id { get; set; }

        public int AnimalId { get; set; }
        [Required]
        public Animal Animal { get; set; }

        public int VetId { get; set; }
        [Required]
        public Vet Vet { get; set; }

        public ICollection<ProcedureAnimalAid> ProcedureAnimalAids { get; set; }

        [NotMapped]
        public decimal Cost => ProcedureAnimalAids.Select(p => p.AnimalAid.Price).Sum();

        [Required]
        public DateTime Date { get; set; }


    }
}
