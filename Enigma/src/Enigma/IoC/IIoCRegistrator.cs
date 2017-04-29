using System;

namespace Enigma.IoC
{
    public interface IIoCRegistrator
    {
        void Register(IIoCRegistration registration);
        void Register(Type type, IIoCRegistration registration);
        bool TryRegister(Type type, IIoCRegistration registration);

        IIoCRegistration Get(Type type);
        bool TryGet(Type type, out IIoCRegistration registration);

        bool Contains(Type type);
    }
}