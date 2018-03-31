using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using Enigma.PerformanceTest.Fakes.Entities;
using Enigma.ProofOfConcept;
using Enigma.Serialization.Json;
using Enigma.Testing.Fakes.Entities;

namespace Enigma.PerformanceTest.Serializer
{
    [CoreJob]
    [RankColumn]
    public class BenchmarkJsonSerializerConsoleCommand : BenchmarkConsoleCommand
    {
        private readonly JsonDataBlock _block = JsonDataBlock.Filled();
        private byte[] _blob;

        //[Params(1000)]
        //public int N;

        [GlobalSetup]
        public void Setup()
        {
            //_block = DataBlock.Filled();
            var serializer = new JsonSerializer<JsonDataBlock>();
            var stream = new MemoryStream();
            serializer.Serialize(stream, _block);
            _blob = stream.ToArray();
        }

        [Benchmark]
        public byte[] Serialize()
        {
            var serializer = new JsonSerializer<JsonDataBlock>();
            var stream = new MemoryStream();
            serializer.Serialize(stream, _block);
            return stream.ToArray();
        }

        [Benchmark]
        public JsonDataBlock Deserialize()
        {
            var serializer = new JsonSerializer<JsonDataBlock>();
            var stream = new MemoryStream(_blob);
            return serializer.Deserialize(stream);
        }
    }
}
