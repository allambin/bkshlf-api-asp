using System.Threading.Tasks;
using AutoMapper;
using BKSHLF.Data;
using BKSHLF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BKSHLF.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BkshlfContext _context;
        private readonly UserManager<User> _userManager;

        public UsersController(BkshlfContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] Dto.RegisterInfo registerInfo)
        {
            if (registerInfo == null)
            {
                return BadRequest();
            }

            var passwordValidator = new PasswordValidator<User>();
            var hasValidatedData = await passwordValidator.ValidateAsync(_userManager, null, registerInfo.Password);

            if (!hasValidatedData.Succeeded)
            {
                return BadRequest(hasValidatedData.Errors);
            }

            User user = new User
            {
                UserName = registerInfo.UserName,
                Password = registerInfo.Password,
                Email = registerInfo.Email
            };
            var hasCreatedUser = await _userManager.CreateAsync(user, user.Password);

            if (hasCreatedUser.Succeeded)
            {
                var userDto = new Dto.User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };
                return Ok(userDto);
            }
            else
            {
                return BadRequest(hasCreatedUser.Errors);
            }
        }
    }
}