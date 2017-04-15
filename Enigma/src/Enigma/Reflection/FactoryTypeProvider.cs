using System;

namespace Enigma.Reflection
{
    public class FactoryTypeProvider : ITypeProvider
    {
        public static readonly FactoryTypeProvider Instance = new FactoryTypeProvider();

        public ExtendedType Extend(Type type)
        {
            return new ExtendedType(type, this);
        }
    }
}