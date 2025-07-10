using System.ComponentModel.DataAnnotations;

namespace RetailxAPI.Data.Models
{
    public class UserModel
    {
        public short UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Passwd { get; set; } = string.Empty;
        public string Statu { get; set; } = string.Empty;
        public short IsActive { get; set; } = 1;
    }
}
