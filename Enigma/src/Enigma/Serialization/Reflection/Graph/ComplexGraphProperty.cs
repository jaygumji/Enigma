/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;

namespace Enigma.Serialization.Reflection.Graph
{
    public class ComplexGraphProperty : IGraphProperty
    {
        private readonly SerializableProperty _property;
        private readonly IGraphType _propertyType;
        private readonly VisitArgs _args;

        public ComplexGraphProperty(SerializableProperty property, IGraphType propertyType, VisitArgs args)
        {
            _property = property;
            _propertyType = propertyType;
            _args = args;
        }

        public void Visit(object graph, IReadVisitor visitor)
        {
            var state = visitor.TryVisit(_args);
            if (state == ValueState.NotFound) return;

            if (state == ValueState.Null) {
                _property.Ref.SetValue(graph, null);
                visitor.Leave(_args);
                return;
            }

            var childGraph = Activator.CreateInstance(_property.Ref.PropertyType);

            _propertyType.Visit(childGraph, visitor);

            _property.Ref.SetValue(graph, childGraph);
            visitor.Leave(_args);
        }

        public void Visit(object graph, IWriteVisitor visitor)
        {
            var childGraph = _property.Ref.GetValue(graph);
            visitor.Visit(childGraph, _args);
            if (childGraph == null) {
                visitor.Leave(null, _args);
                return;
            }

            _propertyType.Visit(childGraph, visitor);
            visitor.Leave(childGraph, _args);
        }
    }
}