using System;

namespace TodoApp.Contracts.Helpers
{
    public interface IUriHelper
    {
        Uri BuildUri(Guid id, string atRoute);
    }
}
