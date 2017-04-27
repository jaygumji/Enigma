using System;
using System.Collections.Generic;

namespace Enigma.IoC
{
    public class IoCScope : IDisposable
    {

        private Dictionary<Type, IoCScopedInstance> _instances;

        public IoCScope(IoCContainer container, IoCScope parent)
        {
            Container = container;
            Parent = parent;
            _instances = new Dictionary<Type, IoCScopedInstance>();
        }

        public IoCContainer Container { get; }
        public IoCScope Parent { get; private set; }

        public int InstanceCount => _instances.Count;

        public void Reroute(IoCScope parent)
        {
            Parent = parent;
        }

        public void Dispose()
        {
            if (_instances != null) {
                foreach (var instance in _instances.Values) {
                    if (instance.Registration != null) {
                        instance.Registration.Unload(instance.Instance);
                    }
                    else {
                        (instance.Instance as IDisposable)?.Dispose();
                    }
                }

                _instances = null;
            }
            Container.EndScope(this);
        }

        public void Register(IIoCRegistration registration, object instance)
        {
            _instances.Add(registration.Type, new IoCScopedInstance(registration, instance));
        }

        public void Register(object instance)
        {
            if (instance == null) return;
            var type = instance.GetType();
            _instances.Add(type, new IoCScopedInstance(null, instance));
        }

        public bool TryGetInstance(Type type, out object instance)
        {
            if (_instances.TryGetValue(type, out IoCScopedInstance instReg)) {
                instance = instReg.Instance;
                return true;
            }
            instance = null;
            return false;
        }
    }
}