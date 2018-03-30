/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Enigma.Testing.Fakes.Entities;
using Xunit;

namespace Enigma.Test.Fakes.Entities
{
    public class DataBlock
    {
        public Guid Id { get; set; }
        public string String { get; set; }
        public Int16 Int16 { get; set; }
        public Int32 Int32 { get; set; }
        public Int64 Int64 { get; set; }
        public UInt16 UInt16 { get; set; }
        public UInt32 UInt32 { get; set; }
        public UInt64 UInt64 { get; set; }
        public Single Single { get; set; }
        public Double Double { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public Decimal Decimal { get; set; }
        public DateTime DateTime { get; set; }
        public Byte Byte { get; set; }
        public Boolean Boolean { get; set; }
        public byte[] Blob { get; set; }

        public ICollection<string> Messages { get; set; }
        public IList<DateTime> Stamps { get; set; }

        public Relation Relation { get; set; }
        public Relation DummyRelation { get; set; }
        public List<Relation> SecondaryRelations { get; set; }

        public Dictionary<string, int> IndexedValues { get; set; }
        public Dictionary<Identifier, Category> Categories { get; set; }

        public static DataBlock Filled()
        {
            return new DataBlock {
                Id = Guid.Parse("F5159142-B9A3-45FA-85AE-E0C9E60990A9"),
                Blob = new byte[] { 1, 2, 3},
                Boolean = true,
                Byte = 42,
                DateTime = new DateTime(2014, 04, 01, 10, 00, 00),
                Decimal = 44754.324M,
                Double = 4357.32,
                Int16 = 20234,
                Int32 = 43554654,
                Int64 = 4349893895849554545,
                Messages = new Collection<string> { "Test1", "Test2", "Test3", "Test4", "Test5"},
                Single = 32.1f,
                String = "Hello World",
                TimeSpan = new TimeSpan(10, 30, 00),
                UInt16 = 64322,
                UInt32 = 3454654454,
                UInt64 = 9859459485984955454,
                Stamps = new []{new DateTime(2010, 03, 01, 22, 00, 00)},
                Relation = new Relation {
                    Id = Guid.Parse("F68EF7D4-6F62-476B-BC5E-71AD86549A63"),
                    Name = "Connection",
                    Description = "Generic connection between relations",
                    Value = 77
                },
                DummyRelation = null,
                SecondaryRelations = new List<Relation> {
                    new Relation {
                        Id = Guid.Parse("C9EDB616-26EC-44BB-9E70-3F38C7C18C91"),
                        Name = "Line1",
                        Description = "First line of cascade",
                        Value = 187
                    }
                },
                IndexedValues = new Dictionary<string, int> {
                    {"V1", 1},
                    {"V2", 2},
                    {"V3", 3},
                    {"V4", 4}
                },
                Categories = new Dictionary<Identifier, Category> {
                    {new Identifier {Id = 1, Type = ApplicationType.Api}, new Category {Name = "Warning", Description = "Warning of something", Image = new byte[]{1, 2, 3, 4, 5}}},
                    {new Identifier {Id = 2, Type = ApplicationType.Api}, new Category {Name = "Error", Description = "Error of something", Image = new byte[]{1, 2, 3, 4, 5, 6, 7, 8, 9}}},
                    {new Identifier {Id = 3, Type = ApplicationType.Service}, new Category {Name = "Temporary"}}
                }
            };
        }

        public void AssertEqualTo(DataBlock expected)
        {
            Assert.Equal(expected.Id, Id);
            Assert.Equal(expected.Int16, Int16);
            Assert.Equal(expected.Int32, Int32);
            Assert.Equal(expected.Int64, Int64);
            Assert.Equal(expected.UInt16, UInt16);
            Assert.Equal(expected.UInt32, UInt32);
            Assert.Equal(expected.UInt64, UInt64);
            Assert.Equal(expected.Single, Single);
            Assert.Equal(expected.Double, Double);
            Assert.Equal(expected.Decimal, Decimal);
            Assert.Equal(expected.TimeSpan, TimeSpan);
            Assert.Equal(expected.DateTime, DateTime);
            Assert.Equal(expected.String, String);
            Assert.Equal(expected.Boolean, Boolean);
            Assert.Equal(expected.Byte, Byte);
            Assert.True(expected.Blob.SequenceEqual(Blob));

            Assert.True(expected.Messages.SequenceEqual(Messages));
            Assert.True(expected.Stamps.SequenceEqual(Stamps));

            Assert.NotNull(Relation);
            Assert.Equal(expected.Relation.Id, Relation.Id);
            Assert.Equal(expected.Relation.Name, Relation.Name);
            Assert.Equal(expected.Relation.Description, Relation.Description);
            Assert.Equal(expected.Relation.Value, Relation.Value);
            Assert.Null(DummyRelation);

            Assert.True(expected.IndexedValues.Keys.SequenceEqual(IndexedValues.Keys));
            Assert.True(expected.IndexedValues.Values.SequenceEqual(IndexedValues.Values));

            Assert.NotNull(Categories);
            Assert.Equal(3, Categories.Count);
            Assert.True(expected.Categories.Keys.SequenceEqual(Categories.Keys));
            Assert.True(expected.Categories.Values.SequenceEqual(Categories.Values));
        }

    }
    
}
