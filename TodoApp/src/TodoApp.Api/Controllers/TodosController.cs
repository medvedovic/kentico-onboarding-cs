using Microsoft.Web.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoApp.Contracts.Dtos;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;

namespace TodoApp.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/todos/{id:guid?}", Name = DEFAULT_ROUTE)]
    public class TodosController : ApiController
    {
        public const string DEFAULT_ROUTE = "TodosDefault";

        private readonly ITodoRepository _repository;
        private readonly IPostTodoService _postTodoService;
        private readonly IUriHelper _uriHelper;

        public TodosController(ITodoRepository todoRepository, IPostTodoService postTodoService, IUriHelper uriHelper)
        {
            _postTodoService = postTodoService;
            _repository = todoRepository;
            _uriHelper = uriHelper;
        }

        public async Task<IHttpActionResult> GetAllTodosAsync()
        {
            var todos = await _repository.RetrieveAllAsync();

            return Ok(todos);
        }

        public async Task<IHttpActionResult> GetTodoAsync(Guid id)
        {
            var todo = await _repository.RetrieveAsync(id);

            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        public async Task<IHttpActionResult> PostTodoAsync(TodoDto todo)
        {
            if (ModelState.IsValid)
            {
                var newTodo = await _postTodoService.CreateTodoAsync(todo);

                var location = _uriHelper.BuildRouteUri(newTodo.Id);

                return Created(location, newTodo);
            }

            return BadRequest(ModelState);
        }

        public async Task<IHttpActionResult> DeleteTodoAsync(Guid id)
        {
            if (await _repository.RemoveAsync(id))
                return await Task.FromResult(StatusCode(HttpStatusCode.NoContent));

            return await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
        }

        public async Task<IHttpActionResult> PutTodoAsync(Guid id, [FromBody] Todo updated)
        {
            var newTodo = await _repository.UpdateAsync(updated);

            return await Task.FromResult(Ok(newTodo));
        }
    }
}
