namespace Enigma.Test.IoC.Fakes
{
    public class CoreConfig
    {
        public int Delta { get; set; }

        public static CoreConfig Default()
        {
            return new CoreConfig { Delta = 2 };
        }
    }
}