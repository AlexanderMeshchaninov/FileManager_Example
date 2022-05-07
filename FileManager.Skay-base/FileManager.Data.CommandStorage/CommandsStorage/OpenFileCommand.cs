using System;
using System.Diagnostics;
using System.IO;
using FileManager.Core.CommandLine;
using FileManager.Core.Constructor;
using FileManager.Core.Data;
using FileManager.Core.Settings;
using Serilog;

namespace FileManager.Data.CommandStorage.CommandsStorage
{
    public sealed class OpenFileCommand : ICommands
    {
        public Guid Type { get; set; }
        public string CommandIdentifier { get; set; }

        private bool _isWorking;
        private readonly ILogger _logger;
        private readonly IConstructor _constructor;
        private readonly ISettings _settings;
        private readonly ICommandLine _commandLine;
        private ICommandsMessages _messages;

        public OpenFileCommand(
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
            _logger.Information("Open file command start");
            var pathFrom = _commandLine.Args.Replace($"{CommandIdentifier}", "");
            if (!File.Exists(pathFrom)) return;
            
            string answer = string.Empty;
            
            _isWorking = true;
            while (_isWorking)
            {
                _messages.OpenQuestionMessage("File");
                answer = Console.ReadLine();
                
                if (!string.IsNullOrEmpty(answer))
                {
                    switch (answer.ToLower())
                    {
                        case "y" :
                            try
                            {
                                FileInfo fInfo = new FileInfo(pathFrom);
                                
                                _constructor.ClearLayer();
                                var openFile = new Process();
                                openFile.StartInfo.FileName = @$"{fInfo.FullName}";
                                openFile.StartInfo.UseShellExecute = true;
                                _messages.InProgressMessage();
                                openFile.Start();
                                _constructor.ClearLayer();
                            }
                            catch (Exception ex)
                            {
                                _logger.Error($"{ex}");
                                _constructor.SetElementPosition(_settings.MiddlePosition - 6, 3);
                                _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Red);
                                _constructor.SetElement("Something goes wrong, try it again");
                                continue;
                            }
                            
                            _messages.OpenSuccessMessage("File");
                            _logger.Information("Open file command successfully");
                            _isWorking = false;
                            break;
                        
                        case "n" :
                            _messages.NotOpenedMessage("File");
                            _logger.Information("Open file command stop by user");
                            _isWorking = false;
                            return;
                    }
                }
            }
            _constructor.ClearLayer();
            _constructor.SetColorsDefault();
            _logger.Information("Open file command stop");
        }
    }
}