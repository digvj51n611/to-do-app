using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using ToDoApp.Data.Entities;
namespace ToDoApp.Data.Validators
{
    public class TaskItemValidator : AbstractValidator<User>
    {
        public TaskItemValidator()
        {
            RuleFor(user => user.Username)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
