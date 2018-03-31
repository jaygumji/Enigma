/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using Enigma.PerformanceTest.ObjectPool;
using Enigma.PerformanceTest.Serializer;
using Enigma.ProofOfConcept;

namespace Enigma.PerformanceTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ui = new ConsoleUI();
            //ui.AddCommand(new StressTestMassiveInserts());
            //ui.AddCommand(new InstrumentationProfiling());
            //ui.AddCommand(new ResourceContentionProfiling());
            ui.AddCommand(new SerializationProfilingAndTimesTest());
            ui.AddCommand(new ProtoBufTimesTest());
            ui.AddCommand(new BufferPoolPerformanceTest());
            ui.AddCommand(new BenchmarkJsonSerializerConsoleCommand());
            ui.Run();
        }
    }
}
