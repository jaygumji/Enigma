/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System.IO;
using System.Linq;
using Enigma.Serialization.PackedBinary;
using Enigma.Serialization.Reflection.Emit;
using Enigma.Test.Fakes.Entities;
using Enigma.Test.Serialization.HardCoded;
using Xunit;


namespace Enigma.Test.Serialization.Binary
{

    
    public class PackedDataReadVisitorTests
    {

        [Fact]
        public void ReadHardCodedTravelTest()
        {
            var bytes = BinarySerializationTestContext.GetFilledDataBlockBlob();
            var stream = new MemoryStream(bytes);
            var visitor = new PackedDataReadVisitor(stream);

            var traveller = DataBlockHardCodedTraveller.Create();

            var graph = new DataBlock();
            traveller.Travel(visitor, graph);

            var expected = DataBlock.Filled();
            graph.AssertEqualTo(expected);
        }

        [Fact]
        public void ReadDynamicTravelTest()
        {
            var bytes = BinarySerializationTestContext.GetFilledDataBlockBlob();
            var stream = new MemoryStream(bytes);
            var visitor = new PackedDataReadVisitor(stream);

            var context = new DynamicTravellerContext();
            var traveller = context.GetInstance<DataBlock>();

            var graph = new DataBlock();
            traveller.Travel(visitor, graph);

            var expected = DataBlock.Filled();
            graph.AssertEqualTo(expected);
        }

    }
}
