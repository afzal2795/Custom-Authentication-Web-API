using System.ComponentModel.DataAnnotations;

namespace Food_Order_Custom_Authentication.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
