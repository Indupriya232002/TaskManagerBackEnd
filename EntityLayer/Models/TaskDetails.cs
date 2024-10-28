using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using EntityLayer.Interfaces;

namespace EntityLayer.Models
{
    public class TaskDetails
    {
        [Key]
        public int taskID {  get; set; }

        [Required(ErrorMessage = "TaskName is required")]
        public string taskName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string taskDescription { get; set; }

        [Required(ErrorMessage = "TaskDate is required")]
        public DateTime taskDate { get; set; }

        [Required]
        [DefaultValue("Not Completed")]
        public string status { get; set; } = "Not Completed";

        public int userId { get; set; }
    }
}
