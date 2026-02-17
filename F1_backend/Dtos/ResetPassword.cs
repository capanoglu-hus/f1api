namespace f1api.Dtos
{
    public class ResetPassword
    {
        public required string OldPassword {  get; set; }
        public required string NewPassword { get; set; }
        public required string NewPasswordConfrim {  get; set; }
        public required string Email { get; set; }

    }
}
