using System;
using TodoApp.Contracts.Helpers;

namespace TodoApp.Services.Helpers
{
    internal class GuidGenerator: IGuidGenerator
    {
        public Guid GenerateGuid() => Guid.NewGuid();
    }
}
