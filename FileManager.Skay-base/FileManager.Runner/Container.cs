using Autofac;
using FileManager.Core.Runner;
using FileManager.Runner.Registration.CommandLineRegistration;
using FileManager.Runner.Registration.CommandRepoRegistration;
using FileManager.Runner.Registration.CommandStorageRegistration;
using FileManager.Runner.Registration.ConstructorRegistration;
using FileManager.Runner.Registration.FacadesRegistration;
using FileManager.Runner.Registration.InformationProviderRegistration;
using FileManager.Runner.Registration.RenderingRegistration;
using FileManager.Runner.Registration.RunnerRegistration;
using FileManager.Runner.Registration.SerilogRegistration;
using FileManager.Runner.Registration.SettingsRegistration;

namespace FileManager.Runner
{
    class Container
    {
        static void Main(string[] args)
        {
            //Установка DI Autofac
            var builder = new ContainerBuilder();

            //Регистрации служб
            RunnerRegister.RegisterRunner(builder);
            SerilogRegister.RegisterSerilog(builder);
            SettingsRegister.RegisterSettings(builder);
            ConstructorRegister.RegisterConstructor(builder);
            InformationProviderRegister.RegisterInformationBar(builder);
            RenderingRegister.RegisterRendering(builder);
            CommandRepoRegister.RegisterCommandRepository(builder);
            CommandLineRegister.RegisterCommandsLine(builder);
            FacadeRegister.RegisterFacade(builder);
            CommandMessagesRegister.RegisterCommandMessage(builder);
            InitializeCommandsRegistration.RegisterInitializeCommands(builder);
            
            var container = builder.Build();

            container.Resolve<IApplicationRunner>()
                .StartApplication();
        }
    }
}