using Microsoft.Practices.Unity;

namespace TodoApp.Contracts
{
    public interface IUnityBootstrapper
    {
        void RegisterType(IUnityContainer container);
    }
}
