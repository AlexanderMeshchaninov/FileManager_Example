using System;

namespace FileManager.Core.Constructor
{
    public interface IConstructor
    {
        void SetColorElement(ConsoleColor backgroundColor, ConsoleColor textColor);
        void SetColorsDefault();
        void SetElementPosition(int horizontal = 0, int vertical = 0);
        void SetElement(string inputTitle);
        void ClearLayer();
        void SetPageElement(int horizontal, int vertical, int page);
        void SetNextPageMessage(int horizontal, int vertical);
        bool IsExitButton();
        void SetPercentageScale(int horizontal = 0, int vertical = 0);
        void HideCursor();
    }
}
