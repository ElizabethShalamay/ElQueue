using System.Security.Claims;
using System.Threading.Tasks;
using ElQueue.BLL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = ElQueue.BLL.Services.AuthenticationService.IAuthenticationService;

namespace ElQueue.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public UsersController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        //[HttpPost("/login")]
        //public async Task<IActionResult> Login(LoginModel model)
        //{
        //    var claim = await _authenticationService.SignInAsync(model.Login, model.Password);
        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claim));
        //    return Ok();
        //}

        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = await _authenticationService.RegisterAsync(model);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return NoContent();
        }
    }
}