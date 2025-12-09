using f1api.Data;
using f1api.Dtos;
using f1api.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace f1api.Services
{
    public class RaceService(AppDbContext context) : IRaceService
    {

        public async Task<RaceResponse?> GetRaceById(int id)
        {
            var team = await context.Races
                .Where(a => a.Id == id)
                .Select(a => new RaceResponse
                {
                    RaceName = a.RaceName,
                    RaceDate = a.RaceDate,
                    Winner = a.Winner.Name,
                    Second = a.Second.Name,
                    Third = a.Third.Name,
                }).FirstAsync();
            return team;
        }

        public async Task<bool> UpdateRaceResult(int id, UpdateRaceRequest updateRace)
        {
            var race = context.Races.Find(id);

            if (race is null)
                return false;

            race.WinnerId = updateRace.WinnerId;
            race.SecondId = updateRace.SecondId;
            race.ThirdId = updateRace.ThirdId;

            await context.SaveChangesAsync();
            return true;

        }

        public async Task<RaceResponse> CreateRace(CreateRaceRequest createRace)
        {
            var newRace = new Race
            {
                RaceName = createRace.RaceName,
                //RaceDate = DateTime.UtcNow,

            };

            context.Races.Add(newRace);
            await context.SaveChangesAsync();

            return new RaceResponse
            {

                RaceName = createRace.RaceName,
                RaceDate = createRace.RaceDate,

            };
        }

        public async Task<List<RaceResponse>> GetAllRacesAsync()
            => await context.Races.Select(a => new RaceResponse
            {
                Id = a.Id,
                RaceName = a.RaceName,
                RaceDate = a.RaceDate,
                Winner = a.Winner.Name,
                Second = a.Second.Name,
                Third = a.Third.Name,
            }).ToListAsync();
    }

}
