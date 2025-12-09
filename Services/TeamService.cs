using f1api.Data;
using f1api.Dtos;
using f1api.Models;
using Microsoft.EntityFrameworkCore;

namespace f1api.Services
{
    public class TeamService(AppDbContext context) : ITeamService
    {
        public async Task<TeamResponse> AddTeamAsync(CreateTeamRequest team)
        {
            var newteam = new Team
            {
                Name = team.Name,
                Principal = team.Principal,
                
            };

            context.Teams.Add(newteam);
            await context.SaveChangesAsync();

            return new TeamResponse
            {
                Id = newteam.Id,
                Name = newteam.Name,
                Principal = newteam.Principal,
               
            };
        }

        public async Task<bool> DeleteTeamAsync(int id)
        {
            var deleteTeam = context.Teams.Find(id);
            if (deleteTeam is null)
                return false;

            context.Teams.Remove(deleteTeam);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TeamResponse>> GetAllTeamAsync()
            => await context.Teams.Select(c => new TeamResponse
            {
                Id = c.Id,
                Name = c.Name,
                Principal = c.Principal,
                Drivers = c.Drivers.Select(d => new CreateDriverRequest
                {
                    Name = d.Name,
                    RacingNumber = d.RacingNumber,
                    Description = d.Description,
                    
                }).ToList()
            }).ToListAsync();

        public async Task<TeamResponse?> GetTeamByIdAsync(int id)
        {
            var TeamID = await context.Teams
                .Where(a => a.Id == id)
                .Select(c => new TeamResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Principal = c.Principal,
                    Drivers = c.Drivers.Select(d => new CreateDriverRequest
                    {
                        Name = d.Name,
                        RacingNumber = d.RacingNumber,
                        Description = d.Description,
                       
                    }).ToList()
                }).FirstOrDefaultAsync();

            return TeamID;
        }

        public async Task<bool> UpdateTeamAsync(int id, UpdateTeamRequest team)
        {
            var teamById = await context.Teams.FindAsync(id);

            if(teamById is null) 
                return false;

            teamById.Name = team.Name; 
            teamById.Principal = team.Principal;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignDriverToTeamAsync(DriverChanges changes)
        {
            var team = await context.Teams
                .Include(t => t.Drivers)
                .FirstOrDefaultAsync(t => t.Id == changes.teamId);

            if (team == null)
                return false ;

           if(changes.oldDriverId != null)
           {
                var driver0 = await context.Drivers.FindAsync(changes.oldDriverId);

                if (driver0 == null)
                    return false;

                driver0.TeamId = null;
                await context.SaveChangesAsync();
                
            }
            
            if(team.Drivers.Count >=2)
            {
                return false;
            }

            var driver = await context.Drivers.FindAsync(changes.newDriverId);

            if (driver == null)
                return false;

            driver.TeamId = changes.teamId;
            await context.SaveChangesAsync();
            return true;
        }

       
    }
}
