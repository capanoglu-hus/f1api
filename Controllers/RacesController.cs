using f1api.Dtos;
using f1api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace f1api.Controllers
{
    [Route("api/races/[controller]")]
    [ApiController]
    public class RacesController(IRaceService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<RaceResponse>>> GetAllRaces()
            => Ok(await service.GetAllRacesAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<RaceResponse>> GetRaceById(int id)
        {
            var race = await service.GetRaceById(id);
            return race is null ? NotFound("Race was not found") : Ok(race);
        }

        [HttpPost]
        public async Task<ActionResult<RaceResponse>> CreateRace(CreateRaceRequest createRace)
        {
            var createdRace = await service.CreateRace(createRace);
            return createdRace is null ? NotFound("CREATE RACE NOT") :  Ok(createdRace);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateRace (int id, UpdateRaceRequest updateRace)
        {
            var updatedRace = await service.UpdateRaceResult(id, updateRace);
            return updatedRace ? NoContent() : NotFound("Update not success");
        }
    }
}
