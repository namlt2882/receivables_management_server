using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RCM.Model;
using RCM.Helper;

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
                    Id = Constant.CONTACT_RELATION_CODE,
                    Name =Constant.CONTACT_RELATION
                },
                new
                {
                    Id = Constant.CONTACT_RELATION_CODE,
                    Name =Constant.CONTACT_RELATION
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
                    Id = Constant.ACTION_SMS_CODE,
                    Name =Constant.ACTION_SMS
                },
                new
                {
                    Id = Constant.ACTION_PHONECALL_CODE,
                    Name =Constant.ACTION_PHONECALL
                },
                new
                {
                    Id = Constant.ACTION_NOTIFICATION_CODE,
                    Name =Constant.ACTION_NOTIFICATION
                },
                new
                {
                    Id = Constant.ACTION_REPORT_CODE,
                    Name =Constant.ACTION_REPORT
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
                    Id = Constant.COLLECTION_STATUS_COLLECTION_CODE,
                    Name =Constant.COLLECTION_STATUS_COLLECTION
                },
                new
                {
                    Id = Constant.COLLECTION_STATUS_CANCEL_CODE,
                    Name =Constant.COLLECTION_STATUS_CANCEL
                },
                new
                {
                    Id = Constant.COLLECTION_STATUS_DONE_CODE,
                    Name =Constant.COLLECTION_STATUS_DONE
                }
            };
            return Ok(list);
        }
    }
}
