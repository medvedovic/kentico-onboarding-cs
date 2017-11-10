using Unity;

namespace TodoApp.Contracts.Bootstrap
{
    public interface IUnityBootstrapper
    {
        IUnityContainer RegisterType(IUnityContainer container);
    }
}
