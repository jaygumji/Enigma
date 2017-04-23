namespace Enigma.Test.IoC.Fakes
{
    public class Core
    {
        private readonly CoreConfig _config;
        private readonly ICoreCalculator _calculator;
        private readonly ICoreValidator _validator;

        public Core(CoreConfig config, ICoreCalculator calculator, ICoreValidator validator)
        {
            _config = config;
            _calculator = calculator;
            _validator = validator;
        }

        public int Calculate(int x, int y, int z)
        {
            var value = _calculator.Calculate(_config.Delta, x, y, z);
            _validator.Validate(value);
            return value;
        }

    }
}
