using System;
using System.Reflection;

namespace Enigma.Serialization.Reflection
{
    public class AcquirePropertyMetadataArgs
    {
        private readonly Type _type;
        private readonly PropertyInfo _property;
        private readonly StateBag _args;

        public UInt32? Index { get; set; }

        public AcquirePropertyMetadataArgs(Type type, PropertyInfo property)
        {
            _type = type;
            _property = property;
            _args = new StateBag();
        }

        public Type Type
        {
            get { return _type; }
        }

        public PropertyInfo Property
        {
            get { return _property; }
        }

        public StateBag Args { get { return _args; } }
    }
}