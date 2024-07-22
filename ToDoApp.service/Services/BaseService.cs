using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using System.Runtime.InteropServices;
using ToDoApp.Data.Entities;
using ToDoApp.Service.Models;

namespace ToDoApp.Service.Services
{
    public class BaseService<TDto>
    {
        private readonly IValidator<TDto> _validator;
        public BaseService(IValidator<TDto> validator)
        {
            _validator = validator;
        }
        protected DtoValidationResult Validate(TDto baseDto)
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
            //ValidationResult validationResult = validator.Invoke(baseDto);
            ValidationResult validationResult = _validator.Validate(baseDto);
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
