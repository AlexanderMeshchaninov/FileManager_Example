using System.IO;
using Autofac;
using Serilog;

namespace FileManager.Runner.Registration.SerilogRegistration
{
    public static class SerilogRegister
    {
        public static void RegisterSerilog(this ContainerBuilder builder)
        {
            var currentDir = Directory.GetCurrentDirectory();

            builder.Register<ILogger>((c, p) =>
            {
                return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File($"{currentDir}/logs/SKAY-BASE_log.txt", rollingInterval: RollingInterval.Day)
                  .CreateLogger();
            }).SingleInstance();
        }
    }
}
