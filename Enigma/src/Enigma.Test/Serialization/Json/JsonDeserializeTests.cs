﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enigma.Test.Fakes.Entities;
using Enigma.Testing.Fakes.Graphs;
using Xunit;

namespace Enigma.Test.Serialization.Json
{
    public class JsonDeserializeTests
    {

        private readonly JsonSerializationTestContext _context = new JsonSerializationTestContext();

        [Fact]
        public void DeserializeBool()
        {
            _context.AssertDeserializeNullableSingle<NullableBooleanGraph, bool>(
                "{\"value\": true}", new NullableBooleanGraph {Value = true});
        }

        [Fact]
        public void DeserializeInt16()
        {
            _context.AssertDeserializeNullableSingle<NullableInt16Graph, Int16>(
                "{\"value\": 42}", new NullableInt16Graph { Value = 42 });
        }

        [Fact]
        public void DeserializeInt32()
        {
            _context.AssertDeserializeNullableSingle<NullableInt32Graph, Int32>(
                "{\"value\": 42}", new NullableInt32Graph { Value = 42 });
        }

        [Fact]
        public void DeserializeInt64()
        {
            _context.AssertDeserializeNullableSingle<NullableInt64Graph, Int64>(
                "{\"value\": 42}", new NullableInt64Graph { Value = 42 });
        }

        [Fact]
        public void DeserializeUInt16()
        {
            _context.AssertDeserializeNullableSingle<NullableUInt16Graph, UInt16>(
                "{\"value\": 42}", new NullableUInt16Graph { Value = 42 });
        }

        [Fact]
        public void DeserializeUInt32()
        {
            _context.AssertDeserializeNullableSingle<NullableUInt32Graph, UInt32>(
                "{\"value\": 42}", new NullableUInt32Graph { Value = 42 });
        }

        [Fact]
        public void DeserializeUInt64()
        {
            _context.AssertDeserializeNullableSingle<NullableUInt64Graph, UInt64>(
                "{\"value\": 42}", new NullableUInt64Graph { Value = 42 });
        }

        [Fact]
        public void DeserializeSingle()
        {
            _context.AssertDeserializeNullableSingle<NullableSingleGraph, Single>(
                "{\"value\": 42.3}", new NullableSingleGraph { Value = 42.3F });
        }

        [Fact]
        public void DeserializeDouble()
        {
            _context.AssertDeserializeNullableSingle<NullableDoubleGraph, Double>(
                "{\"value\": 42.3}", new NullableDoubleGraph { Value = 42.3 });
        }

        [Fact]
        public void DeserializeDecimal()
        {
            _context.AssertDeserializeNullableSingle<NullableDecimalGraph, Decimal>(
                "{\"value\": 42.3}", new NullableDecimalGraph { Value = 42.3M });
        }

        [Fact]
        public void DeserializeTimeSpan()
        {
            _context.AssertDeserializeNullableSingle<NullableTimeSpanGraph, TimeSpan>(
                "{\"value\": \"5.22:10:18.672\"}", new NullableTimeSpanGraph {
                    Value = new TimeSpan(5, 22, 10, 18, 672)
                });
        }

        [Fact]
        public void DeserializeDateTime()
        {
            _context.AssertDeserializeNullableSingle<NullableDateTimeGraph, DateTime>(
                "{\"value\": \"2120-02-27T15:24:04Z\"}", new NullableDateTimeGraph {
                    Value = new DateTime(2120, 02, 27, 15, 24, 04, DateTimeKind.Utc)
                });
        }

        [Fact]
        public void DeserializeString()
        {
            _context.AssertDeserializeSingle<StringGraph, String>(
                "{\"value\": \"Hello World\"}", new StringGraph { Value = "Hello World" });
        }

        [Fact]
        public void DeserializeBlob()
        {
            var expected = new BlobGraph {
                Value = Encoding.Unicode.GetBytes("Hello World")
            };
            _context.AssertDeserialize("{\"value\": \"SABlAGwAbABvACAAVwBvAHIAbABkAA==\"}",
                expected, (l, r) => BlobComparer.CompareBlobs(l.Value, r.Value));
        }

        [Fact]
        public void DeserializeGuid()
        {

            _context.AssertDeserializeNullableSingle<NullableGuidGraph, Guid>(
                "{\"value\": \"a79734f9-3aac-47d0-9a9b-b881907ca015\"}", new NullableGuidGraph { Value = Guid.Parse("a79734f9-3aac-47d0-9a9b-b881907ca015") });
        }

        [Fact]
        public void DeserializeCollection()
        {
            _context.AssertDeserialize("{\"value\": [\"Hello\", \"big\", \"world\"]}", new CollectionGraph {
                Value = new List<string> { "Hello", "big", "world"}
            }, (l, r) => ListComparer<List<string>, string>.Default.Compare(l?.Value, r?.Value));
        }

        [Fact]
        public void DeserializeDictionary()
        {
            _context.AssertDeserialize("{\"value\": {\"1\": \"Hello\", \"2\": \"big\", \"3\": \"world\"}}", new DictionaryGraph {
                Value = new Dictionary<int, string> {
                    {1, "Hello"},
                    {2, "big"},
                    {3, "world"}
                }
            }, (l, r) => new DictionaryComparer<Dictionary<int, string>, int, string>().Compare(l?.Value, r?.Value));
        }

        [Fact]
        public void DeserializeDataBlock()
        {
            _context.AssertSerialize("{\"id\":\"f5159142-b9a3-45fa-85ae-e0c9e60990a9\",\"string\":\"Hello World\",\"int16\":20234,\"int32\":43554654,\"int64\":4349893895849554545,\"uInt16\":64322,\"uInt32\":3454654454,\"uInt64\":9859459485984955454,\"single\":32.1,\"double\":4357.32,\"timeSpan\":\"10:30:00\",\"decimal\":44754.324,\"dateTime\":\"2014-04-01T10:00:00.0000000\",\"byte\":42,\"boolean\":true,\"blob\":\"AQID\",\"messages\":[\"Test1\",\"Test2\",\"Test3\",\"Test4\",\"Test5\"],\"stamps\":[\"2010-03-01T22:00:00.0000000\"],\"relation\":{\"id\":\"f68ef7d4-6f62-476b-bc5e-71ad86549a63\",\"name\":\"Connection\",\"description\":\"Generic connection between relations\",\"value\":77},\"dummyRelation\":null,\"secondaryRelations\":[{\"id\":\"c9edb616-26ec-44bb-9e70-3f38c7c18c91\",\"name\":\"Line1\",\"description\":\"First line of cascade\",\"value\":187}],\"indexedValues\":{\"V1\":1,\"V2\":2,\"V3\":3,\"V4\":4},\"categories\":{\"1\":{\"name\":\"Warning\",\"description\":\"Warning of something\",\"image\":\"AQIDBAU=\"},\"2\":{\"name\":\"Error\",\"description\":\"Error of something\",\"image\":\"AQIDBAUGBwgJ\"},\"3\":{\"name\":\"Temporary\",\"description\":null,\"image\":null}}}", JsonDataBlock.Filled());
        }

        [Fact]
        public void DeserializeRootValue()
        {
            _context.AssertDeserialize("\"MyValue\"", "MyValue");
        }

        [Fact]
        public void DeserializeRootCollection()
        {
            _context.AssertDeserialize("[\"Hello\", \"big\", \"world\"]",
                new List<string> { "Hello", "big", "world" },
                (l, r) => ListComparer<List<string>, string>.Default.Compare(l, r));
        }

        [Fact]
        public void DeserializeRootDictionary()
        {
            _context.AssertDeserialize("{\"1\": \"Hello\", \"2\": \"big\", \"3\": \"world\"}",
                new Dictionary<int, string> {
                    {1, "Hello"},
                    {2, "big"},
                    {3, "world"}
                }, (l, r) => new DictionaryComparer<Dictionary<int, string>, int, string>().Compare(l, r));
        }

    }
}
