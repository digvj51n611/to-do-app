using Microsoft.AspNetCore.Mvc;
using ToDoApp.Server.Models;
using ToDoApp.Service.Models;

namespace ToDoApp.Server.Helpers
{
    public static class ResponseHelper
    {
        public static ActionResult<TResult> FailedObjectResult<TResult>(this ControllerBase controller, ServiceResult<TResult> serviceResult)
        {
            return serviceResult.ErrorCode switch
            {
                ErrorCode.NotFoundError => controller.BadRequest(ApiResponse<TResult>.CreateApiResponse(serviceResult)),
                ErrorCode.ValidationError => controller.BadRequest(ApiResponse<TResult>.CreateApiResponse(serviceResult)),
                ErrorCode.AuthenticationError => controller.BadRequest(ApiResponse<TResult>.CreateApiResponse(serviceResult)),
                _ => controller.Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "An unexpected error occured",
                    detail: serviceResult.Message)
            };
        }
    }
}
