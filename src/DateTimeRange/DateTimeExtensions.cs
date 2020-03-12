using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class DateTimeExtensions
    {
        public static bool IsInRange(this DateTime dateTime, DateTimeRange dateTimeRange)
        => dateTime >= dateTimeRange.Start && dateTime <= dateTimeRange.End;

    }
}
