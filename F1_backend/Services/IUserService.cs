using f1api.Dtos;

namespace f1api.Services
{
    public interface IUserService
    {
        Task<UserResponse> GetUserInfo(Guid guid);
        Task<bool> UpdateInfoUser(Guid guid, UpdateUser user);
       
    }
}
