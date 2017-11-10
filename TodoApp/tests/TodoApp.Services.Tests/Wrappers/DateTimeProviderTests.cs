using System;
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
        public void GetCurrentDateTime_ReturnsMultipleDateTimes_InCorrectOrder()
        {
            var time1 = _dateTimeProvider.GetCurrentDateTime();
            var time2 = _dateTimeProvider.GetCurrentDateTime();

            Assert.That(time1, Is.LessThan(time2));
        }
    }
}
