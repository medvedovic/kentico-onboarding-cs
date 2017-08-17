using KenticoOnboardingCs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KenticoOnboardingCs.Api.Controllers
{
    public class DummyController : ApiController
    {
        private IEnumerable<Todo> todos;

        public DummyController()
        {
            todos = new List<Todo>()
            {
                new Todo() { Id = 1, Name = "Make coffee", Done = true },
                new Todo() { Id = 2, Name = "Master ASP.NET web api", Done = false }
            };
        }

        public DummyController(IEnumerable<Todo> todos)
        {
            if(todos != null)
            {
                this.todos = todos;
            }
        }


        public IEnumerable<Todo> GetAllTodos()
        {
            return todos;
        }

        [HttpGet]
        public IHttpActionResult GetTodo(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }
    }
}
