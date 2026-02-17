using f1api.Dtos;
using f1api.Models;

namespace f1api.Services
{
    public interface IAuthService
    {
        Task<User?> Register(RegisterUser register);

        Task<LoginResponse> Login (LoginUser login);

        Task<LoginResponse> RefreshToken(RefreshTokenRequest request);

        Task<bool> ResetPassword(ResetPassword password);

    }
}
