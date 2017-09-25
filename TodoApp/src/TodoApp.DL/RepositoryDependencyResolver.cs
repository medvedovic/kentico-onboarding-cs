using Microsoft.Practices.Unity;
using TodoApp.Contracts.Repositories;
using TodoApp.DL.Repositories;

namespace TodoApp.DL
{
    public static class RepositoryDependencyResolver
    {
        public static void RegisterType(IUnityContainer container)
        {
            container.RegisterType<ITodoRepository, TodoRepository>(new ContainerControlledLifetimeManager());
        }
    }
}
