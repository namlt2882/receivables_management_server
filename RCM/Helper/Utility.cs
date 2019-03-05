using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RCM.Helper
{
    public static class Utility
    {
        //Ex Input: 700 to 07:00 or 1700 to 17:00
        public static string ConvertIntToTimeStringForView(int time)
        {
            string tmp = time.ToString();
            if (tmp.Length != 4 && tmp.Length != 3)
            {
                return null;
            }

            if (tmp.Length == 3)
            {
                tmp = "0" + tmp;
            }
            string result = tmp.Substring(0, 2) + ":" + tmp.Substring(2);

            return result;
        }

        //Ex Input: 19970214 to 14/02/1997.
        public static string ConvertIntToDateStringForView(int time)
        {
            string tmp = time.ToString();
            if (tmp.Length != 8)
            {
                return null;
            }

            string result = tmp.Substring(6, 2) + "/" + tmp.Substring(4, 2) + "/" + tmp.Substring(0, 4);
            return result;
        }

        //Convert 19970214 to Datetime
        public static DateTime ConvertIntToDatetime(int time)
        {
            string tmp = time.ToString();
            return DateTime.ParseExact(tmp, Constant.DATE_FORMAT, CultureInfo.CurrentCulture);
        }

        public static string ConvertDatetimeToString(DateTime dt)
        {
            return dt.ToString(Constant.DATE_FORMAT);
        }

        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public static double GetGeoLocation()
        {
            return 0.0;
        }

        public static string SendSMS(string phoneNo, string content)
        {
            SpeedSMSAPI api = new SpeedSMSAPI(Constant.SPEEDSMS_TOKEN);
            /**
            * Để lấy thông tin về tài khoản như: email, số dư tài khoản bạn sử dụng hàm getUserInfo()
            */
            String userInfo = api.getUserInfo();
            /* * Hàm getUserInfo() sẽ trả về một json string như sau:
             * /
            {"email": "test@speedsms.vn", "balance": 100000.0, "currency": "VND"}

            /** Để gửi SMS bạn sử dụng hàm sendSMS như sau:
            */
            String[] phones = new String[] { phoneNo };
            int type = 2;
            /**
            sms_type có các giá trị như sau:
            2: tin nhắn gửi bằng đầu số ngẫu nhiên
            3: tin nhắn gửi bằng brandname
            4: tin nhắn gửi bằng brandname mặc định (Verify hoặc Notify)
            5: tin nhắn gửi bằng app android
            */
            String sender = "Receivable management system";
            /**
            brandname là tên thương hiệu hoặc số điện thoại đã đăng ký với SpeedSMS hoặc android deviceId của bạn
            */

            String response = api.sendSMS(phones, content, type, sender);
            return response;
        }

        public static async Task<string> MakePhoneCallAsync(string phoneNo, string content)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("cache-control", "no-cache");
            client.DefaultRequestHeaders.Add("X-STRINGEE-AUTH", Constant.STRINGEE_TOKEN);
            string data = "{\"from\":{\"type\":\"external\",\"number\":\"842471008859\",\"alias\":\"STRINGEE_NUMBER\"},\"to\":[{\"type\":\"external\",\"number\":\"" + phoneNo + "\",\"alias\":\"Thong\"}],\"answer_url\":\"https://example.com/answerurl\",\"actions\":[{\"action\":\"talk\",\"voice\":\"hatieumai\",\"text\":\"" + content + "\",\"speed\":-3,\"silenceTime\":1000}]}";
            var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            var stringTask = await client.PostAsync("https://api.stringee.com/v1/call2/callout", stringContent);
            var msg = stringTask.Content.ReadAsStringAsync().Result;
            return msg;
        }

    }
}
