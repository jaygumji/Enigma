using System;
using Enigma.Reflection;

namespace Enigma
{
    public static class TypeExtensions
    {

        public static WrappedType Wrap(this Type type)
        {
            return new WrappedType(type);
        }

    }
}
