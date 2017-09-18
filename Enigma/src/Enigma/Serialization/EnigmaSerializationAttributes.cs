using System;
using System.Reflection;

namespace Enigma.Serialization
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NameAttribute : Attribute
    {
        public string Name { get; }

        public NameAttribute(string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class IndexAttribute : Attribute
    {
        public uint? Index { get; }

        public IndexAttribute(uint index)
        {
            Index = index;
        }
    }

    public class EnigmaSerializationAttributes
    {

        public static EnigmaSerializationAttributes Empty { get; } = new EnigmaSerializationAttributes();

        private EnigmaSerializationAttributes()
        {
        }

        public string Name { get; }

        public EnigmaSerializationAttributes(string name)
        {
            Name = name;
        }

        public static EnigmaSerializationAttributes FromMember(MemberInfo member)
        {
            var name = member.GetCustomAttribute<NameAttribute>()?.Name;
            return new EnigmaSerializationAttributes(name);
        }

    }
}