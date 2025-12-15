using f1api.Enums;

namespace f1api.Dtos
{
    public class UpdateUser
    {
       
        public  string Name { get; set; }

        public  string Email { get; set; }

        public string? WalletAddress { get; set; }


        public DateTime? PasswordChangeDate { get; set; }

        public string RefreshToken { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

     
    }
}
