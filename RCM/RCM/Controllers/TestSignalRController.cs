using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RCM.Controllers
{
    [Route("[controller]")]
    public class TestSignalRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
