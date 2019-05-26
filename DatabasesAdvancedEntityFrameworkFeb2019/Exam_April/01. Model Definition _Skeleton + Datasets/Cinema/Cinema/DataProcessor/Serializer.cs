namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Cinema.Data.Models;
    using Cinema.DataProcessor.ExportDto;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context
                .Movies
                .Where(r => r.Rating >= (double)rating && r.Projections.Any(t => t.Tickets.Count > 0))
                .Select(m => new MovieExportDto()
                {
                    MovieName = m.Title,
                    Rating = $"{m.Rating:f2}",
                    TotalIncomes = $"{ m.Projections.Sum(p => p.Tickets.Sum(tp => tp.Price)):f2}",
                    Customers = m.Projections
                    .SelectMany(c => c.Tickets)
                    .Select(c => new CustomerExprtDto()
                    {
                        FirstName = c.Customer.FirstName,
                        LastName = c.Customer.LastName,
                        Balance = $"{c.Customer.Balance:f2}"
                    })
                    .OrderByDescending(b => b.Balance)
                    .ThenBy(f => f.FirstName)
                    .ThenBy(l => l.LastName)
                    .ToList(),
                })
                .OrderByDescending(r => double.Parse(r.Rating))
                .ThenByDescending(t => decimal.Parse(t.TotalIncomes))
                .Take(10)
                .ToArray();

            string result = JsonConvert.SerializeObject(movies, Newtonsoft.Json.Formatting.Indented);

            return result;

        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            StringBuilder sb = new StringBuilder();

            var customers = context
                .Customers
                .Where(a => a.Age >= age)
                .Select(c => new CustExportDto()
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    SpentMoney = $"{}",
                    SpentTime = Duration(c.Tickets.Select(p => p.Projection.Movie))
                })
                .OrderByDescending(s => (decimal.Parse(s.SpentMoney)))
                .Take(10)
                .ToArray();

            var serializer = new XmlSerializer(typeof(CustExportDto[]), new XmlRootAttribute("Customers"));
            serializer.Serialize(new StringWriter(sb), customers, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString();
            return result;
        }

        private static string Duration(IEnumerable<Movie> movies)
        {
            TimeSpan ts = new TimeSpan();
            foreach (var movie in movies)
            {
                ts += movie.Duration;
            }

            return ts.ToString(@"hh\:mm\:ss");
        }

    }
}