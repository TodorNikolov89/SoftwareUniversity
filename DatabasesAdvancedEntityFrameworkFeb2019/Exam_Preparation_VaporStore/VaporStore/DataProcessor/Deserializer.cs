namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.Dtos;

    public static class Deserializer
    {
        public static string ErrorMessage = $"Invalid Data";
        public static string SuccessImportGames = "Added {0} ({1}) with {2} tags";

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var deserializedGames = JsonConvert.DeserializeObject<GameDto[]>(jsonString);

            List<Game> games = new List<Game>();
            List<Developer> developers = new List<Developer>();
            List<Genre> genres = new List<Genre>();
            List<Tag> tags = new List<Tag>();

            StringBuilder sb = new StringBuilder();

            foreach (var dto in deserializedGames)
            {
                if (!IsValid(dto) || !dto.Tags.All(IsValid))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var dev = context.Developers.SingleOrDefault(d => d.Name == dto.Developer);
                if (dev == null)
                {
                    Developer developer = new Developer()
                    {
                        Name = dto.Developer
                    };
                    developers.Add(developer);
                }

                var genre = context.Genres.SingleOrDefault(g => g.Name == dto.Genre);
                if (genre == null)
                {
                    Genre gen = new Genre()
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
                    Developer = dev,
                    Genre = genre,
                    GameTags = gameTags.Select(t => new GameTag { Tag = t }).ToArray()
                };

                games.Add(game);
                sb.AppendLine(string.Format(SuccessImportGames, game.Name, game.Genre, game.GameTags.Count));
            }
            context.Games.AddRange(games);
            context.SaveChanges();

            string result = sb.ToString();
            return result;

        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            throw new NotImplementedException();
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