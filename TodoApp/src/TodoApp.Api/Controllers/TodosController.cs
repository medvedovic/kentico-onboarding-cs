using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;

namespace TodoApp.Api.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/todos")]
    public class TodosController : ApiController
    {
        private readonly ITodoRepository _repository;
        private readonly IUriHelper _uriHelper;

        private const string POST_TODO_ROUTE = "PostTodo";

        public TodosController(ITodoRepository todoRepository, IUriHelper uriHelper)
        {
            _repository = todoRepository;
            _uriHelper = uriHelper;
        }

        [Route("")]
        public async Task<IEnumerable<Todo>> GetAllTodos()
        {
            return await _repository.GetAll();
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> GetTodo(Guid id)
        {
            var todo = await _repository.Get(id);

            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [Route("", Name = POST_TODO_ROUTE)]
        public async Task<IHttpActionResult> PostTodo(Todo todo)
        {
            var newTodo = await _repository.Add(todo);

            if (newTodo == null)
                return BadRequest();

            return Created(_uriHelper.BuildUri(newTodo.Id, POST_TODO_ROUTE), newTodo);          
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteTodo(Guid id)
        {
            if (!await _repository.Remove(id))
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> PutTodo(Guid id, [FromBody] Todo updated)
        {
            if (!await _repository.Update(id, updated))
                return BadRequest();

            return Ok(updated);
        }
    }
}
