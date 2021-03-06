﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System.Collections.Generic;
using Enigma.Testing.Fakes.Entities;

namespace Enigma.Test.Serialization.Fakes
{
    public class ComplexDictionary
    {
        public Dictionary<Identifier, Category> Test { get; set; }
    }
}
