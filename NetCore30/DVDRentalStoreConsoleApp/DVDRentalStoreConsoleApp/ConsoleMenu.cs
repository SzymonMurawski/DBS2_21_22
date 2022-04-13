using System;
using System.Collections.Generic;
using System.Text;

namespace DVDRentalStoreConsoleApp
{
    class ConsoleMenu
    {
        public int CurrentElement { get; set; }
        private ConsoleMenuElement[] MenuElements { get; set; }
        public ConsoleMenu(ConsoleMenuElement[] menuElements)
        {
            CurrentElement = 0;
            MenuElements = menuElements;
        }
        public void PrintMenu()
        {
            for (int i = 0; i < MenuElements.Length; i++)
            {
                if (i == CurrentElement)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(MenuElements[i]);
                Console.ResetColor();
            }
        }
        public void RunMenu()
        {
            while (true)
            {
                PrintMenu();
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        CurrentElement = (CurrentElement - 1 + MenuElements.Length) % MenuElements.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        CurrentElement = (CurrentElement + 1) % MenuElements.Length;
                        break;
                    case ConsoleKey.Enter:
                        MenuElements[CurrentElement].ActionToRun();
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                        break;
                }
                Console.Clear();
            }
        }
    }
}
