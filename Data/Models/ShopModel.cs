

namespace RetailxAPI.Data.Models
{
    public class ShopModel
    {
        public short ShopId { get; set; }
        public string ShopName { get; set; } = string.Empty;
        public string? ShopPhone { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? Address { get; set; }
    }
}
