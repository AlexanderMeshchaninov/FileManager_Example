using Autofac;
using FileManager.CommonLogic.Facades;
using FileManager.Core.Facades;

namespace FileManager.Runner.Registration.FacadesRegistration
{
    public static class FacadeRegister
    {
        public static void RegisterFacade(this ContainerBuilder builder)
        {
            builder
                .RegisterType<Facade>()
                .As<IFacade>()
                .SingleInstance();
        }
    }
}