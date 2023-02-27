namespace Trucks.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using Trucks.Data.Models;
    using Trucks.Data.Models.Enums;
    using Trucks.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";

        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";

        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var xmlRoot = new XmlRootAttribute("Despatchers");
            var serializer = new XmlSerializer(typeof(ImportDespatcherDto[]), xmlRoot);
            var stringReader = new StringReader(xmlString);
            var dispatchers = (ImportDespatcherDto[])serializer.Deserialize(stringReader);
            var validDespatchers = new List<Despatcher>();
            foreach (var despatcher in dispatchers)
            {
                if (!IsValid(despatcher))
                {
                    sb.AppendLine("Invalid data!");
                    continue;
                }
                if (despatcher.Position == null || despatcher.Position == String.Empty)
                {
                    sb.AppendLine("Invalid data!");
                    continue;
                }
                var validDespatcher = new Despatcher
                {
                    Name = despatcher.Name,
                    Position = despatcher.Position
                };
                foreach (var truck in despatcher.Trucks)
                {
                    if (!IsValid(truck))
                    {
                        sb.AppendLine("Invalid data!");
                        continue;
                    }
                    var validTruck = new Truck()
                    {
                        RegistrationNumber = truck.RegistrationNumber,
                        VinNumber = truck.VinNumber,
                        TankCapacity = truck.TankCapacity,
                        CargoCapacity = truck.CargoCapacity,
                        CategoryType = (CategoryType)truck.CategoryType,
                        MakeType = (MakeType)truck.MakeType
                    };
                    validDespatcher.Trucks.Add(validTruck);
                }
                validDespatchers.Add(validDespatcher);
                sb.AppendLine($"Successfully imported despatcher - {validDespatcher.Name} with {validDespatcher.Trucks.Count} trucks.");

            }
            context.Despatchers.AddRange(validDespatchers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }
        public static string ImportClient(TrucksContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var clients = JsonConvert.DeserializeObject<ImportClientsDto[]>(jsonString);
            var validClients = new List<Client>();
            foreach (var client in clients)
            {
                if (!IsValid(client) || client.Type == "usual")
                {
                    sb.AppendLine("Invalid data!");
                    continue;
                }
                var validClient = new Client()
                {
                    Name = client.Name,
                    Nationality = client.Nationality,
                    Type = client.Type
                };
                foreach (var truck in client.Trucks.Distinct())
                {
                    var IsValidTruck = context.Trucks.FirstOrDefault(t=>t.Id == truck);
                    if (IsValidTruck == null)
                    {
                        sb.AppendLine("Invalid data!");
                        continue;
                    }
                    var ValidTruckId = new ClientTruck()
                    {
                        Client = validClient,
                        TruckId = truck
                    };
                    validClient.ClientsTrucks.Add(ValidTruckId);
                }
                validClients.Add(validClient);
                sb.AppendLine($"Successfully imported client - {validClient.Name} with {validClient.ClientsTrucks.Count} trucks.");
            }
            context.Clients.AddRange(validClients);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
