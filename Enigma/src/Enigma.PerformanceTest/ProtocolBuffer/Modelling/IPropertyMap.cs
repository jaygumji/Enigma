/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
namespace Enigma.Modelling
{
    public interface IPropertyMap
    {
        string PropertyName { get; }
        string Name { get; set; }
        int Index { get; set; }
    }

    public interface IPropertyMap<T> : IPropertyMap
    {
    }
}
