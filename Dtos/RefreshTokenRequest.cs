namespace f1api.Dtos
{
    public class RefreshTokenRequest
    {
        public Guid UserId { get; set; }

        public required string RefreshToken { get; set; }
    }
}
