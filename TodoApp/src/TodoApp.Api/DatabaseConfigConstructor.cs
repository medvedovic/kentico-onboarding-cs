using TodoApp.Contracts.Bootstrap;
using TodoApp.Contracts.Models;
using Unity;

namespace TodoApp.Api
{
    internal class DatabaseConfigConstructor
    {
        internal static IDatabaseConfig Create(IUnityContainer arg) => 
            new DatabaseConfig
            {
                ConnectionString = System.Configuration.ConfigurationManager
                    .ConnectionStrings["DefaultConnection"].ConnectionString
            };
    }
}