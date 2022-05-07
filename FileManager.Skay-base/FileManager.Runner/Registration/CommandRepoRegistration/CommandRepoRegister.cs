using Autofac;
using FileManager.Core.CommandRepo;
using FileManager.Data.CommandRepository;

namespace FileManager.Runner.Registration.CommandRepoRegistration
{
    public static class CommandRepoRegister
    {
        public static void RegisterCommandRepository(this ContainerBuilder builder)
        {
            builder
                .RegisterType<CommandRepository>()
                .As<ICommandRepository>()
                .SingleInstance();
        }
    }
}