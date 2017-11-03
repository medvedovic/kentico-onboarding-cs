using System;
using TodoApp.Contracts.Wrappers;

namespace TodoApp.Services.Wrappers
{
    internal class DateTimeProvider: IDateTimeProvider
    {
        public DateTime GetCurrentDateTime() => DateTime.Now;
    }
}
