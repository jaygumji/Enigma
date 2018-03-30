/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using System.Collections.Generic;
namespace Enigma.Modelling
{
    public interface IEntityMap
    {
        Type EntityType { get; }
        string Name { get; }
        string KeyName { get; }
        IEnumerable<IPropertyMap> Properties { get; }
        IEnumerable<IIndex> Indexes { get; }
    }

    public interface IEntityMap<T> : IEntityMap
    {
    }
}
