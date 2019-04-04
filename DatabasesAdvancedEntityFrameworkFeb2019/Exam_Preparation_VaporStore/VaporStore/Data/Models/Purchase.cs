using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VaporStore.Data.Models.Enums;

namespace VaporStore.Data.Models
{
    public class Purchase
    {
        public Purchase(PurchaseType type, string productKey, DateTime date, int cardId, int gameId)
        {
            this.Type = type;
            this.ProductKey = productKey;
            this.Date = date;
            this.CardId = cardId;
            this.GameId = gameId;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public PurchaseType Type { get; set; }

        [Required]
        [RegularExpression(@"^[\dA-Z]{4}-[\dA-Z]{4}-[\dA-Z]{4}$")]
        public string ProductKey { get; set; }
        
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CardId { get; set; }
        public Card Card { get; set; }

        [Required]
        public int GameId { get; set; }
        public Game Game { get; set; }

    }
}
