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
            var levelType = GetLevelTypeFromClass(property.Ext.Class);
            var name = property.Ref.Name;
            var idx = property.Metadata.Index;
            var stateBag = property.Metadata.Args;

            var args = new ConstructStateBagArgs(property, stateBag, levelType);
            OnConstructStateBag(args);

            return new VisitArgs(name, levelType, idx, stateBag);
        }

        private static LevelType GetLevelTypeFromClass(TypeClass cls)
        {
            switch (cls) {
                case TypeClass.Dictionary:
                    return LevelType.Dictionary;
                case TypeClass.Collection:
                    return LevelType.Collection;
                case TypeClass.Complex:
                    return LevelType.Single;
                default:
                    return LevelType.Value;
            }
        }

        /// <summary>
        /// Used to add additional states to the visitargs for use with visitors later
        /// </summary>
        /// <param name="args">Arguments used to create the state bag</param>
        protected virtual void OnConstructStateBag(ConstructStateBagArgs args)
        {
        }
    }
}