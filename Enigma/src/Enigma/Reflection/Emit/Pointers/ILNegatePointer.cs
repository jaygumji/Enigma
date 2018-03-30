﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit.Pointers
{
    public class ILNegatePointer : ILPointer
    {
        private readonly IILPointer _value;

        public override Type Type => typeof(bool);

        public ILNegatePointer(IILPointer value)
        {
            _value = value;
        }

        protected override void Load(ILGenerator il)
        {
            _value.Load(il);
            il.Load(0);
            il.Emit(OpCodes.Ceq);
        }
    }
}
