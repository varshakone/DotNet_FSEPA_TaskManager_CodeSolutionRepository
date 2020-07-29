using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BusinessLayer.Interface;
using TaskManager.DataLayer;
using TaskManager.Entities;
using TaskStatus = TaskManager.Entities.TaskStatus;

namespace TaskManager.BusinessLayer.Services
{
    public class TaskService : ITaskService
    {
        private readonly IMongoDBContext _mongoDBContext;
        private readonly IMongoCollection<TaskItem> _mongoCollection;

        private readonly IMongoCollection<TaskGroup> _mongoCollectionGroup;
        public TaskService(IMongoDBContext mongoDBContext)
        {
            _mongoDBContext = mongoDBContext;
            _mongoCollection = _mongoDBContext.GetCollection<TaskItem>(typeof(TaskItem).Name);
            _mongoCollectionGroup = _mongoDBContext.GetCollection<TaskGroup>(typeof(TaskGroup).Name);

        }
        public long EditTask(TaskItem task)
        {
            try
            {
                long result = 0;
                var filterCriteria = Builders<TaskItem>.Filter.Eq("Name", task.Name);

                var updateElements = Builders<TaskItem>.Update.Set("Priority",task.Priority).Set("TaskStatus", task.TaskStatus).Set("TaskStartDate", task.TaskStartDate).Set("TaskEndDate", task.TaskEndDate.AddDays(5)).Set("TaskColorCode", task.TaskColorCode);
            
                
                var updateResult =_mongoCollection.UpdateOne(filterCriteria, updateElements,null);
                if(updateResult.IsAcknowledged)
                {
                    result = updateResult.ModifiedCount;
                }
                
                return result;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<TaskItem> GetAllTask()
        {
            try
            {
                var LstTask = _mongoCollection.Find(FilterDefinition<TaskItem>.Empty).ToList();
                return LstTask;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public List<TaskGroup> GetAllTaskGroup()
        {
            try
            {
                var LstGroups = _mongoCollectionGroup.Find(FilterDefinition<TaskGroup>.Empty).ToList();
                return LstGroups;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public TaskDashboard GetDashboard()
        {
            try
            {
                int completedTask = 0;
                int pendingTask = 0;
                var LstGroups = _mongoCollectionGroup.Find(FilterDefinition<TaskGroup>.Empty).ToList();
                var LstTask = _mongoCollection.Find(FilterDefinition<TaskItem>.Empty).ToList();
                TaskDashboard dashboard = new TaskDashboard();
                dashboard.TotalGroups = LstGroups.Count;
                dashboard.TotalTask = LstTask.Count;
                LstTask.ForEach(item =>
                {
                    if (item.TaskStatus == TaskStatus.Finished)
                    {
                        completedTask++;
                    }
                    else if (item.TaskStatus == TaskStatus.On_Hold || item.TaskStatus == TaskStatus.Progress || item.TaskStatus == TaskStatus.Yet_To_Start)
                    {
                        pendingTask++;
                    }
                });
                dashboard.CompletedTask = completedTask;
                dashboard.PendingTask = pendingTask;

                return dashboard;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string NewTask(TaskItem newtask)
        {
            try
            {
                
                newtask.TaskEndDate = newtask.TaskStartDate.AddDays(5);
                _mongoCollection.InsertOne(newtask);
                return "New Task Added";
            }

            catch (Exception exception)
            {
                throw exception;
            }
        }

        public string NewTaskGroup(TaskGroup taskGroup)
        {
            try
            {
                _mongoCollectionGroup.InsertOne(taskGroup);
                return "New Group Added";

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
