using NUnit.Framework;
using System;
using System.Linq;

namespace DateTimeRangeTests
{
    [TestFixture]
    public class DateTimeRangeExtensionsTests
    {
        [Test]
        public void Intersect_Test()
        {
            // Arrange
            var a = DateTime.Now;
            var b = a.AddDays(-1);
            var c = a.AddDays(1);
            
            DateTimeRange first = new DateTimeRange(b, a);
            DateTimeRange second = new DateTimeRange(a, c);

            // Act
            var result = first.Intersect(second);

            // Assert
            Assert.That(result.Start == b && result.End == c);
        }

        [Test]
        public void Contains_Test()
        {
            // Arrange
            DateTimeRange dateTimeRange = new DateTimeRange(DateTime.Now, DateTime.Now.AddDays(2));
            DateTime dateTime = DateTime.Now.AddDays(1);

            // Act
            var result = dateTimeRange.Contains(dateTime: dateTime, excludeStart: true, excludeEnd: true);

            // Assert
            Assert.That(result);
        }

        [Test]
        public void Enumerate_Test()
        {
            // Arrange
            DateTimeRange dateTimeRange = new DateTimeRange(DateTime.Now.Date, DateTime.Now.Date.AddDays(2));
            DateSpan dateSpan = DateSpan.Day;

            // Act
            var result = dateTimeRange.Enumerate(dateSpan, excludeStart: true, excludeEnd: true).ToArray();

            // Assert
            Assert.That(result.Length == 3);
            Assert.That(result.SequenceEqual(new DateTime[] { DateTime.Now.Date, DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2) }));
        }

        [Test]
        public void Enumerate_Test2()
        {
            // Arrange
            DateTimeRange dateTimeRange = new DateTimeRange(DateTime.Now.Date, DateTime.Now.Date.AddDays(2));
            TimeSpan step = new TimeSpan(1,0,0,0);

            // Act
            var result = dateTimeRange.Enumerate(step).ToArray();

            // Assert
            Assert.That(result.Length == 3);
            Assert.That(result.SequenceEqual(new DateTime[] { DateTime.Now.Date, DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2) }));
        }
    }
}
