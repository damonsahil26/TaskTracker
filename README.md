# Task Tracker Console Application
Project Task URL : https://roadmap.sh/projects/task-tracker

.NET 8 Console app solution for the task-tracker [challenge](https://roadmap.sh/projects/task-tracker) from [roadmap.sh](https://roadmap.sh/).

Task Tracker is a simple console-based application designed to help you manage and track tasks. This application allows you to add, update, delete, and list tasks with various statuses such as "To-Do," "In-Progress," and "Done."

## Features

- **Add a New Task**: Create new tasks with a simple command.
- **Update Task**: Modify the description of an existing task.
- **Delete Task**: Remove tasks by their ID.
- **List Tasks**: Display all tasks or filter tasks by status.
- **Change Task Status**: Mark tasks as "To-Do," "In-Progress," or "Done."
- **Help Command**: Display a list of available commands.
- **Clear Console**: Clear the console and display the welcome message again.

## Installation

To run this application, follow these steps:

1. Clone this repository:
    ```bash
    git clone https://github.com/damonsahil26/TaskTracker.git
    ```

2. Navigate to the project directory:
    ```bash
    cd TaskTracker
    ```

3. Restore dependencies:
    ```bash
    dotnet restore
    ```

4. Build the project:
    ```bash
    dotnet build
    ```

5. Run the application:
    ```bash
    dotnet run
    ```

## Usage

After running the application, you will be greeted with a welcome message. You can then start entering commands.

### Available Commands

- **help**: Displays a list of all available commands.
- **add [description]**: Adds a new task with the provided description.
- **update [id] [new description]**: Updates the task with the given ID.
- **delete [id]**: Deletes the task with the given ID.
- **list**: Lists all tasks.
- **list [status]**: Lists tasks filtered by status ("todo", "in-progress", "done").
- **mark-todo [id]**: Marks the task with the given ID as "To-Do".
- **mark-in-progress [id]**: Marks the task with the given ID as "In-Progress".
- **mark-done [id]**: Marks the task with the given ID as "Done".
- **clear**: Clears the console and redisplays the welcome message.
- **exit**: Exits the application.

### Example Usage

```bash
Enter command : add "Finish the report"
Task added successfully with Id : 1

Enter command : list
Task Id          Description                         Status          Created Date    
1               Finish the report                   todo            25-08-2024

Enter command : mark-in-progress 1
Task status set successfully with Id : 1

Enter command : list in-progress
Task Id          Description                         Status          Created Date    
1               Finish the report                   in-progress      25-08-2024

Enter command : exit
