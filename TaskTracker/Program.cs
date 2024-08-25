using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using TaskTracker.Interfaces;
using TaskTracker.Services;
using TaskTracker.Utilities;

var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection);
var serviceProvider = serviceCollection.BuildServiceProvider();
var _taskService = serviceProvider.GetService<ITaskService>();

Utility.PrintInfoMessage("Hello, Welcome to Task Tracker!");
Utility.PrintInfoMessage("Type \"help\" to know the set of commands");

while (true)
{
    Utility.PrintInfoMessage("Enter command : ");
    string input = Console.ReadLine() ?? string.Empty;

    if (string.IsNullOrEmpty(input))
    {
        Utility.PrintInfoMessage("\n No input detected, Try again!");
        continue;
    }

    string[] commands = input.Trim().Split(' ');

    string command = commands[0].ToLower();

    bool exit = false;

    switch (command)
    {
        case "help":
            PrintHelpCommands();
            break;
        case "exit":
            exit = true;
            break;

        default:
            break;
    }

    if (exit)
    {
        break;
    }

}

void PrintHelpCommands()
{
    var helpCommands = _taskService?.GetAllHelpCommands();
    if (helpCommands != null)
    {
        foreach (var item in helpCommands)
        {
           Utility.PrintHelpMessage(item);
        }
    }
}

 static void ConfigureServices(IServiceCollection services)
{
    // Register services here
    services.AddSingleton<ITaskService, TaskService>();
}