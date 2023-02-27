namespace Trucks.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Trucks.Data.Models.Enums;
    using Trucks.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            var sb = new StringBuilder();
            var xmlRoot = new XmlRootAttribute("Despatchers");
            StringWriter stringWriter = new StringWriter(sb);
            var serializer = new XmlSerializer(typeof(ExportDespatcherDto[]), xmlRoot);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            var despatchers = context
                .Despatchers
                .Include(t=>t.Trucks)
                .ToArray()
                .Where(t => t.Trucks.Count > 0)
                .Select(ed => new ExportDespatcherDto
                {
                    DespatcherName = ed.Name,
                    AllTrucksCount = ed.Trucks.Count,
                    Trucks = ed.Trucks.Select(dt => new DespatchersTrucksDto
                    {
                        RegNumb = dt.RegistrationNumber,
                        Make = dt.MakeType
                    })
                    .OrderBy(r=>r.RegNumb)
                    .ToArray()
                })
                .OrderByDescending(tc=>tc.AllTrucksCount)
                .ThenBy(dn=>dn.DespatcherName)
                .ToArray();
            serializer.Serialize(stringWriter,despatchers, namespaces);
            return sb.ToString().TrimEnd();
        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {

            var clientsWithTrucks = context
                .Clients
                .Include(ct => ct.ClientsTrucks)
                .ThenInclude(t => t.Truck)
                .ToList()
                .Where(t => t.ClientsTrucks.Any(t => t.Truck.TankCapacity >= capacity))
                .Select(ec => new ExportClientsDto
                {
                    Name = ec.Name,
                    Trucks = ec.ClientsTrucks
                    .Select(t => t.Truck)
                    .ToList()
                    .Where(tc => tc.TankCapacity >= capacity)
                    .Select(etwc=> new ExportTrucksWithCapacityDto
                    {
                        TruckRegistrationNumber = etwc.RegistrationNumber,
                        VinNumber = etwc.VinNumber,
                        TankCapacity = etwc.TankCapacity,
                        CargoCapacity = etwc.CargoCapacity,
                        CategoryType = etwc.CategoryType.ToString(),
                        MakeType = etwc.MakeType.ToString()
                    })
                    .OrderBy(mt=>mt.MakeType)
                    .ThenByDescending(cp=>cp.CargoCapacity)
                    .ToList()
                    
                })
                .OrderByDescending(tc=>tc.Trucks.Count)
                .ThenBy(cn=>cn.Name)
                .Take(10)
                .ToList();
            return JsonConvert.SerializeObject(clientsWithTrucks, Formatting.Indented);
        }
    }
}
