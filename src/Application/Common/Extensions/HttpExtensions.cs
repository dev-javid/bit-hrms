using Microsoft.AspNetCore.Http;

namespace Application.Common.Extensions
{
    public static class HttpExtensions
    {
        public static string GetDeviceIdentifier(this IHttpContextAccessor httpContextAccessor)
        {
            var deviceId = httpContextAccessor.HttpContext?.Request?.Headers["X-DeviceId"];

            if (string.IsNullOrEmpty(deviceId?.ToString()?.Trim()))
            {
                throw CustomException.WithBadRequest("deviceId not found in request body");
            }

            return deviceId.ToString()!;
        }
    }
}
