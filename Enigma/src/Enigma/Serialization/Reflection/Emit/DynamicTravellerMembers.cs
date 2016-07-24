using System;
using System.Reflection;

namespace Enigma.Serialization.Reflection.Emit
{
    public sealed class DynamicTravellerMembers
    {

        public readonly Type VisitArgsType;
        public readonly Type VisitArgsFactoryType;
        public readonly MethodInfo ConstructVisitArgsMethod;
        public readonly MethodInfo ConstructVisitArgsWithTypeMethod;

        public readonly Type[] TravellerConstructorTypes;

        public DynamicTravellerMembers()
        {
            VisitArgsType = typeof (VisitArgs);
            VisitArgsFactoryType = typeof (IVisitArgsFactory);

            var visitArgsFactoryTypeInfo = VisitArgsFactoryType.GetTypeInfo();
            ConstructVisitArgsMethod = visitArgsFactoryTypeInfo.GetMethod("Construct");
            ConstructVisitArgsWithTypeMethod = visitArgsFactoryTypeInfo.GetMethod("ConstructWith");

            TravellerConstructorTypes = new[] {VisitArgsFactoryType};
        }

    }
}
