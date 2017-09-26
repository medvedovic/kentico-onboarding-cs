using Microsoft.Practices.Unity;
using TodoApp.Contracts;
using TodoApp.Contracts.Repositories;
using TodoApp.DL.Repositories;

namespace TodoApp.DL
{
    public class RepositoryDependencyBootstrapper: IUnityBootstrapper
    {
        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<ITodoRepository, TodoRepository>(new ContainerControlledLifetimeManager());
        }
    }
}
