using Microsoft.Practices.Unity;

namespace TodoApp.Contracts
{
    public interface IUnityBootstrapper
    {
        IUnityContainer RegisterType(IUnityContainer container);
    }
}
