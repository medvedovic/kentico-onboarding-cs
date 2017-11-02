using Microsoft.Practices.Unity;
using TodoApp.Contracts.Bootstrap;

namespace TodoApp.Api
{
    internal class DatabaseConfigGenerator
    {
        internal static IDatabaseConfig Generate(IUnityContainer arg) => 
            new DatabaseConfig
            {
                ConnectionString = System.Configuration.ConfigurationManager
                    .ConnectionStrings["DefaultConnection"].ConnectionString
            };
    }
}