using System;
using System.Globalization;

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

        //Ex Input: 20180131 to 31/01/2018.
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

        //Convert 20180131 to Datetime
        public static DateTime ConvertIntToDatetime(int time)
        {
            string tmp = time.ToString();
            return DateTime.ParseExact(tmp, "yyyyMMdd", CultureInfo.CurrentCulture);
        }

        public static string ConvertDatetimeToString(DateTime dt)
        {
            return dt.ToString("yyyyMMdd");
        }

    }
}
