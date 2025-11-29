using Application.Common.Model;
using Microsoft.AspNetCore.Mvc;

namespace Api.Common
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result) where T : class
        {
            if (!result.isAuthorized)
                return new UnauthorizedResult();

            if (result.HasError)
            {
                var errors = result.Errors.Select(e => new { e.Code, e.Message });
                return new BadRequestObjectResult(errors);
            }
            return new OkObjectResult(result.Value);
        }
    }
}
