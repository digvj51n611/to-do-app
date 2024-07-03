using ToDoApp.Data.Entities;
using ToDoApp.Service.Models;

namespace ToDoApp.Service.IServices
{
    public interface ITaskItemService
    {
        public Task<ServiceResult<TaskDto>> GetTaskServiceAsync(int id);
        public Task<ServiceResult<List<TaskDto>>> GetTasksServiceAsync(string username);
        public Task<ServiceResult<TaskDto>> AddTaskServiceAsync(TaskDto taskItem,int UserID);
        public Task<ServiceResult<TaskDto>> UpdateTaskServiceAsync(TaskDto taskItem);
        public Task<ServiceResult<TaskDto>> DeleteTaskServiceAsync(int id);

    }
}
