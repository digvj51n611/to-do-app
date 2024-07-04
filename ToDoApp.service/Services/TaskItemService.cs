using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoApp.Data.Entities;
using ToDoApp.Data.IRepos;
using ToDoApp.Service.IServices;
using ToDoApp.Service.Models;

namespace ToDoApp.Service.Services
{
    public class TaskItemService : ITaskItemService
    {
        private ITaskItemRepo _taskItemRepo;
        private IMapper _mapper;
        private  IValidator<TaskDto> _validator;
        private IHttpContextAccessor _httpContextAccessor;
        public TaskItemService(ITaskItemRepo taskItemRepo , IMapper mapper ,IValidator<TaskDto> validator,IHttpContextAccessor httpContextAccessor)
        {
            _taskItemRepo = taskItemRepo;
            _mapper = mapper;
            _validator = validator;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResult<TaskDto>> GetTaskServiceAsync(int id)
        {
            try
            {
                TaskItem result = await _taskItemRepo.GetTaskItemAsync(id);
                return ServiceResult<TaskDto>.SuccessResult(_mapper.Map<TaskDto>(result));
            }
            catch (Exception ex)
            {
                var result =  ServiceResult<TaskDto>.FailureResult(ex, ErrorCode.ServerError);
                result.ErrorCode = ErrorCode.ServerError;
                return result;
            }
        }
        public async Task<ServiceResult<List<TaskDto>>> GetTasksByUserServiceAsync()
        {
            try
            {
                string? name = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
                List<TaskItem> tasks = await _taskItemRepo.GetTaskItemsAsync();
                List<TaskItem> userNameTasks = tasks.Where(task => task.User.Username == name).ToList();
                return ServiceResult<List<TaskDto>>.SuccessResult(ConvertList(userNameTasks));
            }
            catch(Exception ex)
            {
                var serviceResult = ServiceResult<List<TaskDto>>.FailureResult(ex , ErrorCode.ServerError);
                serviceResult.ErrorCode = ErrorCode.ServerError;
                return serviceResult;
            }
        }
        //public  async Task<ServiceResult<List<TaskDto>>> GetTasksServiceAsync()
        //{
        //    try
        //    {
        //        var tasks = await _taskItemRepo.GetTaskItemsAsync();
        //        return ServiceResult<List<TaskDto>>.SuccessResult(ConvertList(tasks));
        //    }
        //    catch(Exception ex)
        //    {
        //        return ServiceResult<List<TaskDto>>.FailureResult(ex);
        //    }
        //}
        public async Task<ServiceResult<TaskDto>> AddTaskServiceAsync(TaskDto taskDto)
        {
            try
            {
                try
                {
                    ValidateTask(taskDto);  
                }
                catch(Exception ex )
                {
                    return ServiceResult<TaskDto>.FailureResult(ex , ErrorCode.ValidationError);
                }
                TaskItem taskItem = _mapper.Map<TaskItem>(taskDto);
                taskItem.UserId = AssignUser();
                var result = await _taskItemRepo.AddTaskItemAsync(taskItem);
                return ServiceResult<TaskDto>.SuccessResult(_mapper.Map<TaskDto>(result));
            }
            catch( Exception ex)
            {
                return ServiceResult<TaskDto>.FailureResult(ex,ErrorCode.ServerError);
            }
        }
        public async Task<ServiceResult<TaskDto>> UpdateTaskServiceAsync(TaskDto taskDto)
        {
            try
            {
                try
                {
                    ValidateTask(taskDto);
                }
                catch(Exception ex)
                {
                    return ServiceResult<TaskDto>.FailureResult(ex, ErrorCode.ValidationError);
                }
                TaskItem task = _mapper.Map<TaskItem>(taskDto);
                var result = await _taskItemRepo.UpdateTaskItemAsync(task);
                return ServiceResult<TaskDto>.SuccessResult(_mapper.Map<TaskDto>(result));
            }
            catch( Exception ex)
            {
                return ServiceResult<TaskDto>.FailureResult(ex, ErrorCode.ServerError);
            }
        }
        public async Task<ServiceResult<TaskDto>> DeleteTaskServiceAsync(int id)
        {
            try
            {
                var result = await _taskItemRepo.DeleteTaskItemAsync(id);
                return ServiceResult<TaskDto>.SuccessResult(_mapper.Map<TaskDto>(result));
            }
            catch (Exception ex)
            {
                return ServiceResult<TaskDto>.FailureResult(ex,ErrorCode.ServerError);
            }
        }
        private bool ValidateTask(TaskDto taskDto)
        {
            if (taskDto == null)
            {
                return false;
            }
            string errorMessageString = "";
            var validationResult = _validator.Validate(taskDto);
            if (validationResult.IsValid) return true;
            foreach (var error in validationResult.Errors)
            {
                errorMessageString += "\n" + error.ErrorMessage;
            }
            throw new Exception(errorMessageString);
        }
        private List<TaskDto> ConvertList(List<TaskItem> tasks)
        {
            List<TaskDto> taskDtos = new List<TaskDto>();
            foreach (var task in tasks)
            {
                taskDtos.Add(_mapper.Map<TaskDto>(task));
            }
            return taskDtos;
        }
        private int AssignUser()
        {
            var result = _httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub);
            return 1;
        }
    }
}
