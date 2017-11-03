using System;
using System.Threading;
using NUnit.Framework;
using TodoApp.Contracts.Wrappers;
using TodoApp.Services.Wrappers;

namespace TodoApp.Services.Tests.Wrappers
{
    [TestFixture]
    internal class DateTimeProviderTests
    {
        private IDateTimeProvider _dateTimeProvider;

        [SetUp]
        public void Init()
        {
            _dateTimeProvider = new DateTimeProvider();
        }

        [Test]
        public void ReturnsDifferentDateTimes_OnMultipleCalls()
        {
            var time1 = _dateTimeProvider.GetCurrentDateTime();
            Thread.Sleep(1000);
            var time2 = _dateTimeProvider.GetCurrentDateTime();

            Assert.That(time1, Is.Not.EqualTo(time2));
        }

        [Test]
        public void ReturnsApproximatelyCurrentDateTime()
        {
            var now = DateTime.Now;

            var time1 = _dateTimeProvider.GetCurrentDateTime();

            Assert.That(time1, Is.InRange(now.AddSeconds(-1), now.AddSeconds(1)));
        }
    }
}
