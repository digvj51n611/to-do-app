
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Service.IServices;
using ToDoApp.Service.Models;
using Microsoft.AspNetCore.Authorization;
using ToDoApp.Server.Models;
using NuGet.Protocol;
using ToDoApp.Server.Helpers;

namespace ToDoApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskItemService _taskService;

        public TasksController(ITaskItemService service)
        {
            _taskService = service;
        }
        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> GetTasks()
        {
            var serviceResult = await _taskService.GetTasksByUserServiceAsync();
            if(serviceResult.IsSuccess)
            {
                return Ok(ApiResponse<List<TaskDto>>.CreateApiResponse(serviceResult));
            }
            return this.FailedObjectResult(serviceResult);
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskItem(int id)
        {
            var serviceResult = await _taskService.GetTaskServiceAsync(id);
            if (serviceResult.IsSuccess)
            {
                return Ok(ApiResponse<TaskDto>.CreateApiResponse(serviceResult));
            }
            return this.FailedObjectResult(serviceResult);
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDto>> PutTaskItem(TaskDto taskDto)
        {
            var serviceResult = await _taskService.UpdateTaskServiceAsync(taskDto);
            if (serviceResult.IsSuccess)
            {
                return Ok(ApiResponse<TaskDto>.CreateApiResponse(serviceResult));
            }
            return this.FailedObjectResult(serviceResult);

        }

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskDto>> PostTaskItem(TaskDto taskDto)
        {
            var serviceResult = await _taskService.AddTaskServiceAsync(taskDto);
            if (serviceResult.IsSuccess)
            {
                return Ok(ApiResponse<TaskDto>.CreateApiResponse(serviceResult));
            }
            return this.FailedObjectResult(serviceResult);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskDto>> DeleteTaskItem(int id)
        {
            var serviceResult = await _taskService.DeleteTaskServiceAsync(id);
            if (serviceResult.IsSuccess)
            {
                return Ok(ApiResponse<TaskDto>.CreateApiResponse(serviceResult));
            }
            return this.FailedObjectResult(serviceResult);
        }

        //private bool TaskItemExists(int id)
        //{
        //    return _context.Tasks.Any(e => e.Id == id);
        //}
    }
}
