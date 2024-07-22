
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using ToDoApp.data;
using ToDoApp.Data.Entities;
using ToDoApp.Data.IRepos;
using ToDoApp.Data.Models;

namespace ToDoApp.Data.Repos
{
    public class TaskItemRepo : GenericRepo<TaskItem>, ITaskItemRepo
    {
        public TaskItemRepo(ToDoDbContext context) : base(context)
        {
        }
    }
}
