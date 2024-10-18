using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Interfaces
{
    public  interface ITaskRepo
    {
        TaskDetails AddTask(TaskDetails taskDetails);
        List<TaskDetails> GetAllTasks();

        object UpdateTaskDetails(TaskDetails taskDetails);

        object DeleteTaskDetailsById(int id);

    }
}
