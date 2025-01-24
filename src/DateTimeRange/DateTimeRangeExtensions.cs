using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// Provides extension methods for working with <see cref="DateTimeRange"/> instances.
    /// </summary>
    public static class DateTimeRangeExtensions
    {
        /// <summary>
        /// Returns an intersection of two <see cref="DateTimeRange"/>s.
        /// The new range starts with the earliest start date and ends with the latest end date of the provided ranges.
        /// </summary>
        /// <param name="first">The first <see cref="DateTimeRange"/> to intersect.</param>
        /// <param name="second">The second <see cref="DateTimeRange"/> to intersect.</param>
        /// <returns>
        /// A new <see cref="DateTimeRange"/> representing the intersection of the two ranges.
        /// </returns>
        public static DateTimeRange Intersect(this DateTimeRange first, DateTimeRange second) =>
            new DateTimeRange(
                first.Start <= second.Start ? first.Start : second.Start,
                first.End >= second.End ? first.End : second.End
            );

        /// <summary>
        /// Checks if a <see cref="DateTime"/> is inside the specified range.
        /// </summary>
        /// <param name="dateTimeRange">The <see cref="DateTimeRange"/> to check against.</param>
        /// <param name="dateTime">The <see cref="DateTime"/> to check.</param>
        /// <param name="excludeStart">
        /// Indicates whether the start of the range should be excluded from the comparison.
        /// </param>
        /// <param name="excludeEnd">
        /// Indicates whether the end of the range should be excluded from the comparison.
        /// </param>
        /// <returns>
        /// <c>true</c> if the <paramref name="dateTime"/> is within the range based on the inclusion/exclusion rules; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(
            this DateTimeRange dateTimeRange,
            DateTime dateTime,
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

        /// <summary>
        /// Enumerates the <see cref="DateTimeRange"/> by returning all <see cref="DateTime"/> values within the range in steps of the specified <see cref="DateSpan"/>.
        /// </summary>
        /// <param name="dateTimeRange">The range to be enumerated.</param>
        /// <param name="dateSpan">The unit of time to step by (e.g., days, weeks, months).</param>
        /// <param name="excludeStart">
        /// Indicates whether the start of the range should be excluded from the enumeration.
        /// </param>
        /// <param name="excludeEnd">
        /// Indicates whether the end of the range should be excluded from the enumeration.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{DateTime}"/> containing all <see cref="DateTime"/> values within the range.
        /// </returns>
        /// <exception cref="Exception">Thrown if the <paramref name="dateSpan"/> is invalid.</exception>
        public static IEnumerable<DateTime> Enumerate(
            this DateTimeRange dateTimeRange,
            DateSpan dateSpan,
            bool excludeStart = false,
            bool excludeEnd = false
        )
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
                _ => throw new Exception("DateSpan invalid."),
            };

            switch (dateSpan)
            {
                case DateSpan.Month:
                    DateTime resultMonth = dateTimeRange.Start;
                    if (excludeStart)
                        resultMonth = resultMonth.AddMonths(1);
                    do
                    {
                        yield return resultMonth;
                        resultMonth = resultMonth.AddMonths(1);
                    } while (
                        excludeEnd
                            ? Convert.ToInt32($"{resultMonth.Year}{GetTwoDigitMonth(resultMonth)}")
                                < Convert.ToInt32(
                                    $"{dateTimeRange.End.Year}{GetTwoDigitMonth(dateTimeRange.End)}"
                                )
                            : Convert.ToInt32($"{resultMonth.Year}{GetTwoDigitMonth(resultMonth)}")
                                <= Convert.ToInt32(
                                    $"{dateTimeRange.End.Year}{GetTwoDigitMonth(dateTimeRange.End)}"
                                )
                    );
                    yield break;
                case DateSpan.Year:
                    DateTime resultYear = dateTimeRange.Start;
                    if (excludeStart)
                        resultYear = resultYear.AddYears(1);
                    do
                    {
                        yield return resultYear;
                        resultYear = resultYear.AddYears(1);
                    } while (
                        excludeEnd
                            ? resultYear.Year < dateTimeRange.End.Year
                            : resultYear.Year <= dateTimeRange.End.Year
                    );
                    yield break;
            }

            foreach (var item in Enumerate(dateTimeRange, step))
                yield return item;
        }

        /// <summary>
        /// Enumerates the <see cref="DateTimeRange"/> by returning all <see cref="DateTime"/> values within the range in steps of the specified <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="dateTimeRange">The range to be enumerated.</param>
        /// <param name="step">The step size as a <see cref="TimeSpan"/>.</param>
        /// <param name="excludeStart">
        /// Indicates whether the start of the range should be excluded from the enumeration.
        /// </param>
        /// <param name="excludeEnd">
        /// Indicates whether the end of the range should be excluded from the enumeration.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{DateTime}"/> containing all <see cref="DateTime"/> values within the range.
        /// </returns>
        public static IEnumerable<DateTime> Enumerate(
            this DateTimeRange dateTimeRange,
            TimeSpan step,
            bool excludeStart = false,
            bool excludeEnd = false
        )
        {
            DateTime result = dateTimeRange.Start;
            if (excludeStart)
                result = result.Add(step);
            do
            {
                yield return result;
                result += step;
            } while (excludeEnd ? result < dateTimeRange.End : result <= dateTimeRange.End);
        }

        /// <summary>
        /// Returns the two-digit string representation of a month from a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to extract the month from.</param>
        /// <returns>A two-digit string representation of the month (e.g., "01" for January).</returns>
        private static string GetTwoDigitMonth(DateTime date)
        {
            var month = date.Month;
            return month < 10 ? $"0{month}" : month.ToString();
        }
    }
}
