using System.ComponentModel.DataAnnotations;

namespace RetailxAPI.Data.Entities
{
    public class Category
    {
        [Key]
        public short CategoryId { get; set; }
        [MaxLength(50)]
        public string CategoryName { get; set; } = string.Empty;
    }
}
