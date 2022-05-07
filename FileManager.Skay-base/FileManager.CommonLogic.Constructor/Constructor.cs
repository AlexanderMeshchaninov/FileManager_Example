using System;
using System.Threading;
using FileManager.Core.Constructor;

namespace FileManager.CommonLogic.Constructor
{
    public sealed class Constructor : IConstructor
    {
        public void SetColorElement(ConsoleColor backgroundColor, ConsoleColor textColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = textColor;
        }
        public void SetColorsDefault()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void SetElementPosition(int horizontal = 0, int vertical = 0)
        {
            Console.SetCursorPosition(horizontal, vertical);
        }
        public void SetElement(string inputTitle = "")
        {
            Console.Write(inputTitle);
        }
        public void ClearLayer()
        {
            Console.Clear();
        }
        public void SetPageElement(int horizontal, int vertical, int page)
        {
            int innerPage = page;
            SetColorsDefault();
            SetElementPosition(horizontal, vertical);
            SetElement($"page <{innerPage + 1}>");
        }
        public void SetNextPageMessage(int horizontal, int vertical)
        {
            SetColorElement(ConsoleColor.Blue, ConsoleColor.Green);
            SetElementPosition(horizontal, vertical);
            SetElement("Press any key to go next page [Press <Esc> for exit]");
            SetColorsDefault();
        }
        public bool IsExitButton()
        {
            ConsoleKeyInfo escKey;
            escKey = Console.ReadKey();
            if (escKey.Key == ConsoleKey.Escape)
            {
                ClearLayer();
                return true;
            }

            return false;
        }
        public void SetPercentageScale(int horizontal = 0, int vertical = 0)
        {
            var percentageThread = new Thread(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Console.SetCursorPosition(horizontal, vertical);
                    Console.WriteLine("Process {0}%", i);
                    Thread.Sleep(100);
                }
            });
            percentageThread.Start();
            percentageThread.Join();
        }
        public void HideCursor()
        {
            Console.CursorVisible = false;
        }
    }
}
