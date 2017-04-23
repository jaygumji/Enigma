using System;

namespace Enigma
{
    public class CodedException : Exception
    {
        public const int IoCAmbigiousConstructor = 1001;

        public CodedException()
        {
        }

        public CodedException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public CodedException(string message) : base(message)
        {
        }

        public CodedException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public CodedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CodedException(string message, int errorCode, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; }

    }
}
