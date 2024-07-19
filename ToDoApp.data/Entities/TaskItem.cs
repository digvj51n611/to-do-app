using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Data.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AddedOnUtc { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int TaskStatusId { get; set; }
        public virtual TaskStatus TaskStatus { get; set; }
    }
}
