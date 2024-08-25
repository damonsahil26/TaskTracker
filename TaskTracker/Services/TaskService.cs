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
        public Task<bool> AddNewTask(string description)
        {
            try
            {
                var task = new AppTask
                {
                    Id = Guid.NewGuid().GetHashCode(),
                    Description = description,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    TaskStatus = Enums.Status.todo
                };

                var fileCreatedSuccessfully = CreateFileIfNotExist();

                if (fileCreatedSuccessfully)
                {
                    string jsonString = JsonSerializer.Serialize<Models.AppTask>(task);
                    File.WriteAllText(FilePath, jsonString);
                    return Task.FromResult(true);
                }

                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Task addition failed. Error - " + ex.Message);
                return Task.FromResult(false);
            }
        }

        public Task<bool> DeleteTask(int id)
        {
            throw new NotImplementedException();
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

        public Task<bool> SetStatus(Models.AppTask task)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTask(Models.AppTask task)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllCommands()
        {
            return new List<string>
            {
                "add \" Task Description\" - To add a new task, type add with task description",
                "update \" Task Id\" \" Task Description\" - To update a task, type update with task id and task description",
                "delete \" Task Id\" - To delete a task, type delete with task id",
                "mark-in-progress \" Task Id\" - To mark a task to in progress, type mark-in-progress with task id",
                "mark-done \" Task Id\" - To mark a task to done, type mark-done with task id",
                "list - To list all task with its current status",
                "list done - To list all task with done status",
                "list todo  - To list all task with todo status",
                "list in-progress  - To list all task with in-progress status"
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
