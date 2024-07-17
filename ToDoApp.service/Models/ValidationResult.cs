using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Service.Models
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> ValidationErrors { get; set; }
        public ValidationResult()
        {
            ValidationErrors = new List<string>();
        }
    }
}
