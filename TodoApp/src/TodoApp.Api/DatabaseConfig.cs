using TodoApp.Contracts.Bootstrap;

namespace TodoApp.Api
{
    public class DatabaseConfig : IDatabaseConfig
    {
        public string ConnectionString { get; set; }
    }
}
