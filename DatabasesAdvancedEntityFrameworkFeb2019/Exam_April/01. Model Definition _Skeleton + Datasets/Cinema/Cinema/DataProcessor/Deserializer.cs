namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Cinema.Data.Models;
    using Cinema.Data.Models.Enums;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var deserializedMovies = JsonConvert.DeserializeObject<MovieDto[]>(jsonString);

            var movies = new List<Movie>();

            StringBuilder sb = new StringBuilder();

            foreach (var dto in deserializedMovies)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (movies.Any(m => m.Title.Equals(dto.Title, StringComparison.OrdinalIgnoreCase)))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Genre genre;
                Enum.TryParse(dto.Genre, out genre);
                TimeSpan time = TimeSpan.Parse(dto.Duration);

                var movie = new Movie()
                {
                    Title = dto.Title,
                    Genre = genre,
                    Duration = time,
                    Rating = dto.Rating,
                    Director = dto.Director
                };

                movies.Add(movie);
                string s = string.Format("{0:N2}", dto.Rating);
                sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre, s));
            }

            context.Movies.AddRange(movies);
            context.SaveChanges();

            string result = sb.ToString();

            return result;

        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var deserializedHalls = JsonConvert.DeserializeObject<HallSeatDto[]>(jsonString);

            List<Hall> halls = new List<Hall>();
            List<Seat> seats = new List<Seat>();

            StringBuilder sb = new StringBuilder();

            foreach (var dto in deserializedHalls)
            {
                if (!IsValid(dto) || dto.Seats <= 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Hall hall = new Hall()
                {
                    Name = dto.Name,
                    Is4Dx = dto.Is4Dx,
                    Is3D = dto.Is3D
                };

                for (int i = 0; i < dto.Seats; i++)
                {
                    var seat = new Seat()
                    {
                        HallId = hall.Id
                    };
                    hall.Seats.Add(seat);
                }

                var type = string.Empty;
                if (hall.Is3D && !hall.Is4Dx)
                {
                    type = "3D";
                }
                else if (hall.Is4Dx && !hall.Is3D)
                {
                    type = "4Dx";
                }
                else if (hall.Is3D && hall.Is4Dx)
                {
                    type = "4Dx/3D";
                }
                else
                {
                    type = "Normal";
                }

                sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, type, hall.Seats.Count));

                halls.Add(hall);
            }
            context.Halls.AddRange(halls);
            context.SaveChanges();

            string result = sb.ToString();

            return result;
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ProjectionDto[]), new XmlRootAttribute("Projections"));
            var deserialized = (ProjectionDto[])serializer.Deserialize(new StringReader(xmlString));

            var projections = new List<Projection>();

            StringBuilder sb = new StringBuilder();

            foreach (var dto in deserialized)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var movie = context.Movies.SingleOrDefault(m => m.Id == dto.MovieId);

                if (movie == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var hall = context.Halls.SingleOrDefault(h => h.Id == dto.HallId);

                if (hall == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime currentDate = DateTime.ParseExact(dto.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                var projection = new Projection()
                {
                    MovieId = movie.Id,
                    HallId = hall.Id,
                    DateTime = currentDate
                };

                projections.Add(projection);
                sb.AppendLine(string.Format(SuccessfulImportProjection, movie.Title, currentDate.Date.ToString("MM/dd/yyyy")));
            }

            context.Projections.AddRange(projections);
            context.SaveChanges();

            string retult = sb.ToString();

            return retult;
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("Customers"));
            var deserialized = (CustomerDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Customer> customers = new List<Customer>();
            List<Ticket> tickets = new List<Ticket>();

            StringBuilder sb = new StringBuilder();

            foreach (var dto in deserialized)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = new Customer()
                {
                    FirstName = dto.FistName,
                    LastName = dto.LastName,
                    Age = dto.Age,
                    Balance = dto.Balance
                };

                bool hasProject = true;
                foreach (var ticketDto in dto.Tickets)
                {
                    var proj = context.Projections.SingleOrDefault(p => p.Id == ticketDto.ProjectionId);

                    
                    if (proj == null)
                    {
                        hasProject = false;
                        break; 
                    }
                    var ticket = new Ticket()
                    {
                        ProjectionId = proj.Id,
                        Price = ticketDto.Price
                    };

                    customer.Tickets.Add(ticket);
                }

                if (!hasProject)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                customers.Add(customer);
                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName, customer.Tickets.Count));
            }

            context.Customers.AddRange(customers);
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