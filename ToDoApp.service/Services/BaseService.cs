using FluentValidation;
using FluentValidation.Results;
using ToDoApp.Service.Models;

namespace ToDoApp.Service.Services
{
    public class BaseService
    {
        protected static DtoValidationResult Validate<TDto>(TDto baseDto, Func<TDto, ValidationResult> validator)
        {
            List<string> validationMessages = new List<string>();
            if (baseDto == null)
            {
                validationMessages.Add("Null can't be validated");
                return new DtoValidationResult
                {
                    IsValid = false,
                    ValidationErrors = validationMessages
                };
            }
            ValidationResult validationResult = validator.Invoke(baseDto);
            if (validationResult.IsValid)
            {
                return new DtoValidationResult
                {
                    IsValid = validationResult.IsValid,
                    ValidationErrors = validationMessages
                };
            }
            foreach (var error in validationResult.Errors)
            {
                validationMessages.Add(error.ErrorMessage);
            }
            return new DtoValidationResult
            {
                IsValid = false,
                ValidationErrors = validationMessages
            };
        }
    }
}
