﻿using System.Xml.Serialization;
using Trucks.Data.Models.Enums;

namespace Trucks.DataProcessor.ExportDto
{
    [XmlType("Truck")]
    public class DespatchersTrucksDto
    {
        [XmlElement("RegistrationNumber")]
        public string RegNumb { get; set; }
        [XmlElement("Make")]
        public MakeType Make { get; set; }
    }
}