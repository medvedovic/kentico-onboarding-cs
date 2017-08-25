using KenticoOnboardingCs.Api.Models;
using KenticoOnboardingCs.Api.Models.Repositories;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace KenticoOnboardingCs.Api.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/Todos")]
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
    }
}
