using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Trucks.DataProcessor.ImportDto
{
    [XmlType("Despatcher")]
    public class ImportDespatcherDto
    {
        [Required]
        [MaxLength(40)]
        [MinLength(2)]
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Position")]
        public string Position { get; set; }
        [XmlArray("Trucks")]
        public ImportDespatchersTrucksDto[] Trucks { get; set; }
    }
}
