using Enigma.Test.IoC.Fakes;

namespace Enigma.Test.Fakes.IoC
{
    public interface ICommandInitializer
    {
        void Init(Command command);
    }
}
