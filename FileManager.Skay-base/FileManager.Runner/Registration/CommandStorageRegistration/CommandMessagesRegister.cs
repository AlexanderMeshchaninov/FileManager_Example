using Autofac;
using FileManager.Core.Data;
using FileManager.Data.CommandStorage.CommandsMessages;

namespace FileManager.Runner.Registration.CommandStorageRegistration
{
    public static class CommandMessagesRegister
    {
        public static void RegisterCommandMessage(this ContainerBuilder builder)
        {
            builder
                .RegisterType<CommandsMessages>()
                .As<ICommandsMessages>()
                .SingleInstance();
        }
    }
}