using System;

namespace TodoApp.Contracts.Helpers
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentDateTime();
    }
}
