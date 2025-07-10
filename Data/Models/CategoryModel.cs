using System.ComponentModel.DataAnnotations;

namespace RetailxAPI.Data.Models
{
    public class CategoryModel
    {
        public short CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
