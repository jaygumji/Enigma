using System;
using Enigma.Binary;

namespace Enigma.Db
{
    public interface IBinaryConverterProvider
    {
        EntityBinaryConverter Get(Type entityType);
    }

    public class EntityBinaryConverter
    {
        public EntityBinaryConverter(IKeyExtractor keyExtractor, IBinaryConverter entityConverter, IBinaryConverter keyConverter)
        {
            KeyExtractor = keyExtractor;
            EntityConverter = entityConverter;
            KeyConverter = keyConverter;
        }


        public IKeyExtractor KeyExtractor { get; }
        public IBinaryConverter EntityConverter { get; }
        public IBinaryConverter KeyConverter { get; }

    }
}