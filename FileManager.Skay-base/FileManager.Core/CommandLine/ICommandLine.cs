using System.Text;

namespace FileManager.Core.CommandLine
{
    public interface ICommandLine
    {
        string Args { get; set; }
        StringBuilder PathBuilder { get; set; }

        void Navigate();
    }
}