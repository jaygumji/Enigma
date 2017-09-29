using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Enigma.Testing.Fakes.Entities;

namespace Enigma.ProofOfConcept
{
    public class ProtoBufTimesTest : IConsoleCommand
    {
        public void Invoke()
        {
            var graph = DataBlock.Filled();

            var watch = new Stopwatch();
            watch.Start();
            Console.WriteLine("Initializing ProtoBuf.NET serialization test...");
            var protobuf = new ProtocolBuffer.ProtocolBufferBinaryConverter<DataBlock>();
            var length = protobuf.Convert(graph).Length;

            Console.WriteLine("Initialization time: " + watch.Elapsed);
            Console.WriteLine("MinSize of data: " + length);
            watch.Restart();

            for (var i = 0; i < 10000; i++) {
                protobuf.Convert(graph);
            }

            Console.WriteLine("Serialization time: " + watch.Elapsed);
            watch.Restart();

            Parallel.For(0, 10000, new ParallelOptions { MaxDegreeOfParallelism = 4 }, (idx) => {
                protobuf.Convert(graph);
            });

            Console.WriteLine("Parallel serialization time: " + watch.Elapsed);

            Console.WriteLine("Enigma serialization test completed");
        }
    }
}