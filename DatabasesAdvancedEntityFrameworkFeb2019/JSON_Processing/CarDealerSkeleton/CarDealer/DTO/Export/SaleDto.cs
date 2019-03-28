using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO.Export
{
    public class SaleDto
    {
        public CarExportDto car { get; set; }

        public string customerName { get; set; }

        public decimal Discount { get; set; }

        public decimal price { get; set; }

        public decimal priceWithDiscount { get; set; }

    }
}
