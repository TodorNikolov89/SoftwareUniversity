namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.Dtos;

    public static class Deserializer
    {
        public static string ErrorMessage = $"Invalid Data";
        public static string SuccessImportGames = "Added {0} ({1}) with {2} tags";
        public static string SuccessImportUsers = "Imported {0} with {1} cards";
        public static string SuccessImportPurchases = "Imported {0} for {1}";

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var deserializedGames = JsonConvert.DeserializeObject<GameDto[]>(jsonString);

            var games = new List<Game>();
            var developers = new List<Developer>();
            var genres = new List<Genre>();
            var tags = new List<Tag>();

            StringBuilder sb = new StringBuilder();

            foreach (var dto in deserializedGames)
            {
                if (!IsValid(dto) || !dto.Tags.All(IsValid))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var developer = developers.SingleOrDefault(d => d.Name == dto.Developer);
                if (developer == null)
                {
                    developer = new Developer()
                    {
                        Name = dto.Developer
                    };
                    developers.Add(developer);
                }

                var genre = genres.SingleOrDefault(g => g.Name == dto.Genre);
                if (genre == null)
                {
                    genre = new Genre()
                    {
                        Name = dto.Genre
                    };
                    genres.Add(genre);
                }

                var gameTags = new List<Tag>();

                foreach (var tagName in dto.Tags)
                {
                    var tag = tags.SingleOrDefault(t => t.Name == tagName);
                    if (tag == null)
                    {
                        tag = new Tag() { Name = tagName };
                        tags.Add(tag);
                    }

                    gameTags.Add(tag);
                }

                Game game = new Game()
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    ReleaseDate = dto.ReleaseDate,
                    Developer = developer,
                    Genre = genre,
                    GameTags = gameTags.Select(t => new GameTag { Tag = t }).ToArray()
                };

                games.Add(game);
                sb.AppendLine(string.Format(SuccessImportGames, game.Name, game.Genre.Name, game.GameTags.Count));
            }
            context.Games.AddRange(games);
            context.SaveChanges();

            string result = sb.ToString();
            return result;

        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var deserializedUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var users = new List<User>();

            StringBuilder sb = new StringBuilder();

            foreach (var dto in deserializedUsers)
            {
                if (!IsValid(dto) || !dto.Cards.All(IsValid))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var user = new User(dto.FullName, dto.Username, dto.Email, dto.Age, dto.Cards);

                users.Add(user);
                sb.AppendLine(string.Format(SuccessImportUsers, user.Username, user.Cards.Count));
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            string result = sb.ToString();
            return result;
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(PurchaseDto[]), new XmlRootAttribute("Purchases"));
            var deserializedPurchases = (PurchaseDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Purchase> purchases = new List<Purchase>();

            StringBuilder sb = new StringBuilder();

            foreach (var dto in deserializedPurchases)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                var card = context.Cards.SingleOrDefault(c => c.Number == dto.CardNumber);
                if (card == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var user = context.Users.SingleOrDefault(u => u.Cards.Any(c => c.Number == card.Number));
                if (user == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var game = context.Games.SingleOrDefault(g => g.Name == dto.Title);
                if (game == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                sb.AppendLine(string.Format(SuccessImportPurchases, dto.Title, user.Username));
                var date = DateTime.ParseExact(dto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                var purchase = new Purchase(dto.Type, dto.ProductKey,date, card.Id, game.Id);

                purchases.Add(purchase);
            }

            context.Purchases.AddRange(purchases);
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