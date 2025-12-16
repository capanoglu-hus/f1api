using f1api.Dtos;
using f1api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace f1api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {
        [HttpPost("login")]
        public  async Task<ActionResult<LoginResponse>> Login(LoginUser loginUser)
        {
            var result = await service.Login(loginUser);
            return result is null ? BadRequest("Invalid email or password") : Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterUser register)
        {
            var user = await service.Register(register);
            return user is null ? BadRequest("Email alreadty exists") : Ok(user);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<LoginResponse>> RefreshToken(RefreshTokenRequest request)
        {
            var user = await service.RefreshToken(request);
            if( user is null || user.AccessToken is null || user.RefreshToken is null)
                return BadRequest("Invalid token or user id");
            return Ok(user);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPassword reset)
        {
            var user = await service.ResetPassword(reset);
            if (!user)
            {
                return BadRequest("ıt ıs wrong ");
            }
            return NoContent();
        }
    }
}
