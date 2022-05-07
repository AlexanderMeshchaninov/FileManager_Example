using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileManager.Core.Response
{
    public abstract class AbstractBaseDto
    {
        public abstract long FileSize { get; set; }
        public abstract double DirectorySize { get; set; }
        public abstract int NumberOfFiles { get; set; }
        public abstract int NumberOfFolders { get; set; }
        public abstract int DayCreation { get; set; }
        public abstract int MonthCreation { get; set; }
        public abstract int YearCreation { get; set; }
        public abstract StringBuilder LogicalDrives { get; set; }
        public abstract DirectoryInfo DirectoryInfo { get; set; }
        public abstract FileInfo FileInfo { get; set; }
        public abstract IReadOnlyList<FileSystemInfo> ExistsFilesAndFolders { get; set; }
        public abstract string Suffix { get; set; }
    }
}