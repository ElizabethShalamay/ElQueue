using ElQueue.BLL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IAuthenticationService = ElQueue.BLL.Services.AuthenticationService.IAuthenticationService;

namespace ElQueue.Web.Controllers
{   
    public class TokenController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private IConfiguration _config;

        public TokenController(IAuthenticationService authenticationService, IConfiguration config)
        {
            _authenticationService = authenticationService;
            _config = config;
        }

        //Endpoint is used to generate a bearer token
        [AllowAnonymous]
        [HttpPost]
        [Route("api/token")]
        public async Task<IActionResult> CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = await Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        //[HttpGet]
        //[Route("Account/Login")]
        //public async Task<IActionResult> Login(string returnUrl = null)
        //{
        //    return Unauthorized();
        //}

        private string BuildToken(UserBm user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
               };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserBm> Authenticate(LoginModel model)
        {
            var user = await _authenticationService.SignInAsync(model.Login, model.Password);
            return user;
        }
    }
}
