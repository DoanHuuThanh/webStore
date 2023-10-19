using doanhuuthanh_web.Application.System.Users;
using doanhuuthanh_web.ViewModel.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace doanhuuthanh_web.backendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) {
            _userService = userService;
        }
        [HttpPost("authenticate")]
        [AllowAnonymous] //ếu bạn sử dụng [AllowAnonymous] trước một action, điều này có nghĩa là bất kỳ ai cũng có thể truy cập vào action đó mà không cần phải đăng nhập.
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request) {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultToken = await _userService.Authencate(request);
            if(string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("UserName or Password is incorrect.");
            } 
            return Ok(new {token = resultToken });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Register(request);
            if (!result)
            {
                return BadRequest("Register is unsuccessful");
            }
            return Ok();
        }

    }
}
