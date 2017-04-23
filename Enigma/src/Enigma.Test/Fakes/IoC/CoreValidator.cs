using Xunit;

namespace Enigma.Test.IoC.Fakes
{
    public class CoreValidator : ICoreValidator
    {
        private readonly int _expected;

        public CoreValidator(int expected)
        {
            _expected = expected;
        }

        public void Validate(int value)
        {
            Assert.Equal(_expected, value);
        }

    }
}