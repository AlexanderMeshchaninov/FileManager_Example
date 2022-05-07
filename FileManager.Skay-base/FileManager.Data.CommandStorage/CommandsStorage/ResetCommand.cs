using System;
using FileManager.Core.CommandLine;
using FileManager.Core.Data;
using Serilog;

namespace FileManager.Data.CommandStorage.CommandsStorage
{
    public sealed class ResetCommand : ICommands
    {
        public Guid Type { get; set; }
        public string CommandIdentifier { get; set; }

        private readonly ILogger _logger;
        private readonly ICommandLine _commandLine;
        public ResetCommand(ILogger logger, ICommandLine commandLine)
        {
            _logger = logger;
            _commandLine = commandLine;
        }

        public void Execute()
        {
            _logger.Information("Reset command line start");
            _commandLine.Args = string.Empty;
            _commandLine.PathBuilder.Clear();
            _logger.Information("Reset command line stop");
        }
    }
}