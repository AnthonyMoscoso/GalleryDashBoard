using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Exceptions
{
    public class BadRequestException : Exception
    {
        private static readonly string DefaultMessage = "BadRequest";
        private static readonly int HRESULT = 400;
        public BadRequestException(string? message = null) : base(message ?? DefaultMessage)
        {
            HResult = HRESULT;
        }

        public static void ThrowIfTrue(bool condition, string? paramName = null)
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
          throw new BadRequestException(paramName);
    }

}