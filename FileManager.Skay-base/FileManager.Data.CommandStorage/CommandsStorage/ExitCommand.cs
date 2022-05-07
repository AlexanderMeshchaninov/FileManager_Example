using System;
using FileManager.Core.CommandLine;
using FileManager.Core.Data;
using FileManager.Core.Settings;
using Serilog;

namespace FileManager.Data.CommandStorage.CommandsStorage
{
    public sealed class ExitCommand : ICommands
    {
        public Guid Type { get; set; }
        public string CommandIdentifier { get; set; }

        private readonly ILogger _logger;
        private readonly ISettings _settings;
        private readonly ICommandLine _commandLine;
        public ExitCommand(ILogger logger, ISettings settings, ICommandLine commandLine)
        {
            _logger = logger;
            _settings = settings;
            _commandLine = commandLine;
        }

        public void Execute()
        {
            _logger.Information("Exit command start");
            try
            {
                var pathFrom = _commandLine.Args.Replace($"{CommandIdentifier}", "");
                _logger.Information("Exit command stop");
                _settings.SaveCommandLineStringAsync(pathFrom);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex}");
            }
        }
    }
}