using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskTracker.Utilities
{
    public static class Utility
    {
        public static void PrintInfoMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + message);
            Console.ResetColor();
        }

        public static void PrintHelpMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n" + message);
            Console.ResetColor();
        }

        public static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n" + message);
            Console.ResetColor();
        }

        public static void PrintCommandMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n" + message + "\n");
            Console.ResetColor();
        }


        public static void PrintNumberMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static List<string> ParseInput(string input)
        {
            var commandArgs = new List<string>();

            // Regex to match arguments, including those inside quotes
            var regex = new Regex(@"[\""].+?[\""]|[^ ]+");
            var matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                // Remove surrounding quotes if any
                string value = match.Value.Trim('"');
                commandArgs.Add(value);
            }

            return commandArgs;
        }

        public static void ClearConsole()
        {
            Console.Clear(); 
        }
    }
}
