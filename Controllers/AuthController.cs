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
        public  async Task<ActionResult<string>> Login(LoginUser loginUser)
        {
            var token = await service.Login(loginUser);
            return token is null ? BadRequest("Invalid email or password") : Ok(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterUser register)
        {
            var user = await service.Register(register);
            return user is null ? BadRequest("Email alreadty exists") : Ok(user);
        }
    }
}
