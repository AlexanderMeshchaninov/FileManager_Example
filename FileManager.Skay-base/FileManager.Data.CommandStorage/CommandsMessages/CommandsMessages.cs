using System;
using FileManager.Core.Constructor;
using FileManager.Core.Data;
using FileManager.Core.Settings;

namespace FileManager.Data.CommandStorage.CommandsMessages
{
    public sealed class CommandsMessages : ICommandsMessages
    {
        private readonly IConstructor _constructor;
        private readonly ISettings _settings;

        public CommandsMessages(IConstructor constructor, ISettings settings)
        {
            _constructor = constructor;
            _settings = settings;
        }

        public void FromToMessage(string currentDirectory)
        {
            _constructor.ClearLayer();
            _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
            _constructor.SetElement($"FROM: {currentDirectory}");
            _constructor.SetElementPosition(_settings.MiddlePosition - 13, 2);
            _constructor.SetElement($"TO: ");
        }
        public void FolderOrFileNameMessage(string folderOrFileName)
        {
            _constructor.ClearLayer();
            _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
            _constructor.SetElement($"{folderOrFileName} name: ");
        }
        public void CopySuccessMessage(string folderOrFileName)
        {
            _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
            _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Green);
            _constructor.SetElement($"{folderOrFileName} successfully copied");
            Console.ReadKey();
            _constructor.ClearLayer();
            _constructor.SetColorsDefault();
        }
        public void DeleteSuccessMessage(string folderOrFileName)
        {
            _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
            _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Green);
            _constructor.SetElement($"{folderOrFileName} successfully delete");
            Console.ReadKey();
            _constructor.ClearLayer();
            _constructor.SetColorsDefault();
        }
        public void NotCopiedMessage(string folderOrFileName)
        {
            _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
            _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Red);
            _constructor.SetElement($"{folderOrFileName} has not been copied");
            Console.ReadKey();
            _constructor.ClearLayer();
            _constructor.SetColorsDefault();
        }
        public void NotDeletedMessage(string folderOrFileName)
        {
            _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
            _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Red);
            _constructor.SetElement($"{folderOrFileName} has not been deleted");
            Console.ReadKey();
            _constructor.ClearLayer();
            _constructor.SetColorsDefault();
        }
        public void WrongDestinationMessage()
        {
            _constructor.ClearLayer();
            _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
            _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Red);
            _constructor.SetElement("Wrong destination!");
            Console.ReadKey();
            _constructor.SetColorsDefault();
        }
        public void CopyQuestionMessage(string folderOrFileName)
        {
            _constructor.ClearLayer();
            _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
            _constructor.SetElement($"Copy {folderOrFileName}? [y/n]");
        }
        public void DeleteQuestionMessage(string folderOrFileName)
        {
            _constructor.ClearLayer();
            _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
            _constructor.SetElement($"Delete {folderOrFileName}? [y/n]");
        }
        public void InProgressMessage()
        {
            _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Yellow);
            _constructor.SetElementPosition(_settings.MiddlePosition - 6, 1);
            _constructor.SetElement("In progress...");
        }
        public void OpenQuestionMessage(string folderOrFileName)
        {
            _constructor.ClearLayer();
            _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
            _constructor.SetElement($"Open {folderOrFileName}? [y/n]");
        }
        public void OpenSuccessMessage(string folderOrFileName)
        {
            _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
            _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Green);
            _constructor.SetElement($"{folderOrFileName} successfully opened");
            Console.ReadKey();
            _constructor.ClearLayer();
            _constructor.SetColorsDefault();
        }
        public void NotOpenedMessage(string folderOrFileName)
        {
            _constructor.SetElementPosition(_settings.MiddlePosition - 13, 1);
            _constructor.SetColorElement(ConsoleColor.Blue, ConsoleColor.Red);
            _constructor.SetElement($"{folderOrFileName} has not been opened");
            Console.ReadKey();
            _constructor.ClearLayer();
            _constructor.SetColorsDefault();
        }
    }
}