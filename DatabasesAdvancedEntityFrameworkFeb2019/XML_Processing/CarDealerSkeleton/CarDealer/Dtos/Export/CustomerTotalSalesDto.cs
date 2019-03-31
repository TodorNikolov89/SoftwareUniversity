using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.Dtos.Export
{
    [XmlType("customer")]
    public class CustomerTotalSalesDto
    {
        [XmlElement("full-name")]
        public string Name { get; set; }

        [XmlElement("bought-cars")]
        public int BougthCars { get; set; }

        [XmlElement("spent-money")]
        public decimal SpentMoney { get; set; }
    }
}
