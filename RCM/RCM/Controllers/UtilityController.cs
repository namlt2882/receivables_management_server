using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RCM.Model;
using RCM.Helper;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using RCM.SpeedSMS;

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

        [HttpPost("MakeOutboundCalls")]
        public async Task<IActionResult> PhoneAsync(string number, string content)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("cache-control", "no-cache");
            client.DefaultRequestHeaders.Add("X-STRINGEE-AUTH", "eyJjdHkiOiJzdHJpbmdlZS1hcGk7dj0xIiwidHlwIjoiSldUIiwiYWxnIjoiSFMyNTYifQ.eyJqdGkiOiJTSzhMa1FQVmVvcEdwSnY3UjdwanpsN3h3TG1iVkZVdDZZLTE1NTA2MDEyNjAiLCJpc3MiOiJTSzhMa1FQVmVvcEdwSnY3UjdwanpsN3h3TG1iVkZVdDZZIiwiZXhwIjoxNTUzMTkzMjYwLCJyZXN0X2FwaSI6dHJ1ZX0.fOcqwWeCpE53CAxTGmQzTVWAJdhF6yjMRhMGTTjl3qA");
            string data = "{\"from\":{\"type\":\"external\",\"number\":\"842471008859\",\"alias\":\"STRINGEE_NUMBER\"},\"to\":[{\"type\":\"external\",\"number\":\"" + number + "\",\"alias\":\"Thong\"}],\"answer_url\":\"https://example.com/answerurl\",\"actions\":[{\"action\":\"talk\",\"voice\":\"hatieumai\",\"text\":\"" + content + "\",\"speed\":-3,\"silenceTime\":1000}]}";
            var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            var stringTask = await client.PostAsync("https://api.stringee.com/v1/call2/callout", stringContent);
            var msg = stringTask.Content.ReadAsStringAsync().Result;
            return Ok(msg);
        }


        [HttpPost("SendSMS")]
        public IActionResult SendSMS(string number, string content)
        {
            SpeedSMSAPI api = new SpeedSMSAPI("H-TVWpn8KOmBc_mO9D1WOTKkV0IDWexj");
            /**
            * Để lấy thông tin về tài khoản như: email, số dư tài khoản bạn sử dụng hàm getUserInfo()
            */
            String userInfo = api.getUserInfo();
            /* * Hàm getUserInfo() sẽ trả về một json string như sau:
             * /
            {"email": "test@speedsms.vn", "balance": 100000.0, "currency": "VND"}

            /** Để gửi SMS bạn sử dụng hàm sendSMS như sau:
            */
            String[] phones = new String[] { number };
            int type = 2;
            /**
            sms_type có các giá trị như sau:
            2: tin nhắn gửi bằng đầu số ngẫu nhiên
            3: tin nhắn gửi bằng brandname
            4: tin nhắn gửi bằng brandname mặc định (Verify hoặc Notify)
            5: tin nhắn gửi bằng app android
            */
            String sender = "Thong";
            /**
            brandname là tên thương hiệu hoặc số điện thoại đã đăng ký với SpeedSMS hoặc android deviceId của bạn
            */

            String response = api.sendSMS(phones, content, type, sender);

            return Ok(response);
        }
       
    }

}
