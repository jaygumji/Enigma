/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using BenchmarkDotNet.Running;

namespace Enigma.ProofOfConcept
{
    public interface IConsoleCommand
    {
        void Invoke();
    }

    public abstract class BenchmarkConsoleCommand : IConsoleCommand
    {
        public void Invoke()
        {
            var summary = BenchmarkRunner.Run(GetType());
        }
    }
}