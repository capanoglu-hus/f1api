using System.ComponentModel.DataAnnotations;

namespace f1api.Dtos
{
    public class LoginUser
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
