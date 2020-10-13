using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Tournament.WebApi.ExceptionHandler
{
    public static class CustomExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var exceptionId = Guid.NewGuid().ToString();

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var theException = contextFeature.Error;
                        var exceptionType = theException.GetType();

                        var message = new StringBuilder();

                        message.Append("The operation generated an exception.").Append(Environment.NewLine)
                               .Append($"Exception ID : {exceptionId}").Append(Environment.NewLine);

                       if (exceptionType == typeof(InvalidOperationException))
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                            message.Append(theException.Message).Append(Environment.NewLine);
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            message.Append($"Request: {context.Request.Method} {context.Request.GetDisplayUrl()}").Append(Environment.NewLine)
                                   .Append($"Exception Type: {theException.GetType()}").Append(Environment.NewLine)
                                   .Append($"Exception Message: {theException.GetBaseException().Message}");
                        }

                        context.Response.ContentType = "text/plain";

                        await context.Response.WriteAsync(message.ToString());
                    }
                });
            });
        }
    }
}