using f1api.Enums;

namespace f1api.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public string? WalletAddress { get; set; }

        public string PasswordHash { get; set; }

      

        public DateTime? PasswordChangeDate { get; set; }

        public string? RefreshToken { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

        public DateTime? CreatedDate { get; set; }= DateTime.UtcNow;
    }
}
