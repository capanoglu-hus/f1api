using f1api.Data;
using f1api.Dtos;
using f1api.Models;
using Microsoft.EntityFrameworkCore;

namespace f1api.Services
{
    public class VoteService(AppDbContext context) : IVoteService
    {
        public async Task<bool> GetDriverVote(DriverVote vote, Guid guid)
        {
            var oldvote = await context.UserDriverVotes
                .AnyAsync(v => v.User.Id == guid);
            if (oldvote) return false;
            var user = await context.Users.FirstOrDefaultAsync(s => s.Id == guid);
            if (user is null) return false;
            var first = await context.Drivers.FindAsync(vote.FirstDriverId);
            if (first is null) return false;
            var second = await context.Drivers.FindAsync(vote.SecondDriverId);
            if (second is null) return false;
            var third = await context.Drivers.FindAsync(vote.ThirdDriverId);
            if (third is null) return false;

            var veto = new UserDriverVote
            {
                User = user,
                FirstDriver = first,
                SecondDriver = second,
                ThirdDriver = third,
                VoteDate = DateTime.UtcNow
            };

            context.UserDriverVotes.AddAsync(veto);

            await UpdateVoteDriver(vote.FirstDriverId, 5);
            await UpdateVoteDriver(vote.SecondDriverId, 3);
            await UpdateVoteDriver(vote.ThirdDriverId, 1);

            await context.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> GetTeamVote(TeamVote vote, Guid guid)
        {
            var oldvote = await context.UserTeamVotes
                .AnyAsync(v => v.User.Id == guid);
            if (oldvote) return false;

            var user = await context.Users.FindAsync(guid);
            if (user is null) return false;
            
            var team = await context.Teams.FindAsync(vote.TeamId);
            if (team is null) return false;

            var votes = new UserTeamVote
            {
                User = user,
                Team = team,
                VoteDate = DateTime.UtcNow
            };

            context.UserTeamVotes.AddAsync(votes);
            await UpdateTeamVote(team.Id);
            await context.SaveChangesAsync();
            return true;

        }

        private async Task<bool> UpdateVoteDriver(int driver ,int point) 
        {
            var rating = await context.DriverFanRatings.FirstOrDefaultAsync(r => r.DriverId == driver);
            if (rating == null)
            {
                rating = new DriverFanRating
                {
                    DriverId = driver,
                    TotalScore = point,
                    TotalVotes = 1
                };
                context.DriverFanRatings.Add(rating);
            }
            else
            {
                rating.TotalScore += point;
                rating.TotalVotes++;
                rating.RatedDate = DateTime.UtcNow;
                context.DriverFanRatings.Update(rating);
            }

            await context.SaveChangesAsync();
            return true;


        }

        private async Task<bool> UpdateTeamVote(int team)
        {
            var rating = await context.TeamFanRatings.FirstOrDefaultAsync(r => r.TeamId == team);
            if (rating == null)
            {
                var newRating = new TeamFanRating
                {
                    TeamId = team,
                    TotalVotes = 1,
                    RatedDate = DateTime.UtcNow
                };
                context.TeamFanRatings.Add(newRating);
            }
            else
            {
                rating.TotalVotes++;
                rating.RatedDate = DateTime.UtcNow;
                context.TeamFanRatings.Update(rating);
            }
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Driver>> BestDrivers()
        {
            var bestDrivers = await context.DriverFanRatings
                .OrderByDescending(r => r.TotalScore)
                .Take(3)
                .Include(r => r.Driver)
                .ToListAsync();

            return bestDrivers.Select(r => r.Driver).ToList();
        }

        public async Task<Team> BestTeams()
        {
            var team = await context.TeamFanRatings
                .OrderByDescending(r => r.TotalVotes)
                .Include(r => r.Team)
                .FirstOrDefaultAsync();

            return team?.Team;
        }

        public async Task<bool> RacePrediction(RacePre prediction, Guid guid)
        {
            // bir daha tahmin yapamaz 
            var oldPrediction = await context.RacePredictions
                .AnyAsync(p => p.User.Id == guid && p.Race.Id == prediction.RaceId);
            if (oldPrediction) return false;

            var RaceDate = await context.Races
                .Where(r => r.Id == prediction.RaceId)
                .Select(r => r.RaceDate)
                .FirstOrDefaultAsync(); 



            if (RaceDate == DateTime.UtcNow || RaceDate < DateTime.UtcNow) return false;

            var pre = new RacePrediction
            {
                UserId = guid,
                RaceId = prediction.RaceId,
                WinnerId = prediction.FirstPlaceDriverId,
                SecondId = prediction.SecondPlaceDriverId,
                ThirdId = prediction.ThirdPlaceDriverId,
                PredictionTime = DateTime.UtcNow,
            };

             context.RacePredictions.AddAsync(pre);
             await context.SaveChangesAsync();

            return true;

        }


    }
}
