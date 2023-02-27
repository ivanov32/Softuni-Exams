using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Trucks.DataProcessor.ExportDto
{
    [XmlType("Despatcher")]
    public class ExportDespatcherDto
    {
        [XmlAttribute("TrucksCount")]
        public int AllTrucksCount { get; set; }
        [XmlElement("DespatcherName")]
        public string DespatcherName { get; set; }
        [XmlArray("Trucks")]
        public DespatchersTrucksDto[] Trucks { get; set; }
    }
}
