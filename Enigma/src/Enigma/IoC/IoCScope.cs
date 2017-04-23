using System;
using System.Collections.Generic;

namespace Enigma.IoC
{
    public class IoCScope : IServiceLocator, IDisposable
    {

        private List<IoCScopedInstance> _instances;

        public IoCScope(IoCContainer container)
        {
            Container = container;
            _instances = new List<IoCScopedInstance>();
        }

        public IoCContainer Container { get; }

        public T GetInstance<T>()
        {
            var registration = Container.GetRegistration(typeof(T));
            var instance = (T)registration.GetInstance();

            if (registration.CanBeScoped) {
                _instances.Add(new IoCScopedInstance(registration, instance));
            }

            return instance;
        }

        public void Dispose()
        {
            if (_instances != null) {
                foreach (var instance in _instances) {
                    instance.Registration.Unload(instance.Instance);
                }

                _instances = null;
            }
        }

        public object GetInstance(Type type)
        {
            var registration = Container.GetRegistration(type);
            var instance = registration.GetInstance();

            if (registration.CanBeScoped) {
                _instances.Add(new IoCScopedInstance(registration, instance));
            }

            return instance;
        }

        public bool TryGetInstance(Type type, out object instance)
        {
            if (!Container.TryGetRegistration(type, out IIoCRegistration registration)) {
                instance = null;
                return false;
            }

            instance = registration.GetInstance();

            if (registration.CanBeScoped) {
                _instances.Add(new IoCScopedInstance(registration, instance));
            }

            return true;
        }
    }
}