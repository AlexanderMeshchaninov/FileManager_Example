using System.Collections.Generic;
using System.IO;
using System.Text;
using FileManager.Core.Response;

namespace FileManager.CommonLogic.InformationProvider.Responses
{
    public class ResponseFromInfoProvider : AbstractBaseDto
    {
        public override long FileSize { get; set; }
        public override double DirectorySize { get; set; }
        public override int NumberOfFiles { get; set; }
        public override int NumberOfFolders { get; set; }
        public override int DayCreation { get; set; }
        public override int MonthCreation { get; set; }
        public override int YearCreation { get; set; }
        public override StringBuilder LogicalDrives { get; set; }
        public override DirectoryInfo DirectoryInfo { get; set; }
        public override FileInfo FileInfo { get; set; }
        public override IReadOnlyList<FileSystemInfo> ExistsFilesAndFolders { get; set; }
        public override string Suffix { get; set; }
    }
}