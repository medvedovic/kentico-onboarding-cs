namespace TodoApp.Contracts.Bootstrap
{
    public class DatabaseConfig : IDatabaseConfig
    {
        public string ConnectionString { get; set; }
    }
}
