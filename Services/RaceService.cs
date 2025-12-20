using f1api.Data;
using f1api.Dtos;
using f1api.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;

namespace f1api.Services
{
    public class RaceService(AppDbContext context, IServiceScopeFactory _scope) : IRaceService
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
                RaceDate = DateTime.UtcNow,

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

        public async Task<bool> FinishRace(int id)
        {
            var RealRaceResult = await context.Races.FindAsync(id);
            if (RealRaceResult is null)
                return false;   
            
            var RacePredictions = await context.RacePredictions
                .Where(r => r.RaceId == id)
                .ToListAsync();

            foreach (var prediction in RacePredictions)
            {
                if (prediction.WinnerId == RealRaceResult.WinnerId &&
                    prediction.SecondId == RealRaceResult.SecondId &&
                    prediction.ThirdId == RealRaceResult.ThirdId)
                {
                    prediction.IsWinner = true;
                    context.RacePredictions.Update(prediction);
                    
                }
            }

            var TruePredictions = RacePredictions.Where(r => r.IsWinner).ToList();
             
            foreach (var winner in TruePredictions)
            {
               
                var nftReward = new UserNftReward
                {
                    UserId = winner.UserId,
                    RaceId = id,
                    NFTHash = "Pending", // Gerçek NFT hash'i burada olmalı
                    NftImageUrl = "https://example.com/nft-image.png", // Gerçek NFT görsel URL'si burada olmalı
                    AwardedDate = DateTime.UtcNow

                };

                context.UserNftRewards.Add(nftReward);

               
            }

            await context.SaveChangesAsync();

            Task.Run(async () =>
            {
                await NFTMind(id);
            });


            return true;
        }

        private async Task NFTMind(int raceId )
        {
            using (var scope = _scope.CreateScope())
            {


                var scopedContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var RacePredictions = await scopedContext.UserNftRewards
                    .Where(r => r.RaceId == raceId && r.NFTHash == "Pending")
                    .Select(r => r.UserId)
                    .ToListAsync();

                foreach (var userId in RacePredictions)
                {
                    var address = await scopedContext.Users
                   .Include(u => u.Id.Equals(userId))
                   .Select(u => u.WalletAddress)
                   .FirstOrDefaultAsync();


                    if (address is null)
                        continue;
                    // NFT mintleme işlemi burada yapılacak - address - raceId 
                }

                await scopedContext.SaveChangesAsync();
            }
           
        }
    }

}
