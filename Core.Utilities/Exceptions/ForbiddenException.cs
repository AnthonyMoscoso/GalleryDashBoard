using System.Diagnostics.CodeAnalysis;

namespace Core.Utilities.Exceptions
{
    public class ForbiddenException : Exception
    {
        private static readonly string DefaultMessage = "Forbidden access";
        private static readonly int HRESULT = 403;
        public ForbiddenException(string? message = null) : base(message ?? DefaultMessage)
        {
            HResult = HRESULT;
        }

        public static void ThrowIfTrue(bool condition,string? paramName = null)
        {
            if (condition)
            {
                Throw(paramName);
            }
        }
        public static void ThrowIfFalse(bool condition, string? paramName = null)
        {
            if (!condition)
            {
                Throw(paramName);
            }
        }

        [DoesNotReturn]
        private static void Throw(string? paramName) =>
          throw new ForbiddenException(paramName);
    }

}
