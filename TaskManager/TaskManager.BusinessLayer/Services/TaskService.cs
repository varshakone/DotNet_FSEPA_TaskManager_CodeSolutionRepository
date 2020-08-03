using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BusinessLayer.Interface;
using TaskManager.BusinessLayer.Services.Repository;
using TaskManager.DataLayer;
using TaskManager.Entities;
using TaskStatus = TaskManager.Entities.TaskStatus;

namespace TaskManager.BusinessLayer.Services
{
    public class TaskService : ITaskService
    {
        /// <summary>
        /// reference of type ITaskRepository
        /// </summary>
        private readonly ITaskRepository _taskRepository;
       
        /// <summary>
        /// Injecting object of type TaskRepository to access it's methods
        /// </summary>
        /// <param name="taskRepository"></param>
        public TaskService(ITaskRepository taskRepository)
        {

            _taskRepository = taskRepository;
        }

        /// <summary>
        /// call repository method to update task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<long> EditTask(TaskItem task)
        {
            //business logic goes here
            try
            {
                long result = 0;
                result =await _taskRepository.EditTask(task);
                return result;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        /// <summary>
        /// Call method to retrieve all task present in db
        /// </summary>
        /// <returns></returns>
        public async Task<List<TaskItem>> GetAllTask()
        {
            //business logic goes here
            try
            {
                var LstTask =await _taskRepository.GetAllTask();
                return LstTask;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }


        /// <summary>
        /// Call repository method to retrieve all task group
        /// </summary>
        /// <returns></returns>
        public async Task<List<TaskGroup>> GetAllTaskGroup()
        {
            //business logic goes here
            try
            {
                var LstGroups =await _taskRepository.GetAllTaskGroup();
                return LstGroups;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        /// <summary>
        /// Call method to retrieve task dashoard
        /// </summary>
        /// <returns></returns>
        public async Task<TaskDashboard> GetDashboard()
        {
            //business logic goes here
            try
            {
                var dashboard = await _taskRepository.GetDashboard();
                return dashboard;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Call repository method to add new task into db
        /// </summary>
        /// <param name="newtask"></param>
        /// <returns></returns>
        public async Task<string> NewTask(TaskItem newtask)
        {
            //business logic goes here
            try
            {
                var result = await _taskRepository.NewTask(newtask);
                return result;
            }

            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Call repository method to add new task group into db
        /// </summary>
        /// <param name="taskGroup"></param>
        /// <returns></returns>
        public async Task<string> NewTaskGroup(TaskGroup taskGroup)
        {
            //business logic goes here
            try
            {
                var result = await _taskRepository.NewTaskGroup(taskGroup);
                return result;

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
