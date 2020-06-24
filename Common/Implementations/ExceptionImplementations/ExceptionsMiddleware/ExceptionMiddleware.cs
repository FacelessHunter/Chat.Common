using System;
using System.Threading.Tasks;
using Common.Domain.DTOs;
using Common.Domain.Enums;
using Common.Domain.ActionResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Common.Implementations.ExceptionImplementations.Exceptions;

namespace Common.Implementations.ExceptionImplementations.ExceptionsMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IActionResultExecutor<ObjectResult> _actionResultExecutor;

        public ExceptionMiddleware(RequestDelegate next, IActionResultExecutor<ObjectResult> actionResultExecutor)
        {
            _next = next;
            _actionResultExecutor = actionResultExecutor;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException ex)
            {
                await SendResponseAsync(httpContext, new NotFoundObjectResult(ex.Error));
            }
            catch (InvalidDataException ex)
            {
                await SendResponseAsync(httpContext, new BadRequestObjectResult(ex.Error));
            }
            catch (InvalidPermissionException ex)
            {
                await SendResponseAsync(httpContext, new ForbiddenObjectResult(ex.Error));
            }
            catch (Exception ex)
            {
                await SendResponseAsync(httpContext, new InternalServerErrorObjectResult(new ErrorDTO(ErrorCode.UnknownError, ex.Message)));
            }
        }

        /// <summary>
        /// Executes passed action result.
        /// </summary>
        /// <param name="context">HttpContext of current request.</param>
        /// <param name="objectResult">Instance of ObjectResult implementation, contains error data.</param>
        private Task SendResponseAsync(HttpContext context, ObjectResult objectResult) =>
                _actionResultExecutor.ExecuteAsync(new ActionContext() { HttpContext = context }, objectResult);

    }
}
