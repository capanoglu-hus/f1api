using f1api.Dtos;
using f1api.Models;

namespace f1api.Services
{
    public interface IVoteService
    {
        Task<bool> GetDriverVote(DriverVote vote , Guid guid);

        Task<bool> GetTeamVote(TeamVote vote, Guid guid);

        Task<List<Driver>> BestDrivers();

        Task<Team> BestTeams();

        Task<bool> RacePrediction(RacePre prediction, Guid guid);

    }
}
