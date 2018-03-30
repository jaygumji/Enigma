/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System.Reflection;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class MethodBuilder
    {

        private readonly System.Reflection.Emit.MethodBuilder _methodBuilder;

        public MethodBuilder(System.Reflection.Emit.MethodBuilder methodBuilder)
        {
            _methodBuilder = methodBuilder;
            IL = _methodBuilder.GetILGenerator();
        }

        public MethodInfo Method => _methodBuilder;
        public ILGenerator IL { get; }
    }
}