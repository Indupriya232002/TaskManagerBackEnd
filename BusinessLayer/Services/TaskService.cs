using EntityLayer.Interfaces;
using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TaskService
    {
        ITaskRepo _taskRepo;

        public TaskService(ITaskRepo taskRepo)
        {
            _taskRepo = taskRepo;
        }

        public TaskDetails AddTask(TaskDetails taskDetails)
        {
            return _taskRepo.AddTask(taskDetails);
        }
        public  List<TaskDetails> GetAllTasks()
        {
            return _taskRepo.GetAllTasks();

        }

        public object UpdateTaskDetails(TaskDetails taskDetails)
        {
            return _taskRepo.UpdateTaskDetails(taskDetails);
        }

        public object DeleteTaskDetailsById(int id)
        {
            return _taskRepo.DeleteTaskDetailsById(id);

        }
    }
}
