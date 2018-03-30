using System;
using Enigma.Serialization.Reflection.Emit;
using Enigma.IoC;
using Enigma.Serialization.Manual;

namespace Enigma.Serialization
{
    public class SerializationEngine
    {

        public static readonly DynamicTravellerContext Context = new DynamicTravellerContext();

        private readonly SerializationInstanceFactory _instanceFactory;
        private readonly RootTravellerCollection _rootTravellers;

        public SerializationEngine()
            : this(new IoCContainer(), new GraphTravellerCollection(Context))
        {
        }

        public SerializationEngine(GraphTravellerCollection travellers)
            : this(new IoCContainer(), travellers)
        {
        }

        public SerializationEngine(IInstanceFactory instanceFactory)
            : this(instanceFactory, new GraphTravellerCollection(Context))
        {
        }

        public SerializationEngine(DynamicTravellerContext context)
            : this(new IoCContainer(), new GraphTravellerCollection(context))
        {
        }

        public SerializationEngine(IInstanceFactory instanceFactory, GraphTravellerCollection travellers)
        {
            _instanceFactory = new SerializationInstanceFactory(instanceFactory);
            _rootTravellers = new RootTravellerCollection(travellers, _instanceFactory);
        }

        public void Serialize(IWriteVisitor visitor, object graph)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            var type = graph.GetType();

            var traveller = _rootTravellers.GetOrAdd(type, out var level);
            if (level == LevelType.Value) {
                var valueVisitor = ValueVisitor.Create(type);
                valueVisitor.VisitValue(visitor, VisitArgs.CreateRoot(level), graph);
            }
            else {
                var rootArgs = VisitArgs.CreateRoot(level);
                visitor.Visit(graph, rootArgs);
                traveller.Travel(visitor, graph);
                visitor.Leave(graph, rootArgs);
            }
        }

        public void Serialize<T>(IWriteVisitor visitor, T graph)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            var traveller = _rootTravellers.GetOrAdd<T>(out var level);
            if (level == LevelType.Value) {
                var valueVisitor = ValueVisitor.Create<T>();
                valueVisitor.VisitValue(visitor, VisitArgs.CreateRoot(level), graph);
            }
            else {
                var rootArgs = VisitArgs.CreateRoot(level);
                visitor.Visit(graph, rootArgs);
                traveller.Travel(visitor, graph);
                visitor.Leave(graph, rootArgs);
            }
        }
        
        public object Deserialize(IReadVisitor visitor, Type type)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));
            if (type == null) throw new ArgumentNullException(nameof(type));

            var traveller = _rootTravellers.GetOrAdd(type, out var level);
            if (level == LevelType.Value) {
                var valueVisitor = ValueVisitor.Create(type);
                return valueVisitor.TryVisitValue(visitor, VisitArgs.CreateRoot(level), out var value)
                    ? value : default;
            }

            var rootArgs = VisitArgs.CreateRoot(level);
            if (visitor.TryVisit(rootArgs) != ValueState.Found) {
                return default;
            }

            var graph = _instanceFactory.CreateInstance(type);
            traveller.Travel(visitor, graph);
            visitor.Leave(rootArgs);

            return graph;
        }

        public void DeserializeTo(IReadVisitor visitor, Type type, object graph)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            var traveller = _rootTravellers.GetOrAdd(type, out var level);
            if (level == LevelType.Value) {
                throw new NotSupportedException("Values are not supported when deserializing into.");
            }

            var rootArgs = VisitArgs.CreateRoot(level);
            if (visitor.TryVisit(rootArgs) != ValueState.Found) {
                return;
            }

            traveller.Travel(visitor, graph);
            visitor.Leave(rootArgs);
        }

        public T Deserialize<T>(IReadVisitor visitor)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));

            var traveller = _rootTravellers.GetOrAdd<T>(out var level);
            if (level == LevelType.Value) {
                var valueVisitor = ValueVisitor.Create<T>();
                return valueVisitor.TryVisitValue(visitor, VisitArgs.CreateRoot(level), out var value)
                    ? value : default;
            }

            var rootArgs = VisitArgs.CreateRoot(level);
            if (visitor.TryVisit(rootArgs) != ValueState.Found) {
                return default;
            }

            var graph = (T) _instanceFactory.CreateInstance(typeof(T));
            traveller.Travel(visitor, graph);
            visitor.Leave(rootArgs);

            return graph;
        }

        public void DeserializeTo<T>(IReadVisitor visitor, T graph)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            var traveller = _rootTravellers.GetOrAdd<T>(out var level);
            if (level == LevelType.Value) {
                throw new NotSupportedException("Values are not supported when deserializing into.");
            }

            var rootArgs = VisitArgs.CreateRoot(level);
            if (visitor.TryVisit(rootArgs) != ValueState.Found) {
                return;
            }

            traveller.Travel(visitor, graph);
            visitor.Leave(rootArgs);
        }

    }
}
