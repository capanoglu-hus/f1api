using f1api.Dtos;

namespace f1api.Services
{
    public interface IRaceService
    {
        Task<List<RaceResponse>> GetAllRacesAsync();

        Task<RaceResponse> GetRaceById(int id);

        Task<RaceResponse> CreateRace(CreateRaceRequest createRace);

        Task<bool> UpdateRaceResult(int id, UpdateRaceRequest updateRace);



    }
}
