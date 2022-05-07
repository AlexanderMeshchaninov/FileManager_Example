using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FileManager.CommonLogic.InformationProvider.Responses;
using FileManager.Core.InformationProvider;
using Serilog;

namespace FileManager.CommonLogic.InformationProvider
{
    public interface IInformationProvider : IInformationProvider<ResponseFromInfoProvider>
    {
    }

    public sealed class InformationProvider : BaseInformationProvider, IInformationProvider
    {
        public StringBuilder LogicalDrives
        {
            get => _logicalDrives;
        }

        private readonly ILogger _logger;
        
        public InformationProvider(
            ILogger logger)
        {
            _logger = logger;
            
            _logicalDrives = new StringBuilder();
            GetExistsLogicalDrives();
        }
        
        public ResponseFromInfoProvider RequestToProvider(string args)
        {
            try
            {
                _logger.Information($"Request with [args {args}] to information provider start");
                if (!string.IsNullOrEmpty(args))
                {
                    _existsFilesAndFolders = new List<FileSystemInfo>();
                    _directoryInfo = new DirectoryInfo(args);
                    _fileInfo = new FileInfo(args);
                    
                    GetFileSystemEntries(args);
                    GetFilesSize(_directoryInfo);
                    GetFilesCount(_directoryInfo);
                    GetFoldersCount(_directoryInfo);
                    GetCreationDate(_directoryInfo);

                    var response = new ResponseFromInfoProvider()
                    {
                        FileSize = _fileSize,
                        Suffix = _suffix,
                        DirectorySize = _directorySize,
                        NumberOfFiles = _numberOfFiles,
                        NumberOfFolders = _numberOfFolders,
                        DayCreation = _day,
                        MonthCreation = _month,
                        YearCreation = _year,
                        LogicalDrives = _logicalDrives,
                        DirectoryInfo = _directoryInfo,
                        FileInfo = _fileInfo,
                        ExistsFilesAndFolders = _existsFilesAndFolders,
                    };

                    _logger.Information($"Request with [args {args}] to information provider stop successfully");
                    return response;
                }
                _logger.Information($"Request to information provider stop with empty args");
                return null;
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex}");
                return null;
            }
        }
        
        protected override void GetCreationDate(DirectoryInfo dirInfo)
        {
            _day = dirInfo.CreationTime.Day;
            _month = dirInfo.CreationTime.Month;
            _year = dirInfo.CreationTime.Year;
        }
        
        protected override void GetExistsLogicalDrives()
        {
            string[] logicalDrives = Directory.GetLogicalDrives();

            foreach (string drives in logicalDrives)
            {
                _logicalDrives.Append("[");

                _logicalDrives.Append(drives);

                _logicalDrives.Append("]");

                _logicalDrives.Append(" ");
            }
        }
        
        protected override void GetFilesSize(DirectoryInfo dirInfo)
        {
            _fileSize = 0;
            
            //Если информацию о файле присутствует
            if (_fileInfo.Exists)
            {
                _fileSize += _fileInfo.Length;
                ConvertBytes(_fileSize);
            }
            
            FileInfo[] currentFileInfo = dirInfo.GetFiles();

            foreach (FileInfo fileSize in currentFileInfo)
            {
                _fileSize += fileSize.Length;
            }

            ConvertBytes(_fileSize);
        }
        
        protected override void GetFilesCount(DirectoryInfo dirInfo)
        {
            if (dirInfo.Exists == false)
            {
                _numberOfFiles = 0;
            }

            var filesNumber = new List<FileInfo>();

            foreach (var files in dirInfo.GetFiles())
            {
                filesNumber.Add(files);
            }

            _numberOfFiles = filesNumber.Count;

            filesNumber.Clear();
        }
        
        protected override void GetFoldersCount(DirectoryInfo dirInfo)
        {
            if (dirInfo.Exists == false)
            {
                _numberOfFolders = 0;
            }

            List<DirectoryInfo> foldersNumber = new List<DirectoryInfo>();

            foreach (var folders in dirInfo.GetDirectories())
            {
                foldersNumber.Add(folders);
            }

            _numberOfFolders = foldersNumber.Count;

            foldersNumber.Clear();
        }
        
        protected override void ConvertBytes(long bytes)
        {
            string[] suffix = { "B", "KB", "MB", "GB", "TB" };
            double doubleSbyte = bytes;
            int i;
            
            for (i = 0; i < suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                doubleSbyte = bytes / 1024.0;
            }
            
            _suffix = suffix[i];
            _directorySize = doubleSbyte;
        }
        
        protected override void GetFileSystemEntries(string args)
        {
            foreach (string folders in Directory.GetDirectories(args))
            {
                _existsFilesAndFolders.Add(new DirectoryInfo(folders));
            }
            
            foreach (string files in Directory.GetFiles(args))
            {
                _existsFilesAndFolders.Add(new FileInfo(files));
            }
        }
    }
}
