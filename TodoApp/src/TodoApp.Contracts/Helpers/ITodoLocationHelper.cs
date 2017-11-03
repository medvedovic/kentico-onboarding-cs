using System;

namespace TodoApp.Contracts.Helpers
{
    public interface ITodoLocationHelper
    {
        Uri BuildRouteUri(Guid id);
    }
}
