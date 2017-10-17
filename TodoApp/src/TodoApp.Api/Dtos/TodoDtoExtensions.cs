using TodoApp.Contracts.Models;

namespace TodoApp.Api.Dtos
{
    public static class TodoDtoExtensions
    {
        public static Todo ConvertToTodoModel(this TodoDto dto) =>
            new Todo
            {
                Value = dto.Value
            };
    }
}
