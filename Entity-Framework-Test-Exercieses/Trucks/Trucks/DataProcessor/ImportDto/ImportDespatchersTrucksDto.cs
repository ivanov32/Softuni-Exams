using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Trucks.DataProcessor.ImportDto
{
    [XmlType("Truck")]
    public class ImportDespatchersTrucksDto
    {
        [XmlElement("RegistrationNumber")]
        [StringLength(8)]
        [RegularExpression(@"^[A-Z][A-Z][0-9][0-9][0-9][0-9][A-Z][A-Z]$")]
        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        [StringLength(17)]
        [XmlElement("VinNumber")]
        public string VinNumber { get; set; }
        [Range(950, 1420)]
        [XmlElement("TankCapacity")]
        public int TankCapacity { get; set; }
        [XmlElement("CargoCapacity")]
        [Range(5000, 29000)]
        public int CargoCapacity { get; set; }
        [XmlElement("CategoryType")]
        [Required]
        [Range(0,3)]
        public int CategoryType { get; set; }
        [XmlElement("MakeType")]
        [Required]
        [Range(0,4)]
        public int MakeType { get; set; }
    }
}