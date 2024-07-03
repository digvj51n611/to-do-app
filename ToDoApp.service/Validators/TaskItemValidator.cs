using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop.Infrastructure;
using ToDoApp.Data.Entities;
using ToDoApp.Service.Models;
namespace ToDoApp.Data.Validators
{
    public class TaskDtoValidator : AbstractValidator<TaskDto>
    {
        public TaskDtoValidator()
        {
            RuleFor(dto => dto.Title)
                .NotEmpty()
                .MinimumLength(1)
                .WithMessage("Atleast one character for title")
                .MaximumLength(50)
                .WithMessage("Atmost 50 char for the title");
            RuleFor(dto => dto.Description)
                .NotEmpty()
                .MinimumLength(1)
                .WithMessage("Atleast one character for title");
            RuleFor(dto => dto.AddedOnUtc)
                .NotEmpty()
                .Must(date => date <= DateTime.UtcNow)
                .WithMessage("The date entered can't exceed current date and time ");
        }
    }
}
