using System;
using System.Linq;
using System.Reflection;

namespace Enigma.Serialization.Reflection.Emit
{
    public class DynamicTraveller
    {
        private readonly DynamicTravellerMembers _members;
        private bool _isConstructing;
        private DynamicActivator _activator;

        public DynamicTraveller(Type travellerType, ConstructorInfo constructor, MethodInfo travelWriteMethod, MethodInfo travelReadMethod, DynamicTravellerMembers members)
        {
            TravellerType = travellerType;
            Constructor = constructor;
            TravelWriteMethod = travelWriteMethod;
            TravelReadMethod = travelReadMethod;
            _members = members;
            _isConstructing = true;
        }

        public Type TravellerType { get; private set; }

        public ConstructorInfo Constructor { get; private set; }

        public MethodInfo TravelWriteMethod { get; private set; }

        public MethodInfo TravelReadMethod { get; private set; }

        public void Complete(Type actualTravellerType)
        {
            var actualTravellerTypeInfo = actualTravellerType.GetTypeInfo();

            TravellerType = actualTravellerType;
            Constructor = actualTravellerTypeInfo.GetConstructor(_members.TravellerConstructorTypes);
            TravelWriteMethod = actualTravellerTypeInfo.GetMethod("Travel", TravelWriteMethod.GetParameters().Select(p => p.ParameterType).ToArray());
            TravelReadMethod = actualTravellerTypeInfo.GetMethod("Travel", TravelReadMethod.GetParameters().Select(p => p.ParameterType).ToArray());
            _activator = new DynamicActivator(Constructor);
            _isConstructing = false;
        }

        public IGraphTraveller GetInstance(IVisitArgsFactory factory)
        {
            if (_isConstructing) throw new InvalidOperationException("The type is still being constructed");

            return (IGraphTraveller)_activator.Activate(factory);
        }
    }
}