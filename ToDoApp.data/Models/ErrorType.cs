using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Data.Models
{
    public enum ErrorType
    {
        None,
        UnknownError,
        ConnectionError,
        SourceError,
        ValidationError,
        NotFoundError,
        SaveChangesError
    }
}
