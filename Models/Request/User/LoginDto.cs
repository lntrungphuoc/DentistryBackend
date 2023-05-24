using System.ComponentModel.DataAnnotations;

namespace AppDentistry.Models.Request.User
{
    public class LoginDto
    {
        [Required]
        public virtual string Username { get; set; }
        [Required]
        public virtual string Password { get; set; }
    }
}
