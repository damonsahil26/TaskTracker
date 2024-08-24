using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Interfaces;

namespace TaskTracker.Services
{
    public class TaskService : ITaskService
    {
        public Task<bool> AddNewTask(string description)
        {
            var task = new Models.Task
            {
                Id = Guid.NewGuid().GetHashCode(),
                Description = description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TaskStatus = Enums.Status.todo
            };

            return Task.FromResult(true);
        }

        public Task<bool> DeleteTask(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Task>> GetAllTasks()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Task>> GetTaskByStatus(int status)
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
    }
}
