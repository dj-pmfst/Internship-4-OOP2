using Microsoft.AspNetCore.Mvc;

namespace Api.Common
{
    public static class ResponseExtra
    {
        public static ActionResult ToActionResult<TValue>(this Result<TValue> result, ControllerBase controller) where TValue : class
        {
            var response = new Response<TValue>(result);

            if (result.HasError)
            {
                return controller.BadRequest(response);
            }
            return controller.Ok(response);
        }
    }
}
