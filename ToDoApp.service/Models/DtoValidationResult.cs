using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Service.Models
{
    public class DtoValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> ValidationErrors { get; set; }
        public DtoValidationResult()
        {
            ValidationErrors = new List<string>();
        }
    }
}
