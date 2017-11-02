using System;
using TodoApp.Contracts.Helpers;

namespace TodoApp.Services.Helpers
{
    internal class DateTimeProvider: IDateTimeProvider
    {
        public DateTime GetCurrentDateTime() => DateTime.Now;
    }
}
