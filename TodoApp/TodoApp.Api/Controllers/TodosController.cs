using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoApp.Api.Helpers;
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

        public TodosController(ITodoRepository todoRepository, IUriHelper uriHelper)
        {
            _repository = todoRepository;
            _uriHelper = uriHelper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Todo>> GetAllTodos()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> GetTodo(Guid id)
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

                return Created(_uriHelper.BuildUri(Request, newTodo.Id), newTodo);
            }
            catch(ArgumentNullException ex)
            {
                ModelState.AddModelError("TodoNullError", ex);

                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteTodo(Guid id)
        {
            var isRemoved = await _repository.RemoveAsync(id);

            if (!isRemoved)
                return NotFound();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> PutTodo(Guid id, [FromBody] Todo updated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _repository.UpdateAsync(id, updated))
                    return NotFound();

                return Ok(updated);
            }
            catch(ArgumentNullException ex)
            {
                ModelState.AddModelError("TodoNullError", ex);

                return BadRequest(ModelState);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("IdsNoncompliance", ex);

                return BadRequest(ModelState);
            }
        }
    }
}
