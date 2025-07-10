using System.ComponentModel.DataAnnotations;

namespace RetailxAPI.Data.Entities
{
    public class Qform
    {
        [Key]
        public short QformId { get; set; }
        [MaxLength(50)]
        public string QformName { get; set; } = string.Empty;
    }
}
