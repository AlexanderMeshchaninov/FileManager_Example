using System;
using System.Linq;
using FileManager.Core.CommandLine;
using FileManager.Core.Data;
using Serilog;

namespace FileManager.Data.CommandStorage.CommandsStorage
{
    public sealed class StepBackCommand : ICommands
    {
        public Guid Type { get; set; }
        public string CommandIdentifier { get; set; }

        private bool _isWorking;
        private readonly ILogger _logger;
        private readonly ICommandLine _commandLine;

        public StepBackCommand(ILogger logger, ICommandLine commandLine)
        {
            _logger = logger;
            _commandLine = commandLine;
        }

        public void Execute()
        {
            _logger.Information("Step back command start");
            var currentPath = _commandLine.Args.Replace($"{CommandIdentifier}", "");
            _isWorking = true;
            while (_isWorking)
            {
                try
                {
                    var stringToSplit = currentPath;
                    _commandLine.PathBuilder.Clear();
                    _commandLine.PathBuilder.Append(currentPath);
                    
                    var allString = currentPath.Length - 1;

                    string[] separatedStringArray = new string[0];
                    if (stringToSplit.Contains('\\'))
                    {
                        separatedStringArray = stringToSplit.Split('\\');
                    }
                    if (stringToSplit.Contains('/'))
                    {
                        separatedStringArray = stringToSplit.Split('/');
                    }
                    
                    for (int i = 0; i < separatedStringArray.Length; i++)
                    {
                        if (separatedStringArray[i] == string.Empty)
                        {
                            separatedStringArray[i] = null;
                        }
                    }
                    
                    //Пересобираем массив
                    separatedStringArray = separatedStringArray.Where(x => x != null).ToArray();
                    
                    var index = separatedStringArray.Length - 1;

                    int deleteString = separatedStringArray[index].Length;

                    var startIndex = (allString - deleteString);

                    _commandLine.PathBuilder.Remove(startIndex, deleteString + 1);
                    _commandLine.Args = _commandLine.PathBuilder.ToString();
                }
                catch(Exception ex)
                {
                    _logger.Error($"{ex}");
                }

                _logger.Information("Step back command stop");
                break;
            }
        }
    }
}