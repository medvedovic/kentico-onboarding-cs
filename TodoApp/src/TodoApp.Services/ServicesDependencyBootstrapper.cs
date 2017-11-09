using TodoApp.Contracts.Bootstrap;
using TodoApp.Contracts.Services.Todos;
using TodoApp.Contracts.Wrappers;
using TodoApp.Services.Todos;
using TodoApp.Services.Wrappers;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Services
{
    public class ServicesDependencyBootstrapper : IUnityBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container) =>
            container
                .RegisterType<ICreateTodoService, CreateTodoService>(new HierarchicalLifetimeManager())
                .RegisterType<IUpdateTodoService, UpdateTodoService>(new HierarchicalLifetimeManager())
                .RegisterType<IRetrieveTodoService, RetrieveTodoService>(new HierarchicalLifetimeManager())
                .RegisterType<IGuidGenerator, GuidGenerator>(new ContainerControlledLifetimeManager())
                .RegisterType<IDateTimeProvider, DateTimeProvider>(new ContainerControlledLifetimeManager());
    }
}
