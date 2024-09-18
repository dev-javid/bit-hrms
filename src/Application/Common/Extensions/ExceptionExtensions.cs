using System.Text;
using Serilog;

namespace Application.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static string UnwrapExceptionMessage(this Exception exception)
        {
            var exceptions = GetExceptions(exception);
            var sb = new StringBuilder();
            foreach (var item in exceptions)
            {
                sb.AppendLine(item.Message);
                sb.AppendLine(item.StackTrace);
            }

            return sb.ToString().Trim();
        }

        public static void LogException(this Exception exception)
        {
            var errorDetails = exception.UnwrapExceptionMessage();
            Log.Error(errorDetails);
        }

        private static List<Exception> GetExceptions(Exception exception, List<Exception> exceptions = null!)
        {
            exceptions ??= [];

            exceptions.Add(exception);
            if (exception.InnerException != null)
            {
                return GetExceptions(exception.InnerException, exceptions);
            }
            else
            {
                exceptions.Reverse();
                return exceptions;
            }
        }
    }
}
