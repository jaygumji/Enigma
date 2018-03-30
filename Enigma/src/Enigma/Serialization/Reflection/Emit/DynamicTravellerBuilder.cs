using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Enigma.Reflection.Emit;
using Enigma.Reflection.Emit.Pointers;
using ConstructorBuilder = Enigma.Reflection.Emit.ConstructorBuilder;
using MethodBuilder = Enigma.Reflection.Emit.MethodBuilder;

namespace Enigma.Serialization.Reflection.Emit
{

    public class DynamicTravellerBuilder
    {
        private readonly Type _type;
        private readonly DynamicTravellerContext _dtContext;
        private readonly ClassBuilder _classBuilder;
        private readonly SerializableTypeProvider _typeProvider;
        private readonly ConstructorBuilder _constructorBuilder;
        private readonly MethodBuilder _travelWriteMethod;
        private readonly MethodBuilder _travelReadMethod;

        public DynamicTravellerBuilder(DynamicTravellerContext dtContext, ClassBuilder classBuilder, SerializableTypeProvider typeProvider, Type type)
        {
            _dtContext = dtContext;
            _classBuilder = classBuilder;
            _typeProvider = typeProvider;
            _type = type;
            _constructorBuilder = _classBuilder.DefineConstructor(typeof(IVisitArgsFactory));

            var baseConstructor = typeof(object).GetTypeInfo().GetConstructor(Type.EmptyTypes);
            var il = _constructorBuilder.IL;
            il.Load(ILPointer.This());
            il.Emit(OpCodes.Call, baseConstructor);

            _travelWriteMethod = _classBuilder.DefineOverloadMethod("Travel", typeof(void), new[] { typeof(IWriteVisitor), _type });
            _travelReadMethod = _classBuilder.DefineOverloadMethod("Travel", typeof(void), new[] { typeof(IReadVisitor), _type });

            DynamicTraveller = new DynamicTraveller(_classBuilder.Type, _constructorBuilder.Reference, _travelWriteMethod.Method, _travelReadMethod.Method, dtContext.Members);
        }

        public DynamicTraveller DynamicTraveller { get; }

        public void BuildTraveller()
        {
            if (_classBuilder.IsSealed) throw new InvalidOperationException("Classification builder is sealed");
            var target = _typeProvider.GetOrCreate(_type);
            var members = _dtContext.Members;
            var factoryArgument = new ILArgPointer(members.VisitArgsFactoryType, 1);


            var childTravellers = new Dictionary<Type, ChildTravellerInfo>();
            var argFields = new Dictionary<SerializableProperty, FieldInfo>();

            var il = _constructorBuilder.IL;
            var travellerIndex = 0;
            foreach (var property in target.Properties) {
                var argField = _classBuilder.DefinePrivateField("_arg" + property.Ref.Name, members.VisitArgsType);
                var visitArgsCode = new ILCallMethodSnippet(factoryArgument, members.ConstructVisitArgsMethod, property.Ref.Name);
                il.Set(ILPointer.This(), argField, visitArgsCode);
                argFields.Add(property, argField);

                if (!ReflectionAnalyzer.TryGetComplexTypes(property.Ext, out var types)) {
                    continue;
                }
                
                foreach (var type in types) {
                    if (childTravellers.ContainsKey(type)) {
                        continue;
                    }

                    var dynamicTraveller = _dtContext.Get(type);
                    var interfaceType = typeof (IGraphTraveller<>).MakeGenericType(type);
                    var fieldBuilder = _classBuilder.DefinePrivateField(string.Concat("_traveller", type.Name, ++travellerIndex), interfaceType);
                    childTravellers.Add(type, new ChildTravellerInfo {
                        Field = fieldBuilder,
                        TravelWriteMethod = dynamicTraveller.TravelWriteMethod,
                        TravelReadMethod = dynamicTraveller.TravelReadMethod
                    });
                    
                    var getFactoryCode = ILSnippet.Call(factoryArgument, members.ConstructVisitArgsWithTypeMethod, type);
                    var newTraveller = ILPointer.New(dynamicTraveller.Constructor, getFactoryCode);
                    il.Set(ILPointer.This(), fieldBuilder, newTraveller);
                }
            }
            il.Emit(OpCodes.Ret);

            var context = new TravellerContext(childTravellers, argFields);
            BuildWriteMethods(target, context);
            BuildReadMethods(target, context);

            _classBuilder.Seal();
            DynamicTraveller.Complete(_classBuilder.Type);
        }

        private void BuildWriteMethods(SerializableType target, TravellerContext context)
        {
            var typedMethodBuilder = _travelWriteMethod;
            var writeBuilder = new DynamicWriteTravellerBuilder(typedMethodBuilder, target, context, _typeProvider.Provider);
            writeBuilder.BuildTravelWriteMethod();

            var untypedMethodBuilder = _classBuilder.DefineOverloadMethod("Travel", typeof(void), new[] { typeof(IWriteVisitor), typeof(object) });
            var il = untypedMethodBuilder.IL;
            il.InvokeMethod(ILPointer.This(),
                typedMethodBuilder.Method,
                ILPointer.Arg(1, typeof(IWriteVisitor)),
                ILPointer.Arg(2, typeof(object)).Cast(target.Type));

            il.Emit(OpCodes.Ret);
        }

        private void BuildReadMethods(SerializableType target, TravellerContext context)
        {
            var typedMethodBuilder = _travelReadMethod;
            var readBuilder = new DynamicReadTravellerBuilder(typedMethodBuilder, target, context, _typeProvider.Provider);
            readBuilder.BuildTravelReadMethod();

            var untypedMethodBuilder = _classBuilder.DefineOverloadMethod("Travel", typeof(void), new[] { typeof(IReadVisitor), typeof(object) });
            var il = untypedMethodBuilder.IL;

            il.InvokeMethod(ILPointer.This(),
                typedMethodBuilder.Method,
                ILPointer.Arg(1, typeof(IReadVisitor)),
                ILPointer.Arg(2, typeof(object)).Cast(target.Type));

            il.Emit(OpCodes.Ret);
        }

    }
}
