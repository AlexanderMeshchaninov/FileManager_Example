using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FileManager.Core.CommandLine;
using FileManager.Core.Constructor;
using FileManager.Core.Data;
using FileManager.Core.Rendering;
using FileManager.Core.Settings;
using FileManager.Data.CommandStorage.CommandsStorage;
using Serilog;

namespace FileManager.Data.CommandStorage.CommandsInitialize
{
    public sealed class InitializeCommands : IInitializeCommands
    {
        private readonly ILogger _logger;
        private readonly IRendering _render;
        private readonly ICommandLine _commandLine;
        private readonly IConstructor _constructor;
        private readonly ISettings _settings;
        private readonly ICommandsMessages _messages;
        
        public InitializeCommands(
            ILogger logger,
            IRendering render,
            ICommandLine commandLine,
            IConstructor constructor,
            ISettings settings,
            ICommandsMessages messages)
        {
            _logger = logger;
            _render = render;
            _commandLine = commandLine;
            _constructor = constructor;
            _settings = settings;
            _messages = messages;
        }
        public IReadOnlyCollection<ICommands> Initialize()
        {
            var exit = new ExitCommand(_logger, _settings, _commandLine);
            var help = new HelpCommand(_logger, _constructor, _settings);
            var res = new ResetCommand(_logger, _commandLine);
            var copyF = new CopyFileCommand(_logger, _constructor, _settings, _commandLine, _messages);
            var crF = new CreateFolderCommand(_logger, _constructor, _settings, _commandLine);
            var delF = new DeleteFileCommand(_logger, _constructor, _settings, _commandLine, _messages);
            var opF = new OpenFileCommand(_logger, _constructor, _settings, _commandLine, _messages);
            var stpB = new StepBackCommand(_logger, _commandLine);
            var copyAll = new CopyAllFolderCommand(_logger, _constructor, _settings, _commandLine, _messages);
            var delAll = new DeleteAllFolderCommand(_logger, _constructor, _settings, _commandLine, _messages);
            
            //Комманды
            exit.CommandIdentifier = "#exit$";
            help.CommandIdentifier = "#help$";
            res.CommandIdentifier = "#res$";
            copyF.CommandIdentifier = "#copyfile$";
            crF.CommandIdentifier = "#createfolder$";
            delF.CommandIdentifier = "#deletefile$";
            opF.CommandIdentifier = "#openfile$";
            stpB.CommandIdentifier = "#cd..$";
            copyAll.CommandIdentifier = "#copyallfolder$";
            delAll.CommandIdentifier = "#deleteallfolder$";
            
            var commandsCollection = new Collection<ICommands>()
            {
                exit, help, res, copyF, crF, delF, opF, stpB, copyAll, delAll,
            };
            
            foreach (var command in commandsCollection) command.Type = Guid.NewGuid();

            return commandsCollection;
        }
    }
}