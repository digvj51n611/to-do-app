using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Data.Entities
{
    public enum TaskStatus
    {
        Pending,
        Active
    }
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AddedOnUtc { get; set; }
        public TaskStatus Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
