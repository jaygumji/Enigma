/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Enigma.ProofOfConcept;
using Enigma.Serialization.PackedBinary;
using Enigma.Testing.Fakes.Entities;

namespace Enigma.PerformanceTest
{
    public abstract class StressTestConsoleCommand : IConsoleCommand
    {

        public int IterationCount { get; set; }

        protected abstract string Name { get; }

        protected abstract void OnInitialize();
        protected abstract void OnRun();
        protected abstract void OnDestroy();

        protected virtual void OnWarmup()
        {
            OnRun();
        }

        public void Invoke()
        {
            OnInitialize();

            var watch = new Stopwatch();
            watch.Start();
            Console.WriteLine($"Initializing {Name} test...");

            OnWarmup();

            Console.WriteLine("Warmup time: " + watch.Elapsed);

            OnDestroy();

            watch.Restart();

            OnInitialize();
            for (var i = 0; i < IterationCount; i++) {
                OnRun();
            }

            Console.WriteLine("Serial time: " + watch.Elapsed);

            OnDestroy();

            watch.Restart();

            OnInitialize();
            Parallel.For(0, IterationCount, new ParallelOptions { MaxDegreeOfParallelism = 4 }, (idx) => {
                OnRun();
            });

            Console.WriteLine("Parallel time: " + watch.Elapsed);

            Console.WriteLine($"{Name} test completed");

            OnDestroy();
        }
    }
}
