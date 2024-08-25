using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using TaskTracker.Interfaces;
using TaskTracker.Services;
using TaskTracker.Utilities;

var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection);
var serviceProvider = serviceCollection.BuildServiceProvider();
var _taskService = serviceProvider.GetService<ITaskService>();

DisplayWelcomeMessage();
List<string> commands = [];
while (true)
{
    Utility.PrintCommandMessage("Enter command : ");
    string input = Console.ReadLine() ?? string.Empty;

    if (string.IsNullOrEmpty(input))
    {
        Utility.PrintInfoMessage("\n No input detected, Try again!");
        continue;
    }

    commands = Utility.ParseInput(input);

    string command = commands[0].ToLower();

    bool exit = false;

    switch (command)
    {
        case "help":
            PrintHelpCommands();
            break;

        case "add":
            AddNewTask();
            break;

        case "delete":
            DeleteTask();
            break;

        case "update":
            UpdateTask();
            break;

        case "clear":
            Utility.ClearConsole();
            DisplayWelcomeMessage();
            continue;

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

void UpdateTask()
{
    if (!IsUserInputValid(commands, 3))
    {
        return;
    }

    int id = IsValidIdProvided(commands, 0).Item2;


    if (id == 0)
    {
        return;
    }

    var result = _taskService?.UpdateTask(id, commands[2]).Result;

    if (result != null && result.Value)
    {
        Utility.PrintInfoMessage($"Task updated successfully with Id : {id}");
    }
    else
    {
        Utility.PrintInfoMessage($"Task with Id : {id}, does not exist!");
    }
}

void DeleteTask()
{
    if (!IsUserInputValid(commands, 2))
    {
        return;
    }

    int id = IsValidIdProvided(commands, 0).Item2;

    if (id == 0)
    {
        return;
    }

    var result = _taskService?.DeleteTask(id).Result;

    if (result != null && result.Value)
    {
        Utility.PrintInfoMessage($"Task deleted successfully with Id : {id}");
    }
    else
    {
        Utility.PrintInfoMessage($"Task with Id : {id}, does not exist!");
    }
}

void AddNewTask()
{
    if (!IsUserInputValid(commands, 2))
    {
        return;
    }

    var taskAdded = _taskService?.AddNewTask(commands[1]);

    if (taskAdded != null && taskAdded.Result != 0)
        Utility.PrintInfoMessage($"Task added successfully with Id : {taskAdded.Result}");
    else
        Utility.PrintInfoMessage("Task not saved!");
}


void PrintHelpCommands()
{
    var helpCommands = _taskService?.GetAllHelpCommands();
    int count = 1;
    if (helpCommands != null)
    {
        foreach (var item in helpCommands)
        {
            Utility.PrintHelpMessage(count + ". " + item);
            count++;
        }
    }
}

static void ConfigureServices(IServiceCollection services)
{
    // Register services here
    services.AddSingleton<ITaskService, TaskService>();
}

static bool IsUserInputValid(List<string> commands, int parameterRequired)
{
    if (parameterRequired == 2)
    {
        if (commands.Count != parameterRequired || string.IsNullOrEmpty(commands[1]))
        {
            Utility.PrintErrorMessage("Wrong command! Try again.");
            Utility.PrintInfoMessage("Type \"help\" to know the set of commands");
            return false;
        }
    }

    if (parameterRequired == 3)
    {
        if (commands.Count != parameterRequired || string.IsNullOrEmpty(commands[1]) || string.IsNullOrEmpty(commands[2]))
        {
            Utility.PrintErrorMessage("Wrong command! Try again.");
            Utility.PrintInfoMessage("Type \"help\" to know the set of commands");
            return false;
        }
    }

    return true;
}

static Tuple<bool, int> IsValidIdProvided(List<string> commands, int id)
{
    Int32.TryParse(commands[1], out id);

    if (id == 0)
    {
        Utility.PrintErrorMessage("Wrong command! Try again.");
        Utility.PrintInfoMessage("Type \"help\" to know the set of commands");
        return new Tuple<bool, int>(false, id);
    }

    return new Tuple<bool, int>(true, id);
}

static void DisplayWelcomeMessage()
{
    Utility.PrintInfoMessage("Hello, Welcome to Task Tracker!");
    Utility.PrintInfoMessage("Type \"help\" to know the set of commands");
}