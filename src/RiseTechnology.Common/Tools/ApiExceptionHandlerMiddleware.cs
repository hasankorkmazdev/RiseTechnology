using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
using RiseTechnology.Common.Models.Base;
using System.Net;
using RiseTechnology.Common.Tools.Exceptions;

namespace RiseTechnology.Common.Tools
{
    public class ApiExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ApiExceptionHandlerMiddleware> logger;
        private readonly IHostingEnvironment environment;
        public ApiExceptionHandlerMiddleware(ILogger<ApiExceptionHandlerMiddleware> logger, IHostingEnvironment environment)
        {
            this.environment = environment;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (BadRequestException ex)
            {
                var exceptionKey = Guid.NewGuid();
                logger.LogError(message: $"ApiExceptionMiddleware -> ExceptionKey:{exceptionKey} Something went wrong. ", exception: ex);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(
                  JsonSerializer.Serialize(
                     new ServiceResultModel(
                         message: ex.Message+$".ExceptionKey:{exceptionKey}",
                         code: HttpStatusCode.BadRequest)
                  ));
            }
            catch (Exception ex)
            {
                var exceptionKey = Guid.NewGuid();
                logger.LogError(message: $"ApiExceptionMiddleware -> ExceptionKey:{exceptionKey} Something went wrong. ", exception: ex);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(
                   JsonSerializer.Serialize(
                      new ServiceResultModel(
                          message: $"Something went wrong.ExceptionKey:{exceptionKey}",
                          code: HttpStatusCode.InternalServerError)
                   ));
            }
        }
    }
}
