using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace RCM.SpeedSMS
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class DeliverySms
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("tranId")]
        public string TranId { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class DeliverySms
    {
        public static DeliverySms FromJson(string json) => JsonConvert.DeserializeObject<DeliverySms>(json, RCM.SpeedSMS.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this DeliverySms self) => JsonConvert.SerializeObject(self, RCM.SpeedSMS.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
    public static class SmsErrorCode
    {
        public const string IP_LOCKED_CODE = "007";
        public const string IP_LOCKED = "IP locked! Please report to manager";

        public const string ACCOUNT_LOCKED_CODE = "008";
        public const string ACCOUNT_LOCKED = "SMS account is blocked! Please report to manager";

        public const string ACCOUNT_NOT_ALLOW_CODE = "009";
        public const string ACCOUNT_NOT_ALLOW = "SMS Account not allow to using service! Please report to manager";

        public const string INVALID_PHONE_CODE = "105";
        public const string INVALID_PHONE = "Phone number invalid! Please report to manager";

        public const string CONTENT_NOT_SUPPORT_CODE = "110";
        public const string CONTENT_NOT_SUPPORT = "Not support sms content! Please report to manager";

        public const string CONTENT_TOO_LONG_CODE = "113";
        public const string CONTENT_TOO_LONG = "Sms content too long! Please report to manager";

        public const string ACCOUNT_NOT_ENOUGH_BALANCE_CODE = "300";
        public const string ACCOUNT_NOT_ENOUGH_BALANCE = "Your account balance not enough to send sms! Please report to manager";

        public const string PROVIDER_ERROR_CODE = "500";
        public const string PROVIDER_ERROR = "Service not available please contact provider";
    }
}

