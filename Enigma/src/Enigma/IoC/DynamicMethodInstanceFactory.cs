using Enigma.Reflection;
using Enigma.Reflection.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Enigma.IoC
{
    public class DynamicMethodInstanceFactory<TInstance> : IInstanceFactory<TInstance>
    {
        private readonly DynamicActivator _activator;
        private readonly IEnumerable<IInstanceFactory> _parameterFactories;

        public DynamicMethodInstanceFactory(Type type, ConstructorInfo constructor, ITypeProvider provider, IEnumerable<IInstanceFactory> parameterFactories)
        {
            _activator = new DynamicActivator(type, constructor, provider);
            _parameterFactories = parameterFactories;
        }

        public TInstance GetInstance()
        {
            var parameters = _parameterFactories
                .Select(f => f.GetInstance())
                .ToArray();

            return (TInstance) _activator.Activate(parameters);
        }

        object IInstanceFactory.GetInstance()
        {
            return GetInstance();
        }
    }
}
