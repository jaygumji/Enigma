using System;

namespace Enigma.IoC
{
    public class IoCFactoryException : CodedException
    {

        public IoCFactoryException(string message, int errorCode) : base(message, errorCode)
        {

        }

        public static Exception AmbigiousConstructor(Type type)
        {
            var msg = $"Unabled to find suitable constructor for {type.FullName}";
            return new IoCFactoryException(msg, IoCAmbigiousConstructor);
        }
    }
}