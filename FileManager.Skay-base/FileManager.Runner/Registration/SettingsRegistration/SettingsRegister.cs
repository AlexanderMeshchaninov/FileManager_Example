using Autofac;
using FileManager.CommonLogic.Settings;
using FileManager.Core.Settings;

namespace FileManager.Runner.Registration.SettingsRegistration
{
    public static class SettingsRegister
    {
        public static void RegisterSettings(this ContainerBuilder builder)
        {
            builder
                .RegisterType<Settings>()
                .As<ISettings>()
                .SingleInstance();
        }
    }
}