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
    [Route("api/Task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        
        [Route("newtask")]
        [HttpPost]
        public async Task<ActionResult<String>> NewTask(TaskItem newtask)
        {
            try
            {
                var result =await _taskService.NewTask(newtask);
                return result;
            }
            catch(Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }

        [Route("newgroup")]
        [HttpPost]
        public async Task<ActionResult<String>> NewTaskGroup(TaskGroup newgroup)
        {
            try
            {
                var resultGroup =await _taskService.NewTaskGroup(newgroup);
                return resultGroup;
              
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }

        [Route("edittask")]
        [HttpPut]
        public async Task<ActionResult<long>> EditTask(TaskItem task)
        {
            try
            {
                var resultEdit =await _taskService.EditTask(task);
                return resultEdit;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }

        [Route("alltask")]
        [HttpGet]
        public async Task< ActionResult<List<TaskItem>>> GetAllTask()
        {
            try
            {
                var resultLstTask =await _taskService.GetAllTask();
                return resultLstTask ;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }

        [Route("dashboard")]
        [HttpGet]
        public async Task<ActionResult<TaskDashboard>> GetTaskDashboard()
        {
            try
            {

                var resultDashboard =await _taskService.GetDashboard();
                return resultDashboard;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }
        [Route("allgroups")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskGroup>>> GetAllTaskGroups()
        {
            try
            {
                var resultGroups =await _taskService.GetAllTaskGroup();
                return resultGroups;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.ToString());
            }

        }
    }
}
