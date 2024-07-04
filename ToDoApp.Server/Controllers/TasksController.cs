using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Entities;
using ToDoApp.data;
using ToDoApp.Service.Services;
using ToDoApp.Service.IServices;
using ToDoApp.Service.Models;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ToDoApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskItemService _taskService;

        public TasksController(ITaskItemService service)
        {
            _taskService = service;
        }
        private ActionResult<T> ResultFromCode<T>(ErrorCode code,Exception ex)
        {
            if(code == ErrorCode.NotFoundError) return NotFound(ex.Message);
            if(code == ErrorCode.AuthenticationError) return Unauthorized("Unauthorized");
            if(code == ErrorCode.ValidationError) return BadRequest("Validation Errors:\n"+ex.Message);
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An unexpected error occurred.");
        }
        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
        {
            var serviceResult = await _taskService.GetTasksByUserServiceAsync();
            if(serviceResult.IsSuccess)
            {
                return Ok(serviceResult.Result);
            }
            else
            {
                return ResultFromCode<IEnumerable<TaskDto>>(serviceResult.ErrorCode, serviceResult.Exception!);
            }
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskItem(int id)
        {
            var serviceResult = await _taskService.GetTaskServiceAsync(id);
            if (serviceResult.IsSuccess)
            {
                return Ok(serviceResult.Result);
            }
            else
            {
                return ResultFromCode<TaskDto>(serviceResult.ErrorCode, serviceResult.Exception!);
            }
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDto>> PutTaskItem(TaskDto taskDto)
        {
            var serviceResult = await _taskService.UpdateTaskServiceAsync(taskDto);
            if (serviceResult.IsSuccess)
            {
                return Ok(serviceResult.Result);
            }
            else
            {
                return ResultFromCode<TaskDto>(serviceResult.ErrorCode, serviceResult.Exception!);
            }
        }

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskDto>> PostTaskItem(TaskDto taskDto)
        {
            var serviceResult = await _taskService.AddTaskServiceAsync(taskDto);
            if (serviceResult.IsSuccess)
            {
                return Created();
            }
            else
            {
                return ResultFromCode<TaskDto>(serviceResult.ErrorCode, serviceResult.Exception!);
            }
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskDto>> DeleteTaskItem(int id)
        {
            var serviceResult = await _taskService.DeleteTaskServiceAsync(id);
            if (serviceResult.IsSuccess)
            {
                return Ok(serviceResult.Result);
            }
            else
            {
                return ResultFromCode<TaskDto>(serviceResult.ErrorCode, serviceResult.Exception!);
            }
        }

        //private bool TaskItemExists(int id)
        //{
        //    return _context.Tasks.Any(e => e.Id == id);
        //}
    }
}
