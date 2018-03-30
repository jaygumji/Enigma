/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using System.Collections.Generic;
using Enigma.Reflection;
using Enigma.Serialization.Reflection.Emit;

namespace Enigma.Serialization.Manual
{
    public class RootTravellerCollection
    {
        private readonly GraphTravellerCollection _travellers;
        private readonly SerializationInstanceFactory _instanceFactory;
        private readonly Dictionary<Type, RootTraveller> _roots;

        public RootTravellerCollection(GraphTravellerCollection travellers, SerializationInstanceFactory instanceFactory)
        {
            _travellers = travellers;
            _instanceFactory = instanceFactory;
            _roots = new Dictionary<Type, RootTraveller>();
        }

        private IGraphTraveller Create(Type type, out LevelType level)
        {
            if (_roots.TryGetValue(type, out var root)) {
                level = root.Level;
                return root.Traveller;
            }

            IGraphTraveller traveller;

            var containerTypeInfo = type.GetContainerTypeInfo();
            var classification = type.GetClassification(containerTypeInfo);

            if (classification == TypeClassification.Collection) {
                level = LevelType.Collection;

                var elementType = containerTypeInfo.AsCollection().ElementType;
                var travellerType = typeof(CollectionGraphTraveller<,>).MakeGenericType(type, elementType);

                var elementTraveller = Create(elementType, out var _);

                traveller = (IGraphTraveller) Activator.CreateInstance(travellerType, elementTraveller, _instanceFactory);
            }
            else if (classification == TypeClassification.Dictionary) {
                level = LevelType.Dictionary;

                var dictContainer = containerTypeInfo.AsDictionary();
                var keyType = dictContainer.KeyType;
                var valueType = dictContainer.ValueType;
                var travellerType = typeof(DictionaryGraphTraveller<,,>).MakeGenericType(type, keyType, valueType);

                var keyTraveller = Create(keyType, out var _);
                var valueTraveller = Create(valueType, out var _);

                traveller = (IGraphTraveller)Activator.CreateInstance(travellerType, keyTraveller, valueTraveller, _instanceFactory);
            }
            else if (classification == TypeClassification.Complex) {
                level = LevelType.Single;
                traveller = _travellers.GetOrAdd(type);
            }
            else if (classification == TypeClassification.Value) {
                level = LevelType.Value;
                return null;
            }
            else if (classification == TypeClassification.Nullable) {
                var underlyingType = containerTypeInfo.AsNullable().ElementType;
                traveller = Create(underlyingType, out level);
            }
            else {
                throw new NotSupportedException($"The type {type.FullName} is currenty not supported.");
            }

            _roots.Add(type, new RootTraveller(traveller, level));
            return traveller;
        }

        public IGraphTraveller GetOrAdd(Type type, out LevelType level)
        {
            lock (_roots) {
                if (_roots.TryGetValue(type, out var root)) {
                    level = root.Level;
                    return root.Traveller;
                }

                return Create(type, out level);
            }
        }

        public IGraphTraveller<T> GetOrAdd<T>(out LevelType level)
        {
            var type = typeof(T);
            return (IGraphTraveller<T>) GetOrAdd(type, out level);
        }

    }
}