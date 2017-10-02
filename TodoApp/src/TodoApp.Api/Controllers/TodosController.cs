using Microsoft.Web.Http;
using System;
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

        public const string POST_TODO_ROUTE = "PostTodo";

        public TodosController(ITodoRepository todoRepository, IUriHelper uriHelper)
        {
            _repository = todoRepository;
            _uriHelper = uriHelper;
        }

        [Route("")]
        public async Task<IHttpActionResult> GetAllTodos()
        {
            var todos = await _repository.RetrieveAllAsync();

            return Ok(todos);
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> GetTodo(Guid id)
        {
            var todo = await _repository.RetrieveAsync(id);

            return Ok(todo);
        }

        [Route("", Name = POST_TODO_ROUTE)]
        public async Task<IHttpActionResult> PostTodo(Todo todo)
        {
            var newTodo = await _repository.CreateAsync(todo);

            var location = _uriHelper.BuildUriForPostTodo(newTodo.Id);

            return Created(location, newTodo);          
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteTodo(Guid id)
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> PutTodo(Guid id, [FromBody] Todo updated)
        {
            return Ok(updated);
        }
    }
}
