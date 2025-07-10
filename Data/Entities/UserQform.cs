using System.ComponentModel.DataAnnotations;

namespace RetailxAPI.Data.Entities
{
    public class UserQform
    {
        //[Key]
        //public int Id { get; set; }
        public short? UserId { get; set; }
        public short? QformId { get; set; }
    }
}
