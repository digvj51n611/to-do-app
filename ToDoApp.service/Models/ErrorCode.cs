using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Service.Models
{
    public enum ErrorCode
    {
        NoError,
        NotFoundError,
        ServerError,
        ValidationError,
        AuthenticationError,
        UnknownError,
        ServiceError
    }
}
