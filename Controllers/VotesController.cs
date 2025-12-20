using f1api.Dtos;
using f1api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace f1api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController(IVoteService service) : ControllerBase
    {
        [HttpPost("driverforvote")]
        public async Task<IActionResult> DriverGetVote(DriverVote vote)
        {

            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;


            _ = Guid.TryParse(userIdString, out Guid Id) ? Id : Guid.Empty;

            var result = await service.GetDriverVote(vote, Id);
            if (!result) 
                return NotFound();
            return Ok(result);
        }

        [HttpPost("teamforvote")]
        public async Task<IActionResult> TeamGetVote(TeamVote vote)
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            _ = Guid.TryParse(userIdString, out Guid Id) ? Id : Guid.Empty;
            var result = await service.GetTeamVote(vote, Id);
            if (!result)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("bestTeam")]
        public async Task<IActionResult> GetBestTeam()
        {
            var result = await service.BestTeams();
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("bestDrivers")]
        public async Task<IActionResult> GetBestDrivers()
        {
            var result = await service.BestDrivers();
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("racePrediction")]
        public async Task<IActionResult> RacePrediction (RacePre prediction)
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            _ = Guid.TryParse(userIdString, out Guid Id) ? Id : Guid.Empty;
            var result = await service.RacePrediction(prediction, Id);
            if (!result)
                return NotFound();
            return Ok(result);
        }

    }
}
