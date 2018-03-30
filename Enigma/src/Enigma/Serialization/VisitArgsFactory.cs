﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using Enigma.Reflection;
using Enigma.Serialization.Reflection;

namespace Enigma.Serialization
{
    public class VisitArgsFactory : IVisitArgsFactory
    {
        protected SerializableTypeProvider Provider { get; }
        protected SerializableType SerializableType { get; }

        public VisitArgsFactory(SerializableTypeProvider provider, Type type)
        {
            Provider = provider;
            SerializableType = provider.GetOrCreate(type);
        }

        public virtual IVisitArgsFactory ConstructWith(Type type)
        {
            return new VisitArgsFactory(Provider, type);
        }

        public virtual VisitArgs Construct(string propertyName)
        {
            var property = SerializableType.FindProperty(propertyName);
            var levelType = GetLevelTypeFromClass(property.Ext.Classification);
            var name = property.Ref.Name;
            var idx = property.Metadata.Index;
            var attributes = EnigmaSerializationAttributes.FromMember(property.Ref);

            var args = new ConstructStateArgs(property, attributes, levelType);
            OnConstructState(args);

            return new VisitArgs(name, levelType, idx, attributes, args.State);
        }

        private static LevelType GetLevelTypeFromClass(TypeClassification cls)
        {
            switch (cls) {
                case TypeClassification.Dictionary:
                    return LevelType.Dictionary;
                case TypeClassification.Collection:
                    return LevelType.Collection;
                case TypeClassification.Complex:
                    return LevelType.Single;
                default:
                    return LevelType.Value;
            }
        }

        /// <summary>
        /// Used to add additional state to the visitargs passed on to the visitors later
        /// </summary>
        /// <param name="args">Arguments used to create the state</param>
        protected virtual void OnConstructState(ConstructStateArgs args)
        {
        }
    }
}