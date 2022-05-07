using System;
using System.IO;
using System.Threading;
using FileManager.Core.CommandLine;
using FileManager.Core.Constructor;
using FileManager.Core.Data;
using FileManager.Core.Settings;
using Serilog;

namespace FileManager.Data.CommandStorage.CommandsStorage
{
    public sealed class CopyFileCommand : ICommands
    {
        public Guid Type { get; set; }
        public string CommandIdentifier { get; set; }
        private bool _isWorking;
        private readonly ILogger _logger;
        private readonly IConstructor _constructor;
        private readonly ISettings _settings;
        private readonly ICommandLine _commandLine;
        private ICommandsMessages _messages;

        public CopyFileCommand(
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
            _logger.Information("Copy file command start");
            var pathFrom = _commandLine.Args.Replace($"{CommandIdentifier}", "");
            if (!File.Exists(pathFrom)) return;
            
            string answer = string.Empty;
            
            _isWorking = true;
            while (_isWorking)
            {
                _messages.CopyQuestionMessage("file");
                answer = Console.ReadLine();
                
                if (!string.IsNullOrEmpty(answer))
                {
                    switch (answer.ToLower())
                    {
                        case "y" :
                            _messages.FromToMessage(pathFrom);
                            string pathTo = Console.ReadLine();
                            if (!string.IsNullOrEmpty(pathTo) && Directory.Exists(pathTo))
                            {
                                _messages.FolderOrFileNameMessage("File");
                                string newFileName = Console.ReadLine();
                                
                                if (!string.IsNullOrEmpty(newFileName))
                                {
                                    try
                                    {
                                        _constructor.ClearLayer();
                                        var copyThread = CopyFileThread(pathFrom, pathTo, newFileName);
                                        copyThread.Priority = ThreadPriority.Highest;
                                        copyThread.Start();
                                        _messages.InProgressMessage();
                                        copyThread.Join();
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
                                    
                                    _messages.CopySuccessMessage("File");
                                    _logger.Information("Copy file command successfully");
                                    _isWorking = false;
                                    break;
                                }
                            }
                            _logger.Warning("Copy file command wrong path");
                            _messages.WrongDestinationMessage();
                            continue;
                        
                        case "n" :
                            _messages.NotCopiedMessage("File");
                            _logger.Information("Copy file command stop by user");
                            _isWorking = false;
                            return;
                    }
                }
            }
            _constructor.ClearLayer();
            _constructor.SetColorsDefault();
            _logger.Information("Copy file command stop");
        }

        private Thread CopyFileThread(string pathFrom, string pathTo, string newFileName)
        {
            var copyThread = new Thread(() =>
            {
                var pathToCombine = Path.Combine(pathTo, newFileName);
                var pathFromInfo = new FileInfo(pathFrom);
                
                pathFromInfo.CopyTo(pathToCombine, true);
            });

            return copyThread;
        }
    }
}