using System;
using System.Collections.Generic;
using Enigma.Reflection.Emit;
using Enigma.Reflection;

namespace Enigma.Serialization.Reflection.Emit
{
    public class DynamicTravellerContext
    {
        private readonly SerializableTypeProvider _typeProvider;
        private readonly Dictionary<Type, DynamicTraveller> _travellers;
        private readonly AssemblyBuilder _assemblyBuilder;

        public DynamicTravellerContext() : this(
            new SerializableTypeProvider(new SerializationReflectionInspector(), new CachedTypeProvider()))
        {
        }

        public DynamicTravellerContext(SerializableTypeProvider typeProvider)
        {
            _typeProvider = typeProvider;
            _travellers = new Dictionary<Type, DynamicTraveller>();
            _assemblyBuilder = new AssemblyBuilder(typeProvider.Provider);
            Members = new DynamicTravellerMembers();
        }

        public DynamicTravellerMembers Members { get; }

        public DynamicTraveller Get(Type graphType)
        {
            DynamicTraveller traveller;
            if (_travellers.TryGetValue(graphType, out traveller))
                return traveller;

            DynamicTravellerBuilder builder;
            lock (_travellers) {
                if (_travellers.TryGetValue(graphType, out traveller))
                    return traveller;

                builder = new DynamicTravellerBuilder(this, CreateClassBuilder(graphType), _typeProvider, graphType);
                traveller = builder.DynamicTraveller;
                _travellers.Add(graphType, traveller);
            }
            builder.BuildTraveller();
            return traveller;
        }

        public IGraphTraveller GetInstance(Type graphType)
        {
            var dyn = Get(graphType);
            return dyn.GetInstance();
        }

        public IGraphTraveller<T> GetInstance<T>()
        {
            var dyn = Get(typeof (T));
            var instance = (IGraphTraveller<T>) dyn.GetInstance();
            return instance;
        }

        private ClassBuilder CreateClassBuilder(Type graphType)
        {
            var graphTravellerType = typeof(IGraphTraveller<>).MakeGenericType(graphType);
            return _assemblyBuilder.DefineClass(graphType.Name + "Traveller", typeof(object), new[] { graphTravellerType });
        }

    }
}