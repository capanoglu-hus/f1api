using f1api.Dtos;
using f1api.Models;
using f1api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

namespace f1api.Controllers
{
    [Authorize]
    [Route("api/driver/[controller]")]
    [ApiController]
    public class DriversController(IDriverService service) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<DriverResponse>>> GetDrivers()
            => Ok(await service.GetAllDriverAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<DriverResponse>> GetDriver( int id)
        {
            var driver = await service.GetDriverByIdAsync(id);
            return driver is null? NotFound("Driver was not found") : Ok(driver);
            /*if ( driver is null)
            {
                return NotFound("Driver was not found");

            }
            return Ok(driver);
             */

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<DriverResponse>> AddDriver(CreateDriverRequest driver)
        {
            var createdDriver = await service.AddDriverAsync(driver);
            return CreatedAtAction(nameof(GetDriver), new { id = createdDriver.Id }, createdDriver);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateDriver(int id , UpdateDriverRequest driver)
        {
            var updatedDriver = await service.UpdateDriverAsync(id, driver);
            return updatedDriver ? NoContent() : NotFound("Driver was not found");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDriver(int id)
        {
            var deleted = await service.DeleteDriverAsync(id);
            return deleted? NoContent() : NotFound("Driver was not found");
        }


    }
}
