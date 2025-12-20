using f1api.Data;
using f1api.Dtos;
using f1api.Enums;
using f1api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace f1api.Services
{
    public class AuthService(AppDbContext context , IConfiguration configuration) : IAuthService
    {
        public async Task<User?> Register(RegisterUser register)
        {
            if (await context.Users.AnyAsync(u => u.Email == register.Email))
            {
                return null;
            }

            if(register.Password != register.PasswordConfirm)
            {
                return null;
            }

            var user = new User()
            {
                Name = register.Name,
                Email = register.Email,
                Id = Guid.NewGuid()

            };

            var hashPassword = new PasswordHasher<User>()
              .HashPassword(user, register.Password);


            user.PasswordHash = hashPassword;
            user.CreatedDate = DateTime.UtcNow;
            user.Role = UserRole.User;
            user.RefreshToken = string.Empty;

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<LoginResponse> Login(LoginUser login)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == login.Email);

            if (user is null) { return null; } 

            if(new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash,login.Password )
                == PasswordVerificationResult.Failed)
            {
                return null;
            }
            var response = new LoginResponse() 
            { 
                AccessToken = CreateJWT(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user)
            };
            return response;
        }

        private string GenerateRefreshToken()
        {
            // güvenli rastgele bir dizi bayt oluşturuyoruz
            var randomNumber = new byte[32];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
            
        }

        // refresh token oluşturma ve db kayıt 
        private async Task<string> GenerateAndSaveRefreshToken(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await context.SaveChangesAsync();
            return refreshToken;
        }


         private string CreateJWT(User user)
         {
            // token bilgilerini ayarmak için gerekli olan claim'leri oluşturuyoruz
            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, user.Name.ToString()),
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Role, user.Role.ToString())


             };

             var key = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")));

             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

             var tokenDescriptor = new JwtSecurityToken(
                 issuer : configuration.GetValue<string>("AppSettings:Issuer"),
                 audience : configuration.GetValue<string>("AppSettings:Audience"),
                 claims : claims,
                 expires : DateTime.UtcNow.AddDays(1),
                 signingCredentials : creds
                 );

             return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

         }

        private async Task<User?> ValidationRefreshToken(Guid userId , string refreshToken)
        {
            var user = await context.Users.FindAsync(userId);
            if(user is null || user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow) 
            {
                return null;
            }

            return user;    
        }

       public async  Task<LoginResponse> RefreshToken(RefreshTokenRequest request)
        {
            var user = await ValidationRefreshToken(request.UserId, request.RefreshToken);
            if(user is null) 
            {
                return null;
            }
            return new LoginResponse()
            {
                AccessToken = CreateJWT(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user)
            };
       }

        public async Task<bool> ResetPassword(ResetPassword password)
        {
            var user = await context.Users
                 .FirstOrDefaultAsync(u => u.Email == password.Email);

            if (user is null)
                return false;


            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password.OldPassword)
                == PasswordVerificationResult.Failed)
            {
                return false;
            }

            if (password.NewPassword != password.NewPasswordConfrim)
            {
                return false;
            }

            var hashPassword = new PasswordHasher<User>()
              .HashPassword(user, password.NewPassword);

            user.PasswordChangeDate = DateTime.UtcNow;
            user.PasswordHash = hashPassword;

            await context.SaveChangesAsync();

            return true;
        }
    }
}
