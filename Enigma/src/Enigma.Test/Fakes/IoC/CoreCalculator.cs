namespace Enigma.Test.IoC.Fakes
{
    public class CoreCalculator : ICoreCalculator
    {
        public int Calculate(int delta, int x, int y, int z)
        {
            return delta * ((x + y) / z);
        }
    }
}