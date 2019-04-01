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
using Newtonsoft.Json.Linq;

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
            client.DefaultRequestHeaders.Add("X-STRINGEE-AUTH", "eyJjdHkiOiJzdHJpbmdlZS1hcGk7dj0xIiwidHlwIjoiSldUIiwiYWxnIjoiSFMyNTYifQ.eyJqdGkiOiJTSzdCUlpiQXZZU3FDRjRsQmRvcTNES3NETWNjR3pvTy0xNTU0MDM4OTU3IiwiaXNzIjoiU0s3QlJaYkF2WVNxQ0Y0bEJkb3EzREtzRE1jY0d6b08iLCJleHAiOjE1NTY2MzA5NTcsInJlc3RfYXBpIjp0cnVlfQ.ETVB2D70cNpvtDwo0uY8UEFp21V09UXvQbuinFFjQyY");
            string data = "{\"from\":{\"type\":\"external\",\"number\":\"84901701062\",\"alias\":\"STRINGEE_NUMBER\"},\"to\":[{\"type\":\"external\",\"number\":\"" + number + "\",\"alias\":\"Thong\"}],\"answer_url\":\"https://example.com/answerurl\",\"actions\":[{\"action\":\"talk\",\"voice\":\"hatieumai\",\"text\":\"" + content + "\",\"speed\":-3,\"silenceTime\":1000}]}";
            var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            var stringTask = await client.PostAsync("https://api.stringee.com/v1/call2/callout", stringContent);
            var msg = stringTask.Content.ReadAsStringAsync().Result;
            JObject call = JObject.Parse(msg);
            var callId = call.SelectToken("call_id");
            return Ok(msg);
        }
        [HttpPost("Getstring")]
        public async Task<IActionResult> TestPhone(string phone)
        {
            //var callId = "call-vn-1-OZQMBPBUWM-1553965647526";
            //var stringeeMsg = await Utility.MakePhoneCallAsync(phone, "Chào mừng bạn đã đến với Stringee");
            //#region get CallId
            //JObject call = JObject.Parse(stringeeMsg);
            //var callId = call.SelectToken("call_id").ToString();
            //#endregion
            //Check phone call
            //if (await Utility.CheckCall(callId, phone))
            //{
            //    //_progressStageActionService.MarkAsDone(progressStageAction);
            //    //_progressStageActionService.SaveProgressStageAction();
            //    return Ok(true);
            //}
            return Ok(false);
        }
        [HttpPost("Epoch")]
        public IActionResult Epoch()
        {
            // Example of a UNIX timestamp for 11-29-2013 4:58:30
            double timestamp = 1554042622308;

            // Format our new DateTime object to start at the UNIX Epoch
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);

            // Add the timestamp (number of seconds since the Epoch) to be converted
            dateTime = dateTime.AddMilliseconds(timestamp);

            return Ok(dateTime);
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

        [HttpGet("GetServerDay")]
        public IActionResult GetServerDay()
        {
            return Ok(Utility.ConvertDatetimeToString(DateTime.Now));
        }

        [HttpPost("SendNotificationFireBase/{token}")]
        public async Task<IActionResult> SendNotificationFireBase(string token, string content, string title)
        {
            //var client = new RestClient("https://fcm.googleapis.com/fcm/send");
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("Authorization", "key=AIzaSyBPVURwPYjlk3U3rPBrujsl9cIHN6Q53Zg");
            //request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("undefined", "{\n    \n    \"data\": {\n        \"title\": \"Hello Xamarin\",\n        \"image\": \"https://firebase.google.com/images/social.png\",\n        \"message\": \"FCM\"\n    },\n    \"to\": \"cbPM_pge0_Y:APA91bGkQZBXHHuR2LY9K7tPNVMyTEKyvqJ4WiF0-AXxFyZk0Q3Hkmn7g_992FZx-T1VV1VoFay3BHblKQUosdJ1UZiMvqGn3HqU1Hu50UGpQ0AojnNaUbbccRTdSxmeDP6et2ibvPfg\"\n}", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);

            //FirebaseApp.Create(new AppOptions()
            //{
            //    Credential = GoogleCredential.FromFile("path/to/serviceAccountKey.json"),
            //});

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("cache-control", "no-cache");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=AIzaSyBPVURwPYjlk3U3rPBrujsl9cIHN6Q53Zg");
            string data = "{\n    \n    \"data\": {\n        \"title\": \"" + title + "\",\n        \"image\": \"https://firebase.google.com/images/social.png\",\n        \"message\": \"" + content + "\"\n    },\n    \"to\": \"" + token + "\"}";
            var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            var stringTask = await client.PostAsync("https://fcm.googleapis.com/fcm/send", stringContent);
            var msg = stringTask.Content.ReadAsStringAsync().Result;
            return Ok(msg);
        }

    }

}
