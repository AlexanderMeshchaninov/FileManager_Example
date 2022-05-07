using Autofac;
using FileManager.CommonLogic.CommandLine;
using FileManager.Core.CommandLine;

namespace FileManager.Runner.Registration.CommandLineRegistration
{
    public static class CommandLineRegister
    {
        public static void RegisterCommandsLine(this ContainerBuilder builder)
        {
            builder
                .RegisterType<CommandLine>()
                .As<ICommandLine>()
                .SingleInstance();
        }
    }
}