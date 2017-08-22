using KenticoOnboardingCs.Api.Models;
using KenticoOnboardingCs.Api.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace KenticoOnboardingCs.Api.Controllers
{
    public class DummyController : ApiController
    {
        private ITodoRepository _repository;

        public DummyController(ITodoRepository todoRepository)
        {
            _repository = todoRepository;
        }

        public DummyController()
        {
            _repository = new TodoRepository();
        }

        //Get api/dummy/
        [HttpGet]
        public IEnumerable<Todo> GetAllTodos()
        {
            return _repository.GetAll();
        }

        //Get api/dummy/{id}
        [HttpGet]
        public IHttpActionResult GetTodo(int id)
        {
            var todo = _repository.Get(id);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        //Post api/dummy/
        [HttpPost]
        public IHttpActionResult PostTodo(Todo todo)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newTodo =_repository.Add(todo);
                return Ok(newTodo);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
