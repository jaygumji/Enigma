/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using System.Linq;
using System.Reflection;
using Enigma.Reflection;

namespace Enigma.Modelling
{
    public class Index<T> : Index, IIndex<T>
    {
        public Index(string uniqueName) : base(uniqueName, typeof(T))
        {
        }
    }

    public abstract class Index : IIndex
    {
        private readonly string _uniqueName;
        private readonly Type _valueType;

        protected Index(string uniqueName, Type valueType)
        {
            _uniqueName = uniqueName;

            var extendedValueType = new ExtendedType(valueType);
            switch (extendedValueType.Classification) {
                case TypeClassification.Complex:
                    throw new ArgumentException("Only values is accepted as an index");
                case TypeClassification.Dictionary:
                    throw new NotSupportedException("Indexed dictionaries are currently not supported");
                case TypeClassification.Nullable:
                    valueType = extendedValueType.Container.AsNullable().ElementType;
                    break;
                case TypeClassification.Collection:
                    valueType = extendedValueType.Container.AsCollection().ElementType;
                    break;
            }

            _valueType = valueType;
        }

        public string UniqueName { get { return _uniqueName; } }
        public Type ValueType { get { return _valueType; } }

        public static Index Create(Type entityType, string uniqueName)
        {
            var propertyInfo = PropertyExtractor.GetProperties(entityType, uniqueName).Last();
            var type = typeof(Index<>).MakeGenericType(propertyInfo.PropertyType);
            return (Index) Activator.CreateInstance(type, uniqueName);
        }

        public void CopyFrom(Index index)
        {
        }
    }
}
