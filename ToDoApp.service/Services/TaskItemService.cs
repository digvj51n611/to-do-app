using AutoMapper;
using Azure.Core;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using ToDoApp.Data.Entities;
using ToDoApp.Data.IRepos;
using ToDoApp.Data.Models;
using ToDoApp.Service.IServices;
using ToDoApp.Service.Models;

namespace ToDoApp.Service.Services
{
    public class TaskItemService : BaseService<TaskDto>, ITaskItemService
    {
        private ITaskItemRepo _taskItemRepo;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private IUserHelper _userHelper;
        public TaskItemService(ITaskItemRepo taskItemRepo,IMapper mapper,IValidator<TaskDto> validator,IHttpContextAccessor httpContextAccessor,IUserHelper helper):base(validator)
        {
            _taskItemRepo = taskItemRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userHelper = helper;
        }
        public async Task<ServiceResult<TaskDto>> GetTaskServiceAsync(int id)
        {
            try
            {
                var result = await _taskItemRepo.GetAsync(id);
                if(result.IsSuccess)
                {
                    return ServiceResult<TaskDto>.SuccessResult(_mapper.Map<TaskDto>(result.Data));
                }
                else
                {
                    return ServiceResult<TaskDto>.FailureResult(result.ErrorType , result.Message);
                }
            }
            catch (Exception ex)
            {
                return ServiceResult<TaskDto>.FailureResult(ErrorCode.ServerError,ex.Message);
            }
        }
        public async Task<ServiceResult<List<TaskDto>>> GetTasksByUserServiceAsync()
        {
            try
            {
                string name = GetCurrentUserName();
                if( name == "")
                {
                    return ServiceResult<List<TaskDto>>.FailureResult(ErrorCode.AuthenticationError,"User Not Found");
                }
                var result = await _taskItemRepo.GetAllAsync();
                if(!result.IsSuccess)
                {
                    return ServiceResult<List<TaskDto>>.FailureResult(result.ErrorType , result.Message);
                }
                List<TaskItem> userTasks = result.Data!.Where(task => task.User.Username == name).ToList();
                return ServiceResult<List<TaskDto>>.SuccessResult(ConvertListToDto(userTasks));
            }
            catch(Exception ex)
            {
                return  ServiceResult<List<TaskDto>>.FailureResult(ErrorCode.ServiceError,ex.Message);
                
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
                DtoValidationResult validationResult = Validate(taskDto);
                
                if(!validationResult.IsValid)
                {
                    return ServiceResult<TaskDto>.FailureResult(ErrorCode.ValidationError,"Validation Error",validationResult.ValidationErrors);
                }

                TaskItem taskItem = _mapper.Map<TaskItem>(taskDto);
                taskItem.UserId = GetCurrentUserId();
                var result = await _taskItemRepo.AddAsync(taskItem);
                return ServiceResult<TaskDto>.SuccessResult(_mapper.Map<TaskDto>(result));
            }
            catch(Exception ex)
            {
                return ServiceResult<TaskDto>.FailureResult(ErrorCode.ServerError,ex.Message);
            }
        }
        public async Task<ServiceResult<TaskDto>> UpdateTaskServiceAsync(TaskDto taskDto)
        {
            try
            {
                DtoValidationResult validationResult = Validate(taskDto);
                if (!validationResult.IsValid)
                {
                    return ServiceResult<TaskDto>.FailureResult(ErrorCode.ValidationError,"Validation Error",validationResult.ValidationErrors);
                }
                TaskItem task = _mapper.Map<TaskItem>(taskDto);
                DataResponse<TaskItem> result = await _taskItemRepo.UpdateAsync( taskDto.Id, (taskItem) =>
                {
                    taskItem.Id = taskDto.Id;
                    taskItem.Title = taskDto.Title;
                    taskItem.Description = taskDto.Description;
                    taskItem.TaskStatus = taskDto.Status;
                    return taskItem;
                });
                if(result.IsSuccess)
                {
                    return ServiceResult<TaskDto>.SuccessResult(_mapper.Map<TaskDto>(result.Data));
                }
                return ServiceResult<TaskDto>.FailureResult(result.ErrorType,result.Message);
            }
            catch( Exception ex)
            {
                return ServiceResult<TaskDto>.FailureResult(ErrorCode.ServiceError,ex.Message);
            }
        }
        public async Task<ServiceResult<TaskDto>> DeleteTaskServiceAsync(int id)
        {
            try
            {
                var result = await _taskItemRepo.DeleteAsync(id);
                if(result.IsSuccess)
                {
                    return ServiceResult<TaskDto>.SuccessResult(_mapper.Map<TaskDto>(result.Data));
                }
                return ServiceResult<TaskDto>.FailureResult(ErrorCode.ServiceError);
            }
            catch (Exception ex)
            {
                return ServiceResult<TaskDto>.FailureResult(ErrorCode.ServerError,ex.Message);
            }
        }
        private List<TaskDto> ConvertListToDto(List<TaskItem> tasks)
        {
            List<TaskDto> taskDtos = new List<TaskDto>();
            foreach (var task in tasks)
            {
                taskDtos.Add(_mapper.Map<TaskDto>(task));
            }
            return taskDtos;
        }
        private string GetCurrentUserName()
        {
            var subject = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if(subject != null )
            {
                return subject;
            }
            return "";
        }
        private int GetCurrentUserId()
        {
            var userName = GetCurrentUserName();
            return 1;
        }
    }
}
