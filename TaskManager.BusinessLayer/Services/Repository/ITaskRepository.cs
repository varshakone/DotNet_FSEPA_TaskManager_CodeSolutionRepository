using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entities;

namespace TaskManager.BusinessLayer.Services.Repository
{
   public interface ITaskRepository
    {
        Task<List<TaskGroup>> GetAllTaskGroup();

        Task<String> NewTaskGroup(TaskGroup taskGroup);

        Task<List<TaskItem>> GetAllTask();

        Task<String> NewTask(TaskItem newtask);

        Task<long> EditTask(TaskItem task);

        Task<TaskDashboard> GetDashboard();
    }
}
