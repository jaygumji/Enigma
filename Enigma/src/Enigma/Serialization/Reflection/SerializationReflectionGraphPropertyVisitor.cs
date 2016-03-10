using System.Reflection;
using Enigma.Reflection;

namespace Enigma.Serialization.Reflection
{
    public class SerializationReflectionGraphPropertyVisitor : IReflectionGraphPropertyVisitor
    {
        private readonly SerializationReflectionGraph _graph;

        public SerializationReflectionGraphPropertyVisitor(SerializationReflectionGraph graph)
        {
            _graph = graph;
        }

        public void Visit(PropertyInfo property)
        {
            if (!property.CanRead) return;
            if (!property.CanWrite) return;
            if (property.GetGetMethod().IsStatic) return;

            
        }

    }
}