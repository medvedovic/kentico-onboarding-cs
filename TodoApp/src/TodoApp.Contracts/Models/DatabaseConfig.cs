using TodoApp.Contracts.Bootstrap;

namespace TodoApp.Contracts.Models
{
    public class DatabaseConfig : IDatabaseConfig
    {
        public string ConnectionString { get; set; }
    }
}
