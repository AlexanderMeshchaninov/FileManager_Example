using Autofac;
using FileManager.Core.Data;
using FileManager.Data.CommandStorage.CommandsInitialize;

namespace FileManager.Runner.Registration.CommandStorageRegistration
{
    public static class InitializeCommandsRegistration
    {
        public static void RegisterInitializeCommands(this ContainerBuilder builder)
        {
            builder
                .RegisterType<InitializeCommands>()
                .As<IInitializeCommands>()
                .SingleInstance();
        }
    }
}