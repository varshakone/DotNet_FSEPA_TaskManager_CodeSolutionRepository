using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entities;

namespace TaskManager.BusinessLayer.Interface
{
public interface ITaskService
    {
        List<TaskGroup> GetAllTaskGroup();

        String NewTaskGroup( TaskGroup taskGroup);

        List<TaskItem> GetAllTask();

        String NewTask(TaskItem newtask);

        long EditTask( TaskItem task);

        TaskDashboard GetDashboard();
    }
}
