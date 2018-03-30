﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
#if DNX451

using System.IO;    

namespace Enigma.IO
{
    internal class PooledFileSystemStream : IWriteStream
    {

        private IStreamProvider _provider;
        private FileStream _stream;

        public PooledFileSystemStream(IStreamProvider provider, FileStream stream)
        {
            _provider = provider;
            _stream = stream;
        }

        public Stream Stream { get { return _stream; } }
        public long Length { get { return _stream.Length; } }

        public long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public void Dispose()
        {
            _provider.Return(this);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }

        public void Flush()
        {
            _stream.Flush();
        }

        public void FlushForced()
        {
            _stream.Flush(true);
        }

    }
}

#endif
