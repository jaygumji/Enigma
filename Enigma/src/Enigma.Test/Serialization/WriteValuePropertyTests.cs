﻿using System;
using System.Collections.Generic;
using Enigma.Serialization;
using Enigma.Testing.Fakes.Entities;
using Enigma.Testing.Fakes.Graphs;
using Xunit;


namespace Enigma.Test.Serialization
{
    
    public class WriteValuePropertyTests
    {

        [Fact]
        public void WriteInt16Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new Int16Graph { Value = 42 });
        }

        [Fact]
        public void WriteInt32Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new Int32Graph { Value = 42 });
        }

        [Fact]
        public void WriteInt64Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new Int64Graph { Value = 42 });
        }

        [Fact]
        public void WriteUInt16Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new UInt16Graph { Value = 42 });
        }

        [Fact]
        public void WriteUInt32Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new UInt32Graph { Value = 42 });
        }

        [Fact]
        public void WriteUInt64Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new UInt64Graph { Value = 42 });
        }

        [Fact]
        public void WriteBooleanTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new BooleanGraph { Value = true });
        }

        [Fact]
        public void WriteSingleTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new SingleGraph { Value = 42.3f });
        }

        [Fact]
        public void WriteDoubleTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new DoubleGraph { Value = 42.7d });
        }

        [Fact]
        public void WriteDecimalTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new DecimalGraph { Value = 42.74343M });
        }

        [Fact]
        public void WriteTimeSpanTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new TimeSpanGraph { Value = new TimeSpan(12, 30, 00) });
        }

        [Fact]
        public void WriteDateTimeTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new DateTimeGraph { Value = new DateTime(2001, 01, 07, 15, 30, 24) });
        }

        [Fact]
        public void WriteStringTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new StringGraph { Value = "Hello World" });
        }

        [Fact]
        public void WriteGuidTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new GuidGraph { Value = Guid.Empty });
        }

        [Fact]
        public void WriteBlobTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new BlobGraph { Value = new byte[]{1,2,3} });
        }

        [Fact]
        public void WriteEnumTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new EnumGraph { Value = ApplicationType.Api });
        }

        [Fact]
        public void WriteComplexTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(4, new ComplexGraph { Value = new Relation {Id = Guid.Empty, Name = "Test", Value = 1} });
            stats.AssertVisitOrderExact(LevelType.Single, LevelType.Value, LevelType.Value, LevelType.Value, LevelType.Value);
        }

        [Fact]
        public void WriteDictionaryTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(2, new DictionaryGraph { Value = new Dictionary<int, string> { {2, "Test"}}});
            stats.AssertVisitOrderExact(LevelType.Dictionary, LevelType.DictionaryKey, LevelType.DictionaryValue);
        }

        [Fact]
        public void WriteDictionaryWithComplexValueTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(12, new DictionaryWithComplexValueGraph {
                Value = new Dictionary<string, Category> {
                    {"A", new Category {Name = "Warning", Description = "Warning of something", Image = new byte[]{1, 2, 3, 4, 5}}},
                    {"B", new Category {Name = "Error", Description = "Error of something", Image = new byte[]{1, 2, 3, 4, 5, 6, 7, 8, 9}}},
                    {"C", new Category {Name = "Temporary"}}
                }
            });
            stats.AssertVisitOrderExact(LevelType.Dictionary, LevelType.DictionaryKey, LevelType.DictionaryValue,
                LevelType.Value, LevelType.Value, LevelType.Value, LevelType.DictionaryKey, LevelType.DictionaryValue,
                LevelType.Value, LevelType.Value, LevelType.Value, LevelType.DictionaryKey, LevelType.DictionaryValue,
                LevelType.Value, LevelType.Value, LevelType.Value);
        }

        [Fact]
        public void WriteDictionaryWithComplexKeyAndValueTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(15, new DictionaryWithComplexKeyAndValueGraph {
                Value = new Dictionary<Identifier, Category> {
                    {new Identifier {Id = 1, Type = ApplicationType.Api}, new Category {Name = "Warning", Description = "Warning of something", Image = new byte[]{1, 2, 3, 4, 5}}},
                    {new Identifier {Id = 2, Type = ApplicationType.Api}, new Category {Name = "Error", Description = "Error of something", Image = new byte[]{1, 2, 3, 4, 5, 6, 7, 8, 9}}},
                    {new Identifier {Id = 3, Type = ApplicationType.Service}, new Category {Name = "Temporary"}}
                }
            });
            stats.AssertVisitOrderExact(LevelType.Dictionary, LevelType.DictionaryKey, LevelType.Value, LevelType.Value,
                LevelType.DictionaryValue, LevelType.Value, LevelType.Value, LevelType.Value, LevelType.DictionaryKey,
                LevelType.Value, LevelType.Value, LevelType.DictionaryValue, LevelType.Value, LevelType.Value,
                LevelType.Value, LevelType.DictionaryKey, LevelType.Value, LevelType.Value, LevelType.DictionaryValue,
                LevelType.Value, LevelType.Value, LevelType.Value);
        }

        [Fact]
        public void WriteDictionaryWithComplexKeyTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(9, new DictionaryWithComplexKeyGraph {
                Value = new Dictionary<Identifier, string> {
                    {new Identifier {Id = 1, Type = ApplicationType.Api}, "A"},
                    {new Identifier {Id = 2, Type = ApplicationType.Api}, "B"},
                    {new Identifier {Id = 3, Type = ApplicationType.Service}, "C"}
                }
            });
            stats.AssertVisitOrderExact(LevelType.Dictionary, LevelType.DictionaryKey, LevelType.Value, LevelType.Value,
                LevelType.DictionaryValue, LevelType.DictionaryKey, LevelType.Value, LevelType.Value,
                LevelType.DictionaryValue, LevelType.DictionaryKey, LevelType.Value, LevelType.Value,
                LevelType.DictionaryValue);
        }

        [Fact]
        public void WriteDictionaryWithDictionaryKeyTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(3, new DictionaryWithDictionaryKeyGraph {
                Value = new Dictionary<Dictionary<int, string>, string> {
                    {new Dictionary<int, string> {{42, "No 42"}}, "Hello World"}
                }
            });
            stats.AssertVisitOrderExact(LevelType.Dictionary, LevelType.DictionaryInDictionaryKey,
                LevelType.DictionaryKey, LevelType.DictionaryValue, LevelType.DictionaryValue);
        }

        [Fact]
        public void WriteDictionaryWithDictionaryValueTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(3, new DictionaryWithDictionaryValueGraph {
                Value = new Dictionary<string, Dictionary<int, string>> {
                    {"X", new Dictionary<int, string> {{42, "No 42"}}}
                }
            });
            stats.AssertVisitOrderExact(LevelType.Dictionary, LevelType.DictionaryKey,
                LevelType.DictionaryInDictionaryValue, LevelType.DictionaryKey, LevelType.DictionaryValue);
        }

        [Fact]
        public void WriteDictionaryWithDictionaryKeyAndValueTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(4, new DictionaryWithDictionaryKeyAndValueGraph {
                Value = new Dictionary<Dictionary<string, int>, Dictionary<int, string>> {
                    {new Dictionary<string, int> {{"No 42", 42}}, new Dictionary<int, string> {{42, "No 42"}}}
                }
            });
            stats.AssertVisitOrderExact(LevelType.Dictionary, LevelType.DictionaryInDictionaryKey,
                LevelType.DictionaryKey, LevelType.DictionaryValue, LevelType.DictionaryInDictionaryValue,
                LevelType.DictionaryKey, LevelType.DictionaryValue);
        }

        [Fact]
        public void WriteDictionaryWithCollectionKeyTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(2, new DictionaryWithCollectionKeyGraph {
                Value = new Dictionary<List<int>, string> {
                    {new List<int> {42}, "Hello World"}
                }
            });
            stats.AssertVisitOrderExact(LevelType.Dictionary, LevelType.CollectionInDictionaryKey,
                LevelType.CollectionItem, LevelType.DictionaryValue);
        }

        [Fact]
        public void WriteDictionaryWithCollectionValueTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(2, new DictionaryWithCollectionValueGraph {
                Value = new Dictionary<string, List<int>> {
                    {"X", new List<int> {42}}
                }
            });
            stats.AssertVisitOrderExact(LevelType.Dictionary, LevelType.DictionaryKey,
                LevelType.CollectionInDictionaryValue, LevelType.CollectionItem);
        }

        [Fact]
        public void WriteDictionaryWithCollectionKeyAndValueTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(2, new DictionaryWithCollectionKeyAndValueGraph {
                Value = new Dictionary<List<int>, List<string>> {
                    {new List<int> {42}, new List<string> {"No 42"}}
                }
            });
            stats.AssertVisitOrderExact(LevelType.Dictionary, LevelType.CollectionInDictionaryKey,
                LevelType.CollectionItem, LevelType.CollectionInDictionaryValue, LevelType.CollectionItem);
        }

        [Fact]
        public void WriteCollectionTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(1, new CollectionGraph {Value = new List<string> {"Test"}});
            stats.AssertVisitOrderExact(LevelType.Collection, LevelType.CollectionItem);
        }

        [Fact]
        public void WriteCollectionOfComplexTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(4, new CollectionOfComplexGraph {
                Value = new List<Relation> {new Relation {Id = Guid.Empty, Name = "Test", Value = 1}}
            });
            stats.AssertVisitOrderExact(LevelType.Collection, LevelType.CollectionItem, LevelType.Value, LevelType.Value,
                LevelType.Value, LevelType.Value);
        }

        [Fact]
        public void WriteCollectionOfDictionaryTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(2, new CollectionOfDictionaryGraph {
                Value = new List<Dictionary<string, int>> {
                    new Dictionary<string, int> {{"Test", 42}}
                }
            });
            stats.AssertVisitOrderExact(LevelType.Collection, LevelType.DictionaryInCollection, LevelType.DictionaryKey,
                LevelType.DictionaryValue);
        }

        [Fact]
        public void WriteCollectionOfCollectionTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(1, new CollectionOfCollectionGraph {
                Value = new List<List<string>> {
                    new List<string> {"Test"}
                }
            });
            stats.AssertVisitOrderExact(LevelType.Collection, LevelType.CollectionInCollection, LevelType.CollectionItem);
        }

        [Fact]
        public void WriteJaggedArrayTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(6, new JaggedArrayGraph {
                Value = new[] { new []{5, 2, 3}, new []{1, 2, 3} }
            });
            stats.AssertVisitOrderExact(LevelType.Collection, LevelType.CollectionInCollection, LevelType.CollectionItem,
                LevelType.CollectionItem, LevelType.CollectionItem, LevelType.CollectionInCollection,
                LevelType.CollectionItem, LevelType.CollectionItem, LevelType.CollectionItem);
        }

        [Fact]
        public void WriteMultidimensionalArrayTest()
        {
            var context = new SerializationTestContext();
            var stats = context.AssertWrite(6, new MultidimensionalArrayGraph {
                Value = new[,] {{5, 2, 3}, {1, 2, 3}}
            });
            stats.AssertVisitOrderExact(LevelType.Collection, LevelType.CollectionInCollection, LevelType.CollectionItem,
                LevelType.CollectionItem, LevelType.CollectionItem, LevelType.CollectionInCollection,
                LevelType.CollectionItem, LevelType.CollectionItem, LevelType.CollectionItem);
        }

    }
}
