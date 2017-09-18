﻿using Xunit;

namespace Enigma.Test.Serialization.Json
{
    public class JsonSerializerTests
    {
        private readonly JsonSerializationTestContext _context = new JsonSerializationTestContext();

        [Fact]
        public void SerializeLarge()
        {
            const string expected = "{\"id\":\"f5159142-b9a3-45fa-85ae-e0c9e60990a9\",\"string\":\"Hello World\",\"int16\":20234,\"int32\":43554654,\"int64\":4349893895849554545,\"uInt16\":64322,\"uInt32\":3454654454,\"uInt64\":9859459485984955454,\"single\":32.1,\"double\":4357.32,\"timeSpan\":\"10:30:00\",\"decimal\":44754.324,\"dateTime\":\"2014-04-01T10:00:00.0000000\",\"byte\":42,\"boolean\":true,\"blob\":[1,2,3],\"messages\":[\"Test1\",\"Test2\",\"Test3\",\"Test4\",\"Test5\"],\"stamps\":[\"2010-03-01T22:00:00.0000000\"],\"relation\":{\"id\":\"f68ef7d4-6f62-476b-bc5e-71ad86549a63\",\"name\":\"Connection\",\"description\":\"Generic connection between relations\",\"value\":77},\"dummyRelation\":null,\"secondaryRelations\":[{\"id\":\"c9edb616-26ec-44bb-9e70-3f38c7c18c91\",\"name\":\"Line1\",\"description\":\"First line of cascade\",\"value\":187}],\"indexedValues\":{\"V1\":1,\"V2\":2,\"V3\":3,\"V4\":4},\"categories\":{\"1\":{\"name\":\"Warning\",\"description\":\"Warning of something\",\"image\":[1,2,3,4,5]},\"2\":{\"name\":\"Error\",\"description\":\"Error of something\",\"image\":[1,2,3,4,5,6,7,8,9]},\"3\":{\"name\":\"Temporary\",\"description\":null,\"image\":null}}}";
            var block = JsonDataBlock.Filled();
            _context.AssertSerialize(expected, block);
        }

    }
}
