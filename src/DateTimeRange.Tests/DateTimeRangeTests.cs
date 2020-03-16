using NUnit.Framework;
using System;

namespace DateTimeRangeTests
{
    public class DateTimeRangeTests
    {

        [Test]
        public void Constructor_Test_Success()
        {
            Assert.DoesNotThrow(() =>
            {
                var a = new DateTimeRange();
                var b = new DateTimeRange(DateTime.Now, DateTime.Now.AddMonths(3));
                var c = new DateTimeRange(DateTime.Now, new TimeSpan(100, 0, 0, 0));
                var d = new DateTimeRange(new TimeSpan(100, 0, 0, 0), DateTime.Now);
            }, "Constructor call failed");
        }

        [Test]
        public void Equals_Test_Success()
        {
            Assert.IsTrue(new DateTimeRange().Equals(new DateTimeRange()));
            Assert.IsFalse(new DateTimeRange().Equals(new DateTimeRange(DateTime.Now, DateTime.Now.AddMonths(3))));
        }
    }
}