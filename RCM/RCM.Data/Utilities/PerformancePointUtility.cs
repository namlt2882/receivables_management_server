using System;
using System.Collections.Generic;
using System.Text;

namespace RCM.Data.Utilities
{
    public class PerformancePointUtility
    {
        public static double GetWeight(double amount)
        {
            double transferedAmount = CurrencyUtility.ToDollar(amount);
            if (transferedAmount < 1000)
            {
                return 1;
            }
            if (transferedAmount < 3000)
            {
                return 1.4;
            }
            if (transferedAmount < 5000)
            {
                return 1.7;
            }
            return 2;
        }

        public static int GetExpectedTime(double amount)
        {
            double transferedAmount = CurrencyUtility.ToDollar(amount);
            if (transferedAmount < 1000)
            {
                return 30;
            }
            if (transferedAmount < 3000)
            {
                return 35;
            }
            if (transferedAmount < 5000)
            {
                return 40;
            }
            return 50;
        }
        public static int GetReceivableTimeRate(int payableDay, int closedDay, int processTotalDay)
        {
            double rs = 0;
            DateTime PaybleDay = DateTimeUtility.ConvertIntToDatetime(payableDay);
            DateTime ClosedDay = DateTimeUtility.ConvertIntToDatetime(closedDay);
            double totalDay = (ClosedDay - PaybleDay).TotalDays;
            rs = Math.Ceiling(totalDay / processTotalDay);
            if (rs <= 0)
            {
                rs = 1;
            }
            return Convert.ToInt32(rs);
        }
    }
}
