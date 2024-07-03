using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Service.Models;

namespace ToDoApp.Data.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(dto => dto.Username)
                 .NotEmpty()
                 .MinimumLength(3)
                 .WithMessage("Username must be atleast 3 char long")
                 .MaximumLength(15)
                 .WithMessage("Username should be atmost 15 char long")
                 .Matches("^[a-z][a-z0-9\\._]*$")
                 .WithMessage("User name must have only the lowercase alphabet,'.','_'");
            RuleFor(dto => dto.Password)
                .NotEmpty()
                .MinimumLength(5)
                .WithMessage("Password must be atleast 5 chars long")
                .MaximumLength(50)
                .WithMessage("Password must be atmost 50 chars long")
                .Matches("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_])([a-zA-Z0-9\\W_]+)$")
                .WithMessage("Password includes Uppercase and lowercase english alphabet,digts,special characters atleast one of each type");
        }
    }
}
