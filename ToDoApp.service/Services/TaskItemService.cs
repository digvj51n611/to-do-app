using AutoMapper;
using FluentValidation;
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
        public TaskItemService(ITaskItemRepo taskItemRepo , IMapper mapper ,IValidator<TaskDto> validator)
        {
            _taskItemRepo = taskItemRepo;
            _mapper = mapper;
            _validator = validator;
        }
        private bool ValidateTask(TaskDto taskDto)
        {
            if(taskDto == null)
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
        public async Task<ServiceResult<TaskDto>> GetTaskServiceAsync(int id)
        {
            try
            {
                TaskItem result = await _taskItemRepo.GetTaskItemAsync(id);
                return ServiceResult<TaskDto>.SuccessResult(_mapper.Map<TaskDto>(result));
            }
            catch (Exception ex)
            {
                return ServiceResult<TaskDto>.FailureResult(ex);
            }
        }
        public async Task<ServiceResult<List<TaskDto>>> GetTasksServiceAsync(string username)
        {
            try
            {
                List<TaskItem> tasks = await _taskItemRepo.GetTaskItemsAsync();
                List<TaskItem> userNameTasks = tasks.Where(task => task.User.Username == username).ToList();
                return ServiceResult<List<TaskDto>>.SuccessResult(ConvertList(userNameTasks));
            }
            catch(Exception ex)
            {
                return ServiceResult<List<TaskDto>>.FailureResult(ex);
            }
        }
        public  async Task<ServiceResult<List<TaskDto>>> GetTasksServiceAsync()
        {
            try
            {
                var tasks = await _taskItemRepo.GetTaskItemsAsync();
                return ServiceResult<List<TaskDto>>.SuccessResult(ConvertList(tasks));
            }
            catch(Exception ex)
            {
                return ServiceResult<List<TaskDto>>.FailureResult(ex);
            }
        }
        public async Task<ServiceResult<TaskDto>> AddTaskServiceAsync(TaskDto taskDto , int UserId )
        {
            try
            {
                ValidateTask(taskDto);
                TaskItem taskItem = _mapper.Map<TaskItem>(taskDto);
                taskItem.UserId = UserId;
                //AssignUser(taskItem);
                //will assign the user id from taken from the token 
                var result = await _taskItemRepo.AddTaskItemAsync(taskItem);
                return ServiceResult<TaskDto>.SuccessResult(_mapper.Map<TaskDto>(result));
            }
            catch( Exception ex)
            {
                return ServiceResult<TaskDto>.FailureResult(ex);
            }
        }
        public async Task<ServiceResult<TaskDto>> UpdateTaskServiceAsync(TaskDto taskDto)
        {
            try
            {
                TaskItem task = _mapper.Map<TaskItem>(taskDto);
                var result = await _taskItemRepo.UpdateTaskItemAsync(task);
                return ServiceResult<TaskDto>.SuccessResult(_mapper.Map<TaskDto>(result));
            }
            catch( Exception ex)
            {
                return ServiceResult<TaskDto>.FailureResult(ex);
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
                return ServiceResult<TaskDto>.FailureResult(ex);
            }
        }
    }
}
