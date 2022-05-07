using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileManager.Core.InformationProvider
{
    public abstract class BaseInformationProvider
    {
        protected long _fileSize;
        protected double _directorySize;
        protected int _numberOfFiles;
        protected int _numberOfFolders;
        protected int _day;
        protected int _month;
        protected int _year;
        protected StringBuilder _logicalDrives;
        protected DirectoryInfo _directoryInfo;
        protected FileInfo _fileInfo;
        protected List<FileSystemInfo> _existsFilesAndFolders;
        protected string _suffix;
        protected abstract void GetCreationDate(DirectoryInfo dirInfo);
        protected abstract void GetExistsLogicalDrives();
        protected abstract void GetFilesSize(DirectoryInfo dirInfo);
        protected abstract void GetFilesCount(DirectoryInfo dirInfo);
        protected abstract void GetFoldersCount(DirectoryInfo dirInfo);
        protected abstract void ConvertBytes(long bytes);
        protected abstract void GetFileSystemEntries(string args);
    }
}