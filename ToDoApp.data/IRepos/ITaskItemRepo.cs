using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Data.Entities;
using ToDoApp.Data.Models;

namespace ToDoApp.Data.IRepos
{
    public interface ITaskItemRepo
    {
        public Task<DataResponse<TaskItem>> GetTaskItemAsync(int id);
        public Task<DataResponse<List<TaskItem>>> GetTaskItemsAsync();
        public Task<DataResponse<TaskItem>> AddTaskItemAsync(TaskItem taskItem);
        public Task<DataResponse<TaskItem>> UpdateTaskItemAsync(TaskItem taskItem);
        public Task<DataResponse<TaskItem>> DeleteTaskItemAsync(int id);
    }
}
