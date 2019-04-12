using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RCM.Model.Algorithm;
using RCM.Service;

namespace RCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointController : ControllerBase
    {
        private readonly IPointService _pointService;

        public PointController(IPointService pointService)
        {
            _pointService = pointService;
        }

        [HttpGet("GetAllCollectorCpp")]
        public IActionResult GetAllCollectorCpp()
        {
            var cpps = _pointService.GetAllCollectorCpp();
            var rs = cpps.Select(cpp => new CollectorCppVM {
                CollectorId = cpp.CollectorId,
                CurrentReceivable = cpp.CurrentReceivable,
                CPP = cpp.CPP,
                TotalReceivableCount = cpp.PPPRs.Count()
            });
            return Ok(rs);
        }
    }
}
