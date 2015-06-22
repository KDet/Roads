using System;

namespace LvivRoads.Core.Extensions
{
    public static class DateExtension
    {
        //Функция конвертирования Unix Timestamp в DateTime
        public static DateTime FromUnixTimestamp(this double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        //Функция обратного конвертирования DateTime в Unix Timestamp
        public static double ToUnixTimestamp(this DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}