using System.ComponentModel.DataAnnotations;

namespace RetailxAPI.Data.Entities
{
    public class User
    {
        [Key]
        public short UserId { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }=string.Empty;
        [MaxLength(50)]
        public string Passwd { get; set; }=  string.Empty;
        [MaxLength(50)]
        public string Statu { get; set; } = string.Empty;
        public short IsActive { get; set; }=1;
    }
}
