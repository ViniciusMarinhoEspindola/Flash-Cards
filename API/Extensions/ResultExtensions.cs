using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller)
        {
            if (result.IsSuccess)
                return controller.Ok(result.Data);

            return result.Error!.Code switch
            {
                ErrorCode.NotFound => controller.NotFound(result.Error.Message),
                ErrorCode.Unauthorized => controller.Unauthorized(result.Error.Message),
                ErrorCode.Validation => controller.BadRequest(result.Error.Message),
                ErrorCode.Conflict => controller.Conflict(result.Error.Message),
                _ => controller.StatusCode(500, "Erro interno.")
            };
        }
    }
}
