namespace System
{
    public struct DateTimeRange
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public DateTimeRange(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate) throw new DateTimeInvalidRangeException("Start date is later than end date or equal.");
            Start = startDate;
            End = endDate;
        }

        public DateTimeRange(DateTime startDate, TimeSpan duration)
        {
            Start = startDate;
            End = startDate.Add(duration);
        }
        
        public DateTimeRange(TimeSpan duration, DateTime endDate)
        {
            Start = endDate.Add(-duration);
            End = endDate;
        }
    }
}
