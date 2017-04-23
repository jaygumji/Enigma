using System;

namespace Enigma.IoC
{
    public class IoCRegistration<T> : IIoCRegistration<T>
    {
        public Type Type { get; }
        public Func<T> Factory { get; }
        public IoCOptions Options { get; }

        public IoCRegistration(IoCOptions options) : this(null, options)
        {
        }

        public IoCRegistration(Func<T> factory, IoCOptions options)
        {
            Type = typeof(T);
            Factory = factory;
            Options = options;
        }

        public bool CanBeScoped { get; set; }
        public Action<T> Unloader { get; set; }

        public bool HasInstanceGetter => Factory != null;

        public object GetInstance()
        {
            return Factory.Invoke();
        }

        T IInstanceFactory<T>.GetInstance()
        {
            return Factory.Invoke();
        }

        void IIoCRegistration.Unload(object instance)
        {
            if (Unloader != null) {
                var typedInstance = (T)instance;
                Unloader.Invoke(typedInstance);
            }

            (instance as IDisposable)?.Dispose();
        }
    }
}