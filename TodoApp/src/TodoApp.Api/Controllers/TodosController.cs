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
        private readonly IGetTodoService _getTodoService;
        private readonly IUriHelper _uriHelper;

        public TodosController(ITodoRepository todoRepository, IPostTodoService postTodoService, IUriHelper uriHelper, IPutTodoService putTodoService, IGetTodoService getTodoService)
        {
            _postTodoService = postTodoService;
            _repository = todoRepository;
            _uriHelper = uriHelper;
            _putTodoService = putTodoService;
            _getTodoService = getTodoService;
        }

        public async Task<IHttpActionResult> GetAllTodosAsync()
        {
            var todos = await _repository.RetrieveAllAsync();

            return Ok(todos);
        }

        public async Task<IHttpActionResult> GetTodoAsync(Guid id)
        {
            if (!await _getTodoService.IsTodoInDbAsync(id))
            {
                return NotFound();
            }

            return Ok(await _getTodoService.RetrieveTodoAsync(id));
        }

        public async Task<IHttpActionResult> PostTodoAsync(TodoViewModel todo)
        {
            ValidateViewModelForNull(todo);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await CreateNewTodoAsync(todo);
        }


        public async Task<IHttpActionResult> DeleteTodoAsync(Guid id)
        {
            if (!await _getTodoService.IsTodoInDbAsync(id))
            {
                return NotFound();
            }

            await _repository.RemoveAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        public async Task<IHttpActionResult> PutTodoAsync(Guid id, [FromBody] TodoViewModel updated)
        {
            ValidateViewModelForNull(updated);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _getTodoService.IsTodoInDbAsync(id))
            {
                return await CreateNewTodoAsync(updated);
            }

            return Ok(await _putTodoService.UpdateTodoAsync(updated));
        }

        private void ValidateViewModelForNull(TodoViewModel updated)
        {
            if (updated == null)
            {
                ModelState.AddModelError(string.Empty, "TodoViewModel cannot be null");
            }
        }

        private async Task<IHttpActionResult> CreateNewTodoAsync(TodoViewModel todo)
        {
            var newTodo = await _postTodoService.CreateTodoAsync(todo);

            var location = _uriHelper.BuildRouteUri(newTodo.Id);

            return Created(location, newTodo);
        }
    }
}
