using Enigma.Binary;

namespace Enigma.Db
{
    public interface IEnigmaCommand
    {

        void Add(string name, BufferedCommandHandler handler);
        void Modify(string name, BufferedCommandHandler handler);
        void Remove(string name, BufferedCommandHandler handler);

    }

    public delegate void BufferedCommandHandler(BinaryWriteBuffer writeBuffer);
}