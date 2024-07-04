using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
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
            ValidationResult result = _validator.Validate(userDto);
            if (result.IsValid) return true;
            string errorMessage = "";
            foreach (var error in result.Errors)
            {
                errorMessage += error+"\n";
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
                var names = users.Select(user => user.Username).ToList();
                return ServiceResult<List<string>>.SuccessResult(names);
            }
            else
            {
                return ServiceResult<List<string>>.FailureResult(result.Exception, ErrorCode.ServerError);
            }
        }
        public async Task<ServiceResult<UserDto>> GetUserServiceAsync(string username)
        {
            try
            {
                var result = await _userRepo.GetUserAsync(username);
                return ServiceResult<UserDto>.SuccessResult(_mapper.Map<UserDto>(result));
            }
            catch (Exception ex)
            {
                if(ex.Message != "NotFoundError")
                return ServiceResult<UserDto>.FailureResult(ex , ErrorCode.ServerError);
                return ServiceResult<UserDto>.FailureResult(ex,ErrorCode.NotFoundError);
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
                return ServiceResult<List<UserDto>>.FailureResult(ex, ErrorCode.ServerError);
            }
        }
        public async Task<ServiceResult<UserDto>> AddUserServiceAsync(UserDto userDto)
        {
            try
            {
                try
                {
                    ValidateUser(userDto);
                }
                catch(Exception ex )
                {
                    Console.WriteLine("validation catch called");
                    return ServiceResult<UserDto>.FailureResult(ex, ErrorCode.ValidationError);
                }
                await IsUserUniqueAsync(userDto);
                userDto.Password = GetHashedPassword(userDto);
                User user = _mapper.Map<User>(userDto);
                var result = await _userRepo.AddUserAsync(user);
                return ServiceResult<UserDto>.SuccessResult(_mapper.Map<UserDto>(result));
            }
            catch (Exception ex)
            {
                return ServiceResult<UserDto>.FailureResult(ex, ErrorCode.ServerError);
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
                return ServiceResult<UserDto>.FailureResult(ex, ErrorCode.ServerError);
            }
        }
        public async Task<ServiceResult<UserDto>> DeleteUserServiceAsync(string username)
        {
            try
            {
                var result = await _userRepo.DeleteUserAsync(username);
                return ServiceResult<UserDto>.SuccessResult(_mapper.Map<UserDto>(result));
            }
            catch (Exception ex)
            {
                return ServiceResult<UserDto>.FailureResult(ex , ErrorCode.ServerError);
            }
        }
    }
}
