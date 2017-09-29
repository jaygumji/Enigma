namespace Enigma.Serialization.Json
{
    public enum JsonLiteral
    {
        ObjectBegin,
        ObjectEnd,
        ArrayBegin,
        ArrayEnd,
        Assignment,
        Quote,
        Comma,
        Number,
        Null,
        True,
        False,
        Undefined
    }
}