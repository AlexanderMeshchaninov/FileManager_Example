using System.Collections.Generic;

namespace FileManager.Core.Data
{
    public interface IInitializeCommands
    {
        IReadOnlyCollection<ICommands> Initialize();
    }
}