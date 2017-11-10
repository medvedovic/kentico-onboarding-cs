using TodoApp.Contracts.Bootstrap;
using Unity;

namespace TodoApp.Api
{
    internal class DatabaseConfig : IDatabaseConfig
    {
        public string ConnectionString { get; set; }

        internal static IDatabaseConfig Create(IUnityContainer arg) =>
            new DatabaseConfig
            {
                ConnectionString = System.Configuration.ConfigurationManager
                    .ConnectionStrings["DefaultConnection"].ConnectionString
            };
    }
}
