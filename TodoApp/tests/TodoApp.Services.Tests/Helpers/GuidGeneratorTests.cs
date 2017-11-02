using NUnit.Framework;
using TodoApp.Contracts.Helpers;
using TodoApp.Services.Helpers;

namespace TodoApp.Services.Tests.Helpers
{
    [TestFixture]
    internal class GuidGeneratorTests
    {
        private IGuidGenerator _guidGenerator;

        [SetUp]
        public void Init()
        {
            _guidGenerator = new GuidGenerator();
        }

        [Test]
        public void ReturnsDifferentGuids()
        {
            var id1 = _guidGenerator.GenerateGuid();
            var id2 = _guidGenerator.GenerateGuid();

            Assert.That(id1, Is.Not.EqualTo(id2));
        }
    }
}
