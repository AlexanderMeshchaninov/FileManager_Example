using FileManager.Core.CommandLine;
using FileManager.Core.Data;
using FileManager.Core.Facades;
using FileManager.Core.Rendering;
using FileManager.Core.Runner;
using Serilog;

namespace FileManager.Runner
{
    public class AppRunner : IApplicationRunner
    {
        private readonly ILogger _logger;
        private readonly IRendering _render;
        private readonly ICommandLine _commandLine;
        private readonly IFacade _facade;
        private readonly IInitializeCommands _initializeCommands;

        public AppRunner(
            ILogger logger,
            IRendering render,
            ICommandLine commandLine,
            IFacade facade,
            IInitializeCommands initializeCommands)
        {
            _logger = logger;
            _render = render;
            _commandLine = commandLine;
            _facade = facade;
            _initializeCommands = initializeCommands;
        }
        
        public void StartApplication()
        {
            _logger.Information("Start runner");

            var commandsCollection = _initializeCommands.Initialize();
            
            _render.MainScreenRender();
            while (true)
            {
                _render.CommandLineRender();

                var fileSystemInfoListResponse = _facade.InformationProviderFacade(_commandLine.Args);
                if (fileSystemInfoListResponse is not null)
                {
                    _render.FilesAndFoldersInfoBarRender(0, fileSystemInfoListResponse);
                    _render.FilesAndFoldersSystemRender(fileSystemInfoListResponse.ExistsFilesAndFolders);
                    continue;
                }

                var commandToExecute = _facade.CommandRepositoryFacade(commandsCollection, _commandLine.Args);
                if (commandToExecute is not null)
                {
                    commandToExecute.Execute();
                    continue;
                }
                
                _render.MainScreenRender();
            }
        }
    }
}