using Autofac;
using FileManager.CommonLogic.Constructor;
using FileManager.Core.Constructor;

namespace FileManager.Runner.Registration.ConstructorRegistration
{
    public static class ConstructorRegister
    {
        public static void RegisterConstructor(this ContainerBuilder buider)
        {
            buider
                .RegisterType<Constructor>()
                .As<IConstructor>()
                .SingleInstance();
        }
    }
}
