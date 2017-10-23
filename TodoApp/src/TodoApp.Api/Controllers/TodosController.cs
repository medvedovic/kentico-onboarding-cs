using Microsoft.Web.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoApp.Api.ViewModels;
using TodoApp.Contracts.Helpers;
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
        private readonly IPutTodoService _putTodoService;
        private readonly IUriHelper _uriHelper;

        public TodosController(ITodoRepository todoRepository, IPostTodoService postTodoService, IUriHelper uriHelper, IPutTodoService putTodoService)
        {
            _postTodoService = postTodoService;
            _repository = todoRepository;
            _uriHelper = uriHelper;
            _putTodoService = putTodoService;
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

        public async Task<IHttpActionResult> PostTodoAsync(TodoViewModel todo)
        {
            if (todo == null)
            {
                ModelState.AddModelError(string.Empty, "Model cannot be null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newTodo = await _postTodoService.CreateTodoAsync(todo);

            var location = _uriHelper.BuildRouteUri(newTodo.Id);

            return Created(location, newTodo);
        }

        public async Task<IHttpActionResult> DeleteTodoAsync(Guid id)
        {
            var todoInDb = await _repository.RetrieveAsync(id);

            if (todoInDb == null)
            {
                return NotFound();
            }

            await _repository.RemoveAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        public async Task<IHttpActionResult> PutTodoAsync(Guid id, [FromBody] TodoViewModel updated)
        {
            if (updated == null)
            {
                ModelState.AddModelError(string.Empty, "Model cannot be null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoInDb = await _repository.RetrieveAsync(id);

            if (todoInDb == null)
            {
                return NotFound();
            }

            _putTodoService.ExistingTodo = todoInDb;

            return Ok(await _putTodoService.UpdateTodoAsync(id, updated));
        }
    }
}
