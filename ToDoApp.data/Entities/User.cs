﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Data.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<TaskItem> Tasks { get; set; }
    }
}
