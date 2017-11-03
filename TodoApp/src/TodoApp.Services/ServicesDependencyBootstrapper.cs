using Microsoft.Practices.Unity;
using TodoApp.Contracts.Bootstrap;
using TodoApp.Contracts.Services.Todos;
using TodoApp.Contracts.Wrappers;
using TodoApp.Services.Todos;
using TodoApp.Services.Wrappers;

namespace TodoApp.Services
{
    public class ServicesDependencyBootstrapper : IUnityBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container) =>
            container
                .RegisterType<ICreateTodoService, CreateTodoService>(new PerResolveLifetimeManager())
                .RegisterType<IUpdateTodoService, UpdateTodoService>(new PerResolveLifetimeManager())
                .RegisterType<IRetrieveTodoService, RetrieveTodoService>(new PerResolveLifetimeManager())
                .RegisterType<IGuidGenerator, GuidGenerator>(new ContainerControlledLifetimeManager())
                .RegisterType<IDateTimeProvider, DateTimeProvider>(new ContainerControlledLifetimeManager());
    }
}
