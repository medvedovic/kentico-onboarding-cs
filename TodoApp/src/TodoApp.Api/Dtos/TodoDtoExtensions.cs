using TodoApp.Contracts.Models;

namespace TodoApp.Api.Dtos
{
    public static class TodoDtoExtensions
    {
        public static Todo ToTodo(this TodoDto dto) =>
            new Todo
            {
                Value = dto.Value
            };
    }
}
