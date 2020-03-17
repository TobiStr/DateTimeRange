namespace System
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Checks if the <see cref="DateTime"/> is inside of the <see cref="DateTimeRange"/>.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dateTimeRange"></param>
        /// <returns></returns>
        public static bool InRange(this DateTime dateTime, DateTimeRange dateTimeRange, bool excludeStart = false, bool excludeEnd = false)
        {
            var ret = (excludeStart, excludeEnd) switch
            {
                (true, true) => dateTime > dateTimeRange.Start && dateTime < dateTimeRange.End,
                (true, false) => dateTime > dateTimeRange.Start && dateTime <= dateTimeRange.End,
                (false, true) => dateTime >= dateTimeRange.Start && dateTime < dateTimeRange.End,
                (false, false) => dateTime >= dateTimeRange.Start && dateTime <= dateTimeRange.End,
            };
            return ret;
        }
    }
}
