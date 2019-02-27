using Microsoft.AspNetCore.Mvc;
using RCM.Model;
using RCM.Service;
using RCM.ViewModels;
using System.Linq;
using RCM.Helper;

namespace RCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IReceivableService _receivableService;

        public ContactController(IContactService contactService, IReceivableService receivableService)
        {
            _contactService = contactService;
            _receivableService = receivableService;
        }

        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = _contactService.GetContact(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet]
        public IActionResult GetContacts(int receivableId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receivable = _receivableService.GetReceivable(receivableId);
            if (receivable == null)
            {
                return NotFound(new { message = "Cannot find receivable id." });
            }

            var contacts = _contactService.GetContacts().Where(x => x.ReceivableId == receivableId);
            if (contacts == null)
            {
                return NotFound();
            }

            return Ok(contacts);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ContactIM contactIM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (contactIM.ReceivableId == null)
            {
                return BadRequest(new { message = "Receivable ID cannot be empty" });
            }

            var contact = new Contact()
            {
                ReceivableId = (int)contactIM.ReceivableId,
                IdNo = contactIM.IdNo,
                Address = contactIM.Address,
                Name = contactIM.Name,
                Phone = contactIM.Phone,
                Type = Constant.CONTACT_RELATION_CODE
            };

            _contactService.CreateContact(contact);
            _contactService.SaveContact();

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] ContactVM contactVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = new Contact()
            {
                Id = contactVM.Id,
                Address = contactVM.Address,
                IdNo = contactVM.IdNo,
                Name = contactVM.Name,
                Phone = contactVM.Phone,
            };

            _contactService.EditContact(contact);
            _contactService.SaveContact();

            return Ok(contact);
        }
    }
}
