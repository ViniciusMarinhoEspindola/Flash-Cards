using FluentValidation.Results;

namespace Application.Common
{
    public static class ValidationExtensions
    {
        public static Result<T> ToFailResult<T>(this ValidationResult validation)
        {
            var message = validation.Errors[0].ErrorMessage;
            return Result<T>.Fail(AppError.Validation(message));
        }
    }
}
