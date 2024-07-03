using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using ToDoApp.Data.Entities;
using ToDoApp.Data.IRepos;
using ToDoApp.Service.IServices;
using ToDoApp.Service.Models;

namespace ToDoApp.Service.Services
{
    public class UserService : IUserService
    {
        private IUserRepo _userRepo;
        private IMapper _mapper;
        private IValidator<UserDto> _validator;
        public UserService(IUserRepo userRepo, IMapper mapper,IValidator<UserDto> validator)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _validator = validator;
        }
        private bool ValidateUser(UserDto userDto)
        {
            if (userDto == null) return false;
            ValidationResult result = _validator.Validate(userDto);
            if (result.IsValid) return true;
            string errorMessage = "";
            foreach (var error in result.Errors)
            {
                errorMessage += "\n" + errorMessage;
            }
            throw new Exception(errorMessage);
        }
        public async Task<ServiceResult<UserDto>> GetUserServiceAsync(int id)
        {
            try
            {
                var result = await _userRepo.GetUserAsync(id);
                return ServiceResult<UserDto>.SuccessResult(_mapper.Map<UserDto>(result));
            }
            catch (Exception ex)
            {
                return ServiceResult<UserDto>.FailureResult(ex);
            }
        }
        public async Task<ServiceResult<List<string>>> GetUsernamesAsync()
        {
            var result = await GetUsersServiceAsync();
            List<string> userNames = new List<string>();
            if( result.IsSuccess )
            {
                var users = result.Result;
                var names = users.Select(user => user.Username).ToList();
                return ServiceResult<List<string>>.SuccessResult(names);    
            }
            else
            {
                return ServiceResult<List<string>>.FailureResult(result.Exception);
            }
        }
        public async Task<ServiceResult<List<UserDto>>> GetUsersServiceAsync()
        {
            try
            {
                var result = await _userRepo.GetUsersAsync();
                List<UserDto> resultList = new List<UserDto>();
                foreach (var task in result)
                {
                    resultList.Add(_mapper.Map<UserDto>(result));
                }
                return ServiceResult<List<UserDto>>.SuccessResult(resultList);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<UserDto>>.FailureResult(ex);
            }
        }
        public async Task<ServiceResult<UserDto>> AddUserServiceAsync(UserDto userDto)
        {
            try
            {
                ValidateUser(userDto);
                User user = _mapper.Map<User>(userDto);
                var result = await _userRepo.AddUserAsync(user);
                return ServiceResult<UserDto>.SuccessResult(_mapper.Map<UserDto>(result));
            }
            catch (Exception ex)
            {
                return ServiceResult<UserDto>.FailureResult(ex);
            }
        }
        public async Task<ServiceResult<UserDto>> UpdateUserServiceAsync(UserDto userDto)
        {
            try
            {
                User user = _mapper.Map<User>(userDto);
                var result = await _userRepo.UpdateUserAsync(user);
                return ServiceResult<UserDto>.SuccessResult(_mapper.Map<UserDto>(result));
            }
            catch (Exception ex)
            {
                return ServiceResult<UserDto>.FailureResult(ex);
            }
        }
        public async Task<ServiceResult<UserDto>> DeleteUserServiceAsync(int id)
        {
            try
            {
                var result = await _userRepo.DeleteUserAsync(id);
                return ServiceResult<UserDto>.SuccessResult(_mapper.Map<UserDto>(result));
            }
            catch (Exception ex)
            {
                return ServiceResult<UserDto>.FailureResult(ex);
            }
        }
    }
}
