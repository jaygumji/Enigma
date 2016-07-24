namespace Enigma.Db
{
    public interface IEnigmaConnection
    {

        IEnigmaCommand CreateCommand();

    }
}