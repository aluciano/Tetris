using System;
using Tetris.Util;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleWindowPositionOnScreen.SetConsoleWindowPositionOnScreen();

            Tetris tetris = new Tetris();
            Console.ReadKey();
        }
    }
}
