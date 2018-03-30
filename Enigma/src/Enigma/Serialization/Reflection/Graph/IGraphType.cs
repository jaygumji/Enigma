/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
namespace Enigma.Serialization.Reflection.Graph
{
    public interface IGraphType
    {
        void Visit(object graph, IReadVisitor visitor);
        void Visit(object graph, IWriteVisitor visitor);
    }
}