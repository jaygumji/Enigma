using Enigma.Reflection;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Enigma.IoC
{
    public class IoCContainer : IIoCRegistrator, IServiceLocator
    {

        private static readonly AsyncLocal<IoCScope> ScopeLocal = new AsyncLocal<IoCScope>();

        private readonly Dictionary<Type, IIoCRegistration> _registrations;
        private readonly IoCFactory _factory;
        private readonly IIoCRegistrator _registrator;

        public IoCContainer() : this(new CachedTypeProvider())
        {
        }

        public IoCContainer(ITypeProvider provider)
        {
            _registrator = this;
            _registrations = new Dictionary<Type, IIoCRegistration>();
            _factory = new IoCFactory(_registrator, provider);
        }

        bool IIoCRegistrator.TryGet(Type type, out IIoCRegistration registration)
        {
            return _registrations.TryGetValue(type, out registration);
        }

        void IIoCRegistrator.Register(IIoCRegistration registration)
        {
            _registrator.Register(registration.Type, registration);
        }

        void IIoCRegistrator.Register(Type type, IIoCRegistration registration)
        {
            if (_registrations.ContainsKey(type)) {
                _registrations[type] = registration;
            }
            else {
                _registrations.Add(type, registration);
            }
        }

        bool IIoCRegistrator.Contains(Type type)
        {
            return _registrations.ContainsKey(type);
        }

        IIoCRegistration IIoCRegistrator.Get(Type type)
        {
            if (_registrations.TryGetValue(type, out IIoCRegistration registration)) {
                return registration;
            }

            throw new IoCRegistrationNotFoundException(type);
        }


        bool IIoCRegistrator.TryRegister(Type type, IIoCRegistration registration)
        {
            if (_registrations.ContainsKey(type)) return false;

            _registrations.Add(type, registration);
            return true;
        }

        public IIoCRegistrationSingletonConfigurator<T> Register<T>(T singleton)
        {
            var registration = new IoCRegistration<T>(() => singleton) {
                CanBeScoped = false
            };

            _registrator.Register(registration);
            return new IoCRegistrationConfigurator<T>(_registrator, registration);
        }

        public IIoCRegistrationConfigurator<TImplementation> Register<T, TImplementation>()
        {
            var registration = new IoCRegistration<TImplementation>() {
                CanBeScoped = true
            };
            _registrator.Register(typeof(T), registration);
            _registrator.Register(registration);
            return new IoCRegistrationConfigurator<TImplementation>(_registrator, registration);
        }

        public IIoCRegistrationConfigurator<T> Register<T>(Func<T> factory)
        {
            var registration = new IoCRegistration<T>(factory) {
                CanBeScoped = true
            };
            _registrator.Register(registration);
            return new IoCRegistrationConfigurator<T>(_registrator, registration);
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
                if (_registrator.TryGet(type, out IIoCRegistration registration)) {
                    if (!registration.CanBeScoped) return newInstance;
                    scope.Register(registration, newInstance);
                }
                else {
                    scope.Register(newInstance);
                }
            }
            return newInstance;
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

                var scope = ScopeLocal.Value;
                if (scope == null) {
                    if (registration.MustBeScoped) {
                        throw new InvalidOperationException($"The requested instance type {type.FullName} must be scoped");
                    }
                    return true;
                }

                scope.Register(registration, instance);
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
