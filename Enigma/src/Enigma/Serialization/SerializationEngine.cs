using System;
using System.Reflection;
using Enigma.Serialization.Reflection.Emit;
using Enigma.IoC;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Enigma.Serialization
{
    public class SerializationEngine
    {

        private static readonly ConcurrentDictionary<Type, DynamicActivator> _activators
            = new ConcurrentDictionary<Type, DynamicActivator>();

        private readonly DynamicTravellerContext _context;
        private readonly IServiceLocator _serviceLocator;

        public SerializationEngine(DynamicTravellerContext context)
            : this(context, new IoCContainer())
        {
        }

        public SerializationEngine(DynamicTravellerContext context, IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            _context = context;
        }

        public void Serialize(IWriteVisitor visitor, object graph)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            var type = graph.GetType();

            var traveller = _context.GetInstance(type);

            var rootArgs = VisitArgs.Root(type.Name);
            visitor.Visit(graph, rootArgs);
            traveller.Travel(visitor, graph);
            visitor.Leave(graph, rootArgs);
        }

        public void Serialize<T>(IWriteVisitor visitor, T graph)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            var type = typeof(T);

            var traveller = (IGraphTraveller<T>)_context.GetInstance(type);

            var rootArgs = VisitArgs.Root(type.Name);
            visitor.Visit(graph, rootArgs);
            traveller.Travel(visitor, graph);
            visitor.Leave(graph, rootArgs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private object CreateInstance(Type type)
        {
            if (_serviceLocator != null
                && _serviceLocator.TryGetInstance(type, out object instance)) {
                return instance;
            }

            if (_activators.TryGetValue(type, out DynamicActivator activator)) {
                return activator.Activate();
            }

            var constructor = type.GetTypeInfo().GetConstructor(Type.EmptyTypes);
            if (constructor == null)
                throw InvalidGraphException.NoParameterLessConstructor(type);

            activator = _activators.GetOrAdd(type, t => new DynamicActivator(constructor));
            return activator.Activate();
        }

        public object Deserialize(IReadVisitor visitor, Type type)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));
            if (type == null) throw new ArgumentNullException(nameof(type));

            var args = VisitArgs.Root(type.Name);
            if (visitor.TryVisit(args) != ValueState.Found)
                return null;

            var graph = CreateInstance(type);
            var traveller = _context.GetInstance(type);
            traveller.Travel(visitor, graph);

            visitor.Leave(args);

            return graph;
        }

        public void DeserializeTo(IReadVisitor visitor, Type type, object graph)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            var args = VisitArgs.Root(type.Name);
            if (visitor.TryVisit(args) != ValueState.Found)
                return;

            var traveller = _context.GetInstance(type);
            traveller.Travel(visitor, graph);

            visitor.Leave(args);
        }

        public T Deserialize<T>(IReadVisitor visitor)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));
            var type = typeof(T);

            var args = VisitArgs.Root(type.Name);
            if (visitor.TryVisit(args) != ValueState.Found)
                return default(T);

            var graph = (T)CreateInstance(type);

            var traveller = _context.GetInstance<T>();
            traveller.Travel(visitor, graph);

            visitor.Leave(args);

            return graph;
        }

        public void DeserializeTo<T>(IReadVisitor visitor, T graph)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            var type = typeof(T);

            var args = VisitArgs.Root(type.Name);
            if (visitor.TryVisit(args) != ValueState.Found)
                return;

            var traveller = _context.GetInstance<T>();
            traveller.Travel(visitor, graph);

            visitor.Leave(args);
        }

    }
}
