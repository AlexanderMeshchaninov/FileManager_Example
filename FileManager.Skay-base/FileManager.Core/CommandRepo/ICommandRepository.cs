using System;
using FileManager.Core.Data;

namespace FileManager.Core.CommandRepo
{
    public interface ICommandRepository
    {
        ICommands GetByType(Guid type);
        ICommands GetByCommand(string args);
    }
}