using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dtos.Import
{
    [XmlType("Procedure")]
    public class ProcedureDto
    {
        [Required]
        [StringLength(40), MinLength(3)]
        [XmlElement("Vet")]
        public string VetName { get; set; }

        [Required]
        [XmlElement("Animal")]
        public string AnimalSerialNumber { get; set; }

        [Required]
        [XmlElement("DateTime")]
        public string DateTime { get; set; }

        [XmlArray("AnimalAids")]
        public List<AnimalAidDto> AnimalAids { get; set; }

    }
}
