using LoginWithTokenAntiForgery30Aug21.CQRS.Auth;
using LoginWithTokenAntiForgery30Aug21.DTOs.Login.Requests;
using LoginWithTokenAntiForgery30Aug21.service.Auth;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWithTokenAntiForgery30Aug21.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public LoginController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        //[HttpGet]
        //public IActionResult Get() => Ok("Please Select [Post] Method");

        [HttpPost]
        [AllowAnonymous]
        //[IgnoreAntiforgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest.UserName != "admin"||loginRequest.Password != "Pa$$WoRd") return BadRequest("your username/password are invalid.");
            
            return Ok(new
            {
                Id = "Kimetsu123",
                Username = loginRequest.UserName,
                Token = _antiforgery.GetAndStoreTokens(HttpContext)
            });
        }
    }
}
