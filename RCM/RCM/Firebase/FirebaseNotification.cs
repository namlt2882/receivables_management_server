using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RCM.Firebase
{
    public class FirebaseNotification
    {
        public string to { get; set; }
        public NotificationObject notification { get; set; }
        public Dictionary<string, string> data { get; set; }
    }
    public class NotificationObject
    {
        public string title { get; set; }
        public string body { get; set; }
    }
    public static class SendFirebaseNotification
    {
        public static async Task SendNotificationToMobileAsync(FirebaseNotification firebaseNotification)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("cache-control", "no-cache");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=AIzaSyBPVURwPYjlk3U3rPBrujsl9cIHN6Q53Zg");
            //string data = "{\n    \n    \"data\": {\n        \"title\": \"" + title + "\",\n        \"image\": \"https://firebase.google.com/images/social.png\",\n        \"message\": \"" + content + "\"\n    },\n    \"to\": \"" + token + "\"}";
            string data = JsonConvert.SerializeObject(firebaseNotification);
            var stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            var stringTask = await client.PostAsync("https://fcm.googleapis.com/fcm/send", stringContent);
            var msg = stringTask.Content.ReadAsStringAsync().Result;
            //JObject call = JObject.Parse(msg);
            //var status=  call.SelectToken($"success").ToString();
        }
    }

}
