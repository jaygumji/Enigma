namespace Enigma.Test.Fakes.IoC
{
    public class Command
    {
        public ICommandInitializer Initializer { get; set; }
        public ICommandEvents Events { get; set; }

    }
}
