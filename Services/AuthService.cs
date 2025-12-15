using f1api.Data;
using f1api.Dtos;
using f1api.Enums;
using f1api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

            var user = new User()
            {
                Name = register.Name,
                Email = register.Email,
               
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

        public async Task<string> Login(LoginUser login)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == login.Email);

            if (user is null) { return null; } 

            if(new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash,login.Password )
                == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return CreateJWT(user);
        }

         private string CreateJWT(User user)
         {
             var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, user.Name.ToString()),
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                 

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


    }
}
