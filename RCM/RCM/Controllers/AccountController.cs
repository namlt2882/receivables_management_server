using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RCM.Helpers;
using RCM.Model;
using RCM.ViewModels;

namespace RCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userIdentity = new User()
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsBanned = false,
                Address="",
            };
            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(Errors.AddErrorsToModelState(result, ModelState));
            }
            await _userManager.AddToRoleAsync(userIdentity, model.RoleName);
            return Ok("Account created");
        }
    }
}
