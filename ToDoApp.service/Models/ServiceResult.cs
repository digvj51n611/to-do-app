using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
namespace ToDoApp.Service.Models
{
    public class ServiceResult<TResult> 
    {
        public bool IsSuccess { get; set; }
        public TResult? Result { get; set; }

        public Exception? Exception { get; set; }
        public ServiceResult(bool success, TResult? result, Exception? message = null )
        {
            IsSuccess = success;
            Result = result;
            Exception = message;
        }
        public static ServiceResult<TResult> SuccessResult(TResult data)
        {
            return new ServiceResult<TResult>(true, data);
        }
        public static ServiceResult<TResult> FailureResult(Exception exception)
        {
            return new ServiceResult<TResult>(false,default,exception);
        }
    }
}
