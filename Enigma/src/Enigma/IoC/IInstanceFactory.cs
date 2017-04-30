namespace Enigma.IoC
{
    public interface IInstanceFactory
    {
        object GetInstance();
    }

    public interface IInstanceFactory<TInstance> : IInstanceFactory
    {
        new TInstance GetInstance();
    }
}