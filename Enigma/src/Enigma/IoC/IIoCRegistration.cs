using System;

namespace Enigma.IoC
{
    public interface IIoCRegistration : IInstanceFactory
    {
        Type Type { get; }
        IoCOptions Options { get; }
        bool CanBeScoped { get; }

        bool HasInstanceGetter { get; }

        void Unload(object instance);
    }

    public interface IIoCRegistration<T> : IIoCRegistration, IInstanceFactory<T>
    {

    }
}