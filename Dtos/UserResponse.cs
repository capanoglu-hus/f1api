using f1api.Enums;

namespace f1api.Dtos
{
    public class UserResponse
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public string? WalletAddress { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
