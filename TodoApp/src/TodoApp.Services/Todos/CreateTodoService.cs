using System.Threading.Tasks;
using TodoApp.Contracts;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services.Todos;
using TodoApp.Contracts.Wrappers;

namespace TodoApp.Services.Todos
{
    internal class CreateTodoService: ICreateTodoService
    {
        private readonly ITodoRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IGuidGenerator _guidGenerator;

        public CreateTodoService(ITodoRepository repository, IDateTimeProvider dateTimeProvider, IGuidGenerator guidGenerator)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
            _guidGenerator = guidGenerator;
        }

        public async Task<Todo> CreateTodoAsync(IConvertibleTo<Todo> todoViewModel)
        {
            var newTodo = todoViewModel.Convert();
            newTodo.Id = _guidGenerator.GenerateGuid();
            var currentDateTime = _dateTimeProvider.GetCurrentDateTime();
            newTodo.CreatedAt = currentDateTime;
            newTodo.UpdatedAt = currentDateTime;

            return await _repository.CreateAsync(newTodo);
        }
    }
}
