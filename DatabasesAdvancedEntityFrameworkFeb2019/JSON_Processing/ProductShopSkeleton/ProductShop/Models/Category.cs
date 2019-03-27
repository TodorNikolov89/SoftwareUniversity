namespace ProductShop.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.CategoryProducts = new List<CategoryProduct>();
        }

        [Required]
        public int Id { get; set; }
                       
        [MinLength(3)]
        [MaxLength(15)]
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string Name { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
