using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ToDoApp.Data.Entities;
using ToDoApp.Data.Models;
namespace ToDoApp.Service.Models
{
    public class ServiceResult<TResult> 
    {
        public bool IsSuccess { get; set; }
        public TResult? Result { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public string Message { get; set; }
        public List<string>? ValidationErrors { get; set; } 
        public ServiceResult()
        {
            Message = "";
            ValidationErrors = new List<string>();
        }
        public ServiceResult(bool success, TResult? result, ErrorCode code = ErrorCode.NoError , string message = "",List<string>? validationErrors = null)
        {
            IsSuccess = success;
            Result = result;
            ErrorCode = code;
            Message = message;
            ValidationErrors = validationErrors;
        }
        public static ServiceResult<TResult> SuccessResult(TResult data)
        {
            return new ServiceResult<TResult>(true, data);
        }
        public static ServiceResult<TResult> FailureResult(ErrorType errorType, string message="")
        {
            
            if (errorType == ErrorType.NotFoundError)
            {
                return new ServiceResult<TResult>(false, default, Models.ErrorCode.NotFoundError,message);
            }
            else if (errorType == ErrorType.SourceError || errorType == ErrorType.UnknownError || errorType == ErrorType.ConnectionError)
            {
                return new ServiceResult<TResult>(false, default, Models.ErrorCode.ServerError,message);
            }
            else
            {
                return new ServiceResult<TResult>(false, default, Models.ErrorCode.UnknownError,message);
            }
        }
        public static ServiceResult<TResult> FailureResult(ErrorCode error, string message = "", List<string>? validationErrors = null )
        {
            return new ServiceResult<TResult>(false,default,error,message,validationErrors);
        }
    }
}
