namespace System
{
    public struct DateTimeRange : IEquatable<DateTimeRange>
    {
        /// <summary>
        /// The <see cref="DateTime"/>, the range starts with
        /// </summary>
        public DateTime Start { get; }

        /// <summary>
        /// The <see cref="DateTime"/>, the range ends with
        /// </summary>
        public DateTime End { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="DateTimeRange"/>
        /// </summary>
        /// <param name="startDate"><see cref="DateTime"/> the range starts with</param>
        /// <param name="endDate"><see cref="DateTime"/> the range ends with</param>
        public DateTimeRange(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate) throw new DateTimeInvalidRangeException("Start date is later than end date");
            Start = startDate;
            End = endDate;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DateTimeRange"/>
        /// </summary>
        /// <param name="startDate"><see cref="DateTime"/> the range starts with</param>
        /// <param name="duration">Duration from StartDate to EndDate</param>
        public DateTimeRange(DateTime startDate, TimeSpan duration)
        {
            if (duration.TotalMilliseconds < 0) throw new DateTimeInvalidRangeException("Duration is negative");
            Start = startDate;
            End = startDate.Add(duration);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DateTimeRange"/>
        /// </summary>
        /// <param name="duration">Duration from StartDate to EndDate</param>
        /// <param name="endDate"><see cref="DateTime"/> the range ends with</param>
        public DateTimeRange(TimeSpan duration, DateTime endDate)
        {
            if (duration.TotalMilliseconds < 0) throw new DateTimeInvalidRangeException("Duration is negative");
            Start = endDate.Add(-duration);
            End = endDate;
        }

        public bool Equals(DateTimeRange other)
        {
            return this.Start == other.Start && this.End == other.End;
        }
    }
}
