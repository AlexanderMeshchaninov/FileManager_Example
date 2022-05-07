using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileManager.Core.Rendering
{
    public abstract class AbstractBaseRenderElements
    {
        protected abstract void ShowCurrentDirectoryFilesAndFolders(
            IReadOnlyList<FileSystemInfo> filesList, 
            int startPosition);
        protected abstract void ShowTime(int horizontal, int vertical);
        protected abstract void ShowConsoleTitle(int horizontal, int vertical);
        protected abstract void ShowExistsLogicalDrives(
            int horizontal, 
            int vertical, 
            StringBuilder logicalDrives);
    }
}