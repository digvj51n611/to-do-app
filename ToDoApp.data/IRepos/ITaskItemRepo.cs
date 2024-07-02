using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Entities;

namespace ToDoApp.Data.IRepos
{
    public interface ITaskItemRepo
    {
        public Task<TaskItem> GetTaskItemAsync(int id);
        public Task<List<TaskItem>> GetTaskItemsAsync();
        public Task<bool> AddTaskItemAsync(TaskItem taskItem);
        public Task<bool> UpdateTaskItemAsync(TaskItem taskItem);
        public Task<bool> DeleteTaskItemAsync(int id);
    }
}
