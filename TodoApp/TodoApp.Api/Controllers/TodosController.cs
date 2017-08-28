using TodoApp.Api.Models;
using TodoApp.Api.Models.Repositories;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace TodoApp.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Todos/{id:int?}", Name = "Todos")]
    public class TodosController : ApiController
    {
        private ITodoRepository _repository;

        public TodosController(ITodoRepository todoRepository)
        {
            _repository = todoRepository;
        }

        public TodosController()
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

                return CreatedAtRoute("Todos", new { id = newTodo.Id }, newTodo);
            }
            catch(ArgumentNullException ex)
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
