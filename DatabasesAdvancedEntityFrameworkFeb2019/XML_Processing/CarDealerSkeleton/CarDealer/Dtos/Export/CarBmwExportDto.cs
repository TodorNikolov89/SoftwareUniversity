using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.Dtos.Export
{
    [XmlType("car")]
    public class CarBmwExportDto
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("travelled-distance")]
        public long TravelledDistance { get; set; }
    }
}
