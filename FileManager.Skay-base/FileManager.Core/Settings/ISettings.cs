namespace FileManager.Core.Settings
{
    public interface ISettings
    { 
        int HorizontalPosition { get; set; }
        int VerticalPosition { get; set; }
        int MiddlePosition { get; set; }
        int MaxOutputElements { get; set; }
        string DefaultApplicationName { get; set; }
        string ShowTime { get; set; }
        string ShowLocalDisksBar { get; set; }
        string ShowApplicationName { get; set; }

        void SaveCommandLineStringAsync(string inputContent);
        string LoadCommandLineStringAsync();
    }
}