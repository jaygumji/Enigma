using Enigma.Test.IoC.Fakes;

namespace Enigma.Test.Fakes.IoC
{
    public interface ICommandEvents
    {
        void PreRun(Command command, object state);
        void PostRun(Command command, object state);
    }
}
