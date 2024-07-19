using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Data.Models
{
    public class DataResponse<TData>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TData Data { get; set; }
        public ErrorType ErrorType { get; set; }
        public static DataResponse<TData> SuccessResult(TData data, string message = "fetch successful")
        {
            return new DataResponse<TData>
            {
                IsSuccess = true,
                Data = data,
                ErrorType = ErrorType.None,
                Message = message
            };
        }
        public static DataResponse<TData> FailureResult(ErrorType type, string message)
        {
            return new DataResponse<TData>
            {
                ErrorType = type,
                Message = message
            };
        }
        public static DataResponse<TData> FailureResult(Exception ex)
        {
            var errorType = GetErrorType(ex);
            return FailureResult(errorType, ex.Message);
        }

        private static ErrorType GetErrorType(Exception ex)
        {
            return ex switch
            {
                SqlException => ErrorType.ConnectionError,
                KeyNotFoundException => ErrorType.NotFoundError,
                DbUpdateException => ErrorType.SourceError,
                DbException => ErrorType.SourceError,
                _ => ErrorType.UnknownError
            };
        }
    }
}
