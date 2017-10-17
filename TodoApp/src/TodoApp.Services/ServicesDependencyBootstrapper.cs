using Microsoft.Practices.Unity;
using TodoApp.Contracts;
using TodoApp.Contracts.Services.Todos;
using TodoApp.Services.Todos;

namespace TodoApp.Services
{
    public class ServicesDependencyBootstrapper: IUnityBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container, DependencyBootstrapperConfig configuration = null) => 
            container.RegisterType<IPostTodoService, PostTodoService>(new ContainerControlledLifetimeManager());
    }
}
