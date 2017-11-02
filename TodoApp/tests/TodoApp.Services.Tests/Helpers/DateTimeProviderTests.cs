using NUnit.Framework;
using TodoApp.Contracts.Helpers;
using TodoApp.Services.Helpers;

namespace TodoApp.Services.Tests.Helpers
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
        public void ReturnsDifferentDateTimes()
        {
            var time1 = _dateTimeProvider.GetCurrentDateTime();
            var time2 = _dateTimeProvider.GetCurrentDateTime();

            Assert.That(time1, Is.Not.EqualTo(time2));
        }
    }
}
