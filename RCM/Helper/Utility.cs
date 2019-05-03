using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
        public static int ConvertDatimeToInt(DateTime dateTime)
        {
            return dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day;
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

        public static string ConvertDateTimeToStringForView(DateTime date)
        {
            return ConvertIntToDateStringForView(ConvertDatimeToInt(date));
        }

        //Ex Input: 700 to 07:00 or 1700 to 17:00
        public static TimeSpan ConvertIntToTimeSpan(int time)
        {
            string tmp = time.ToString();


            if (tmp.Length == 3)
            {
                tmp = "0" + tmp;
            }
            return new TimeSpan(int.Parse(tmp.Substring(0, 2)), int.Parse(tmp.Substring(2)), 0);
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
            SpeedSMSAPI api = new SpeedSMSAPI("H-TVWpn8KOmBc_mO9D1WOTKkV0IDWexj");

            String[] phones = new String[] { phoneNo };
            String str = content;
            String response = api.sendSMS(phones, str, 2, "");
            return response;
        }

        public static async Task<string> MakePhoneCallAsync(string phoneNo, string content)
        {
            string data = "{\"from\":{\"type\":\"external\",\"number\":\"84901707010\",\"alias\":\"STRINGEE_NUMBER\"},\"to\":[{\"type\":\"external\",\"number\":\"" + phoneNo + "\",\"alias\":\"Thong\"}],\"answer_url\":\"https://example.com/answerurl\",\"actions\":[{\"action\":\"talk\",\"voice\":\"hatieumai\",\"text\":\"" + content + "\",\"speed\":-3,\"silenceTime\":1000}]}";
            var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            var stringTask = await GetStringeeClient().PostAsync("https://api.stringee.com/v1/call2/callout", stringContent);
            var msg = stringTask.Content.ReadAsStringAsync().Result;
            return msg;
        }

        public static string GetStringeeCallValue(string stringeeMessage, string value)
        {
            JObject call = JObject.Parse(stringeeMessage);
            return call.SelectToken($"data.calls[0].{value}").ToString();
        }
        public static string GetStringeeCallId(string stringeeMessage, string value)
        {
            JObject call = JObject.Parse(stringeeMessage);
            return call.SelectToken($"data.calls[0].{value}").ToString();
        }
        private static string CheckNoOfCall(string stringeeMessage)
        {
            JObject call = JObject.Parse(stringeeMessage);
            return call.SelectToken($"data.totalCalls").ToString();
        }
        public static async Task<bool> CheckCall(string callId, string phoneNumber)
        {

            #region Stringee Log

            var stringTask = await GetStringeeClient().GetAsync("https://api.stringee.com/v1/call/log?id=" + callId);
            var stringeeMessage = stringTask.Content.ReadAsStringAsync().Result;
            //Check no of call >0 
            if (int.Parse(CheckNoOfCall(stringeeMessage)) == 0) return false;
            GetStringeeCallValue(stringeeMessage, "to_number");
            var number = GetStringeeCallValue(stringeeMessage, "to_number");
            //var startTime = GetStringeeValue(msg, "start_time");
            //var answerTime = GetStringeeValue(msg, "answer_time");
            if (phoneNumber.Equals(number)) return true;
            #endregion

            return false;
        }


        public static DateTime ConvertEpochTimeToDateTimeFromMilliseconds(double milliseconds)
        {
            // Format our new DateTime object to start at the UNIX Epoch
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);

            // Add the timestamp (number of seconds since the Epoch) to be converted
            return dateTime.AddMilliseconds(milliseconds);
        }


        public static HttpClient GetStringeeClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("cache-control", "no-cache");
            client.DefaultRequestHeaders.Add("X-STRINGEE-AUTH", Constant.STRINGEE_TOKEN);
            return client;
        }

        public static string NonUnicode(this string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                "đ",
                "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                "í","ì","ỉ","ĩ","ị",
                "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                "d",
                "e","e","e","e","e","e","e","e","e","e","e",
                "i","i","i","i","i",
                "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                "u","u","u","u","u","u","u","u","u","u","u",
                "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
    }
}
