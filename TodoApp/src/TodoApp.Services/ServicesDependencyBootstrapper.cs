using Microsoft.Practices.Unity;
using TodoApp.Contracts.Bootstrap;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Services.Todos;
using TodoApp.Services.Helpers;
using TodoApp.Services.Todos;

namespace TodoApp.Services
{
    public class ServicesDependencyBootstrapper : IUnityBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container) =>
            container
                .RegisterType<ICreateTodoService, CreateTodoService>(new ContainerControlledLifetimeManager())
                .RegisterType<IUpdateTodoService, UpdateTodoService>(new ContainerControlledLifetimeManager())
                .RegisterType<IRetrieveTodoService, RetrieveTodoService>(new ContainerControlledLifetimeManager())
                .RegisterType<IGuidGenerator, GuidGenerator>(new ContainerControlledLifetimeManager())
                .RegisterType<IDateTimeProvider, DateTimeProvider>(new ContainerControlledLifetimeManager());
    }
}
