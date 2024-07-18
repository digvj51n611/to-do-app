using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;
using System.Security.Permissions;
using ToDoApp.Service.Models;

namespace ToDoApp.Server.Models
{
    public class ApiResponse<TResponse>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public TResponse? Data { get; set; }
        Dictionary<ErrorCode, List<string>> Errors;
        private ApiResponse()
        {
            Errors = new Dictionary<ErrorCode, List<string>>();
        }
        public static ApiResponse<TResponse> CreateApiResponse(ServiceResult<TResponse> serviceResult)
        {
            if (serviceResult.IsSuccess)
            {
                return new ApiResponse<TResponse>()
                {
                    IsSuccess = true,
                    Message = serviceResult.Message,
                    Data = serviceResult.Result,
                    Errors = new Dictionary<ErrorCode, List<string>>()
                };
            }
            if(serviceResult.ErrorCode == ErrorCode.ValidationError)
            {
                var errors = new Dictionary<ErrorCode, List<string>>();
                errors.Add(serviceResult.ErrorCode, serviceResult.ValidationErrors!);
                return new ApiResponse<TResponse>
                {
                    IsSuccess = false,
                    Message = serviceResult.Message,
                    Errors = errors
                };
            }
            //bad code ;;; to be removed
            var errorEntry = new Dictionary<ErrorCode, List<string>>();
            errorEntry.Add(serviceResult.ErrorCode,new List<string>() {serviceResult.Message});
            return new ApiResponse<TResponse>()
            {
                IsSuccess = false,
                Errors = errorEntry
            };
        }
    }
}
