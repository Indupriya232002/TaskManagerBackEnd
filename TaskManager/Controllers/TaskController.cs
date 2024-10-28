using BusinessLayer.Services;
using EntityLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize]
        [Route("AddTask")]
        [HttpPost]
        public IActionResult AddTask(TaskDetails taskdetails)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = int.Parse(userIdClaim);
            taskdetails.userId = userId;
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

        [Authorize]
        [Route("GetUserTasks")]
        [HttpGet]
        public IActionResult GetUserTasks()
        {
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            // Retrieve user ID from JWT token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = int.Parse(userIdClaim);
            var result = _taskService.GetTasksByUserId(userId);
            return Ok(result);
        }


        [Authorize]
        [Route("UpdateTaskDetails")]
        [HttpPut]
        public IActionResult UpdateTaskDetails(TaskDetails taskdetails)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = int.Parse(userIdClaim);
            taskdetails.userId = userId;
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

        //[Authorize]
        //[Route("DeleteTaskByUserIdAndTaskId")]
        //[HttpDelete]
        //public IActionResult DeleteTaskByUserIdAndTaskId(int taskId)
        //{
        //    var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        //    if (string.IsNullOrEmpty(userIdClaim))
        //    {
        //        return Unauthorized("User ID not found in token.");
        //    }

        //    var userId = int.Parse(userIdClaim);
        //    var result = _taskService.DeleteTaskByUserIdAndTaskId(userId, taskId);
        //    return result ? Ok("Task Deleted Successfully") : NotFound("Task Not Found or Unauthorized Access");
        //}

        [Authorize]
        [Route("DeleteTaskByUserIdAndTaskId")]
        [HttpDelete]
        public IActionResult DeleteTaskByUserIdAndTaskId(int userId, int taskId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || int.Parse(userIdClaim) != userId)
            {
                return Unauthorized("User ID not found in token or does not match.");
            }

            // Check if the task belongs to the user
            var result = _taskService.DeleteTaskByUserIdAndTaskId(userId, taskId);
         
            return result ? Ok(new { message = "Task Deleted Successfully" }) : NotFound(new { message = "Task Not Found or Unauthorized Access" });

        }

    }
}
