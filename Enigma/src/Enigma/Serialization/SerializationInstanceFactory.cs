﻿using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Enigma.Serialization
{
    public class SerializationInstanceFactory
    {
        private readonly IInstanceFactory _instanceFactory;

        private static readonly ConcurrentDictionary<Type, DynamicActivator> Activators
            = new ConcurrentDictionary<Type, DynamicActivator>();

        public SerializationInstanceFactory(IInstanceFactory instanceFactory)
        {
            _instanceFactory = instanceFactory;
        }

        public object CreateInstance(Type type)
        {
            if (_instanceFactory != null
                && _instanceFactory.TryGetInstance(type, out object instance)) {
                return instance;
            }

            if (Activators.TryGetValue(type, out DynamicActivator activator)) {
                return activator.Activate();
            }

            var constructor = type.GetTypeInfo().GetConstructor(Type.EmptyTypes);
            if (constructor == null)
                throw InvalidGraphException.NoParameterLessConstructor(type);

            activator = Activators.GetOrAdd(type, t => new DynamicActivator(constructor));
            return activator.Activate();
        }

    }
}