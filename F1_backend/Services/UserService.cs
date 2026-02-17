using f1api.Data;
using f1api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace f1api.Services
{
    public class UserService(AppDbContext context, IConfiguration configuration) : IUserService
    {
        public Task<UserResponse> GetUserInfo(Guid guid)
        {
            var user = context.Users.Where(s => s.Id == guid)
                .Select(s => new UserResponse
                {
                    Name = s.Name,
                    Email =s.Email,
                    WalletAddress = s.WalletAddress,
                    CreatedDate = s.CreatedDate
                }).FirstAsync();

            return user;
            
        }

        public async Task<bool> UpdateInfoUser(Guid guid , UpdateUser user)
        {
            var oldUser = await context.Users.Where(a => a.Id == guid).FirstAsync();
            
            if (oldUser is null)
                return false;

            oldUser.WalletAddress = user.WalletAddress;
            oldUser.Name = user.Name;

            await context.SaveChangesAsync();

            return true;
        }
    }
}
