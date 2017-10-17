using System;
using TodoApp.Contracts.Helpers;

namespace TodoApp.Services.Helpers
{
    class ServiceHelper: IServiceHelper
    {
        public DateTime GetCurrentDateTime() => DateTime.Now;

        public Guid GenerateGuid() => Guid.NewGuid();
    }
}
