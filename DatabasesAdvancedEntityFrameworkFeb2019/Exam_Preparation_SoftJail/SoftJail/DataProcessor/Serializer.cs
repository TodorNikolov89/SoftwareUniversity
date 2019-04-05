namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context
                 .Prisoners
                 .Where(p => ids.Contains(p.Id))
                 .Select(p => new PrisonersExport()
                 {
                     Id = p.Id,
                     Name = p.FullName,
                     CellNumber = p.Cell.CellNumber,
                     Officers = p.PrisonerOfficers.Select(po => new OfficersExport()
                     {
                         OfficerName = po.Officer.FullName,
                         Department = po.Officer.Department.Name
                     })
                     .OrderBy(n => n.OfficerName)
                     .ToList(),

                     TotalOfficerSalary = p.PrisonerOfficers.Select(o => o.Officer.Salary).Sum()
                 })
                 .OrderBy(n => n.Name)
                 .ThenBy(pr => pr.Id)
                 .ToArray();

            string result = JsonConvert.SerializeObject(prisoners, Newtonsoft.Json.Formatting.Indented);

            return result;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            StringBuilder sb = new StringBuilder();

            var names = prisonersNames
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var prisoners = context
                .Prisoners
                .Where(p => names.Contains(p.FullName))
                .Select(p => new PrisonerDto()
                {
                    Id = p.Id,
                    Name = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd"),
                    EncryptedMessages = p.Mails.Select(m => new MailDto()
                    {
                        Description = ReverseString(m.Description)
                    })
                    .ToList()
                })
                .OrderBy(p=>p.Name)
                .ThenBy(pi=>pi.Id)
                .ToArray();

            var serializer = new XmlSerializer(typeof(PrisonerDto[]), new XmlRootAttribute("Prisoners"));
            serializer.Serialize(new StringWriter(sb), prisoners, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString();
            return result;
        }

        private static string ReverseString(string description)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = description.Length-1; i >= 0; i--)
            {
                sb.Append(description[i]);
            }

            return sb.ToString();
        }
    }
}