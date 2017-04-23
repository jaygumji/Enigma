using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Enigma.IoC
{
    public class IoCFactory
    {
        private readonly IDictionary<Type, IIoCRegistration> _registrations;
        private readonly IDictionary<Type, IInstanceFactory> _typeFactories;

        public IoCFactory(IDictionary<Type, IIoCRegistration> registrations)
        {
            _registrations = registrations;
            _typeFactories = new Dictionary<Type, IInstanceFactory>();
        }

        private IInstanceFactory GetFactory(Type type, bool throwError)
        {
            if (_typeFactories.TryGetValue(type, out IInstanceFactory factory)) {
                return factory;
            }

            if (_registrations.TryGetValue(type, out IIoCRegistration registration)) {
                if (registration.HasInstanceGetter) {
                    if (type == registration.Type) {
                        _typeFactories.Add(type, registration);
                        return registration;
                    }
                    factory = (IInstanceFactory) Activator.CreateInstance(typeof(DelegatedInstanceProvider<>).MakeGenericType(type), registration);
                    _typeFactories.Add(type, factory);
                    return factory;
                }
                else if (type != registration.Type) {
                    factory = GetFactory(registration.Type, throwError);
                    if (factory != null) {
                        factory = (IInstanceFactory)Activator.CreateInstance(typeof(DelegatedInstanceProvider<>).MakeGenericType(type), factory);
                        _typeFactories.Add(type, factory);
                    }
                    return factory;
                }
            }

            var info = type.GetTypeInfo();
            var constructors = info.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (constructors.Length > 1) {
                constructors = constructors.Where(c => c.IsPublic).ToArray();
            }

            if (constructors.Length != 1) {
                if (throwError) {
                    throw IoCFactoryException.AmbigiousConstructor(type);
                }
                else {
                    return null;
                }
            }

            var constructor = constructors[0];

            var parameters = constructor.GetParameters();

            var factoryParams = new object[parameters.Length+1];
            var factoryTypes = new Type[parameters.Length+1];
            factoryTypes[0] = type;
            factoryParams[0] = constructor;
            
            for (var i = 0; i < parameters.Length; i++) {
                var parameter = parameters[i];
                var paramFactory = GetFactory(parameter.ParameterType, throwError);
                if (paramFactory == null) return null;
                factoryParams[i+1] = paramFactory;
                factoryTypes[i+1] = parameter.ParameterType;
            }

            var factoryType = GetFactoryType(factoryTypes);
            var factoryConstructor = factoryType.GetTypeInfo().GetConstructors()[0];
            factory = (IInstanceFactory) factoryConstructor.Invoke(factoryParams);
            _typeFactories.Add(type, factory);

            return factory;
        }

        private Type GetFactoryType(Type[] types)
        {
            switch (types.Length) {
                case 1:
                    return typeof(InstanceFactory<>).MakeGenericType(types);
                case 2:
                    return typeof(InstanceFactory<,>).MakeGenericType(types);
                case 3:
                    return typeof(InstanceFactory<,,>).MakeGenericType(types);
                case 4:
                    return typeof(InstanceFactory<,,,>).MakeGenericType(types);
                case 5:
                    return typeof(InstanceFactory<,,,,>).MakeGenericType(types);
                case 6:
                    return typeof(InstanceFactory<,,,,,>).MakeGenericType(types);
                case 7:
                    return typeof(InstanceFactory<,,,,,,>).MakeGenericType(types);
                case 8:
                    return typeof(InstanceFactory<,,,,,,,>).MakeGenericType(types);
                case 9:
                    return typeof(InstanceFactory<,,,,,,,,>).MakeGenericType(types);
                case 10:
                    return typeof(InstanceFactory<,,,,,,,,,>).MakeGenericType(types);
                case 11:
                    return typeof(InstanceFactory<,,,,,,,,,,>).MakeGenericType(types);
                case 12:
                    return typeof(InstanceFactory<,,,,,,,,,,,>).MakeGenericType(types);
                case 13:
                    return typeof(InstanceFactory<,,,,,,,,,,,,>).MakeGenericType(types);
                case 14:
                    return typeof(InstanceFactory<,,,,,,,,,,,,,>).MakeGenericType(types);
                case 15:
                    return typeof(InstanceFactory<,,,,,,,,,,,,,,>).MakeGenericType(types);
                case 16:
                    return typeof(InstanceFactory<,,,,,,,,,,,,,,,>).MakeGenericType(types);
                case 17:
                    return typeof(InstanceFactory<,,,,,,,,,,,,,,,,>).MakeGenericType(types);
            }
            return typeof(DynamicMethodInstanceFactory<>).MakeGenericType(types[0]);
        }

        public object GetInstance(Type type)
        {
            return GetFactory(type, throwError: true).GetInstance();
        }

    }
}
