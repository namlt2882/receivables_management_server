using Mapster;
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

            return Ok(contact.Adapt<ContactVM>());
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
            return StatusCode(201,contact.Adapt<ContactVM>());
        }

        [HttpPut]
        public IActionResult Update([FromBody] ContactVM contactVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contact = _contactService.GetContact(contactVM.Id);
            if (contact != null)
            {
                contact.Id = contactVM.Id;
                contact.Address = contactVM.Address;
                contact.IdNo = contactVM.IdNo;
                contact.Name = contactVM.Name;
                contact.Phone = contactVM.Phone;

                _contactService.EditContact(contact);
                _contactService.SaveContact();

                return Ok(contact);
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contact = _contactService.GetContact(id);
            if (contact == null) return NotFound();
            _contactService.RemoveContact(contact);
            _contactService.SaveContact();
            return Ok();
        }
    }
}
