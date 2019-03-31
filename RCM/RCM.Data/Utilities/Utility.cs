using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RCM.Data.Utilities
{
    public class CurrencyUtility
    {
        public static readonly double VND_DOLLAR_RATE = 23000;
        public static double ToDollar(double amount)
        {
            return amount / VND_DOLLAR_RATE;
        }
    }
    public class DateTimeUtility
    {
        public static DateTime ConvertIntToDatetime(int time)
        {
            string tmp = time.ToString();
            return DateTime.ParseExact(tmp, "yyyyMMdd", CultureInfo.CurrentCulture);
        }
    }
    
}
