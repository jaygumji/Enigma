using System;

namespace Enigma.Reflection
{
    public interface ITypeProvider
    {
        ExtendedType Extend(Type type);
    }
}