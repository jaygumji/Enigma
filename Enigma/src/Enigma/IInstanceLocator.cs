using System;

namespace Enigma
{
    public interface IInstanceFactory
    {
        object GetInstance(Type type);
        bool TryGetInstance(Type type, out object instance);
    }
}
