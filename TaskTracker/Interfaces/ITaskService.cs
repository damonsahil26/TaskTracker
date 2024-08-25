using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Interfaces
{
    public interface ITaskService
    {
        Task<List<Models.AppTask>> GetAllTasks();
        Task<int> AddNewTask(string description);
        Task<bool> UpdateTask(Models.AppTask task);
        Task<bool> DeleteTask(int id);
        Task<bool> SetStatus(Models.AppTask task);
        Task<List<Models.AppTask>> GetTaskByStatus(int status);
        List<string> GetAllHelpCommands();
    }
}
