using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public class Result<T>
    {
        public T? Data { get; private set; }
        public bool IsSuccess { get; private set; }
        public AppError? Error { get; private set; }

        public static Result<T> Ok(T data) =>
            new() { Data = data, IsSuccess = true };

        public static Result<T> Fail(AppError error) =>
            new() { IsSuccess = false, Error = error };
    }

    public record AppError(ErrorCode Code, string Message)
    {
        public static AppError NotFound(string message) => new(ErrorCode.NotFound, message);
        public static AppError Unauthorized(string message) => new(ErrorCode.Unauthorized, message);
        public static AppError Validation(string message) => new(ErrorCode.Validation, message);
        public static AppError Conflict(string message) => new(ErrorCode.Conflict, message);
    }

    public enum ErrorCode
    {
        NotFound,
        Unauthorized,
        Validation,
        Conflict
    }
}
