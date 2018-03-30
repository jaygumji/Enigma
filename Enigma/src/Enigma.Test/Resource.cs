﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System.IO;
using System.Reflection;
using System.Resources;

namespace Enigma.Test
{
    public static class Resource
    {
        public static Stream Get(string name)
        {
            return typeof(Resource).GetTypeInfo().Assembly.GetManifestResourceStream(name);
        }
    }
}
