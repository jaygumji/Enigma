using System;
using System.Linq;
using System.Reflection;

namespace Enigma.IoC
{
    public class ActivatorInstanceFactory<TInstance> : IInstanceFactory<TInstance>
    {
        private readonly ConstructorInfo _constructor;
        private readonly IInstanceFactory[] _factories;

        public ActivatorInstanceFactory(ConstructorInfo constructor, params IInstanceFactory[] factories)
        {
            _constructor = constructor;
            _factories = factories;
        }

        public TInstance GetInstance()
        {
            return (TInstance) _constructor.Invoke(_factories.Select(f => f.GetInstance()).ToArray());
        }

        object IInstanceFactory.GetInstance()
        {
            return GetInstance();
        }
    }
}