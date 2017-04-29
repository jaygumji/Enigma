namespace Enigma.IoC
{
    public interface IIoCRegistrationSingletonConfigurator<T>
    {
        IIoCRegistrationSingletonConfigurator<T> IncludeAllInterfaces();
        IIoCRegistrationSingletonConfigurator<T> IncludeAllBaseTypes();
    }
}