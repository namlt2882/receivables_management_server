using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RCM.Helper;
using RCM.Model;
using RCM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<UserVM> result = new List<UserVM>();
            var data = _userManager.Users.ToList();
            foreach (var item in data)
            {
                result.Add(item.Adapt<UserVM>());
            }
            return Ok(result);
        }

        [HttpGet("GetCollector")]
        public async Task<IActionResult> GetCollectorAsync()
        {
            List<UserLM> result = new List<UserLM>();
            var data = await _userManager.GetUsersInRoleAsync("Collector");
            result = data
                .Select(x => new UserLM()
                {
                    Id = x.Id,
                    IsBanned = x.IsBanned,
                    NumberOfAssignedReceivables = x.AssignedCollectors
                                                    .Select(
                                                    assignedCollector => 
                                                    assignedCollector.Status == Constant.ASSIGNED_STATUS_ACTIVE_CODE 
                                                    && assignedCollector.UserId == x.Id).Count()
                }).ToList();
            return Ok(result);
        }

        [HttpGet("GetByUsername")]
        public async Task<IActionResult> Get(String username)
        {
            try
            {
                var result = await _userManager.FindByNameAsync(username);
                return Ok(result.Adapt<UserVM>());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserCM userCM)
        {
            try
            {
                var userIdentity = new User()
                {
                    UserName = userCM.UserName,
                    FirstName = userCM.FirstName,
                    LastName = userCM.LastName,
                    IsBanned = false,
                    Address = userCM.Address,
                    LocationId = userCM.LocationId,
                };
                userIdentity.IsBanned = false;
                var currentUser = await _userManager.CreateAsync(userIdentity, userCM.Password);
                if (currentUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userIdentity, "Manager");
                    return StatusCode(201);
                }
                else
                {
                    return BadRequest(currentUser.Errors);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody]UserUM userUM)
        {
            try
            {
                var data = await _userManager.FindByIdAsync(userUM.Id);
                if (data == null) return NotFound();
                data = userUM.Adapt(data);
                var result = await _userManager.UpdateAsync(data);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var data = await _userManager.FindByIdAsync(id);
                var result = await _userManager.DeleteAsync(data);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
