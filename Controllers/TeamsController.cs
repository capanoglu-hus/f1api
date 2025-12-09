using f1api.Dtos;
using f1api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace f1api.Controllers
{
    [Route("api/team/[controller]")]
    [ApiController]
    public class TeamsController(ITeamService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<TeamResponse>>> GetTeams()
            => Ok(await service.GetAllTeamAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamResponse>> GetTeam(int id)
        {
            var team = await service.GetTeamByIdAsync(id);
            return team is null ? NotFound("Team was not found") : Ok(team);
        }

        [HttpPost]
        public async Task<ActionResult<TeamResponse>> AddTeam(CreateTeamRequest team)
        {
            var createdTeam = await service.AddTeamAsync(team);
            return CreatedAtAction(nameof(GetTeam), new { id = createdTeam.Id }, createdTeam);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateTeam (int id , UpdateTeamRequest team)
        {
            var updatedTeam = await service.UpdateTeamAsync(id, team);
            return updatedTeam  ? NoContent() : NotFound("TEAM WAS NOT FOUND");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam (int id)
        {
            var deleted = await service.DeleteTeamAsync(id);
            return deleted ? NoContent() : NotFound("TEAM WAS NOT FOUND");
        }

        [HttpPut("teams/drivers/")]
        public async Task<ActionResult> TeamDriver(DriverChanges changes)
        {
            var assigned = await service.AssignDriverToTeamAsync(changes );
            return assigned ? NoContent() : NotFound("TEAM OR DRIVER WAS NOT FOUND");
        }

        

    }
}
