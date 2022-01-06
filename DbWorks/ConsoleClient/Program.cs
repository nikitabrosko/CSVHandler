using System;

namespace ConsoleClient
{
    public class Program
    {
        private static void Main()
        {
            var consoleApp = new ConsoleApp();
            Console.WriteLine("Starting application...");
            consoleApp.Start();
            Console.ReadKey(true);
            Console.WriteLine("Stopping application...");
            consoleApp.Stop();
            Console.ReadKey();
        }
    }
}