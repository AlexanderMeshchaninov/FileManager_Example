using System.Collections.Generic;
using FileManager.Core.Data;
using FileManager.Core.Response;

namespace FileManager.Core.Facades
{
    public interface IFacade
    {
        AbstractBaseDto InformationProviderFacade(string args);
        ICommands CommandRepositoryFacade(IReadOnlyCollection<ICommands> commands, string args);
    }
}