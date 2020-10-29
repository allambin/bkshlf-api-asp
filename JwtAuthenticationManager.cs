using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BKSHLF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BKSHLF
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public JwtAuthenticationManager(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
        }
        public async Task<string> Authenticate(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                var checkPwd = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                if (checkPwd.Succeeded)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                    _config["Tokens:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
            }

            return null;
        }
        // private readonly string key;

        // public JwtAuthenticationManager(string key)
        // {
        //     this.key = key;
        // }

        // public string Authenticate(string username, string password)
        // {
        //     // find user in DB
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var tokenKey = Encoding.ASCII.GetBytes(key);
        //     var tokenDescriptor = new SecurityTokenDescriptor
        //     {
        //         Subject = new ClaimsIdentity(new Claim[]
        //         {
        //             new Claim(ClaimTypes.Name, username)
        //         }),
        //         Expires = DateTime.UtcNow.AddHours(1),
        //         SigningCredentials = new SigningCredentials(
        //             new SymmetricSecurityKey(tokenKey),
        //             SecurityAlgorithms.HmacSha256Signature
        //         )
        //     };
        //     var token = tokenHandler.CreateToken(tokenDescriptor);
        //     return tokenHandler.WriteToken(token);
        // }
    }
}