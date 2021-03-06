﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Enigma.Modelling
{
    public class EntityBuilder<T>
    {
        private readonly EntityMap<T> _entityMap;

        public EntityBuilder(EntityMap<T> entityMap)
        {
            this._entityMap = entityMap;
        }

        public EntityBuilder<T> Key<TProperty>(Expression<Func<T, TProperty>> property)
        {
            _entityMap.KeyName = Expressions.PropertySelector.GetProperty(property).Name;
            return this;
        }

        public PropertyBuilder<TProperty> Property<TProperty>(Expression<Func<T, TProperty>> property)
        {
            return new PropertyBuilder<TProperty>(_entityMap, _entityMap.Property(property));
        }

        public EntityBuilder<T> Index<TProperty>(Expression<Func<T, TProperty>> property)
        {
            _entityMap.Index(property);
            return this;
        }

    }
}
