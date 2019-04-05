namespace PetClinic.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.DataProcessor.Dtos.Import;
    using PetClinic.Models;

    public class Deserializer
    {
        private static string ErrorMessage = $"Error: Invalid data.";

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var deserializedAnimalAids = JsonConvert.DeserializeObject<AnimalAidsDto[]>(jsonString);

            var animalAids = new List<AnimalAid>();
            StringBuilder sb = new StringBuilder();

            foreach (var dto in deserializedAnimalAids)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                AnimalAid animalAid = new AnimalAid(dto.Name, dto.Price);

                if (animalAids.Any(a => a.Name == animalAid.Name))
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                animalAids.Add(animalAid);

                sb.AppendLine($"Record {animalAid.Name} successfully imported.");
            }

            context.AnimalAids.AddRange(animalAids);
            context.SaveChanges();

            string result = sb.ToString();

            return result;

        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var deserializedAnimal = JsonConvert.DeserializeObject<AnimalDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            List<Animal> animals = new List<Animal>();

            foreach (var dto in deserializedAnimal)
            {
                if (!IsValid(dto) || !IsValid(dto.Passport))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime date = DateTime.ParseExact(dto.Passport.RegistrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                Passport passport = new Passport(dto.Passport.SerialNumber, dto.Passport.OwnerName, dto.Passport.OwnerPhoneNumber, date);

                if (animals.Any(p => p.Passport.SerialNumber == passport.SerialNumber))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Animal animal = new Animal(dto.Name, dto.Type, dto.Age, passport);
                animals.Add(animal);

                sb.AppendLine($"Record {animal.Name} Passport №: {passport.SerialNumber} successfully imported.");
            }

            context.Animals.AddRange(animals);
            context.SaveChanges();

            string result = sb.ToString();

            return result;

        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(VetDto[]), new XmlRootAttribute("Vets"));
            var deserializedVets = (VetDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            StringBuilder sb = new StringBuilder();

            List<Vet> vets = new List<Vet>();

            foreach (var dto in deserializedVets)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (vets.Any(v => v.PhoneNumber == dto.PhoneNumber))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Vet vet = new Vet(dto.Name, dto.Profession, dto.Age, dto.PhoneNumber);

                vets.Add(vet);
                sb.AppendLine($"Record {vet.Name} successfully imported.");
            }

            context.Vets.AddRange(vets);
            context.SaveChanges();

            string result = sb.ToString();

            return result;
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));
            var deserializedProcedures = (ProcedureDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var procedures = new List<Procedure>();

            StringBuilder sb = new StringBuilder();

            var vets = context.Vets.ToList();
            var animals = context.Animals.ToList();
            var animalAids = context.AnimalAids.ToList();

            foreach (var dto in deserializedProcedures)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!vets.Any(n => n.Name == dto.VetName))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var vet = vets.SingleOrDefault(n => n.Name == dto.VetName);

                if (!animals.Any(s => s.PassportSerialNumber == dto.AnimalSerialNumber))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var animal = animals.SingleOrDefault(s => s.PassportSerialNumber == dto.AnimalSerialNumber);

                var distenctedListCount = dto.AnimalAids.Select(n => n.Name).Distinct().Count();

                if (distenctedListCount != dto.AnimalAids.Count)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime date = DateTime.ParseExact(dto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                var procedure = new Procedure()
                {
                    Vet = vet,
                    Animal = animal,
                    Date = date
                };

                List<ProcedureAnimalAid> procedureAnimalAids = new List<ProcedureAnimalAid>();
                bool isAidExists = true;

                foreach (var aid in dto.AnimalAids)
                {
                    if (!animalAids.Any(n => n.Name == aid.Name))
                    {
                        isAidExists = false;
                        break;
                    }

                    var ai = animalAids.SingleOrDefault(n => n.Name == aid.Name);

                    var procedureAnimalAid = new ProcedureAnimalAid() { AnimalAid = ai, Procedure = procedure };

                    procedure.ProcedureAnimalAids.Add(procedureAnimalAid);
                }

                if (!isAidExists)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                procedures.Add(procedure);

                sb.AppendLine($"Record successfully imported.");

            }

            context.Procedures.AddRange(procedures);
            context.SaveChanges();

            string result = sb.ToString();

            return result;
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationresult = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationresult, true);
            return isValid;
        }
    }
}
