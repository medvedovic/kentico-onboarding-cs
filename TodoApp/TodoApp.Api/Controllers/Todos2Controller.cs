using TodoApp.Api.Models;
using TodoApp.Api.Models.Repositories;
using Microsoft.Web.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net;
using System;

namespace TodoApp.Api.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/Todos/{id:int?}", Name = "Todos")]
    public class TodosV2Controller : ApiController
    {
        private IAsyncTodoRepository _repository;

        public TodosV2Controller(IAsyncTodoRepository repository)
        {
            _repository = repository;
        }

        public TodosV2Controller()
        {
            _repository = new TodoRepository();
        }

        [HttpGet]
        public async Task<IEnumerable<Todo>> GetAllTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetTodoAsync(int id)
        {
            return await _repository.GetAsync(id)
                .ContinueWith<IHttpActionResult>(res =>
                {
                    if (res.Result == null)
                    {
                        return NotFound();
                    }

                    return Ok(res.Result);
                });
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteTodoAsync(int id)
        {
            return await _repository.RemoveAsync(id)
                .ContinueWith<IHttpActionResult>(res =>
                {
                    if (res.Result == true)
                        return StatusCode(HttpStatusCode.NoContent);

                    return NotFound();
                });
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostTodoAsync([FromBody] Todo todo)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return await _repository.AddAsync(todo)
                    .ContinueWith<IHttpActionResult>(res =>
                    {
                        return CreatedAtRoute("Todos", new { id = res.Result.Id }, res.Result);
                    });
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
