/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Enigma.Serialization.PackedBinary;
using Enigma.Testing.Fakes.Entities;

namespace Enigma.ProofOfConcept
{
    public class SerializationProfilingAndTimesTest : IConsoleCommand
    {
        public void Invoke()
        {
            var graph = DataBlock.Filled();

            var watch = new Stopwatch();
            watch.Start();
            Console.WriteLine("Initializing enigma serialization test...");

            long length;
            var serializer = new PackedDataSerializer<DataBlock>();
            using (var stream = new MemoryStream()) {
                serializer.Serialize(stream, graph);
                length = stream.Length;
            }

            Console.WriteLine("Initialization time: " + watch.Elapsed);
            Console.WriteLine("MinSize of data: " + length);
            watch.Restart();

            for (var i = 0; i < 10000; i++) {
                serializer.Serialize(new MemoryStream(), graph);
            }

            Console.WriteLine("Serialization time: " + watch.Elapsed);
            watch.Restart();

            Parallel.For(0, 10000, new ParallelOptions {MaxDegreeOfParallelism = 4}, (idx) => {
                serializer.Serialize(new MemoryStream(), graph);
            });

            Console.WriteLine("Parallel serialization time: " + watch.Elapsed);

            Console.WriteLine("Enigma serialization test completed");
        }
    }
}