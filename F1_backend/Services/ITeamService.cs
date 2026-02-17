using f1api.Dtos;

namespace f1api.Services
{
    public interface ITeamService
    {
        Task<List<TeamResponse>> GetAllTeamAsync();

        Task<TeamResponse?> GetTeamByIdAsync(int id);

        Task<TeamResponse> AddTeamAsync(CreateTeamRequest team);

        Task<bool> UpdateTeamAsync(int id, UpdateTeamRequest team);

        Task<bool> DeleteTeamAsync(int id);

        Task<bool> AssignDriverToTeamAsync(DriverChanges changes);

        
    }
}
