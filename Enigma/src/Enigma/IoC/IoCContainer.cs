using System;
using System.Collections.Generic;
using System.Reflection;

namespace Enigma.IoC
{
    public class IoCContainer : IServiceLocator
    {

        private readonly Dictionary<Type, IIoCRegistration> _registrations;
        private readonly IoCFactory _factory;

        public IoCContainer()
        {
            _registrations = new Dictionary<Type, IIoCRegistration>();
            _factory = new IoCFactory(_registrations);
        }

        public void Register<T>(T singleton)
        {
            Register(singleton, IoCOptions.Default);
        }

        public void Register<T>(T singleton, IoCOptions options)
        {
            var registration = new IoCRegistration<T>(() => singleton, options) {
                CanBeScoped = false
            };
            AddRegistration(registration);
        }

        public void Register<T, TImplementation>()
        {
            Register<T, TImplementation>(IoCOptions.Default);
        }

        public void Register<T, TImplementation>(IoCOptions options)
        {
            var registration = new IoCRegistration<TImplementation>(options) {
                CanBeScoped = true
            };
            AddRegistrationType(typeof(T), registration, overrideExisting: true);
            AddRegistration(registration);
        }

        public void Register<T>(Func<T> factory)
        {
            Register(factory, IoCOptions.Default);
        }

        public void Register<T>(Func<T> factory, IoCOptions options)
        {
            var registration = new IoCRegistration<T>(factory, options) {
                CanBeScoped = true
            };
            AddRegistration(registration);
        }

        public void Register<T>(Func<T> factory, Action<T> unloader)
        {
            Register(factory, unloader, IoCOptions.Default);
        }

        public void Register<T>(Func<T> factory, Action<T> unloader, IoCOptions options)
        {
            var registration = new IoCRegistration<T>(factory, options) {
                CanBeScoped = true,
                Unloader = unloader
            };
            AddRegistration(registration);
        }

        public bool TryGetRegistration(Type type, out IIoCRegistration registration)
        {
            return _registrations.TryGetValue(type, out registration);
        }

        private void AddRegistrationType(Type type, IIoCRegistration registration, bool overrideExisting)
        {
            if (_registrations.ContainsKey(type)) {
                _registrations[type] = registration;
            }
            else {
                _registrations.Add(type, registration);
            }
        }

        private void AddRegistration(IIoCRegistration registration)
        {
            if (registration.Options.IncludeAllInterfaces) {
                foreach (var type in registration.Type.GetTypeInfo().GetInterfaces()) {
                    AddRegistrationType(type, registration, overrideExisting: false);
                }
            }
            if (registration.Options.IncludeAllBaseTypes) {
                var info = registration.Type.GetTypeInfo();
                while (info.BaseType != null) {
                    AddRegistrationType(info.BaseType, registration, overrideExisting: false);
                    info = info.BaseType.GetTypeInfo();
                }
            }

            AddRegistrationType(registration.Type, registration, overrideExisting: true);
        }

        public T GetInstance<T>()
        {
            return (T) _factory.GetInstance(typeof(T));
        }

        public IIoCRegistration GetRegistration(Type type)
        {
            if (_registrations.TryGetValue(type, out IIoCRegistration registration)) {
                return registration;
            }

            throw new IoCRegistrationNotFoundException(type);
        }

        public object GetInstance(Type type)
        {
            return _factory.GetInstance(type);
        }

        public bool TryGetInstance(Type type, out object instance)
        {
            if (_registrations.TryGetValue(type, out IIoCRegistration registration)) {
                instance = registration.GetInstance();
                return true;
            }

            instance = null;
            return false;
        }
    }
}
