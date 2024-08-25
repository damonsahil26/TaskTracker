using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.Interfaces
{
    public interface ITaskService
    {
        Task<List<AppTask>> GetAllTasks();
        Task<int> AddNewTask(string description);
        Task<bool> UpdateTask(int id, string description);
        Task<bool> DeleteTask(int id);
        Task<bool> SetStatus(string status, int id);
        Task<List<AppTask>> GetTaskByStatus(int status);
        List<string> GetAllHelpCommands();
    }
}
