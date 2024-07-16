
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
    public class TaskItemRepo : ITaskItemRepo
    {
        private ToDoDbContext _context;
        public TaskItemRepo(ToDoDbContext context)
        {
            _context = context;
        }
        public async Task<DataResponse<TaskItem>> GetTaskItemAsync(int id)
        {
            using(var context = _context)
            {
                try
                {
                    var taskItem = await context.Tasks.FindAsync(id);
                    if (taskItem == null)
                    {
                        string message = $"Task with id : {id} Not found";
                        return DataResponse<TaskItem>.FailureResult(ErrorType.NotFoundError,message);
                    }   
                    return DataResponse<TaskItem>.SuccessResult(taskItem);
                }
                catch(Exception ex)
                {
                    return DataResponse<TaskItem>.FailureResult(ex);
                }
            }
        }
        public async Task<DataResponse<List<TaskItem>>> GetTaskItemsAsync()
        {
            using(var context = _context)
            {
                try
                {
                    var result =  await context.Tasks.ToListAsync<TaskItem>();
                    if(result.Any())
                    {
                        
                        return DataResponse<List<TaskItem>>.SuccessResult(result);
                    }
                    string message = "Tasks not found";
                    return DataResponse<List<TaskItem>>.FailureResult(ErrorType.NotFoundError,message);
                }
                catch (Exception ex)
                {
                    return DataResponse<List<TaskItem>>.FailureResult(ex);
                }
            }
        }
        public async Task<DataResponse<TaskItem>> AddTaskItemAsync(TaskItem taskItem)
        {
            using (var context = _context)
            {
                try
                {
                    context.Tasks.Add(taskItem);
                    try
                    {
                        if(await context.SaveChangesAsync() > 0 )
                        return DataResponse<TaskItem>.SuccessResult(taskItem);
                        string message = "Save Changes failed";
                        return DataResponse<TaskItem>.FailureResult(ErrorType.SaveChangesError,message);
                    }
                    catch(DbUpdateException ex)
                    {
                        return DataResponse<TaskItem>.FailureResult(ex);
                    }
                    
                }
                catch(Exception ex)
                {
                    return DataResponse<TaskItem>.FailureResult(ex);
                }
            }
        }
        public async Task<DataResponse<TaskItem>> UpdateTaskItemAsync(TaskItem taskItem)
        {
            using( var context = _context)
            {
                try
                {
                    var task = await context.Tasks.FindAsync(taskItem.Id);
                    if (task == null)
                    {
                        string message = $"Task Not Found with id:{taskItem.Id}";
                        return DataResponse<TaskItem>.FailureResult(ErrorType.NotFoundError,message);
                    }
                    task.Title = taskItem.Title;
                    task.Description = taskItem.Description;
                    task.TaskStatus = taskItem.TaskStatus;
                    if (await context.SaveChangesAsync() > 0)
                    {
                        return DataResponse<TaskItem>.SuccessResult(task);
                    }
                    return DataResponse<TaskItem>.FailureResult(ErrorType.SaveChangesError, "Save Changes Failed");
                    
                }
                catch (Exception ex)
                {
                    return DataResponse<TaskItem>.FailureResult(ex);
                }
            }
        }
        public async Task<DataResponse<TaskItem>> DeleteTaskItemAsync(int id)
        {
            using(var context = _context)
            {
                try
                {
                    var task = await _context.Tasks.FindAsync(id);
                    if(task == null )
                    {
                        string message = $"Task with id:{id} Not Found";
                        return DataResponse<TaskItem>.FailureResult(ErrorType.NotFoundError,message);
                    }
                    context.Remove(task);
                    if(await context.SaveChangesAsync() > 0 )
                    {
                        return DataResponse<TaskItem>.SuccessResult(task);
                    }
                    return DataResponse<TaskItem>.FailureResult(ErrorType.SaveChangesError, "Save Changes Failed");
                }
                catch(Exception ex)
                {
                    return DataResponse<TaskItem>.FailureResult(ex);
                }
            }
        }
    }
}
