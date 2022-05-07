using System.Collections.Generic;
using System.IO;
using FileManager.Core.Response;

namespace FileManager.Core.Rendering
{
    public interface IRendering
    {
        void MainScreenRender();
        void CommandLineRender();
        void FilesAndFoldersSystemRender(IReadOnlyList<FileSystemInfo> existsFilesAndFolders);
        void FilesAndFoldersInfoBarRender(int horizontal, AbstractBaseDto response);
    }
}