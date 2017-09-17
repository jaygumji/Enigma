namespace Enigma.Serialization.Json
{
    public interface IFieldNameResolver
    {
        string Resolve(VisitArgs args);
    }
}