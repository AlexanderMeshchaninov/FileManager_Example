using System;
using System.Collections.Generic;
using FileManager.CommonLogic.InformationProvider;
using FileManager.Core.Data;
using FileManager.Core.Facades;
using FileManager.Core.Response;
using FileManager.Data.CommandRepository;
using Serilog;

namespace FileManager.CommonLogic.Facades
{
    public sealed class Facade : IFacade
    {
        private readonly ILogger _logger;
        private readonly IInformationProvider _informationProvider;

        public Facade(
            ILogger logger,
            IInformationProvider informationProvider)
        {
            _logger = logger;
            _informationProvider = informationProvider;
        }

        public AbstractBaseDto InformationProviderFacade(string args)
        {
            _logger.Information($"Enter request facade with args [{args}] start");
            if (!string.IsNullOrEmpty(args))
            {
                var infoProviderResponse = _informationProvider.RequestToProvider(args);
                if (infoProviderResponse is not null)
                {
                    try
                    {
                        _logger.Information($"Enter request facade with response");
                        return infoProviderResponse;
                    }
                    catch (NullReferenceException ex)
                    {
                        _logger.Error($"{ex}");
                        return null;
                    }
                }
                _logger.Information($"Enter request facade with args [{args}] stop");
                return null;
            }
            _logger.Information($"Enter request facade with stop empty args");
            return null;
        }
        public ICommands CommandRepositoryFacade(IReadOnlyCollection<ICommands> commands, string args)
        {
            _logger.Information("Command repository facade start");
            try
            {
                //Начало команды $
                //Конец команды -
                int startIndex = args.IndexOf("#");
                int endIndex = args.IndexOf("$");
                int length = endIndex - startIndex;
                string pureCommand = args.Substring(startIndex, length + 1);
                
                //TODO:
                var repository = new CommandRepository(commands, _logger);
                var commandToExecute = repository.GetByCommand(pureCommand);
                if (commandToExecute is not null)
                {
                    _logger.Information("Command repository facade return command");
                    return commandToExecute;
                }
                _logger.Information("Command repository facade return null");
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