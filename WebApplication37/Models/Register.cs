
using System.ComponentModel.DataAnnotations;

namespace WebApplication37.Models
{
    public class Register
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public string confrimpassword { get; set; }
        public string Role { get; set; }
    }
}
