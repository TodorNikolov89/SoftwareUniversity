using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Customer")]
    public class CustomerDto
    {
        [Required]
        [StringLength(20), MinLength(3)]
        [XmlElement("FirstName")]
        public string FistName { get; set; }

        [Required]
        [StringLength(20), MinLength(3)]
        [XmlElement("LastName")]
        public string LastName { get; set; }

        [Required]
        [Range(12, 110)]
        [XmlElement("Age")]
        public int Age { get; set; }

        [Required]
        [XmlElement("Balance")]
        public decimal Balance { get; set; }

        [XmlArray("Tickets")]
        public List<TicketDto> Tickets { get; set; }
    }
}
