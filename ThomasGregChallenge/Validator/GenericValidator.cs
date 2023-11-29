using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ThomasGregChallenge.Validators
{
    public static class GenericValidator
    {
        public static async Task<ModelStateDictionary?> ValidateRequest<T>(IValidator<T> validator, T request) where T : class
        {
            ValidationResult validationResult = await validator.ValidateAsync(request);

            if (validationResult.IsValid)
                return null;

            var modelStateDictionary = new ModelStateDictionary();

            foreach (ValidationFailure validationFailure in validationResult.Errors)
                modelStateDictionary.AddModelError(
                    validationFailure.PropertyName,
                    validationFailure.ErrorMessage);

            return modelStateDictionary;
        }
    }
}