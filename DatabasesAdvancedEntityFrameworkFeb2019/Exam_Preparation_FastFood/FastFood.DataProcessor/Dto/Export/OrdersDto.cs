using System;
using System.Collections.Generic;
using System.Text;

namespace FastFood.DataProcessor.Dto.Export
{
    public class OrdersDto
    {
        public string Customer { get; set; }

        public List<ItemExportDto> Items { get; set; }
    }
}
