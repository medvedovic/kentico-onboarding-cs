using System;
using TodoApp.Contracts.Wrappers;

namespace TodoApp.Services.Wrappers
{
    internal class GuidGenerator: IGuidGenerator
    {
        public Guid GenerateGuid() => Guid.NewGuid();
    }
}
