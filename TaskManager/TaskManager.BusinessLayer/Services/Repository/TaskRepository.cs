using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataLayer;
using TaskManager.Entities;
using TaskStatus = TaskManager.Entities.TaskStatus;

namespace TaskManager.BusinessLayer.Services.Repository
{
    public class TaskRepository : ITaskRepository
    {
        /// <summary>
        /// private references to perform Mongodb operations
        /// </summary>
        private readonly IMongoDBContext _mongoDBContext;
        private readonly IMongoCollection<TaskItem> _mongoCollection;
        private readonly IMongoCollection<TaskGroup> _mongoCollectionGroup;

        /// <summary>
        /// Inject mongodbcontext object 
        /// </summary>
        /// <param name="mongoDBContext"></param>
        public TaskRepository(IMongoDBContext mongoDBContext)
        {
            _mongoDBContext = mongoDBContext;
            _mongoCollection = _mongoDBContext.GetCollection<TaskItem>(typeof(TaskItem).Name);
            _mongoCollectionGroup = _mongoDBContext.GetCollection<TaskGroup>(typeof(TaskGroup).Name);

        }
        /// <summary>
        /// MongoDB logic to update task in db
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<long> EditTask(TaskItem task)
        {
            //business logic goes here
            try
            {
                long result = 0;
                var filterCriteria = Builders<TaskItem>.Filter.Eq("Name", task.Name);

                var updateElements = Builders<TaskItem>.Update.Set("Priority", task.Priority).Set("TaskStatus", task.TaskStatus).Set("TaskStartDate", task.TaskStartDate).Set("TaskEndDate", task.TaskEndDate.AddDays(5)).Set("TaskColorCode", task.TaskColorCode);


                var updateResult =await _mongoCollection.UpdateOneAsync(filterCriteria, updateElements, null);
                if (updateResult.IsAcknowledged)
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

        /// <summary>
        /// MongoDB logic to retrieve all task present in db
        /// </summary>
        /// <returns></returns>
        public async Task<List<TaskItem>> GetAllTask()
        {
            //business logic goes here
            try
            {
                var LstTask =await _mongoCollection.FindAsync(FilterDefinition<TaskItem>.Empty);
                return LstTask.ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// MongoDB logic to retrieve all task group
        /// </summary>
        /// <returns></returns>
        public async Task<List<TaskGroup>> GetAllTaskGroup()
        {
            //business logic goes here
            try
            {
                var LstGroups =await _mongoCollectionGroup.FindAsync(FilterDefinition<TaskGroup>.Empty);
                return LstGroups.ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// MongoDB logic to retrieve task dashoard
        /// </summary>
        /// <returns></returns>
        public async Task<TaskDashboard> GetDashboard()
        {
            //business logic goes here
            try
            {
                int completedTask = 0;
                int pendingTask = 0;
                var LstGroups = await _mongoCollectionGroup.FindAsync(FilterDefinition<TaskGroup>.Empty).Result.ToListAsync();
                var LstTask =await _mongoCollection.FindAsync(FilterDefinition<TaskItem>.Empty).Result.ToListAsync();
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

        /// <summary>
        /// MongoDB logic to add new task into db
        /// </summary>
        /// <param name="newtask"></param>
        /// <returns></returns>
        public async Task<string> NewTask(TaskItem newtask)
        {
            //business logic goes here
            try
            {

                newtask.TaskEndDate = newtask.TaskStartDate.AddDays(5);
               await _mongoCollection.InsertOneAsync(newtask);
                return "New Task Added";
            }

            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// MongoDB logic to add new task group into db
        /// </summary>
        /// <param name="taskGroup"></param>
        /// <returns></returns>
        public async Task<string> NewTaskGroup(TaskGroup taskGroup)
        {
            //business logic goes here
            try
            {
               await _mongoCollectionGroup.InsertOneAsync(taskGroup);
               return "New Group Added";

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
