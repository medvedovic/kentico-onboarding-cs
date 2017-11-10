using NUnit.Framework;
using TodoApp.Contracts.Wrappers;
using TodoApp.Services.Wrappers;

namespace TodoApp.Services.Tests.Wrappers
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
        public void GenerateGuid_ReturnsDifferentGuids_OnMultipleCalls()
        {
            var id1 = _guidGenerator.GenerateGuid();
            var id2 = _guidGenerator.GenerateGuid();

            Assert.That(id1, Is.Not.EqualTo(id2));
        }
    }
}
