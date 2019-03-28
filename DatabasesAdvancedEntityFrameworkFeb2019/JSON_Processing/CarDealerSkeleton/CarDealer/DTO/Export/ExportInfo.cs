using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO.Export
{
    public class ExportInfo
    {
        public CarExportDto car { get; set; }

        public List<PartExportDto> parts { get; set; }
    }
}
