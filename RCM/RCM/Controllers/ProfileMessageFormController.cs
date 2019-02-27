using Microsoft.AspNetCore.Mvc;
using RCM.Helper;
using RCM.Model;
using RCM.Service;
using RCM.ViewModels;
using System.Linq;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_profileMessageFormService.GetProfileMessageForms());
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProfileMessageFormIM profileMessageFrom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!profileMessageFrom.Content.Contains(Constant.MESSAGE_PARAMETER_DEBTAMOUNT) || !profileMessageFrom.Content.Contains(Constant.MESSAGE_PARAMETER_NAME))
            {
                return BadRequest(new { Message = "Message form must contain " + Constant.MESSAGE_PARAMETER_DEBTAMOUNT + " and " + Constant.MESSAGE_PARAMETER_NAME + "." });
            }

            //Add messge to Db
            var message = new ProfileMessageForm()
            {
                Name = profileMessageFrom.Name,
                Content = profileMessageFrom.Content.Trim(),
                Type = profileMessageFrom.Type
            };
            _profileMessageFormService.CreateProfileMessageForm(message);
            _profileMessageFormService.SaveProfileMessageForm();

            var result = _profileMessageFormService.GetProfileMessageForms().LastOrDefault();
            return Ok(result);
        }
    }
}
