using System;
using System.Collections;
using System.Collections.Generic;

namespace Enigma.Serialization.Reflection
{
    public class SerializationReflectionGraphCollection : IEnumerable<SerializationReflectionGraph>
    {
        private readonly Dictionary<Type, SerializationReflectionGraph> _graphs;
        private readonly object _lock = new object();

        public SerializationReflectionGraphCollection()
        {
            _graphs = new Dictionary<Type, SerializationReflectionGraph>();
        }

        public IEnumerator<SerializationReflectionGraph> GetEnumerator()
        {
            return _graphs.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public SerializationReflectionGraph GetOrAdd(Type type, out bool wasAdded)
        {
            lock (_lock) {
                SerializationReflectionGraph graph;
                if (_graphs.TryGetValue(type, out graph)) {
                    wasAdded = false;
                    return graph;
                }

                graph = new SerializationReflectionGraph(type);
                _graphs.Add(type, graph);
                wasAdded = true;
                return graph;
            }
        }

        public int Count { get { return _graphs.Count; } }

    }
}