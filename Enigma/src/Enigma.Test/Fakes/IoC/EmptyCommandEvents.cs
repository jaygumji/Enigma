using System;

namespace Enigma.Test.Fakes.IoC
{
    public class EmptyCommandEvents : ICommandEvents
    {
        public void PostRun(Command command, object state)
        {
        }

        public void PreRun(Command command, object state)
        {
        }
    }
}
