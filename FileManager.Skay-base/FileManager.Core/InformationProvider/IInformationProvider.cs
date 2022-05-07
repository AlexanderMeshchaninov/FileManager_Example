using System.Text;

namespace FileManager.Core.InformationProvider
{
    public interface IInformationProvider<TResponse> where TResponse : class
    {
        public StringBuilder LogicalDrives { get; }
        TResponse RequestToProvider(string args);
    }
}