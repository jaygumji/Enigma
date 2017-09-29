using System;
using System.Collections.Generic;

namespace Enigma.Serialization
{
    public class GraphTravellerCollection
    {

        private readonly Dictionary<Type, IGraphTraveller> _travellers;

        public GraphTravellerCollection()
        {
            _travellers = new Dictionary<Type, IGraphTraveller>();
        }

        public IGraphTraveller GetOrAdd(Type type, Func<Type, IGraphTraveller> factory)
        {
            lock (_travellers) {
                if (_travellers.TryGetValue(type, out var traveller)) return traveller;

                traveller = factory.Invoke(type);
                _travellers.Add(type, traveller);
                return traveller;
            }
        }
    }
}