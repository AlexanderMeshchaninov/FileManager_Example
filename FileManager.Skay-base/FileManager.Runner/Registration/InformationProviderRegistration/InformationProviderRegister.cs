using Autofac;
using FileManager.CommonLogic.InformationProvider;

namespace FileManager.Runner.Registration.InformationProviderRegistration
{
    public static class InformationProviderRegister
    {
        public static void RegisterInformationBar(this ContainerBuilder buider)
        {
            buider
                .RegisterType<InformationProvider>()
                .As<IInformationProvider>()
                .SingleInstance();
        }
    }
}