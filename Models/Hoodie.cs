using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SneakX.API.Models
{
    public class Hoodie
    {
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("brand")]
        public string Brand { get; set; } = string.Empty;

        [JsonPropertyName("gender")]
        public string Gender { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [Range(0, 10000)]
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("is_in_inventory")]
        public bool IsInInventory { get; set; }

        [JsonPropertyName("items_left")]
        public int ItemsLeft { get; set; }

        [Url]
        [JsonPropertyName("imageURL")]
        public string? ImageUrl { get; set; }


        [JsonPropertyName("slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonPropertyName("featured")]
        public int Featured { get; set; }
    }
}
