﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System.IO;

namespace Enigma.IO
{
    public interface IReadStream : IStream
    {
        long Seek(long offset, SeekOrigin origin);
        int Read(byte[] buffer, int offset, int count);
    }
}
