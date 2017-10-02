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
    [Route("{id:guid?}", Name = DEFAULT_ROUTE)]
    public class TodosController : ApiController
    {
        private readonly ITodoRepository _repository;
        private readonly IUriHelper _uriHelper;

        public const string DEFAULT_ROUTE = "Default";
        public const string POST_TODO_ROUTE = "PostTodo";

        public TodosController(ITodoRepository todoRepository, IUriHelper uriHelper)
        {
            _repository = todoRepository;
            _uriHelper = uriHelper;
        }

        public async Task<IHttpActionResult> GetAllTodosAsync()
        {
            var todos = await _repository.RetrieveAllAsync();

            return Ok(todos);
        }

        [Route(Name = POST_TODO_ROUTE)]
        public async Task<IHttpActionResult> GetTodoAsync(Guid id)
        {
            var todo = await _repository.RetrieveAsync(id);

            return Ok(todo);
        }

        public async Task<IHttpActionResult> PostTodoAsync(Todo todo)
        {
            var newTodo = await _repository.CreateAsync(todo);

            var location = _uriHelper.BuildUriForPostTodo(newTodo.Id);

            return Created(location, newTodo);          
        }

        public async Task<IHttpActionResult> DeleteTodoAsync(Guid id)
        {
            return await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
        }

        public async Task<IHttpActionResult> PutTodoAsync(Guid id, [FromBody] Todo updated)
        {
            return await Task.FromResult(Ok(updated));
        }
    }
}
