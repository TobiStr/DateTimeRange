namespace System
{
    /// <summary>
    /// Represents a range between two <see cref="DateTime"/> values, with utility constructors and validation for consistency.
    /// </summary>
    public struct DateTimeRange : IEquatable<DateTimeRange>
    {
        /// <summary>
        /// Gets the <see cref="DateTime"/> value that marks the start of the range.
        /// </summary>
        public DateTime Start { get; }

        /// <summary>
        /// Gets the <see cref="DateTime"/> value that marks the end of the range.
        /// </summary>
        public DateTime End { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeRange"/> struct with a specified start and end date.
        /// </summary>
        /// <param name="startDate">The <see cref="DateTime"/> value that marks the start of the range.</param>
        /// <param name="endDate">The <see cref="DateTime"/> value that marks the end of the range.</param>
        /// <exception cref="DateTimeInvalidRangeException">
        /// Thrown if <paramref name="startDate"/> is later than <paramref name="endDate"/>.
        /// </exception>
        public DateTimeRange(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new DateTimeInvalidRangeException("Start date is later than end date");
            Start = startDate;
            End = endDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeRange"/> struct with a specified start date and duration.
        /// </summary>
        /// <param name="startDate">The <see cref="DateTime"/> value that marks the start of the range.</param>
        /// <param name="duration">The duration of the range as a <see cref="TimeSpan"/>.</param>
        /// <exception cref="DateTimeInvalidRangeException">
        /// Thrown if <paramref name="duration"/> is negative.
        /// </exception>
        public DateTimeRange(DateTime startDate, TimeSpan duration)
        {
            if (duration.TotalMilliseconds < 0)
                throw new DateTimeInvalidRangeException("Duration is negative");
            Start = startDate;
            End = startDate.Add(duration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeRange"/> struct with a specified end date and duration.
        /// </summary>
        /// <param name="duration">The duration of the range as a <see cref="TimeSpan"/>.</param>
        /// <param name="endDate">The <see cref="DateTime"/> value that marks the end of the range.</param>
        /// <exception cref="DateTimeInvalidRangeException">
        /// Thrown if <paramref name="duration"/> is negative.
        /// </exception>
        public DateTimeRange(TimeSpan duration, DateTime endDate)
        {
            if (duration.TotalMilliseconds < 0)
                throw new DateTimeInvalidRangeException("Duration is negative");
            Start = endDate.Add(-duration);
            End = endDate;
        }

        /// <summary>
        /// Determines whether the current <see cref="DateTimeRange"/> is equal to another <see cref="DateTimeRange"/>.
        /// </summary>
        /// <param name="other">The other <see cref="DateTimeRange"/> to compare with the current instance.</param>
        /// <returns>
        /// <c>true</c> if both the <see cref="Start"/> and <see cref="End"/> values are equal; otherwise, <c>false</c>.
        /// </returns>
        public readonly bool Equals(DateTimeRange other)
        {
            return this.Start == other.Start && this.End == other.End;
        }
    }
}
