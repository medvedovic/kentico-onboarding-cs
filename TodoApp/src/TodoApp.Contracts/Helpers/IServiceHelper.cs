using System;

namespace TodoApp.Contracts.Helpers
{
    public interface IServiceHelper
    {
        DateTime GetCurrentDateTime();
        Guid GenerateGuid();
    }
}
