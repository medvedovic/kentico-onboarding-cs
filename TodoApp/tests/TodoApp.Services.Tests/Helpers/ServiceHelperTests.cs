using NUnit.Framework;
using TodoApp.Contracts.Helpers;
using TodoApp.Services.Helpers;

namespace TodoApp.Services.Tests.Helpers
{
    [TestFixture]
    class ServiceHelperTests
    {
        private IServiceHelper _serviceHelper;

        [SetUp]
        public void Init()
        {
            _serviceHelper = new ServiceHelper();
        }

        [Test]
        public void ReturnsDifferentGuids()
        {
            var guid1 = _serviceHelper.GenerateGuid();
            var guid2 = _serviceHelper.GenerateGuid();

            Assert.That(guid1, Is.Not.EqualTo(guid2));
        }

        [Test]
        public void ReturnsDifferentDateTimes()
        {
            var time1 = _serviceHelper.GetCurrentDateTime();
            var time2 = _serviceHelper.GetCurrentDateTime();

            Assert.That(time1, Is.Not.EqualTo(time2));
        }
    }
}
