using System;
using System.Collections.Generic;
using System.Text;

namespace FastFood.DataProcessor.Dto.Export
{
    public class OrderExportDto
    {
        public string  Name { get; set; }

        public List<OrdersDto> Orders { get; set; }

    }
}
