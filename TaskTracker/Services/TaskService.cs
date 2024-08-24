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
                var task = new Models.Task
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
                    string jsonString = JsonSerializer.Serialize<Models.Task>(task);
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

        public Task<List<Models.Task>> GetAllTasks()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    return System.Threading.Tasks.Task.FromResult(new List<Models.Task>());
                }

                string jsonString = File.ReadAllText(FilePath);

                if (!string.IsNullOrEmpty(jsonString))
                {
                    List<Models.Task> tasks = JsonSerializer.Deserialize<List<Models.Task>>(jsonString);
                    return System.Threading.Tasks.Task.FromResult(tasks ?? []);
                }

                else
                {
                    return System.Threading.Tasks.Task.FromResult(new List<Models.Task>());
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<List<Models.Task>> GetTaskByStatus(int status)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetStatus(Models.Task task)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTask(Models.Task task)
        {
            throw new NotImplementedException();
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
