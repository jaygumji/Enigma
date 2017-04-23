using System;

namespace Enigma.IoC
{
    internal class IoCRegistrationNotFoundException : Exception
    {
        public IoCRegistrationNotFoundException(Type type) : base("No registration has been made for " + type.FullName)
        {
        }

        public IoCRegistrationNotFoundException(string message) : base(message)
        {
        }

        public IoCRegistrationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}