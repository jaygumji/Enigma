/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class ConstructorBuilder
    {
        private readonly System.Reflection.Emit.ConstructorBuilder _builder;

        public ConstructorBuilder(System.Reflection.Emit.ConstructorBuilder builder)
        {
            _builder = builder;
            IL = builder.GetILGenerator();
        }

        public ILGenerator IL { get; }
        public ConstructorInfo Reference => _builder;
    }
}