using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.Dtos
{
    [XmlType("Purchase")]
    public class PurchaseDto
    {
        [Required]
        [XmlAttribute("title")]
        public string Title { get; set; }

        [Required]
        [XmlElement("Type")]
        public PurchaseType Type { get; set; }

        [Required]
        [XmlElement("Key")]
        [RegularExpression(@"^[\dA-Z]{4}-[\dA-Z]{4}-[\dA-Z]{4}$")]
        public string ProductKey { get; set; }

        [Required]
        [XmlElement("Card")]
        [RegularExpression(@"\d{4} \d{4} \d{4} \d{4}")]
        public string CardNumber { get; set; }

        [Required]
        [XmlElement("Date")]
        public string Date { get; set; }
    }
}
