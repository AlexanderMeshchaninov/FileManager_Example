using System;
using System.Collections.Generic;
using System.Linq;
using FileManager.Core.CommandRepo;
using FileManager.Core.Data;
using Serilog;

namespace FileManager.Data.CommandRepository
{
    public sealed class CommandRepository : ICommandRepository
    {
        private readonly IReadOnlyDictionary<Guid, ICommands> _index;
        private readonly IReadOnlyDictionary<string, ICommands> _commands;
        private readonly ILogger _logger;

        public CommandRepository(IReadOnlyCollection<ICommands> commands, ILogger logger)
        {
            _logger = logger;
            _index = commands.ToDictionary(x => x.Type, x => x);
            _commands = commands.ToDictionary(x => x.CommandIdentifier, x => x);
        }
        
        public ICommands GetByType(Guid type)
        {
            _logger.Information("Get by type from command repository start");
            try
            {
                ICommands command;
                if (_index.TryGetValue(type, out command))
                {
                    _logger.Information("Get by type from command repository return type successfully");
                    return command;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex}");
                return null;
            }
            
            _logger.Information("Get by type from command repository return null");
            return null;
        }
        public ICommands GetByCommand(string args)
        {
            _logger.Information("Get by command from command repository start");

            try
            {
                ICommands command;
                if (_commands.TryGetValue(args, out command))
                {
                    _logger.Information("Get by command from command repository return command");
                    return command;
                }

                _logger.Information("Get by command from command repository return null");
                return null;
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex}");
                return null;
            }
        }
    }
}