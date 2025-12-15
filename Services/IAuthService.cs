using f1api.Dtos;
using f1api.Models;

namespace f1api.Services
{
    public interface IAuthService
    {
        Task<User?> Register(RegisterUser register);

        Task<string> Login (LoginUser login);

      
    }
}
