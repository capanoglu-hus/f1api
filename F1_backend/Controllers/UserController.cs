using f1api.Dtos;
using f1api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace f1api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService service) : ControllerBase
    {
        [Authorize] 
        [HttpGet("my-profile")]
        public async Task<IActionResult> GetUserInfo()
        {
            // Token içindeki NameIdentifier claim'ini (Genelde GUID buradadır) okuruz
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;


            _ = Guid.TryParse(userIdString, out Guid Id) ? Id : Guid.Empty;

            // Service'e GUID'i biz gönderiyoruz, dışarıdan gelmiyor
            var user = await service.GetUserInfo(Id);

            return Ok(user);
        }

        [HttpPost("update-my-profile")]
        public async Task<IActionResult> UpdateUserInfo(UpdateUser user)
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;


            _ = Guid.TryParse(userIdString, out Guid Id) ? Id : Guid.Empty;

            // Service'e GUID'i biz gönderiyoruz, dışarıdan gelmiyor
            var result = await service.UpdateInfoUser(Id , user);
            return Ok(result);
        }
    }
}
