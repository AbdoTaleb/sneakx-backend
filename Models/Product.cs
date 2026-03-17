using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SneakX.API.Models
{
    [Table("products")]
    public class Product
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("name")]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("brand")]
        [Column("brand")]
        public string Brand { get; set; } = string.Empty;

        [JsonPropertyName("gender")]
        [Column("gender")]
        public string Gender { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        [Column("category")]
        public string Category { get; set; } = string.Empty;

        [Range(0, 10000)]
        [JsonPropertyName("price")]
        [Column("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("is_in_inventory")]
        [Column("is_in_inventory")]
        public bool IsInInventory { get; set; }

        [JsonPropertyName("items_left")]
        [Column("items_left")]
        public int ItemsLeft { get; set; }

        [Url]
        [JsonPropertyName("imageURL")]
        [Column("image_url")]
        public string ImageUrl { get; set; } = string.Empty;

        [JsonPropertyName("slug")]
        [Column("slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonPropertyName("featured")]
        [Column("featured")]
        public int Featured { get; set; }
    }
}