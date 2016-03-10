﻿using System;
using Enigma.Reflection;

namespace Enigma.Serialization.Reflection
{
    public class SerializationReflectionGraphFactory : IReflectionGraphFactory
    {
        private readonly SerializationReflectionGraphCollection _graphs;

        public SerializationReflectionGraphFactory(SerializationReflectionGraphCollection graphs)
        {
            _graphs = graphs;
        }

        /// <summary>
        /// Visits the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public void Visit(Type type)
        {
            // System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(type.TypeHandle);

        }

        public bool TryCreatePropertyVisitor(Type type, out IReflectionGraphPropertyVisitor visitor)
        {
            bool wasAdded;
            var graph = _graphs.GetOrAdd(type, out wasAdded);
            visitor = new SerializationReflectionGraphPropertyVisitor(graph);
            return true;
        }


    }
}
