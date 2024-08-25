using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TaskTracker.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class TaskService : ITaskService
    {
        private static string FileName = "task_data.json";
        private static string FilePath = Path.Combine(Directory.GetCurrentDirectory(), FileName);
        public Task<int> AddNewTask(string description)
        {
            try
            {
                var appTasks = new List<AppTask>();
                var task = new AppTask
                {
                    Id = GetTaskId(),
                    Description = description,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    TaskStatus = Enums.Status.todo
                };

                var fileCreatedSuccessfully = CreateFileIfNotExist();

                if (fileCreatedSuccessfully)
                {
                    string tasksFromJsonFileString = File.ReadAllText(FilePath);
                    if (!string.IsNullOrEmpty(tasksFromJsonFileString))
                    {
                        appTasks = JsonSerializer.Deserialize<List<AppTask>>(tasksFromJsonFileString);
                    }

                    appTasks?.Add(task);
                    string updatedAppTasks = JsonSerializer.Serialize<List<AppTask>>(appTasks ?? new List<AppTask>());
                    File.WriteAllText(FilePath, updatedAppTasks);
                    return Task.FromResult(task.Id);
                }

                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Task addition failed. Error - " + ex.Message);
                return Task.FromResult(0);
            }
        }

        private int GetTaskId()
        {
            if (!File.Exists(FilePath))
            {
                return 1;
            }

            else
            {
                string tasksFromJsonFileString = File.ReadAllText(FilePath);
                if (!string.IsNullOrEmpty(tasksFromJsonFileString))
                {
                    var appTasks = JsonSerializer.Deserialize<List<AppTask>>(tasksFromJsonFileString);
                    if (appTasks != null && appTasks.Count > 0) {
                       return appTasks.OrderBy(x => x.Id).Last().Id + 1;
                    }
                }
            }

            return 1;
        }

        public Task<bool> DeleteTask(int id)
        {
            if (!File.Exists(FilePath))
            {
                return Task.FromResult(false);
            }

            var tasksFromJson = GetTasksFromJson();

            if (tasksFromJson.Result.Count > 0)
            {
                var taskToBeDeleted = tasksFromJson.Result
                    .Where(x => x.Id == id)
                    .SingleOrDefault();

                if (taskToBeDeleted != null)
                {
                    tasksFromJson.Result.Remove(taskToBeDeleted);
                    UpdateJsonFile(tasksFromJson);
                    return Task.FromResult(true);
                }
            }

            return Task.FromResult(false);
        }

        private static void UpdateJsonFile(Task<List<AppTask>> tasksFromJson)
        {
            string updatedAppTasks = JsonSerializer.Serialize<List<AppTask>>(tasksFromJson.Result);
            File.WriteAllText(FilePath, updatedAppTasks);
        }

        private static Task<List<AppTask>> GetTasksFromJson()
        {
            string tasksFromJsonFileString = File.ReadAllText(FilePath);
            if (!string.IsNullOrEmpty(tasksFromJsonFileString))
            {
                return Task.FromResult(JsonSerializer.Deserialize<List<AppTask>>(tasksFromJsonFileString) ?? []);
            }

            return Task.FromResult(new List<AppTask>());
        }

        public Task<List<Models.AppTask>> GetAllTasks()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    return System.Threading.Tasks.Task.FromResult(new List<Models.AppTask>());
                }

                string jsonString = File.ReadAllText(FilePath);

                if (!string.IsNullOrEmpty(jsonString))
                {
                    List<Models.AppTask> tasks = JsonSerializer.Deserialize<List<Models.AppTask>>(jsonString);
                    return System.Threading.Tasks.Task.FromResult(tasks ?? []);
                }

                else
                {
                    return System.Threading.Tasks.Task.FromResult(new List<Models.AppTask>());
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<List<Models.AppTask>> GetTaskByStatus(int status)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetStatus(AppTask task)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTask(int id, string description)
        {
            if (!File.Exists(FilePath))
            {
                return Task.FromResult(false);
            }

            var tasksFromJson = GetTasksFromJson();

            if (tasksFromJson.Result.Count > 0)
            {
                var taskToBeUpdated = tasksFromJson.Result
                    .Where(x => x.Id == id)
                    .SingleOrDefault();

                if (taskToBeUpdated != null)
                {
                    var updatedTask = new AppTask{
                        Id = id,
                        Description = description,
                        CreatedAt = taskToBeUpdated.CreatedAt,
                        UpdatedAt = DateTime.UtcNow,
                        TaskStatus = taskToBeUpdated.TaskStatus
                    };

                    tasksFromJson.Result.Remove(taskToBeUpdated);
                    tasksFromJson.Result.Add(updatedTask);
                    UpdateJsonFile(tasksFromJson);
                    return Task.FromResult(true);
                }
            }

            return Task.FromResult(false);
        }

        public List<string> GetAllHelpCommands()
        {
            return new List<string>
            {
                "add \"Task Description\" - To add a new task, type add with task description",
                "update \"Task Id\" \"Task Description\" - To update a task, type update with task id and task description",
                "delete \"Task Id\" - To delete a task, type delete with task id",
                "mark-in-progress \"Task Id\" - To mark a task to in progress, type mark-in-progress with task id",
                "mark-done \"Task Id\" - To mark a task to done, type mark-done with task id",
                "list - To list all task with its current status",
                "list done - To list all task with done status",
                "list todo  - To list all task with todo status",
                "list in-progress  - To list all task with in-progress status",
                "exit - To exit from app",
                "clear - To clear console window"
            };
        }

        #region Helper Methods
        private bool CreateFileIfNotExist()
        {
            try
            {
                // Check if the file exists
                if (!File.Exists(FilePath))
                {
                    // Create the file if it does not exist
                    using (FileStream fs = File.Create(FilePath))
                    {
                        Console.WriteLine($"File {FileName} created successfully.");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File {FileName} creation failed. Error - " + ex.Message);
                return false;
            }
        }

        #endregion
    }
}
