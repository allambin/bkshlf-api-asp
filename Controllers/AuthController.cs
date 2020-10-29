using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BKSHLF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BKSHLF.Data;

namespace BKSHLF.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly IJwtAuthenticationManager _authenticationManager;

        public AuthController(IJwtAuthenticationManager authenticationManager, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _authenticationManager = authenticationManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] Dto.LoginInfo loginInfo)
        {
            var token = await _authenticationManager.Authenticate(loginInfo.UserName, loginInfo.Password);

            if (token == null)
            {
                return BadRequest("Could not create token");
            }
            
            return Ok(token);
        }

        // [HttpPost]
        // public IActionResult Authenticate([FromBody] UserRequest request)
        // {
        //     var token = _authenticationManager.Authenticate(request.Username, request.Passowrd);

        //     if (token == null) {
        //         return Unauthorized();
        //     }
        //     return Ok();
        // }
    }
}