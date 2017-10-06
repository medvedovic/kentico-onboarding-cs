using System;

namespace TodoApp.Contracts.Helpers
{
    public interface IUriHelper
    {
        Uri BuildRouteUri(Guid id);
    }
}
