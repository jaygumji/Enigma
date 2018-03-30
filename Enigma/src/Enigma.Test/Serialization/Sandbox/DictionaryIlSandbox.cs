/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using Enigma.Reflection.Emit;
using Enigma.Serialization;
using Enigma.Reflection;

namespace Enigma.Test.Serialization.Sandbox
{
    public class DictionaryIlSandbox
    {
        private AssemblyBuilder _assemblyBuilder;
        private ClassBuilder _classBuilder;
        private MethodBuilder _methodBuilder;

        public DictionaryIlSandbox()
        {
            _assemblyBuilder = new AssemblyBuilder();
            _classBuilder = _assemblyBuilder.DefineClass("SandboxClass", typeof(object), Type.EmptyTypes);
            _methodBuilder = _classBuilder.DefineMethod("Execute", typeof(void), new [] {typeof(IReadVisitor), typeof(Fakes.ValueDictionary)});


            _classBuilder.Seal();
        }

        public void Invoke(IReadVisitor visitor, Fakes.ValueDictionary graph)
        {
            var instance = Activator.CreateInstance(typeof (Fakes.ValueDictionary));
        }

    }
}
