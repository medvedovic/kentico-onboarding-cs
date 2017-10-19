using TodoApp.Contracts.Models;

namespace TodoApp.Api.ViewModels
{
    public static class TodoViewModelExtensions
    {
        public static Todo ConvertToTodoModel(this TodoViewModel viewModel) =>
            new Todo
            {
                Value = viewModel.Value
            };
    }
}
