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
        public static bool InRange(this DateTime dateTime, DateTimeRange dateTimeRange)
        => dateTime >= dateTimeRange.Start && dateTime <= dateTimeRange.End;

    }
}
