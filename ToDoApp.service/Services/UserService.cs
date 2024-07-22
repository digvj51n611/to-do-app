using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using ToDoApp.Data.Entities;
using ToDoApp.Data.IRepos;
using ToDoApp.Data.Models;
using ToDoApp.Service.IServices;
using ToDoApp.Service.Models;

namespace ToDoApp.Service.Services
{
    public class UserService : BaseService, IUserService
    {
        private IUserRepo _userRepo;
        private IMapper _mapper;
        private IValidator<UserDto> _validator;
        public UserService(IUserRepo userRepo, IMapper mapper, IValidator<UserDto> validator)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _validator = validator;
        }
        private async Task<bool> IsUserUniqueAsync(UserDto userDto)
        {
            var serviceResult = await GetUsernamesAsync();
            List<string> names = serviceResult.Result!;
            if (names.FirstOrDefault(name => name == userDto.Username) != null)
            {
                throw new Exception("User with that username already exists");
            }
            return true;
        }
        private bool ValidateUser(UserDto userDto)
        {
            if (userDto == null) return false;
            var result = _validator.Validate(userDto);
            if (result.IsValid) return true;
            string errorMessage = "";
            foreach (var error in result.Errors)
            {
                errorMessage += error + "\n";
            }
            throw new Exception(errorMessage);
        }
        private string GetHashedPassword(UserDto userDto)
        {
            PasswordHasher<UserDto> hasher = new PasswordHasher<UserDto>();
            return hasher.HashPassword(userDto, userDto.Password);
        }
        private async Task<ServiceResult<List<string>>> GetUsernamesAsync()
        {
            var result = await GetUsersServiceAsync();
            List<string> userNames = new List<string>();
            if (result.IsSuccess)
            {
                var users = result.Result;
                var names = users!.Select(user => user.Username).ToList();
                return ServiceResult<List<string>>.SuccessResult(names);
            }
            else
            {
                return ServiceResult<List<string>>.FailureResult(ErrorCode.ServerError,result.Message);
            }
        }
        public async Task<ServiceResult<UserDto>> GetUserServiceAsync(string username)
        {
            try
            {
                var result = await _userRepo.GetAllAsync();
                if(!result.IsSuccess)
                {
                    return ServiceResult<UserDto>.FailureResult(result.ErrorType, result.Message);
                }
                return ServiceResult<UserDto>.SuccessResult(_mapper.Map<UserDto>(result.Data));
            }
            catch (Exception ex)
            {
                return ServiceResult<UserDto>.FailureResult(ErrorCode.ServiceError,ex.Message);
            }
        }
        public async Task<ServiceResult<List<UserDto>>> GetUsersServiceAsync()
        {
            try
            {
                var result = await _userRepo.GetAllAsync();
                List<UserDto> resultList = new List<UserDto>();
                if(!result.IsSuccess)
                {
                    return ServiceResult<List<UserDto>>.FailureResult(result.ErrorType, result.Message);
                }
                foreach (var task in result.Data)
                {
                    resultList.Add(_mapper.Map<UserDto>(task));
                }
                return ServiceResult<List<UserDto>>.SuccessResult(resultList);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<UserDto>>.FailureResult(ErrorCode.ServiceError,ex.Message);
            }
        }
        public async Task<ServiceResult<UserDto>> AddUserServiceAsync(UserDto userDto)
        {
            try
            {
                var validationResult = Validate(userDto, _validator.Validate);
                if (!validationResult.IsValid)
                {
                    return ServiceResult<UserDto>.FailureResult(ErrorCode.ValidationError,"Validation Error",validationResult.ValidationErrors);
                }
                if(!await IsUserUniqueAsync(userDto))
                {
                    return ServiceResult<UserDto>.FailureResult(ErrorCode.ServiceError,"User already exists");
                }
                userDto.Password = GetHashedPassword(userDto);
                User user = _mapper.Map<User>(userDto);
                var result = await _userRepo.AddAsync(user);
                return ServiceResult<UserDto>.SuccessResult(_mapper.Map<UserDto>(result.Data));
            }
            catch (Exception ex)
            {
                return ServiceResult<UserDto>.FailureResult(ErrorCode.ServerError,ex.Message);
            }
        }
        public async Task<ServiceResult<UserDto>> UpdateUserServiceAsync(UserDto userDto)
        {
            try
            {
                var validationResult = Validate(userDto, _validator.Validate);
                if (!validationResult.IsValid)
                {
                    return ServiceResult<UserDto>.FailureResult(ErrorCode.ValidationError,"Validaiton Errors",validationResult.ValidationErrors);
                }
                if(!await IsUserUniqueAsync(userDto))
                {
                    return ServiceResult<UserDto>.FailureResult(ErrorCode.ServiceError, "User already exists");
                }
                User user = _mapper.Map<User>(userDto);
                // passed some int for now 
                var result = await _userRepo.UpdateAsync(1, (user) =>
                {
                    user.Username = userDto.Username;
                    return user;
                });
                if (!result.IsSuccess)
                {
                    return ServiceResult<UserDto>.FailureResult(result.ErrorType, result.Message);
                }
                return ServiceResult<UserDto>.SuccessResult(_mapper.Map<UserDto>(result.Data));
            }
            catch (Exception ex)
            {
                return ServiceResult<UserDto>.FailureResult(ErrorCode.ServiceError,ex.Message);
            }
        }
        public async Task<ServiceResult<UserDto>> DeleteUserServiceAsync(string username)
        {
            try
            {
                // passed some int for now 
                var result = await _userRepo.DeleteAsync(1);
                if (!result.IsSuccess)
                {
                    return ServiceResult<UserDto>.FailureResult(result.ErrorType,result.Message);
                }
                return ServiceResult<UserDto>.SuccessResult(_mapper.Map<UserDto>(result.Data));
            }
            catch (Exception ex)
            {
                return ServiceResult<UserDto>.FailureResult(ErrorCode.ServiceError,ex.Message);
            }
        }
    }
}

