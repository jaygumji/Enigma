﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace Enigma.Reflection
{
    public class ReflectionGraphTraveller
    {
        private const BindingFlags PropertyBindings =
            BindingFlags.Instance |
            BindingFlags.NonPublic |
            BindingFlags.Public;

        private readonly IReflectionGraphFactory _factory;

        public ReflectionGraphTraveller(IReflectionGraphFactory factory)
        {
            _factory = factory;
        }

        public void Travel(Type type)
        {
            IReflectionGraphPropertyVisitor visitor;
            if (!_factory.TryCreatePropertyVisitor(type, out visitor))
                throw new InvalidOperationException($"The requested type {type.FullName} could not be travelled.");

            var travelledTypes = new HashSet<Type>{type};
            TravelRecursive(travelledTypes, type, visitor);
        }

        private void TravelRecursive(HashSet<Type> travelledTypes, Type type, IReflectionGraphPropertyVisitor visitor)
        {
            var properties = type.GetTypeInfo().GetProperties(PropertyBindings);

            foreach (var property in properties) {
                visitor.Visit(property);

                var childType = property.PropertyType;
                if (travelledTypes.Contains(childType))
                    continue;

                travelledTypes.Add(childType);

                IReflectionGraphPropertyVisitor childVisitor;
                if (_factory.TryCreatePropertyVisitor(childType, out childVisitor)) {
                    TravelRecursive(travelledTypes, childType, childVisitor);
                }
            }
        }
    }
}
