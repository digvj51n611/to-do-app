
using Microsoft.EntityFrameworkCore;
using ToDoApp.data;
using ToDoApp.Data.Entities;
using ToDoApp.Data.IRepos;

namespace ToDoApp.Data.Repos
{
    public class TaskItemRepo : ITaskItemRepo
    {
        private ToDoDbContext _context;
        public TaskItemRepo(ToDoDbContext context)
        {
            _context = context;
        }
        public async Task<TaskItem> GetTaskItemAsync(int id)
        {
            using(var context = _context)
            {
                try
                {
                    TaskItem? taskItem = await context.Tasks.FindAsync(id);
                    if (taskItem == null) throw new Exception("Not Found");
                    return taskItem;
                }
                catch (Exception ex)
                {
                    throw new Exception("SERVER_ERR");
                }
            }
        }
        public async Task<List<TaskItem>> GetTaskItemsAsync()
        {
            using(var context = _context)
            {
                try
                {
                    return await context.Tasks.ToListAsync<TaskItem>();
                }
                catch(Exception ex)
                {
                    throw new Exception("SERVER_ERR");
                }
            }
        }
        public async Task<TaskItem> AddTaskItemAsync(TaskItem taskItem)
        {
            using (var context = _context)
            {
                try
                {
                    context.Tasks.Add(taskItem);
                    try
                    {
                        if(await context.SaveChangesAsync() > 0 )
                        return taskItem;
                        throw new Exception("Save changes failed");
                    }
                    catch( Exception ex )
                    {
                        throw;
                    }
                    
                }
                catch
                {
                    throw;
                }
            }
        }
        public async Task<TaskItem> UpdateTaskItemAsync(TaskItem taskItem)
        {
            using( var context = _context)
            {
                try
                {
                    var task = await context.Tasks.FindAsync(taskItem.Id);
                    if (task == null) throw new Exception("Not Found");
                    task.Title = taskItem.Title;
                    task.Description = taskItem.Description;
                    task.TaskStatus = taskItem.TaskStatus;
                    if (await context.SaveChangesAsync() > 0) return task;
                    throw new Exception("Update Failed");
                    
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<TaskItem> DeleteTaskItemAsync(int id)
        {
            using(var context = _context)
            {
                try
                {
                    var task = await GetTaskItemAsync(id);
                    context.Remove(task);
                    if(await context.SaveChangesAsync() > 0 ) return task;
                    throw new Exception("Deletion Failed!");
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
