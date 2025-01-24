namespace System
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Checks if the specified <see cref="DateTime"/> is within the boundaries of the provided <see cref="DateTimeRange"/>.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> value to check.</param>
        /// <param name="dateTimeRange">The <see cref="DateTimeRange"/> representing the start and end boundaries.</param>
        /// <param name="excludeStart">
        /// Indicates whether the start of the range should be excluded from the comparison.
        /// If <c>true</c>, the start of the range is excluded; otherwise, it is included.
        /// </param>
        /// <param name="excludeEnd">
        /// Indicates whether the end of the range should be excluded from the comparison.
        /// If <c>true</c>, the end of the range is excluded; otherwise, it is included.
        /// </param>
        /// <returns>
        /// <c>true</c> if the <paramref name="dateTime"/> is within the specified range based on the inclusion/exclusion rules;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// The method uses the specified values of <paramref name="excludeStart"/> and <paramref name="excludeEnd"/> to determine
        /// whether the boundaries of the range are inclusive or exclusive:
        /// <list type="bullet">
        /// <item><description><c>(true, true)</c>: Excludes both the start and end of the range.</description></item>
        /// <item><description><c>(true, false)</c>: Excludes the start and includes the end of the range.</description></item>
        /// <item><description><c>(false, true)</c>: Includes the start and excludes the end of the range.</description></item>
        /// <item><description><c>(false, false)</c>: Includes both the start and end of the range.</description></item>
        /// </list>
        /// </remarks>
        public static bool InRange(
            this DateTime dateTime,
            DateTimeRange dateTimeRange,
            bool excludeStart = false,
            bool excludeEnd = false
        )
        {
            return (excludeStart, excludeEnd) switch
            {
                (true, true) => dateTime > dateTimeRange.Start && dateTime < dateTimeRange.End,
                (true, false) => dateTime > dateTimeRange.Start && dateTime <= dateTimeRange.End,
                (false, true) => dateTime >= dateTimeRange.Start && dateTime < dateTimeRange.End,
                (false, false) => dateTime >= dateTimeRange.Start && dateTime <= dateTimeRange.End,
            };
        }
    }
}
