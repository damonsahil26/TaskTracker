using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Interfaces
{
    public interface ITaskService
    {
        Task<List<Models.Task>> GetAllTasks();
        Task<bool> AddNewTask(string description);
        Task<bool> UpdateTask(Models.Task task);
        Task<bool> DeleteTask(int id);
        Task<bool> SetStatus(Models.Task task);
        Task<List<Models.Task>> GetTaskByStatus(int status);
    }
}
