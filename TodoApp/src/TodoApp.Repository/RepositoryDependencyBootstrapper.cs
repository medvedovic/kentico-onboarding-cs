using Microsoft.Practices.Unity;
using TodoApp.Contracts;
using TodoApp.Contracts.Repositories;
using TodoApp.Repository.Repositories;

namespace TodoApp.Repository
{
    public class RepositoryDependencyBootstrapper: IUnityBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container, DependencyBootstrapperConfig configuration) => 
            container.RegisterType<ITodoRepository, TodoRepository>(new ContainerControlledLifetimeManager(), new InjectionConstructor(configuration.ConnectionString));
    }
}
