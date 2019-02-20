using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RCM.Data;
using RCM.Service;

namespace RCM.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly RCMContext _context;
        private readonly IReceivableService _receivableService;

        public ValuesController(RCMContext context, IReceivableService receivableService)
        {
            _context = context;
            _receivableService = receivableService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            var a = _receivableService.GetReceivable(1);
            return Ok(_receivableService.GetReceivables());
            return new string[] { "value1", "value2" };
        }

        [Authorize(Roles = "Admin,Manager")]
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
