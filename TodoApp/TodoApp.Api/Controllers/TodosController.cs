using TodoApp.Api.Models;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using TodoApp.Api.Repositories;

namespace TodoApp.Api.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/todos")]
    public class TodosController : ApiController
    {
        private ITodoRepository _repository;

        public TodosController(ITodoRepository todoRepository)
        {
            _repository = todoRepository;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Todo> GetAllTodos()
        {
            return _repository.GetAll();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetTodo(int id)
        {
            var todo = _repository.Get(id);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpPost]
        [Route("", Name = "PostTodo")]
        public IHttpActionResult PostTodo(Todo todo)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newTodo =_repository.Add(todo);

                return CreatedAtRoute("PostTodo", new { id = newTodo.Id }, newTodo);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteTodo(int id)
        {
            var isRemoved = _repository.Remove(id);

            if (!isRemoved)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult PutTodo(int id, [FromBody] Todo updated)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updated.Id)
                return BadRequest();

            try
            {
                if (!_repository.Update(id, updated))
                    return NotFound();

                updated.Id = id;

                return Ok(updated);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
