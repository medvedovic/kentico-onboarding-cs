using System;

namespace TodoApp.Contracts.Wrappers
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentDateTime();
    }
}
