using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
                    throw;
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
                    throw;
                }
            }
        }
        public async Task<bool> AddTaskItemAsync(TaskItem taskItem)
        {
            using (var context = _context)
            {
                try
                {
                    context.Tasks.Add(taskItem);
                    try
                    {
                        if(await context.SaveChangesAsync() > 0 )
                        return true;
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
        public async Task<bool> UpdateTaskItemAsync(TaskItem taskItem)
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
                    if (await context.SaveChangesAsync() > 0) return true;
                    throw new Exception("Update Failed");
                    
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public async Task<bool> DeleteTaskItemAsync(int id)
        {
            using(var context = _context)
            {
                try
                {
                    var task = await GetTaskItemAsync(id);
                    context.Remove(task);
                    if(await context.SaveChangesAsync() > 0 ) return true;
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
