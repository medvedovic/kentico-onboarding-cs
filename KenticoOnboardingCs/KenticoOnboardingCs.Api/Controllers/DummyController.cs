using KenticoOnboardingCs.Api.Models;
using KenticoOnboardingCs.Api.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
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

        [HttpDelete]
        public IHttpActionResult DeleteTodo(int id)
        {
            var isRemoved = _repository.Remove(id);

            if (!isRemoved)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        public IHttpActionResult PutTodo(int id, [FromBody] Todo updated)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!_repository.Update(id, updated))
                    return NotFound();

                return Ok(updated);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
