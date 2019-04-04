namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dtos.Export;
    using Formatting = Newtonsoft.Json.Formatting;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var data = context.Genres
                .Where(g => genreNames.Contains(g.Name))
                .Select(genre => new GenreDto()
                {
                    Id = genre.Id,
                    Genre = genre.Name,
                    Games = genre.Games
                    .Where(g => g.Purchases.Any())
                    .Select(game => new GameExportDto()
                    {
                        Id = game.Id,
                        Title = game.Name,
                        Developer = game.Developer.Name,
                        Tags = string.Join(", ", game.GameTags.Select(t => t.Tag.Name)),
                        Players = game.Purchases.Count
                    })
                    .OrderByDescending(c => c.Players)
                    .ThenBy(gi => gi.Id)
                    .ToList(),
                    TotalPlayers = genre.Games.Sum(p => p.Purchases.Count)
                })
                .OrderByDescending(tp => tp.TotalPlayers)
                .ThenBy(i => i.Id)
                .ToArray();

            string result = JsonConvert.SerializeObject(data, Formatting.Indented);

            return result;
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var storeTypeValue = Enum.Parse<PurchaseType>(storeType);

            var users = context
                .Users
                .Select(u => new UserExportDto()
                {
                    Username = u.Username,
                    Purchases = u.Cards
                        .SelectMany(c => c.Purchases)
                        .Where(t => t.Type == storeTypeValue)
                        .Select(p => new PurchaseDto()
                        {
                            CardNumber = p.Card.Number,
                            Cvc = p.Card.Cvc,
                            Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                            Game = new GameDto()
                            {
                                Name = p.Game.Name,
                                Genre = p.Game.Genre.Name,
                                Price = p.Game.Price
                            }
                        })
                    .OrderBy(d => d.Date)
                    .ToList(),

                    TotalSpent = u.Cards
                        .SelectMany(c => c.Purchases)
                        .Where(p => p.Type == storeTypeValue)
                        .Sum(p => p.Game.Price)
                })
                .Where(u => u.Purchases.Any())
                .OrderByDescending(t => t.TotalSpent)
                .ThenBy(n => n.Username)
                .ToArray();

            var serializer = new XmlSerializer(typeof(UserExportDto[]), new XmlRootAttribute("Users"));

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
            serializer.Serialize(new StringWriter(sb), users, namespaces);

            var result = sb.ToString();
            return result;
        }
    }
}