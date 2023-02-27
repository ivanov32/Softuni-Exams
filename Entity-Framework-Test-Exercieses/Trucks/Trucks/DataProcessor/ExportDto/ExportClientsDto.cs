using System;
using System.Collections.Generic;
using System.Text;

namespace Trucks.DataProcessor.ExportDto
{
    public class ExportClientsDto
    {
        public string Name { get; set; }
        public List<ExportTrucksWithCapacityDto> Trucks { get; set; }
    }
}
