using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using FileManager.Core.CommandLine;
using FileManager.Core.Constructor;
using FileManager.Core.Settings;
using Serilog;

namespace FileManager.CommonLogic.CommandLine
{
    public sealed class CommandLine : ICommandLine
    {
        public string Args { get; set; }
        public StringBuilder PathBuilder { get; set; }
        
        private bool _isWorked;
        private readonly ILogger _logger;
        private readonly IConstructor _constructor;
        private readonly ISettings _settings;
        
        public CommandLine(ILogger logger, IConstructor constructor, ISettings settings)
        {
            _logger = logger;
            _constructor = constructor;
            _settings = settings;
            PathBuilder = new StringBuilder();
            
            PathBuilder.Append(_settings.LoadCommandLineStringAsync());
        }

        public void Navigate()
        {
            _isWorked = true;

            if (PathBuilder.ToString().Contains("cmd".ToLower())) PathBuilder.Replace("cmd", "");
            
            if (PathBuilder.Length + 9 > _settings.HorizontalPosition)
            {
                var firstPart = SplitToLines(PathBuilder.ToString(), _settings.HorizontalPosition);
                PathBuilder.Append(firstPart);
            }
            else
            {
                Args = PathBuilder.ToString();
            }
            
            while (_isWorked)
            {
                _constructor.SetElementPosition(0, _settings.VerticalPosition);
                _constructor.SetElement($"NAVIGATE:{Args}");
                string commandLine = Console.ReadLine();

                if (commandLine is {Length: 0}) break;
                
                PathBuilder.Append(commandLine);
                
                if (commandLine != null && commandLine.Contains("cmd".ToLower()))
                {
                    Cmd();
                    _isWorked = false;
                    break;
                }
            
                try
                {
                    var checkFile = new FileInfo(PathBuilder.ToString());
                    if (Directory.Exists(PathBuilder.ToString()) || checkFile.Exists)
                    {
                        Args = PathBuilder.ToString();
                        _isWorked = false;
                        break;
                    }
                    if (commandLine != null)
                    {
                        string incorrectStr = commandLine;
                        PathBuilder.Replace(incorrectStr, string.Empty);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error($"{ex}");
                    PathBuilder.Clear();
                }
            }
            _constructor.ClearLayer();
            _constructor.SetElementPosition(0, _settings.VerticalPosition - 1);
        }
        
        private void Cmd()
        {
            _isWorked = true;
            while (_isWorked)
            {
                //_constructor.SetElementPosition(0, _settings.VerticalPosition - 1);
                _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.White);
                _constructor.SetElement($"COMMAND:{Args}");

                var commandString = Console.ReadLine();
                if (commandString != null && commandString.Length + 8 > _settings.HorizontalPosition)
                {
                    var firstPart = SplitToLines(commandString, _settings.HorizontalPosition);
                    Args += firstPart;
                }
                else
                {
                    Args += commandString;
                }
                //base.SaveCommandReport(commandString);
                _isWorked = false;
                break;
            }
        }
        private string SplitToLines(string inputString, int stopPosition)
        {
            return Regex.Replace(inputString, ".{"+stopPosition+"}(?!$)", "$0\n");
        }
    }
}