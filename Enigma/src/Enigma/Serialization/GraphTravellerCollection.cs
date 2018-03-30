using System;
using System.Collections.Generic;
using Enigma.Serialization.Reflection.Emit;

namespace Enigma.Serialization
{
    public class GraphTravellerCollection
    {
        private readonly Func<Type, IGraphTraveller> _factory;
        private readonly Dictionary<Type, IGraphTraveller> _travellers;

        public GraphTravellerCollection(DynamicTravellerContext context)
            : this(t => context.GetInstance(t))
        {

        }

        public GraphTravellerCollection(DynamicTravellerContext context, IVisitArgsFactory visitArgsFactory)
            : this(t => context.GetInstance(t, visitArgsFactory))
        {

        }

        public GraphTravellerCollection(Func<Type, IGraphTraveller> factory)
        {
            _factory = factory;
            _travellers = new Dictionary<Type, IGraphTraveller>();
        }

        public IGraphTraveller GetOrAdd(Type type)
        {
            lock (_travellers) {
                if (_travellers.TryGetValue(type, out var traveller)) return traveller;

                traveller = _factory.Invoke(type);
                _travellers.Add(type, traveller);
                return traveller;
            }
        }

        public IGraphTraveller<T> GetOrAdd<T>()
        {
            return (IGraphTraveller<T>) GetOrAdd(typeof(T));
        }
    }
}