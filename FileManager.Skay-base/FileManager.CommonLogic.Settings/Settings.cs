using System;
using Serilog;
using FileManager.Core.Settings;
using System.Configuration;
using System.IO;
using System.Text.Json;

namespace FileManager.CommonLogic.Settings
{
    public sealed class Settings : AbstractBaseSettings, ISettings
    {
        public int HorizontalPosition { get; set; }
        public int VerticalPosition { get; set; }
        public int MiddlePosition { get; set; }
        public int MaxOutputElements { get; set; }
        public string DefaultApplicationName { get; set; }
        public string ShowTime { get; set; }
        public string ShowLocalDisksBar { get; set; }
        public string ShowApplicationName { get; set; }

        private readonly ILogger _logger;
        
        public Settings(ILogger logger)
        {
            _logger = logger;
            
            _logger.Information("Initialize settings start");
            
            //Загрузка параметров из App.config
            DefaultApplicationName = ReadSettingByKey("applicationName");
            MaxOutputElements = int.Parse(ReadSettingByKey("maxOutputElements"));
            ShowTime = ReadSettingByKey("showTime");
            ShowLocalDisksBar = ReadSettingByKey("showLocalDisksBar");
            ShowApplicationName = ReadSettingByKey("showApplicationName");
            
            //Определение размера по горизонтали, по вертикали и середина консоли
            HorizontalPosition = (Console.WindowWidth);
            VerticalPosition = (Console.WindowHeight);
            MiddlePosition = (HorizontalPosition / 2);
            
            Console.Title = $"Welcome to {DefaultApplicationName}";
            
            _logger.Information("Initialize settings stop");
        }
        public void SaveCommandLineStringAsync(string inputContent)
        {
            string saveFileName = "Skay_Base_commandLine.json";
            var currentWorkingDir = Directory.GetCurrentDirectory();

            var saveFile = Path.Combine(currentWorkingDir, saveFileName);

            if (!File.Exists(saveFile))
            {
                File.Create($"{saveFileName}");
            }

            string json = JsonSerializer.Serialize(inputContent);
            File.WriteAllText($"{saveFileName}", json);
        }
        public string LoadCommandLineStringAsync()
        {
            string saveFileName = "Skay_Base_commandLine.json";
            var currentWorkingDir = Directory.GetCurrentDirectory();

            var saveFile = Path.Combine(currentWorkingDir, saveFileName);

            if (!File.Exists(saveFile))
            {
                File.Create($"{saveFileName}").Close();
            }

            string json = File.ReadAllText($"{saveFileName}");

            if (json != string.Empty)
            {
                string deserializedString = JsonSerializer.Deserialize<string>(json);
                return deserializedString;
            }
            return string.Empty;
        }
        protected override string ReadSettingByKey(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (!string.IsNullOrEmpty(key))
                {
                    var result = appSettings[key];
                    return result;
                }

                _logger.Warning("ReadSettingByKeyMethod return empty");
                return string.Empty;
            }
            catch (ConfigurationErrorsException ex)
            {
                _logger.Error($"{ex}");
                return string.Empty;
            }
        }
    }
}