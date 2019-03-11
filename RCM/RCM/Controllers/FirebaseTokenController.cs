using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RCM.Model;
using RCM.Service;

namespace RCM.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FirebaseTokenController : ControllerBase
    {
        private readonly IFirebaseTokenService _firebaseTokenService;
        private readonly UserManager<User> _userManager;

        public FirebaseTokenController(IFirebaseTokenService firebaseTokenService, UserManager<User> userManager)
        {
            _firebaseTokenService = firebaseTokenService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddFirebaseToken([FromQuery]string firebaseToken)
        {
            var _user = await _userManager.FindByNameAsync(User.Identity.Name);
            _firebaseTokenService.CreateFirebaseToken(new FirebaseToken() { Token = firebaseToken, UserId = _user.Id });
            _firebaseTokenService.SaveFirebaseToken();
            return StatusCode(201);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFirebaseTokenAsync([FromQuery]string firebaseToken)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            _firebaseTokenService.GetFirebaseTokens(_ => _.UserId == user.Id).FirstOrDefault().Token = firebaseToken;
            _firebaseTokenService.SaveFirebaseToken();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFirebaseTokenAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = _firebaseTokenService.GetFirebaseTokens(_ => _.UserId == user.Id).FirstOrDefault();
            _firebaseTokenService.RemoveFirebaseToken(model.Id);
            _firebaseTokenService.SaveFirebaseToken();
            return Ok();
        }
    }
}
