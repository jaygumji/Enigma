using Enigma.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Enigma.IoC
{
    public class IoCContainer : IServiceLocator
    {

        private static readonly AsyncLocal<IoCScope> ScopeLocal = new AsyncLocal<IoCScope>();

        private readonly Dictionary<Type, IIoCRegistration> _registrations;
        private readonly IoCFactory _factory;

        public IoCContainer() : this(new CachedTypeProvider())
        {
        }

        public IoCContainer(ITypeProvider provider)
        {
            _registrations = new Dictionary<Type, IIoCRegistration>();
            _factory = new IoCFactory(_registrations, provider);
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
            var type = typeof(T);
            var scope = ScopeLocal.Value;
            if (scope != null && scope.TryGetInstance(type, out object instance)) {
                return (T)instance;
            }
            var newInstance = (T) _factory.GetInstance(type);
            if (scope != null) {
                if (TryGetRegistration(type, out IIoCRegistration registration)) {
                    if (!registration.CanBeScoped) return newInstance;
                    scope.Register(registration, newInstance);
                }
                else {
                    scope.Register(newInstance);
                }
            }
            return newInstance;
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
                if (!registration.CanBeScoped) return true;

                ScopeLocal.Value?.Register(registration, instance);
                return true;
            }

            instance = null;
            return false;
        }

        public IoCScope BeginScope()
        {
            var scope = new IoCScope(this, ScopeLocal.Value);
            ScopeLocal.Value = scope;
            return scope;
        }

        public void EndScope(IoCScope scope)
        {
            var storedScope = ScopeLocal.Value;
            IoCScope childScope = null;
            while (storedScope != null && !ReferenceEquals(storedScope, scope)) {
                childScope = storedScope;
                storedScope = storedScope.Parent;
            }

            if (storedScope == null) {
                throw new ArgumentException("The scope could not be found in the scope chain");
            }

            if (childScope != null) {
                childScope.Reroute(storedScope.Parent);
            }
            else {
                ScopeLocal.Value = storedScope.Parent;
            }
        }
    }
}
