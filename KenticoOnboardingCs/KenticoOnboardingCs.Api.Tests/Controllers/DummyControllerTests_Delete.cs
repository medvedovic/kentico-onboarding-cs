using KenticoOnboardingCs.Api.Controllers;
using KenticoOnboardingCs.Api.Models.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace KenticoOnboardingCs.Api.Tests.Controllers
{
    [TestFixture]
    class DummyControllerTests_Delete
    {
        private TodosController dummyController;
        private ITodoRepository repository;

        [SetUp]
        public void SetUp()
        {
            repository = new TodoRepository();
            dummyController = new TodosController(repository);
        }

        [Test]
        public void DeleteDummyItem_ReturnsNotFound_WithInvalidId()
        {
            var response = dummyController.DeleteTodo(50);
            
            Assert.IsInstanceOf<NotFoundResult>(response);
        }

        [Test]
        public void DeleteDummyItem_ReturnsNoContent_WithValidId()
        {
            var response = dummyController.DeleteTodo(1);

            Assert.IsInstanceOf<StatusCodeResult>(response);
            Assert.AreEqual(HttpStatusCode.NoContent, (response as StatusCodeResult).StatusCode);
        }
    }
}
