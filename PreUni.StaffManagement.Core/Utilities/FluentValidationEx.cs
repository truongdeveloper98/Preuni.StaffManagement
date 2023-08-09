using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PreUni.StaffManagement.Core.Utilities
{
    public static class FluentValidationEx
    {
        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
