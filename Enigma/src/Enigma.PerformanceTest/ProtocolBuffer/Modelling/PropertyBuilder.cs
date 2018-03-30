/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enigma.Modelling
{
    public class PropertyBuilder<T>
    {
        private readonly EntityMap _entityMap;
        private readonly PropertyMap<T> _propertyMap;

        public PropertyBuilder(EntityMap entityMap, PropertyMap<T> propertyMap)
        {
            _entityMap = entityMap;
            _propertyMap = propertyMap;
        }

        public PropertyBuilder<T> Name(string newName)
        {
            _propertyMap.Name = newName;
            return this;
        }

        public PropertyBuilder<T> Index(int newIndex)
        {
            _propertyMap.Index = newIndex;
            if (_entityMap.PropertyIndexLength < newIndex)
                _entityMap.PropertyIndexLength = newIndex;
            return this;
        }
    }
}
