using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common.Extensions;
using Domain.Common;
using FluentValidation;

namespace Presentation.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (CustomException exception)
            {
                await SendResponseAsync(context, exception.Message, exception.HttpStatusCode);
            }
            catch (ValidationException exception)
            {
                string data = string.Join("\n", exception.Errors);
                await SendResponseAsync(context, data, HttpStatusCode.BadRequest);
            }
            catch (Exception exception)
            {
                string msg = env.IsDevelopment() ? exception.UnwrapExceptionMessage() : $"Something went wrong. Reference Id: {Activity.Current?.Id}";
                exception.LogException();
                await SendResponseAsync(context, msg, HttpStatusCode.InternalServerError);
            }
        }

        private static Task SendResponseAsync(
            HttpContext context, object problemDetails, HttpStatusCode httpStatusCode)
        {
            // https://www.kimgunnarsson.se/bring-clarity-into-your-http-api-errors-using-problem-details/
            string result =
                 JsonSerializer.Serialize(
                     problemDetails,
                     MatchStyleOfExistingMvcProblemDetails());

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;
            return context.Response.WriteAsync(result);

            static JsonSerializerOptions MatchStyleOfExistingMvcProblemDetails()
            {
                return new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.Never,
                };
            }
        }
    }
}
