using System.ComponentModel.DataAnnotations;

namespace RetailxAPI.Data.Entities
{
    public class Shop
    {
        [Key]
        public short ShopId { get; set; }
        [MaxLength(50)]
        public string ShopName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? ShopPhone { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? Address { get; set; }
    }
}
