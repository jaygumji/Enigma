using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Enigma.Testing.Fakes.Entities;
using Enigma.Testing.Fakes.Graphs;
using Xunit;


namespace Enigma.Test.Serialization.Binary
{
    
    public class PackedDataSerializerGraphTests
    {

        [Fact]
        public void WriteInt16Test()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new Int16Graph { Value = 42 });
        }

        [Fact]
        public void WriteInt32Test()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new Int32Graph { Value = 42 });
        }

        [Fact]
        public void WriteInt64Test()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new Int64Graph { Value = 42 });
        }

        [Fact]
        public void WriteUInt16Test()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new UInt16Graph { Value = 42 });
        }

        [Fact]
        public void WriteUInt32Test()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new UInt32Graph { Value = 42 });
        }

        [Fact]
        public void WriteUInt64Test()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new UInt64Graph { Value = 42 });
        }

        [Fact]
        public void WriteBooleanTest()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new BooleanGraph { Value = true });
        }

        [Fact]
        public void WriteSingleTest()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new SingleGraph { Value = 42.3f });
        }

        [Fact]
        public void WriteDoubleTest()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new DoubleGraph { Value = 42.7d });
        }

        [Fact]
        public void WriteDecimalTest()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new DecimalGraph { Value = 42.74343M });
        }

        [Fact]
        public void WriteTimeSpanTest()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new TimeSpanGraph { Value = new TimeSpan(12, 30, 00) });
        }

        [Fact]
        public void WriteDateTimeTest()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new DateTimeGraph { Value = new DateTime(2001, 01, 07, 15, 30, 24) });
        }

        [Fact]
        public void WriteStringTest()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new StringGraph { Value = "Hello World" });
        }

        [Fact]
        public void WriteGuidTest()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new GuidGraph { Value = Guid.NewGuid() });
        }

        [Fact]
        public void WriteBlobTest()
        {
            var graph = new BlobGraph {Value = new byte[] {1, 2, 3}};
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.True(graph.Value.SequenceEqual(actual.Value));
        }

        [Fact]
        public void WriteEnumTest()
        {
            var context = new SerializationTestContext();
            context.AssertBinarySingleProperty(new EnumGraph { Value = ApplicationType.Api });
        }

        [Fact]
        public void WriteComplexTest()
        {
            var graph = new ComplexGraph {Value = new Relation {Id = Guid.NewGuid(), Name = "Test", Description = "Binary", Value = 1}};
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(graph.Value.Id, actual.Value.Id);
            Assert.Equal(graph.Value.Name, actual.Value.Name);
            Assert.Equal(graph.Value.Description, actual.Value.Description);
            Assert.Equal(graph.Value.Value, actual.Value.Value);
        }

        [Fact]
        public void WriteDictionaryTest()
        {
            var graph = new DictionaryGraph {Value = new Dictionary<int, string> {{2, "Test"}}};
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(1, actual.Value.Count);

            var firstActual = actual.Value.First();
            Assert.Equal(2, firstActual.Key);
            Assert.Equal("Test", firstActual.Value);
        }

        [Fact]
        public void WriteDictionaryWithComplexValueTest()
        {
            var graph = new DictionaryWithComplexValueGraph {
                Value = new Dictionary<string, Category> {
                    {"A", new Category {
                        Name = "Warning",
                        Description = "Warning of something",
                        Image = new byte[] {1, 2, 3, 4, 5}
                    }}, {"B", new Category {
                        Name = "Error",
                        Description = "Error of something",
                        Image = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9}
                    }}, {"C", new Category {Name = "Temporary"}}
                }
            };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(3, actual.Value.Count);

            foreach (var kv in graph.Value) {
                var actualValue = actual.Value[kv.Key];
                Assert.Equal(kv.Value, actualValue);
            }
        }

        [Fact]
        public void WriteDictionaryWithComplexKeyAndValueTest()
        {
            var graph = new DictionaryWithComplexKeyAndValueGraph {
                Value = new Dictionary<Identifier, Category> {
                    {new Identifier {Id = 1, Type = ApplicationType.Api}, new Category {Name = "Warning", Description = "Warning of something", Image = new byte[]{1, 2, 3, 4, 5}}},
                    {new Identifier {Id = 2, Type = ApplicationType.Api}, new Category {Name = "Error", Description = "Error of something", Image = new byte[]{1, 2, 3, 4, 5, 6, 7, 8, 9}}},
                    {new Identifier {Id = 3, Type = ApplicationType.Service}, new Category {Name = "Temporary"}}
                }
            };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(3, actual.Value.Count);

            foreach (var kv in graph.Value) {
                var actualValue = actual.Value[kv.Key];
                Assert.Equal(kv.Value, actualValue);
            }
        }

        [Fact]
        public void WriteDictionaryWithComplexKeyTest()
        {
            var graph = new DictionaryWithComplexKeyGraph {
                Value = new Dictionary<Identifier, string> {
                    {new Identifier {Id = 1, Type = ApplicationType.Api}, "A"},
                    {new Identifier {Id = 2, Type = ApplicationType.Api}, "B"},
                    {new Identifier {Id = 3, Type = ApplicationType.Service}, "C"}
                }
            };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(3, actual.Value.Count);

            foreach (var kv in graph.Value) {
                var actualValue = actual.Value[kv.Key];
                Assert.Equal(kv.Value, actualValue);
            }
        }

        [Fact]
        public void WriteDictionaryWithDictionaryKeyTest()
        {
            var graph = new DictionaryWithDictionaryKeyGraph {
                Value = new Dictionary<Dictionary<int, string>, string> {
                    {new Dictionary<int, string> {{42, "No 42"}}, "Hello World"}
                }
            };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(1, actual.Value.Count);

            var firstExpected = graph.Value.First();
            var firstActual = actual.Value.First();

            Assert.Equal(1, firstActual.Key.Count);
            var firstInnerExpected = firstExpected.Key.First();
            var firstInnerActual = firstActual.Key.First();

            Assert.Equal(firstInnerExpected.Key, firstInnerActual.Key);
            Assert.Equal(firstInnerExpected.Value, firstInnerActual.Value);

            Assert.Equal(firstExpected.Value, firstActual.Value);
        }

        [Fact]
        public void WriteDictionaryWithDictionaryValueTest()
        {
            var graph = new DictionaryWithDictionaryValueGraph {
                Value = new Dictionary<string, Dictionary<int, string>> {
                    {"X", new Dictionary<int, string> {{42, "No 42"}}}
                }
            };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(1, actual.Value.Count);

            var firstExpected = graph.Value.First();
            var firstActual = actual.Value.First();

            Assert.Equal(firstExpected.Key, firstActual.Key);

            Assert.Equal(1, firstActual.Value.Count);
            var firstInnerExpected = firstExpected.Value.First();
            var firstInnerActual = firstActual.Value.First();

            Assert.Equal(firstInnerExpected.Key, firstInnerActual.Key);
            Assert.Equal(firstInnerExpected.Value, firstInnerActual.Value);
        }

        [Fact]
        public void WriteDictionaryWithDictionaryKeyAndValueTest()
        {
            var graph = new DictionaryWithDictionaryKeyAndValueGraph {
                Value = new Dictionary<Dictionary<string, int>, Dictionary<int, string>> {
                    {new Dictionary<string, int> {{"No 42", 42}}, new Dictionary<int, string> {{42, "No 42"}}}
                }
            };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(1, actual.Value.Count);

            var firstExpected = graph.Value.First();
            var firstActual = actual.Value.First();

            Assert.Equal(1, firstActual.Key.Count);
            var firstInnerExpectedKey = firstExpected.Key.First();
            var firstInnerActualKey = firstActual.Key.First();

            Assert.Equal(firstInnerExpectedKey.Key, firstInnerActualKey.Key);
            Assert.Equal(firstInnerExpectedKey.Value, firstInnerActualKey.Value);

            Assert.Equal(1, firstActual.Value.Count);
            var firstInnerExpectedValue = firstExpected.Value.First();
            var firstInnerActualValue = firstActual.Value.First();

            Assert.Equal(firstInnerExpectedValue.Key, firstInnerActualValue.Key);
            Assert.Equal(firstInnerExpectedValue.Value, firstInnerActualValue.Value);
        }

        [Fact]
        public void WriteDictionaryWithCollectionKeyTest()
        {
            var graph = new DictionaryWithCollectionKeyGraph {
                Value = new Dictionary<List<int>, string> {
                    {new List<int> {42}, "Hello World"}
                }
            };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(1, actual.Value.Count);

            var firstExpected = graph.Value.First();
            var firstActual = actual.Value.First();

            Assert.Equal(1, firstActual.Key.Count);
            var firstInnerExpectedKey = firstExpected.Key.First();
            var firstInnerActualKey = firstActual.Key.First();

            Assert.Equal(firstInnerExpectedKey, firstInnerActualKey);

            Assert.Equal(firstExpected.Value, firstActual.Value);
        }

        [Fact]
        public void WriteDictionaryWithCollectionValueTest()
        {
            var graph = new DictionaryWithCollectionValueGraph {
                Value = new Dictionary<string, List<int>> {
                    {"X", new List<int> {42}}
                }
            };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(1, actual.Value.Count);

            var firstExpected = graph.Value.First();
            var firstActual = actual.Value.First();

            Assert.Equal(firstExpected.Key, firstActual.Key);

            Assert.Equal(1, firstActual.Value.Count);
            var firstInnerExpectedValue = firstExpected.Value.First();
            var firstInnerActualValue = firstActual.Value.First();

            Assert.Equal(firstInnerExpectedValue, firstInnerActualValue);
        }

        [Fact]
        public void WriteDictionaryWithCollectionKeyAndValueTest()
        {
            var graph = new DictionaryWithCollectionKeyAndValueGraph {
                Value = new Dictionary<List<int>, List<string>> {
                    {new List<int> {42}, new List<string> {"No 42"}}
                }
            };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(1, actual.Value.Count);

            var firstExpected = graph.Value.First();
            var firstActual = actual.Value.First();

            Assert.Equal(1, firstActual.Key.Count);
            var firstInnerExpectedKey = firstExpected.Key.First();
            var firstInnerActualKey = firstActual.Key.First();

            Assert.Equal(firstInnerExpectedKey, firstInnerActualKey);

            Assert.Equal(1, firstActual.Value.Count);
            var firstInnerExpectedValue = firstExpected.Value.First();
            var firstInnerActualValue = firstActual.Value.First();

            Assert.Equal(firstInnerExpectedValue, firstInnerActualValue);
        }

        [Fact]
        public void WriteCollectionTest()
        {
            var graph = new CollectionGraph { Value = new List<string> { "Test" } };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(1, actual.Value.Count);
            Assert.Equal("Test", actual.Value.First());
        }

        [Fact]
        public void WriteCollectionOfComplexTest()
        {
            var graph = new CollectionOfComplexGraph {
                Value = new List<Relation> { new Relation { Id = Guid.Empty, Name = "Test", Value = 1 } }
            };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(1, actual.Value.Count);

            for (var i = 0; i < graph.Value.Count; i++) {
                var expectedValue = graph.Value[i];
                var actualValue = actual.Value[i];
                Assert.Equal(expectedValue, actualValue);
            }
        }

        [Fact]
        public void WriteCollectionOfDictionaryTest()
        {
            var graph = new CollectionOfDictionaryGraph {
                Value = new List<Dictionary<string, int>> {
                    new Dictionary<string, int> {{"Test", 42}}
                }
            };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(1, actual.Value.Count);

            for (var i = 0; i < graph.Value.Count; i++) {
                var expectedValue = graph.Value[i];
                var actualValue = actual.Value[i];
                Assert.Equal(1, actualValue.Count);

                var firstExpected = expectedValue.First();
                var firstActual = actualValue.First();

                Assert.Equal(firstExpected.Key, firstActual.Key);
                Assert.Equal(firstExpected.Value, firstActual.Value);
            }
        }

        [Fact]
        public void WriteCollectionOfCollectionTest()
        {
            var graph = new CollectionOfCollectionGraph {
                Value = new List<List<string>> {
                    new List<string> {"Test"}
                }
            };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(1, actual.Value.Count);

            for (var i = 0; i < graph.Value.Count; i++) {
                var expectedValue = graph.Value[i];
                var actualValue = actual.Value[i];
                Assert.Equal(1, actualValue.Count);

                var firstExpected = expectedValue.First();
                var firstActual = actualValue.First();

                Assert.Equal(firstExpected, firstActual);
            }
        }

        [Fact]
        public void WriteJaggedArrayTest()
        {
            var graph = new JaggedArrayGraph {
                Value = new[] { new[] { 5, 2, 3 }, new[] { 1, 2, 3 } }
            };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(graph.Value.Length, actual.Value.Length);

            for (var r0 = 0; r0 < graph.Value.Length; r0++) {
                var expectedInner = graph.Value[r0];
                var actualInner = graph.Value[r0];
                Assert.Equal(expectedInner.Length, actualInner.Length);
                for (var r1 = 0; r1 < expectedInner.Length; r1++) {
                    var expectedValue = expectedInner[r1];
                    var actualValue = actualInner[r1];

                    Assert.Equal(expectedValue, actualValue);
                }
            }
        }

        [Fact]
        public void WriteMultidimensionalArrayTest()
        {
            var graph = new MultidimensionalArrayGraph {
                Value = new[,] { { 5, 2, 3 }, { 1, 2, 3 } }
            };
            var context = new SerializationTestContext();
            var actual = context.SerializeAndDeserialize(graph);

            Assert.NotNull(actual.Value);
            Assert.Equal(6, actual.Value.Length);
            Assert.Equal(2, actual.Value.GetLength(0));
            Assert.Equal(3, actual.Value.GetLength(1));

            for (var r0 = 0; r0 < graph.Value.GetLength(0); r0++) {
                for (var r1 = 0; r1 < graph.Value.GetLength(1); r1++) {
                    var expectedValue = graph.Value[r0, r1];
                    var actualValue = actual.Value[r0, r1];

                    Assert.Equal(expectedValue, actualValue);
                }
            }
        }

    }
}
