namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private static readonly string ErrorMessage = $"Invalid Data";
        private static readonly string DepartmentsImportMessage = "Imported {0} with {1} cells";
        private static readonly string PrisonersImportMessage = "Imported {0} {1} years old";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var deserializedDepartCells = JsonConvert.DeserializeObject<DepartmentDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();
            var departments = new List<Department>();
            foreach (var dto in deserializedDepartCells)
            {
                if (!IsValid(dto) || !dto.Cells.All(IsValid))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var cells = new List<Cell>();
                foreach (var c in dto.Cells)
                {
                    var cell = new Cell(c.CellNumber, c.HasWindow);
                    cells.Add(cell);
                }

                var department = new Department(dto.Name, cells);
                sb.AppendLine(string.Format(DepartmentsImportMessage, department.Name, department.Cells.Count));
                departments.Add(department);
            }

            context.Departments.AddRange(departments);
            context.SaveChanges();

            string result = sb.ToString();

            return result;
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var deserializedPrisonerMails = JsonConvert.DeserializeObject<PrisonerDto[]>(jsonString);

            var prisoners = new List<Prisoner>();
            StringBuilder sb = new StringBuilder();

            foreach (PrisonerDto dto in deserializedPrisonerMails)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime incarceration;
                if (!DateTime.TryParseExact(dto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out incarceration))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime release;
                if (dto.ReleaseDate != null)
                {
                    if (!DateTime.TryParseExact(dto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out release))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                }

                bool isValidEmails = true;
                foreach (MailDto dtoMail in dto.Mails)
                {
                    if (!IsValid(dtoMail))
                    {
                        isValidEmails = false;
                        break;
                    }
                }

                if (!isValidEmails)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Prisoner prisoner = new Prisoner()
                {
                    FullName = dto.FullName,
                    Nickname = dto.Nickname,
                    Age = dto.Age,
                    IncarcerationDate = incarceration,
                    ReleaseDate = dto.ReleaseDate == null ? (DateTime?)null : DateTime.ParseExact(dto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    Bail = dto.Bail,
                    CellId = dto.CellId
                };

                context.Prisoners.Add(prisoner);
                context.SaveChanges();

                List<Mail> mails = new List<Mail>();
                foreach (MailDto dtoMail in dto.Mails)
                {
                    Mail mail = new Mail()
                    {
                        Description = dtoMail.Description,
                        Sender = dtoMail.Sender,
                        Address = dtoMail.Address,
                        PrisonerId = prisoner.Id
                    };

                    mails.Add(mail);
                }

                context.Mails.AddRange(mails);
                context.SaveChanges();

                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
            }

            return sb.ToString().Trim();

        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(OfficersPrisonersDto[]), new XmlRootAttribute("Officers"));
            var deserialized = (OfficersPrisonersDto[])serializer.Deserialize(new StringReader(xmlString));

            var officersPrisoners = new List<Officer>();

            StringBuilder sb = new StringBuilder();

            foreach (var dto in deserialized)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Weapon weapon;
                if (!Enum.TryParse(dto.Weapon, true, out weapon))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Position position;
                if (!Enum.TryParse(dto.Position, true, out position))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                List<OfficerPrisoner> prisoners = new List<OfficerPrisoner>();
                               
                Officer officer = new Officer()
                {
                    FullName = dto.Name,
                    Salary = dto.Money,
                    Position = position,
                    Weapon = weapon,
                    DepartmentId = dto.DepartmentId,
                };

                foreach (var dtoPrisoner in dto.Prisoners)
                {
                    if (!IsValid(dtoPrisoner))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var prisoner = new OfficerPrisoner()
                    {
                        OfficerId = officer.Id,
                        PrisonerId = dtoPrisoner.Id
                    };

                    officer.OfficerPrisoners.Add(prisoner);
                }

                sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
                officersPrisoners.Add(officer);
            }

            context.Officers.AddRange(officersPrisoners);
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