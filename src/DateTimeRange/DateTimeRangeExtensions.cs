using System.Collections.Generic;

namespace System
{
    public static class DateTimeRangeExtensions
    {
        public static DateTimeRange Intersect(this DateTimeRange first, DateTimeRange second) =>
         new DateTimeRange(first.Start <= second.Start ? first.Start : second.Start, first.End >= second.End ? first.End : second.End);

        public static bool Contains(this DateTimeRange dateTimeRange, DateTime dateTime) =>
            dateTime >= dateTimeRange.Start && dateTime <= dateTimeRange.End;

        public static IEnumerable<DateTime> GetDates(this DateTimeRange dateTimeRange, DateSpan dateSpan)
        {
            TimeSpan step;
            switch (dateSpan)
            {
                case DateSpan.Millisecond:
                    step = new TimeSpan(0, 0, 0, 0, 1);
                    break;
                case DateSpan.Second:
                    step = new TimeSpan(0, 0, 1);
                    break;
                case DateSpan.Minute:
                    step = new TimeSpan(0, 1, 0);
                    break;
                case DateSpan.Hour:
                    step = new TimeSpan(1, 0, 0);
                    break;
                case DateSpan.Day:
                    step = new TimeSpan(1, 0, 0, 0);
                    break;
                case DateSpan.Week:
                    step = new TimeSpan(7, 0, 0, 0);
                    break;
                case DateSpan.Month:
                    DateTime resultMonth = dateTimeRange.Start;
                    do
                    {
                        yield return resultMonth;
                        resultMonth = resultMonth.AddMonths(1);
                    } while (resultMonth <= dateTimeRange.End);
                    yield break;
                case DateSpan.Year:
                    DateTime resultYear = dateTimeRange.Start;
                    do
                    {
                        yield return resultYear;
                        resultYear = resultYear.AddYears(1);
                    } while (resultYear <= dateTimeRange.End);
                    yield break;
                default:
                    yield break;
            }
            foreach (var item in GetDates(dateTimeRange, step)) yield return item;
        }
        
        public static IEnumerable<DateTime> GetDates(this DateTimeRange dateTimeRange, TimeSpan step)
        {
            DateTime result = dateTimeRange.Start;
            do
            {
                yield return result;
                result += step;
            } while (result <= dateTimeRange.End);
        }
    }
}
