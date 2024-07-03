using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Data.Entities
{
    public enum Status : int
    {
        Pending,
        Active
    }
    public class TaskStatus
    {
        public int Id { get; set; }
        public virtual Status Status { get; set; }
    }
}
