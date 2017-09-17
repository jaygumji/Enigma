using System;
using System.Linq;
using System.Reflection;

namespace Enigma.Serialization.Reflection.Emit
{
    public class DynamicTraveller
    {
        private Type _travellerType;
        private ConstructorInfo _constructor;
        private MethodInfo _travelWriteMethod;
        private MethodInfo _travelReadMethod;
        private readonly DynamicTravellerMembers _members;
        private bool _isConstructing;
        private DynamicActivator _activator;

        public DynamicTraveller(Type travellerType, ConstructorInfo constructor, MethodInfo travelWriteMethod, MethodInfo travelReadMethod, DynamicTravellerMembers members)
        {
            _travellerType = travellerType;
            _constructor = constructor;
            _travelWriteMethod = travelWriteMethod;
            _travelReadMethod = travelReadMethod;
            _members = members;
            _isConstructing = true;
        }

        public Type TravellerType => _travellerType;
        public ConstructorInfo Constructor => _constructor;
        public MethodInfo TravelWriteMethod => _travelWriteMethod;
        public MethodInfo TravelReadMethod => _travelReadMethod;

        public void Complete(Type actualTravellerType)
        {
            var actualTravellerTypeInfo = actualTravellerType.GetTypeInfo();

            _travellerType = actualTravellerType;
            _constructor = actualTravellerTypeInfo.GetConstructor(_members.TravellerConstructorTypes);
            _travelWriteMethod = actualTravellerTypeInfo.GetMethod("Travel", _travelWriteMethod.GetParameters().Select(p => p.ParameterType).ToArray());
            _travelReadMethod = actualTravellerTypeInfo.GetMethod("Travel", _travelReadMethod.GetParameters().Select(p => p.ParameterType).ToArray());
            _activator = new DynamicActivator(_constructor);
            _isConstructing = false;
        }

        public IGraphTraveller GetInstance(IVisitArgsFactory factory)
        {
            if (_isConstructing) throw new InvalidOperationException("The type is still being constructed");

            return (IGraphTraveller)_activator.Activate(factory);
        }
    }
}