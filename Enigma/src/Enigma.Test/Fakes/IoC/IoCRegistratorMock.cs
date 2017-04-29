using System;
using Enigma.IoC;

namespace Enigma.Test.Fakes.IoC
{
    public class IoCRegistratorMock : IIoCRegistrator
    {
        public bool Contains(Type type)
        {
            return false;
        }

        public IIoCRegistration Get(Type type)
        {
            throw new NotImplementedException();
        }

        public void Register(IIoCRegistration registration)
        {
            throw new NotImplementedException();
        }

        public void Register(Type type, IIoCRegistration registration)
        {
            throw new NotImplementedException();
        }

        public bool TryGet(Type type, out IIoCRegistration registration)
        {
            registration = null;
            return false;
        }

        public bool TryRegister(Type type, IIoCRegistration registration)
        {
            throw new NotImplementedException();
        }
    }
}
