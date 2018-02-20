using System;
using System.Collections.Generic;
using System.Text;

namespace D.Net
{
    public static class DateTimeHelper
    {
        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero) return dateTime; // Or could throw an ArgumentException
            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }
        public static DateTime Truncate(this DateTime dateTime, long TruncationBasedTicks = TimeSpan.TicksPerSecond)
        {
            return dateTime.AddTicks(-(dateTime.Ticks % TruncationBasedTicks));
        }
        public static string FileStamp(this DateTime dt, string fmt = "yyyyMMddHHmmss")
        {

            return dt.ToString(fmt);
        }
    }
}
