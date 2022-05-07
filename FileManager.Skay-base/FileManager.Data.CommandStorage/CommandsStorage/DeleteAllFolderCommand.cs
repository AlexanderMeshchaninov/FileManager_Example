using System;
using System.IO;
using FileManager.Core.CommandLine;
using FileManager.Core.Constructor;
using FileManager.Core.Data;
using FileManager.Core.Settings;
using Serilog;

namespace FileManager.Data.CommandStorage.CommandsStorage
{
    public sealed class DeleteAllFolderCommand : ICommands
    {
        public Guid Type { get; set; }
        public string CommandIdentifier { get; set; }

        private bool _isWorking;
        private readonly ILogger _logger;
        private readonly IConstructor _constructor;
        private readonly ISettings _settings;
        private readonly ICommandLine _commandLine;
        private ICommandsMessages _messages;

        public DeleteAllFolderCommand(
            ILogger logger, 
            IConstructor constructor,
            ISettings settings,
            ICommandLine commandLine,
            ICommandsMessages messages)
        {
            _logger = logger;
            _constructor = constructor;
            _settings = settings;
            _commandLine = commandLine;
            _messages = messages;
        }
        public void Execute()
        {
            _logger.Information("Delete all folder command start");
            var pathFrom = _commandLine.Args.Replace($"{CommandIdentifier}", "");
            var sourceDir = new DirectoryInfo(pathFrom);
            if(!Directory.Exists(sourceDir.FullName)) return;
            
            string answer = string.Empty;
            
            _isWorking = true;
            while (_isWorking)
            {
                _messages.DeleteQuestionMessage("Folder");
                answer = Console.ReadLine();
                
                if (!string.IsNullOrEmpty(answer))
                {
                    switch (answer.ToLower())
                    {
                        case "y" :
                            try
                            {
                                if (sourceDir.Attributes == FileAttributes.ReadOnly)
                                {
                                    sourceDir.Attributes = FileAttributes.Normal;
                                }
                                _constructor.ClearLayer();
                                _messages.InProgressMessage();
                                Directory.Delete(sourceDir.FullName, true);
                                _constructor.ClearLayer();
                            }
                            catch (Exception ex)
                            {
                                _logger.Error($"{ex}");
                                _constructor.ClearLayer();
                                _constructor.SetElementPosition(_settings.MiddlePosition - 18, 1);
                                _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Red);
                                _constructor.SetElement("Something goes wrong, try it again");
                                Console.ReadKey();
                                continue;
                            }
                            
                            _messages.DeleteSuccessMessage("Folder");
                            _logger.Information("Delete all folder command successfully");
                            _isWorking = false;
                            break;
                        
                        case "n" :
                            _messages.NotDeletedMessage("Folder");
                            _logger.Information("Delete all folder command stop by user");
                            _isWorking = false;
                            return;
                    }
                }
            }
            _constructor.ClearLayer();
            _constructor.SetColorsDefault();
            _logger.Information("Delete all folder command stop");
        }
    }
}