namespace Enigma.IoC
{
    public interface IInstanceFactory<TInstance> : IInstanceFactory
    {
        new TInstance GetInstance();
    }
}