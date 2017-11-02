using System;

namespace TodoApp.Contracts.Helpers
{
    public interface IGuidGenerator
    {
        Guid GenerateGuid();
    }
}
