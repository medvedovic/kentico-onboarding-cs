using TodoApp.Contracts.Bootstrap;
using TodoApp.Contracts.Repositories;
using TodoApp.Repository.Repositories;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Repository
{
    public class RepositoryDependencyBootstrapper: IUnityBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container) => 
            container
                .RegisterType<ITodoRepository, TodoRepository>(new ContainerControlledLifetimeManager());
    }
}
