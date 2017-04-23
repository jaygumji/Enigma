using System;

namespace Enigma.IoC
{
    public class DelegatedInstanceProvider<TInstance> : IInstanceFactory<TInstance>
    {
        private readonly IInstanceFactory _delegated;

        public DelegatedInstanceProvider(IInstanceFactory delegated)
        {
            _delegated = delegated;
        }

        public TInstance GetInstance()
        {
            return (TInstance)_delegated.GetInstance();
        }

        object IInstanceFactory.GetInstance()
        {
            return _delegated.GetInstance();
        }
    }
}