using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FileManager.CommonLogic.InformationProvider;
using FileManager.Core.CommandLine;
using FileManager.Core.Constructor;
using FileManager.Core.Rendering;
using FileManager.Core.Response;
using FileManager.Core.Settings;
using Serilog;

namespace FileManager.CommonLogic.Rendering
{
    public sealed class Rendering : AbstractBaseRenderElements, IRendering
    {
        private readonly ILogger _logger;
        private readonly ISettings _settings;
        private readonly IConstructor _constructor;
        private readonly IInformationProvider _informationProvider;
        private readonly ICommandLine _commandLine;
        public Rendering(
            ILogger logger, 
            ISettings settings, 
            IConstructor constructor,
            IInformationProvider  informationProvider,
            ICommandLine commandLine)
        {
            _logger = logger;
            _settings = settings;
            _constructor = constructor;
            _informationProvider = informationProvider;
            _commandLine = commandLine;
        }

        public void MainScreenRender()
        {
            try
            {
                _logger.Information("Menu rendering start");
                
                int appNameLength = _settings.DefaultApplicationName.Length - 2;
                int timeModuleLength = DateTime.Now.ToString().Length;
            
                //Удаляется предыдущий "слой"
                _constructor.ClearLayer();
            
                //Установка название приложения в консоли
                if (_settings.ShowApplicationName.Contains("true".ToLower()))
                {
                    ShowConsoleTitle(_settings.MiddlePosition - appNameLength, 0);
                    _logger.Information("LocalDiskBar initiated");
                }
            
                //Установка времени
                if (_settings.ShowTime.Contains("true".ToLower()))
                {
                    ShowTime(_settings.MiddlePosition + timeModuleLength, 0);
                    _logger.Information("ShowTime initiated");
                }

                //Установка отображения логических дисков и подключенных устройств
                if (_settings.ShowLocalDisksBar.Contains("true".ToLower()))
                {
                    ShowExistsLogicalDrives(0, _settings.VerticalPosition - 3, _informationProvider.LogicalDrives);
                    _logger.Information("LocalDiskBar initiated");
                }
                
                _logger.Information("Menu rendering stop");
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex}");
            }
        }
        public void CommandLineRender()
        {
            try
            {
                _logger.Information("Command line navigator start");
                
                _commandLine.Navigate();
                _constructor.SetColorsDefault();
                
                _logger.Information("Command line navigator stop");
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex}");
            }
        }
        public void FilesAndFoldersSystemRender(IReadOnlyList<FileSystemInfo> existsFilesAndFolders)
        {
            _logger.Information("Files and Folders facade start");
            if (existsFilesAndFolders.Count is not 0)
            {
                try
                {
                    ShowCurrentDirectoryFilesAndFolders(existsFilesAndFolders, 3);
                    _constructor.SetColorsDefault();
                }
                catch (Exception ex)
                {
                    _logger.Error($"Files and Folders facade stop with {ex}");
                }
            }
            
            _logger.Information("Files and Folders facade start");
        }
        public void FilesAndFoldersInfoBarRender(int horizontal, AbstractBaseDto response)
        {
            //Directory size bar
            _constructor.SetColorsDefault();
            _constructor.SetElementPosition(horizontal, _settings.VerticalPosition - 7);
            _constructor.SetElement($"Size: " +
                                    $"{response.DirectorySize} " +
                                    $"{response.Suffix}");
            _constructor.SetColorsDefault();
        
            //Number of files size bar
            _constructor.SetColorsDefault();
            _constructor.SetElementPosition(horizontal, _settings.VerticalPosition - 6);
            _constructor.SetElement($"Files: " +
                                    $"{response.NumberOfFiles}");
            _constructor.SetColorsDefault();
        
            //Number of folders size bar
            _constructor.SetColorsDefault();
            _constructor.SetElementPosition(horizontal, _settings.VerticalPosition - 5);
            _constructor.SetElement($"Folders: " +
                                    $"{response.NumberOfFolders}");
            _constructor.SetColorsDefault();
        
            //Folder creation date bar
            _constructor.SetColorsDefault();
            _constructor.SetElementPosition(horizontal, _settings.VerticalPosition - 4);
            _constructor.SetElement($"Folder creation: " +
                                    $"{response.DayCreation}/" +
                                    $"{response.MonthCreation}/" +
                                    $"{response.YearCreation}");
            _constructor.SetColorsDefault();
        
            //File creation date bar
            _constructor.SetColorsDefault();
            _constructor.SetElementPosition(horizontal, _settings.VerticalPosition - 3);
            _constructor.SetElement($"File creation: " +
                                    $"{response.DayCreation}/" +
                                    $"{response.MonthCreation}/" +
                                    $"{response.YearCreation}");
            _constructor.SetColorsDefault();
        }
        protected override void ShowCurrentDirectoryFilesAndFolders(IReadOnlyList<FileSystemInfo> filesList, int startPosition)
        {
            int rightColumnMaxLength = _settings.MiddlePosition;
            int columnLeft = startPosition;
            int columnRight = startPosition;
            int buffer = 0;
            int page = 0;
            
            _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Black);
            _constructor.SetElementPosition(_settings.MiddlePosition - 28, 1);
            _constructor.SetElement("/FOLDERS/");
            _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Green);
            _constructor.SetElementPosition(_settings.MiddlePosition + 28, 1);
            _constructor.SetElement("/FILES/");
            
            foreach (var currentElement in filesList)
            {
                var typeOfFileOrDirectory = DisplayFileSystemInfoAttributes(currentElement);
                
                //Колонка папок
                if (typeOfFileOrDirectory.Contains("Directory"))
                {
                    _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Black);
                    _constructor.SetElementPosition(10, columnLeft);

                    if (currentElement.Name.Length > _settings.MiddlePosition)
                    {
                        var firstPart = CutString(
                            currentElement.Name, 
                            _settings.MiddlePosition - 9);
                        _constructor.SetElement($"{firstPart}...");
                    }
                    else
                    {
                        _constructor.SetElement($"{currentElement.Name}");
                    }
                    
                    columnLeft++;
                }
                //Колонка файлов
                if (typeOfFileOrDirectory.Contains("File"))
                {
                    _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Green);
                    _constructor.SetElementPosition(_settings.MiddlePosition, columnRight);
                    
                    if (currentElement.Name.Length > _settings.MiddlePosition - 5)
                    {
                        var firstPart = CutString(
                            currentElement.Name, 
                            rightColumnMaxLength - 8);
                        _constructor.SetElement($"{firstPart}...");
                    }
                    else
                    {
                        _constructor.SetElement($"{currentElement.Name}");
                    }
                    
                    columnRight++;
                }
                
                if (buffer >= 28)
                {
                    string pathStringLength = $"PATH: {_commandLine.Args}";
                    _constructor.SetElementPosition(0, _settings.VerticalPosition - 1);
                    _constructor.SetColorsDefault();
                    _constructor.SetElement($"{pathStringLength}");
                    
                    _constructor.SetNextPageMessage(_settings.MiddlePosition - 25, 0);
                    _constructor.HideCursor();
                    var exit = _constructor.IsExitButton();
                    if (exit)
                    {
                        break;
                    }

                    //При переходе на новую страницу все обновляется
                    _constructor.ClearLayer();
                
                    _constructor.SetPageElement(_settings.MiddlePosition - 2, _settings.VerticalPosition - 3, page);
                
                    columnLeft = startPosition;
                    columnRight = startPosition;
                
                    _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Black);
                    _constructor.SetElementPosition(_settings.MiddlePosition - 28, 1);
                    _constructor.SetElement("/FOLDERS/");
                    _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Green);
                    _constructor.SetElementPosition(_settings.MiddlePosition + 28, 1);
                    _constructor.SetElement("/FILES/");
                    buffer = 0;
                    buffer--;
                }
                buffer++;
            }
        }
        protected override void ShowTime(int horizontal, int vertical)
        {
            _constructor.SetElementPosition(horizontal, vertical);
            _constructor.SetElement($"{DateTime.Now}");
            _constructor.SetColorsDefault();
        }
        protected override void ShowConsoleTitle(int horizontal, int vertical)
        {
            _constructor.SetElementPosition(horizontal, vertical);
            _constructor.SetElement($"{_settings.DefaultApplicationName}");
            _constructor.SetColorsDefault();
        }
        protected override void ShowExistsLogicalDrives(int horizontal, int vertical, StringBuilder logicalDrives)
        {
            _constructor.SetColorsDefault();
            _constructor.SetElementPosition(horizontal, vertical);
            _constructor.SetElement($"Available logical drives and devices: {logicalDrives}");
            _constructor.SetColorsDefault();
        }
        private string CutString(string inputString, int maxStrLength)
        {
            var sb = new StringBuilder(maxStrLength);
            
            for (int i = 0; i < maxStrLength; i++)
            {
                sb.Append(inputString[i]);
            }
            
            return sb.ToString();
        }
        private string DisplayFileSystemInfoAttributes(FileSystemInfo fileSystemInfo)
        {
            string entryType = "File";
            
            if ((fileSystemInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                entryType = "Directory";
            }
            
            return entryType;
        }
    }
}