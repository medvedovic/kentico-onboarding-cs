using System;

namespace TodoApp.Contracts.Wrappers
{
    public interface ITodoLocationHelper
    {
        Uri BuildRouteUri(Guid id);
    }
}
