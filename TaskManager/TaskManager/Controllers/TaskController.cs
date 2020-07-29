using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.BusinessLayer.Interface;
using TaskManager.Entities;

namespace TaskManager.Service.Controllers
{
    
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [Route("api/Task/test")]
        [HttpGet]
        public ActionResult<String> testlink()
        {
            try
            {
             
                return "Hi";
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }
        [Route("api/Task/newtask")]
        [HttpPost]
        public async Task<ActionResult<String>> NewTask(TaskItem newtask)
        {
            try
            {
                var result =_taskService.NewTask(newtask);
                return result;
            }
            catch(Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }

        [Route("api/Task/newgroup")]
        [HttpPost]
        public ActionResult<String> NewTaskGroup(TaskGroup newgroup)
        {
            try
            {
                var resultGroup = _taskService.NewTaskGroup(newgroup);
                return resultGroup;
              
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }

        [Route("api/Task/edittask")]
        [HttpPost]
        public async Task<ActionResult<long>> EditTask(TaskItem task)
        {
            try
            {
                var resultEdit = _taskService.EditTask(task);
                return resultEdit;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }

        [Route("api/Task/alltask")]
        [HttpPost]
        public  ActionResult<List<TaskItem>> GetAllTask()
        {
            try
            {
                var resultLstTask = _taskService.GetAllTask();
                return resultLstTask ;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }

        [Route("api/Task/dashboard")]
        [HttpPost]
        public async Task<ActionResult<TaskDashboard>> GetTaskDashboard()
        {
            try
            {

                var resultDashboard = _taskService.GetDashboard();
                return resultDashboard;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }
        [Route("api/Task/allgroups")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<TaskGroup>>> GetAllTaskGroups()
        {
            try
            {
                var resultGroups = _taskService.GetAllTaskGroup();
                return resultGroups;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }
    }
}