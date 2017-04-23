namespace Enigma.IoC
{
    public class IoCScopedInstance
    {
        public IIoCRegistration Registration { get; }
        public object Instance { get; }

        public IoCScopedInstance(IIoCRegistration registration, object instance)
        {
            Registration = registration;
            Instance = instance;
        }
    }
}