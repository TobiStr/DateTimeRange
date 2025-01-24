namespace System
{
    /// <summary>
    /// Represents different units of time for measuring or manipulating date and time spans.
    /// </summary>
    public enum DateSpan
    {
        /// <summary>
        /// Represents a single millisecond, which is one-thousandth of a second.
        /// </summary>
        Millisecond,

        /// <summary>
        /// Represents a single second, which consists of 1,000 milliseconds.
        /// </summary>
        Second,

        /// <summary>
        /// Represents a single minute, which consists of 60 seconds.
        /// </summary>
        Minute,

        /// <summary>
        /// Represents a single hour, which consists of 60 minutes.
        /// </summary>
        Hour,

        /// <summary>
        /// Represents a single day, which typically consists of 24 hours.
        /// </summary>
        Day,

        /// <summary>
        /// Represents a single week, which consists of 7 days.
        /// </summary>
        Week,

        /// <summary>
        /// Represents a single month. The number of days in a month can vary depending on the specific month and year.
        /// </summary>
        Month,

        /// <summary>
        /// Represents a single year, which typically consists of 12 months.
        /// Leap years may have an additional day.
        /// </summary>
        Year,
    }
}
