/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class ILStringConstantPointer : ILConstantPointer
    {
        private readonly string _value;

        public ILStringConstantPointer(string value) : base(typeof(string))
        {
            _value = value;
        }

        protected override void Load(ILGenerator il)
        {
            il.Emit(OpCodes.Ldstr, _value);
        }

    }
}