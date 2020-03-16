using System.Collections.Generic;

namespace System
{
    public static class DateTimeRangeExtensions
    {
        /// <summary>
        /// Returns an intersection of two <see cref="DateTimeRange"/>s. The new range starts with the earliest startDate and ends with the latest endDate of the provided ranges.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTimeRange Intersect(this DateTimeRange first, DateTimeRange second) =>
         new DateTimeRange(first.Start <= second.Start ? first.Start : second.Start, first.End >= second.End ? first.End : second.End);

        /// <summary>
        /// Checks if a <see cref="DateTime"/> is inside of the range.
        /// </summary>
        /// <param name="dateTimeRange"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool Contains(this DateTimeRange dateTimeRange, DateTime dateTime) =>
            dateTime >= dateTimeRange.Start && dateTime <= dateTimeRange.End;

        /// <summary>
        /// Enumerates the <see cref="DateTimeRange"/>. Returns all <see cref="DateTime"/>s in steps of <paramref name="dateSpan"/>.
        /// </summary>
        /// <param name="dateTimeRange">Range to be enumerated</param>
        /// <param name="dateSpan">Step between emitted values.</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> Enumerate(this DateTimeRange dateTimeRange, DateSpan dateSpan)
        {
            TimeSpan step = dateSpan switch
            {
                DateSpan.Millisecond => new TimeSpan(0, 0, 0, 0, 1),
                DateSpan.Second => new TimeSpan(0, 0, 1),
                DateSpan.Minute => new TimeSpan(0, 1, 0),
                DateSpan.Hour => new TimeSpan(1, 0, 0),
                DateSpan.Day => new TimeSpan(1, 0, 0, 0),
                DateSpan.Week => new TimeSpan(7, 0, 0, 0),
                DateSpan.Month => new TimeSpan(),
                DateSpan.Year => new TimeSpan(),
                _ => throw new Exception("DateSpan invalid.")
            };

            switch (dateSpan)
            {
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
            }

            foreach (var item in Enumerate(dateTimeRange, step)) yield return item;
        }

        /// <summary>
        /// Enumerates the <see cref="DateTimeRange"/>. Returns all <see cref="DateTime"/>s in steps of <paramref name="step"/>.
        /// </summary>
        /// <param name="dateTimeRange">Range to be enumerated</param>
        /// <param name="step">Step between emitted values.</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> Enumerate(this DateTimeRange dateTimeRange, TimeSpan step)
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
