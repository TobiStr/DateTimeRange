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
        public static bool Contains(this DateTimeRange dateTimeRange, DateTime dateTime, bool excludeStart = false, bool excludeEnd = false) {
            var ret = (excludeStart, excludeEnd) switch
            {
                (true, true) => dateTime > dateTimeRange.Start && dateTime < dateTimeRange.End,
                (true, false) => dateTime > dateTimeRange.Start && dateTime <= dateTimeRange.End,
                (false, true) => dateTime >= dateTimeRange.Start && dateTime < dateTimeRange.End,
                (false, false) => dateTime >= dateTimeRange.Start && dateTime <= dateTimeRange.End,
            };
            return ret;
        }

        /// <summary>
        /// Enumerates the <see cref="DateTimeRange"/>. Returns all <see cref="DateTime"/>s in steps of <paramref name="dateSpan"/>.
        /// </summary>
        /// <param name="dateTimeRange">Range to be enumerated</param>
        /// <param name="dateSpan">Step between emitted values.</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> Enumerate(this DateTimeRange dateTimeRange, DateSpan dateSpan, bool excludeStart = false, bool excludeEnd = false) {
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

            switch (dateSpan) {
                case DateSpan.Month:
                    DateTime resultMonth = dateTimeRange.Start;
                    if (excludeStart)
                        resultMonth = resultMonth.AddMonths(1);
                    do {
                        yield return resultMonth;
                        resultMonth = resultMonth.AddMonths(1);
                    } while (excludeEnd
                        ? Convert.ToInt32($"{resultMonth.Year}{ resultMonth.Month}") < Convert.ToInt32($"{dateTimeRange.End.Year}{dateTimeRange.End.Month}")
                        : Convert.ToInt32($"{resultMonth.Year}{ resultMonth.Month}") <= Convert.ToInt32($"{dateTimeRange.End.Year}{dateTimeRange.End.Month}")
                    );
                    yield break;
                case DateSpan.Year:
                    DateTime resultYear = dateTimeRange.Start;
                    if (excludeStart)
                        resultYear = resultYear.AddYears(1);
                    do {
                        yield return resultYear;
                        resultYear = resultYear.AddYears(1);
                    } while (excludeEnd ? resultYear.Year < dateTimeRange.End.Year : resultYear.Year <= dateTimeRange.End.Year);
                    yield break;
            }

            foreach (var item in Enumerate(dateTimeRange, step))
                yield return item;
        }

        /// <summary>
        /// Enumerates the <see cref="DateTimeRange"/>. Returns all <see cref="DateTime"/>s in steps of <paramref name="step"/>.
        /// </summary>
        /// <param name="dateTimeRange">Range to be enumerated</param>
        /// <param name="step">Step between emitted values.</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> Enumerate(this DateTimeRange dateTimeRange, TimeSpan step, bool excludeStart = false, bool excludeEnd = false) {
            DateTime result = dateTimeRange.Start;
            if (excludeStart)
                result = result.Add(step);
            do {
                yield return result;
                result += step;
            } while (excludeEnd ? result < dateTimeRange.End : result <= dateTimeRange.End);
        }
    }
}