using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class ArrayElementILCodeVariable : ILCodeVariable
    {
        private readonly ILCodeParameter _array;
        private readonly int _index;

        public ArrayElementILCodeVariable(ILCodeParameter array, int index) : base(GetTypeFrom(array))
        {
            _array = array;
            _index = index;
        }

        private static Type GetTypeFrom(ILCodeParameter array)
        {
            var type = array.ParameterType;
            if (!type.IsArray) throw new ArgumentException("The supplied type must be an array, type was " + type.FullName);
            return type.GetElementType();
        }

        protected override void OnGet(ILExpressed il)
        {
            il.Var.Load(_array);
            il.LoadValue(_index);

            if (VariableType.GetTypeInfo().IsClass) {
                il.Gen.Emit(OpCodes.Ldelem_Ref);
            }
            else if (VariableType == typeof(byte)) {
                il.Gen.Emit(OpCodes.Ldelem_U1);
            }
            else if (VariableType == typeof(short)) {
                il.Gen.Emit(OpCodes.Ldelem_I2);
            }
            else if (VariableType == typeof(int)) {
                il.Gen.Emit(OpCodes.Ldelem_I4);
            }
            else if (VariableType == typeof(long) || VariableType == typeof(ulong)) {
                il.Gen.Emit(OpCodes.Ldelem_I8);
            }
            else if (VariableType == typeof(float)) {
                il.Gen.Emit(OpCodes.Ldelem_R4);
            }
            else if (VariableType == typeof(double)) {
                il.Gen.Emit(OpCodes.Ldelem_R8);
            }
            else if (VariableType == typeof(ushort)) {
                il.Gen.Emit(OpCodes.Ldelem_U2);
            }
            else if (VariableType == typeof(uint)) {
                il.Gen.Emit(OpCodes.Ldelem_U4);
            }
            else {
                il.Gen.Emit(OpCodes.Ldelem, VariableType);
            }
        }

        protected override void OnSet(ILExpressed il)
        {
            var value = il.DeclareLocal("tmpValue", VariableType);
            il.Var.Set(value);
            il.Var.Load(_array);
            il.LoadValue(_index);
            il.Var.Load(value);

            if (VariableType.GetTypeInfo().IsClass) {
                il.Gen.Emit(OpCodes.Stelem_Ref);
            }
            else if (VariableType == typeof(byte)) {
                il.Gen.Emit(OpCodes.Stelem_I1);
            }
            else if (VariableType == typeof(short) || VariableType == typeof(ushort)) {
                il.Gen.Emit(OpCodes.Stelem_I2);
            }
            else if (VariableType == typeof(int) || VariableType == typeof(uint)) {
                il.Gen.Emit(OpCodes.Stelem_I4);
            }
            else if (VariableType == typeof(long) || VariableType == typeof(ulong)) {
                il.Gen.Emit(OpCodes.Stelem_I8);
            }
            else if (VariableType == typeof(float)) {
                il.Gen.Emit(OpCodes.Stelem_R4);
            }
            else if (VariableType == typeof(double)) {
                il.Gen.Emit(OpCodes.Stelem_R8);
            }
            else {
                il.Gen.Emit(OpCodes.Stelem, VariableType);
            }
        }

        protected override void OnGetAddress(ILExpressed il)
        {
            il.Var.Load(_array);
            il.LoadValue(_index);
            il.Gen.Emit(OpCodes.Ldelema, VariableType);
        }

    }
}