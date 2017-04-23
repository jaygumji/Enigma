using System;

namespace Enigma
{
    public interface IServiceLocator
    {
        object GetInstance(Type type);
        bool TryGetInstance(Type type, out object instance);
    }
}
