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
    public sealed class CopyAllFolderCommand : ICommands
    {
        public Guid Type { get; set; }
        public string CommandIdentifier { get; set; }

        private bool _isWorking;
        private readonly ILogger _logger;
        private readonly IConstructor _constructor;
        private readonly ISettings _settings;
        private readonly ICommandLine _commandLine;
        private ICommandsMessages _messages;

        public CopyAllFolderCommand(
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
            _logger.Information("Copy all directory command start");
            var pathFrom = _commandLine.Args.Replace($"{CommandIdentifier}", "");
            var sourceDir = new DirectoryInfo(pathFrom);
            if (!Directory.Exists(sourceDir.FullName)) return;
            
            string answer = string.Empty;

            _isWorking = true;
            while (_isWorking)
            {
                _messages.CopyQuestionMessage("folder");
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
                                var targetDir = new DirectoryInfo(pathTo);
                                if (targetDir.Exists)
                                {
                                    _messages.FolderOrFileNameMessage("Folder");
                                    string newFolderName = Console.ReadLine();
                                    
                                    if (!string.IsNullOrEmpty(newFolderName))
                                    {
                                        var pathToCombine = Path.Combine(targetDir.FullName, newFolderName);
                                        var combinePath = new DirectoryInfo(pathToCombine);
                                        
                                        Directory.CreateDirectory(combinePath.FullName);
                                        if (Directory.Exists(combinePath.FullName))
                                        {
                                            try
                                            {
                                                _constructor.ClearLayer();
                                                var copyThread = CopyAllDirectoryThread(sourceDir, combinePath);
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
                                            
                                            _messages.CopySuccessMessage("Folder");
                                            _logger.Information("Copy all directory command successfully");
                                            _isWorking = false;
                                            break;
                                        }
                                        _logger.Warning("Copy all directory command not successfully");
                                        _messages.NotCopiedMessage("Folder");
                                    }
                                }
                                _logger.Warning("Copy all directory command wrong path");
                                _messages.WrongDestinationMessage();
                            }
                            continue;
                        
                        case "n" :
                            _messages.NotCopiedMessage("Folder");
                            _logger.Information("Copy all directory command stop by user");
                            _isWorking = false;
                            return;
                    }
                }
            }
            _constructor.ClearLayer();
            _constructor.SetColorsDefault();
            _logger.Information("Copy all directory command stop");
        }
        
        private void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            if (source.FullName.ToLower() == target.FullName.ToLower())
            {
                return;
            }
            foreach (FileInfo fileInfo in source.GetFiles())
            {
                fileInfo.CopyTo(Path.Combine(target.ToString(), fileInfo.Name), true);
            }
            foreach (DirectoryInfo subDirs in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubdirectory = target.CreateSubdirectory(subDirs.Name);
                CopyAll(subDirs, nextTargetSubdirectory);
            }
        }

        private Thread CopyAllDirectoryThread(DirectoryInfo sourceDir, DirectoryInfo combinePath)
        {
            var copyThread = new Thread(() =>
            {
                //Получаем файлы в директории
                foreach (FileInfo fileInfo in sourceDir.GetFiles())
                {
                    fileInfo.CopyTo(Path.Combine(combinePath.ToString(), fileInfo.Name), true);
                }
                //Получаем вложенные директории
                foreach (DirectoryInfo subDirs in sourceDir.GetDirectories())
                {
                    DirectoryInfo nextTargetSubdirectory = combinePath.CreateSubdirectory(subDirs.Name);
                    CopyAll(subDirs, nextTargetSubdirectory);
                }
            });

            return copyThread;
        }
    }
}