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
        private readonly ICreateTodoService _createTodoService;
        private readonly IUpdateTodoService _updateTodoService;
        private readonly IRetrieveTodoService _retrieveTodoService;
        private readonly ITodoLocationHelper _todoLocationHelper;

        public TodosController(ITodoRepository todoRepository, ICreateTodoService createTodoService, ITodoLocationHelper todoLocationHelper, IUpdateTodoService updateTodoService, IRetrieveTodoService retrieveTodoService)
        {
            _createTodoService = createTodoService;
            _repository = todoRepository;
            _todoLocationHelper = todoLocationHelper;
            _updateTodoService = updateTodoService;
            _retrieveTodoService = retrieveTodoService;
        }

        public async Task<IHttpActionResult> GetAllTodosAsync() => 
            Ok(await _repository.RetrieveAllAsync());

        public async Task<IHttpActionResult> GetTodoAsync(Guid id)
        {
            if (!await _retrieveTodoService.IsTodoInDbAsync(id))
            {
                return NotFound();
            }

            return Ok(await _retrieveTodoService.RetrieveTodoAsync(id));
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
            if (!await _retrieveTodoService.IsTodoInDbAsync(id))
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

            if (!await _retrieveTodoService.IsTodoInDbAsync(id))
            {
                return await CreateNewTodoAsync(updated);
            }

            var existringTodo = await _retrieveTodoService.RetrieveTodoAsync(id);
            return Ok(await _updateTodoService.UpdateTodoAsync(existringTodo, updated));
        }

        private void ValidateViewModelForNull(TodoViewModel todo)
        {
            if (todo == null)
            {
                ModelState.AddModelError(string.Empty, "TodoViewModel cannot be null");
            }
        }

        private async Task<IHttpActionResult> CreateNewTodoAsync(TodoViewModel todo)
        {
            var newTodo = await _createTodoService.CreateTodoAsync(todo);

            var location = _todoLocationHelper.BuildRouteUri(newTodo.Id);
            return Created(location, newTodo);
        }
    }
}
