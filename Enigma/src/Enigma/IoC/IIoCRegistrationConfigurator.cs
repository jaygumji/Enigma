using System;

namespace Enigma.IoC
{
    public interface IIoCRegistrationConfigurator<T>
    {
        IIoCRegistrationConfigurator<T> IncludeAllInterfaces();
        IIoCRegistrationConfigurator<T> IncludeAllBaseTypes();

        IIoCRegistrationConfigurator<T> OnUnload(Action<T> unloader);
        IIoCRegistrationConfigurator<T> NeverScope();
        IIoCRegistrationConfigurator<T> MustBeScoped();
    }

}
