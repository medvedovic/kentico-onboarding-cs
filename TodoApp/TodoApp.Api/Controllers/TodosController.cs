using TodoApp.Api.Models;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<Todo>> GetAllTodos()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetTodo(int id)
        {
            var todo = await _repository.GetAsync(id);

            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpPost]
        [Route("", Name = "PostTodo")]
        public async Task<IHttpActionResult> PostTodo(Todo todo)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newTodo = await _repository.AddAsync(todo);

                return CreatedAtRoute("PostTodo", new { id = newTodo.Id }, newTodo);
            }
            catch(ArgumentNullException ex)
            {
                ModelState.AddModelError("TodoNullError", ex);

                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteTodo(int id)
        {
            var isRemoved = await _repository.RemoveAsync(id);

            if (!isRemoved)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> PutTodo(int id, [FromBody] Todo updated)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updated.Id)
                return BadRequest();

            try
            {
                if (!await _repository.UpdateAsync(id, updated))
                    return NotFound();

                updated.Id = id;

                return Ok(updated);
            }
            catch(ArgumentNullException ex)
            {
                ModelState.AddModelError("TodoNullError", ex);

                return BadRequest(ModelState);
            }
        }
    }
}
