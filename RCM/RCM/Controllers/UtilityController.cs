using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RCM.Model;

namespace RCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        [HttpGet("CustomerType")]
        public IActionResult CustomerType()
        {
            List<object> list = new List<object>()
            {
                new
                {
                    Id = Constant.DEBTOR_CODE,
                    Name =Constant.DEBTOR
                },
                new
                {
                    Id = Constant.RELATION_CODE,
                    Name =Constant.RELATION
                }
            };
            return Ok(list);
        }

        [HttpGet("ActionType")]
        public IActionResult ActionType()
        {
            List<object> list = new List<object>()
            {
                new
                {
                    Id = Constant.SMS_CODE,
                    Name =Constant.SMS
                },
                new
                {
                    Id = Constant.PHONECLASS_CODE,
                    Name =Constant.PHONECALL
                },
                new
                {
                    Id = Constant.NOTIFICATION_CODE,
                    Name =Constant.NOTIFICATION
                },
                new
                {
                    Id = Constant.REPORT_CODE,
                    Name =Constant.REPORT
                }
            };
            return Ok(list);
        }

        [HttpGet("CollectionType")]
        public IActionResult CollectionType()
        {
            List<object> list = new List<object>()
            {
                new
                {
                    Id = Constant.COLLECTION1_CODE,
                    Name =Constant.COLLECTION1
                },
                new
                {
                    Id = Constant.COLLECTION2_CODE,
                    Name =Constant.COLLECTION2
                },
                new
                {
                    Id = Constant.COLLECTION3_CODE,
                    Name =Constant.COLLECTION2
                }
            };
            return Ok(list);
        }
    }
}
