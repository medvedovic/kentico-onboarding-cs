using Microsoft.Practices.Unity;
using TodoApp.Contracts;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Services.Todos;
using TodoApp.Services.Helpers;
using TodoApp.Services.Todos;

namespace TodoApp.Services
{
    public class ServicesDependencyBootstrapper : IUnityBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container,DependencyBootstrapperConfig configuration = null) =>
            container
                .RegisterType<IPostTodoService, PostTodoService>(new ContainerControlledLifetimeManager())
                .RegisterType<IServiceHelper, ServiceHelper>(new ContainerControlledLifetimeManager());
    }
}
