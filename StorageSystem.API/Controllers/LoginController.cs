using Microsoft.AspNetCore.Mvc;
using StorageSystem.API.Services;
using StorageSystem.Shared;
using StorageSystem.Shared.Dtos;

namespace StorageSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ApiResponse> Login([FromBody] UserDto param) =>
            await userService.LoginAsync(param.Account, param.Password);

        [HttpPost]
        public async Task<ApiResponse> Register([FromBody] UserDto param) =>
            await userService.RegisterAsync(param);
    }
}
