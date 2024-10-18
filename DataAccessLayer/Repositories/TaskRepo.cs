using DataAccessLayer.Data;
using EntityLayer.Interfaces;
using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class TaskRepo : ITaskRepo
    {
        TaskDbContext _dbContext;

        public TaskRepo(TaskDbContext taskDb)
        {
            _dbContext = taskDb;
        }

        public TaskDetails AddTask(TaskDetails taskDetails)
        {
            _dbContext.TaskDetails.Add(taskDetails);
            _dbContext.SaveChanges();
            return taskDetails;
        }

        public object DeleteTaskDetailsById(int id)
        {
            var obj = _dbContext.TaskDetails.Find(id);
            if(obj != null)
            {
                _dbContext.TaskDetails.Remove(obj);
                _dbContext.SaveChanges();
            }
            return "Task Deleted Successfully";
        }

        public List<TaskDetails> GetAllTasks()
        {
            return _dbContext.TaskDetails.ToList();
        }

        public object UpdateTaskDetails(TaskDetails taskDetails)
        {
            _dbContext.TaskDetails.Update(taskDetails);
            _dbContext.SaveChanges();
            return taskDetails;
        }
    }
}
