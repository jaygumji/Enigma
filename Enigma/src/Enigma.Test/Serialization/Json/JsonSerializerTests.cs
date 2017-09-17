using System.IO;
using Enigma.Serialization;
using Xunit;

namespace Enigma.Test.Serialization.Json
{
    public class JsonSerializerTests
    {
        private readonly JsonSerializationTestContext _context = new JsonSerializationTestContext();
        
    }

    public class JsonWriteVisitorTests
    {

        private readonly JsonSerializationTestContext _context = new JsonSerializationTestContext();

        [Fact]
        public void VisitEnterRoot()
        {
            _context.AssertWriteVisitorCall("{", v => v.Visit(new {}, VisitArgs.CreateRoot(LevelType.Single)));
        }

        [Fact]
        public void VisitEnterCollection()
        {
            _context.AssertWriteVisitorCall("\"fieldName\":[", v => v.Visit(new int[] {}, new VisitArgs("FieldName", LevelType.Collection, 1, StateBag.Empty)));
        }

        [Fact]
        public void VisitEnterCollectionInCollection()
        {
            _context.AssertWriteVisitorCall("[", v => v.Visit(new int[] { }, VisitArgs.CollectionInCollection));
        }

        [Fact]
        public void VisitEnterCollectionInDictionaryKey()
        {
            _context.AssertWriteVisitorCall("[", v => v.Visit(new int[] { }, VisitArgs.CollectionInDictionaryKey));
        }

        [Fact]
        public void VisitEnterCollectionInDictionaryValue()
        {
            _context.AssertWriteVisitorCall("[", v => v.Visit(new int[] { }, VisitArgs.CollectionInDictionaryValue));
        }

        [Fact]
        public void VisitEnterCollectionItem()
        {
            _context.AssertWriteVisitorCall("[1,2", v => {
                v.Visit(new int[] {}, VisitArgs.CollectionInCollection);
                v.VisitValue(1, VisitArgs.CollectionItem);
                v.VisitValue(2, VisitArgs.CollectionItem);
            });
        }
    }
}
