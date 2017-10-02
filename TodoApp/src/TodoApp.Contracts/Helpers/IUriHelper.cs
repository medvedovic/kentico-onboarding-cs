using System;

namespace TodoApp.Contracts.Helpers
{
    public interface IUriHelper
    {
        Uri BuildUriForPostTodo(Guid id);
    }
}
