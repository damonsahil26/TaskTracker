using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<Models.Task>> GetAllTasks();
        Task<bool> AddNewTask(string description);
        Task<bool> UpdateTask(Models.Task task);
        Task<bool> DeleteTask(int id);
        Task<bool> SetStatus(Models.Task task);
        Task<IEnumerable<Models.Task>> GetTaskByStatus(int status);
    }
}
