using Microsoft.AspNetCore.Mvc;
using RCM.Model;
using RCM.Service;
using RCM.ViewModels;

namespace RCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileMessageFormController : ControllerBase
    {
        private readonly IProfileMessageFormService _profileMessageFormService;

        public ProfileMessageFormController(IProfileMessageFormService profileMessageFormService)
        {
            _profileMessageFormService = profileMessageFormService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_profileMessageFormService.GetProfileMessageForms());
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] ProfileMessageFormIM profileMessageFrom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(profileMessageFrom);
            }

            //Add messge to Db
            var message = new ProfileMessageForm()
            {
                Name = profileMessageFrom.Name,
                Content = profileMessageFrom.Content,
                Type = profileMessageFrom.Type
            };
            _profileMessageFormService.CreateProfileMessageForm(message);
            _profileMessageFormService.SaveProfileMessageForm();

            return Ok();
        }
    }
}
