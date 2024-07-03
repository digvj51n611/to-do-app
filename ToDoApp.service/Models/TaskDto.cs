using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Entities;

namespace ToDoApp.Service.Models
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AddedOnUtc { get; set; }
        public Data.Entities.TaskStatus Status { get; set; }
    }
}
