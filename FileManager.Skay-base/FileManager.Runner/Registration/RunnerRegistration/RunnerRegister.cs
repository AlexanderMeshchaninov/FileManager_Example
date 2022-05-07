using Autofac;
using FileManager.Core.Runner;

namespace FileManager.Runner.Registration.RunnerRegistration
{
    public static class RunnerRegister
    {
        public static void RegisterRunner(this ContainerBuilder buider)
        {
            buider
                .RegisterType<AppRunner>()
                .As<IApplicationRunner>()
                .SingleInstance();
        }
    }
}