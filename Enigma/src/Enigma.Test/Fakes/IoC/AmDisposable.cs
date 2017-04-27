using System;

namespace Enigma.Test.Fakes.IoC
{
    public class AmDisposable : IDisposable
    {
        public int DisposeCalled { get; private set; }

        public void Dispose()
        {
            DisposeCalled++;
        }
    }
}
