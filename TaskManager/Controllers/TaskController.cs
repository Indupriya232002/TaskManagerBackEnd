using BusinessLayer.Services;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [Route("AddTask")]
        [HttpPost]
        public IActionResult AddTask(TaskDetails taskdetails)
        {
            var result = _taskService.AddTask(taskdetails);
            return Ok(result);
        }

        [Route("GetAllTasks")]
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var result = _taskService.GetAllTasks();
            return Ok(result);
        }

        [Route("UpdateTaskDetails")]
        [HttpPut]
        public IActionResult UpdateTaskDetails(TaskDetails taskdetails)
        {
            var result = _taskService.UpdateTaskDetails(taskdetails);
            return Ok(result);
        
        }

        [Route("DeleteTaskDetailsById")]
        [HttpDelete]
        public IActionResult DeleteTaskDetailsById(int taskId)
        {
            var result = _taskService.DeleteTaskDetailsById(taskId);
            return Ok(result);  
        }

    }
}
