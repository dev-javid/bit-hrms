using System.Net;

namespace Domain.Common;

public class CustomException : Exception
{
    private CustomException(string message, HttpStatusCode statusCode) : base(message)
    {
        HttpStatusCode = statusCode;
    }

    public HttpStatusCode HttpStatusCode { get; }

    public static CustomException WithBadRequest(string message)
    {
        return new CustomException(message, HttpStatusCode.BadRequest);
    }

    public static CustomException WithNotFound(string message)
    {
        return new CustomException(message, HttpStatusCode.NotFound);
    }

    public static CustomException WithInternalServer(string message)
    {
        return new CustomException(message, HttpStatusCode.InternalServerError);
    }

    public static CustomException WithUnauthorozed(string message)
    {
        return new CustomException(message, HttpStatusCode.Unauthorized);
    }

    public static CustomException WithForbidden(string message)
    {
        return new CustomException(message, HttpStatusCode.Forbidden);
    }
}
