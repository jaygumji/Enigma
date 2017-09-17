using System;
using System.IO;

namespace Enigma.Serialization
{
    public interface ISerializer
    {
        void Serialize(Stream stream, object graph);
        object Deserialize(Type type, Stream stream);
    }

    public interface ITypedSerializer
    {
        void Serialize(Stream stream, object graph);
        object Deserialize(Stream stream);
    }

    public interface ITypedSerializer<T> : ITypedSerializer
    {
        void Serialize(Stream stream, T graph);
        new T Deserialize(Stream stream);
    }
}