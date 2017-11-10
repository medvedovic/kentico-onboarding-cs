using System;

namespace TodoApp.Contracts.Wrappers
{
    public interface IGuidGenerator
    {
        Guid GenerateGuid();
    }
}
