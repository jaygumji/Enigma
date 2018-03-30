﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System.Collections.Generic;

namespace Enigma.Testing.Fakes.Graphs
{
    public class DictionaryWithCollectionKeyAndValueGraph
    {
        public Dictionary<List<int>, List<string>> Value { get; set; }
    }
}