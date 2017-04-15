using Enigma.Testing.Fakes.Entities.Cars;
using System.Linq;

namespace Enigma.Test.Db.Linq
{
    public class EnigmaExpressionVisitorTests
    {

        public void Tree1()
        {
            var cars = new FakeEnigmaQueryableSet<Car>();
            var q = (
                from c in cars
                where c.RegistrationNumber == "X"
                select c);
            //var q = (
            //    from x in )
        }

    }
}
