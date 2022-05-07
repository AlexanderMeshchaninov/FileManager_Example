using System;
using System.IO;
using FileManager.Core.CommandLine;
using FileManager.Core.Constructor;
using FileManager.Core.Data;
using FileManager.Core.Settings;
using Serilog;

namespace FileManager.Data.CommandStorage.CommandsStorage
{
    public sealed class CreateFolderCommand : ICommands
    {
        public Guid Type { get; set; }
        public string CommandIdentifier { get; set; }
        
        private bool _isWorking;
        private readonly ILogger _logger;
        private readonly IConstructor _constructor;
        private readonly ISettings _settings;
        private readonly ICommandLine _commandLine;

        public CreateFolderCommand(
            ILogger logger, 
            IConstructor constructor,
            ISettings settings,
            ICommandLine commandLine)
        {
            _logger = logger;
            _constructor = constructor;
            _settings = settings;
            _commandLine = commandLine;
        }

        public void Execute()
        {
            _logger.Information("Create directory command start");
            string currentDirectory = _commandLine.Args.Replace($"{CommandIdentifier}", "");
            string answer = string.Empty;
            
            _isWorking = true;
            while (_isWorking)
            {
                _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
                _constructor.SetElement($"Create new folder? [y/n]");
                answer = Console.ReadLine();

                if (!string.IsNullOrEmpty(answer))
                {
                    switch (answer.ToLower())
                    {
                        case "y" :
                            Directory.CreateDirectory(currentDirectory);
                            if (Directory.Exists(currentDirectory))
                            {
                                _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
                                _constructor.SetElement("Folder successfully created");
                                Console.ReadKey();

                                _constructor.ClearLayer();
                                _logger.Information("Directory successfully created");

                                _isWorking = false;
                                return;
                            }

                            _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
                            _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Red);
                            _constructor.SetElement("Directory does not exists");
                            Console.ReadKey();
                        
                            _constructor.ClearLayer();
                            _logger.Information("Directory does not exists");
                            return;
                        
                        case "n" :
                            _constructor.ClearLayer();
                            _logger.Information("Directory has not been created");
                            return;
                    }
                }
            }
        }
    }
}