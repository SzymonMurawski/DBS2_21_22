using System;

namespace ConsoleMenuExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleMenuElement[] menuElements = {
                new ConsoleMenuElement("Option1", Option1),
                new ConsoleMenuElement("Option2", Option2),
                new ConsoleMenuElement("AddTwoNumbers", AddTwoNumbers),
                new ConsoleMenuElement("ExitProgram", ExitProgram),
            };
            ConsoleMenu consoleMenu = new ConsoleMenu(menuElements);
            consoleMenu.RunMenu();
        }

        static void Option1()
        {
            Console.WriteLine("Option 1 selected");

        }
        static void Option2()
        {
            Console.WriteLine("Option 2 selected");

        }
        static void DefaultOption()
        {
            Console.WriteLine("Default option selected");

        }
        static void AddTwoNumbers()
        {
            Console.Write("x = ");
            int x = int.Parse(Console.ReadLine());
            Console.Write("y = ");
            int y = int.Parse(Console.ReadLine());
            Console.WriteLine($"x+y = {x+y}");
        }
        static void ExitProgram()
        {
            Console.WriteLine("Bye bye");
            Environment.Exit(0);
        }
    }
}
