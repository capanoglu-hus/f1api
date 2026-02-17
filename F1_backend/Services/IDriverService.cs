using f1api.Dtos;
using f1api.Models;

namespace f1api.Services
{
    public interface IDriverService
    {
        Task<List<DriverResponse>> GetAllDriverAsync();

        Task<DriverResponse?> GetDriverByIdAsync(int id);

        Task<DriverResponse> AddDriverAsync(CreateDriverRequest driver);

        Task<bool> UpdateDriverAsync(int id, UpdateDriverRequest driver);

        Task<bool> DeleteDriverAsync(int id);
    }
}
