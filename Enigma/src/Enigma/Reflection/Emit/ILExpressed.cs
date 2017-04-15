using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public sealed class ILExpressed
    {

        private static readonly MethodInfo MethodGetTypeFromHandleToken =
            typeof (Type).GetTypeInfo().GetMethod("GetTypeFromHandle", new[] {typeof (RuntimeTypeHandle)});

        public ILExpressed(ILGenerator il, ITypeProvider provider)
        {
            Gen = il;
            Provider = provider;
            Snippets = new ILCodeSnippets(this);
            Var = new ILCodeVariableGenerator(this);
        }

        public ILGenerator Gen { get; }
        public ITypeProvider Provider { get; }
        public ILCodeSnippets Snippets { get; }
        public ILCodeVariableGenerator Var { get; }

        public void LoadValue(int value)
        {
            OpCode opCode;
            if (OpCodesLookups.LoadInt32.TryGetValue(value, out opCode)) {
                Gen.Emit(opCode);
                return;
            }

            Gen.Emit(OpCodes.Ldc_I4_S, value);
        }

        public void LoadValue(bool value)
        {
            LoadValue(value ? 1 : 0);
        }

        public void LoadValue(uint value)
        {
            LoadValue((int)value);
        }

        public void LoadValue(string value)
        {
            Gen.Emit(OpCodes.Ldstr, value);
        }

        public void Call(MethodInfo method)
        {
            Gen.EmitCall(OpCodes.Call, method, null);
        }

        public void CallVirt(MethodInfo method)
        {
            Gen.EmitCall(OpCodes.Callvirt, method, null);
        }

        public void CallBaseConstructor(ConstructorInfo constructor)
        {
            Gen.Emit(OpCodes.Call, constructor);
        }

        public void Return()
        {
            Gen.Emit(OpCodes.Ret);
        }

        public void Cast(Type type)
        {
            Gen.Emit(OpCodes.Castclass, type);
        }

        public LocalILCodeVariable DeclareLocal(string name, Type type)
        {
            //int index;
            //if (_locals.TryGetValue(name, out index)) {
            //    _locals[name] = ++index;
            //}
            //else {
            //    index = 0;
            //    _locals.Add(name, index);
            //}
            var local = Gen.DeclareLocal(type);
            return local;
        }

        public Label DefineLabel()
        {
            return Gen.DefineLabel();
        }

        public void MarkLabel(Label label)
        {
            Gen.MarkLabel(label);
        }

        public Label DefineAndMarkLabel()
        {
            var label = Gen.DefineLabel();
            Gen.MarkLabel(label);
            return label;
        }

        public void LoadField(FieldInfo field)
        {
            Gen.Emit(OpCodes.Ldfld, field);
        }

        public void LoadStaticField(FieldInfo field)
        {
            Gen.Emit(OpCodes.Ldsfld, field);
        }

        public void TransferLong(Label label)
        {
            Gen.Emit(OpCodes.Br, label);
        }

        public void TransferLongIfFalse(Label label)
        {
            Gen.Emit(OpCodes.Brfalse, label);
        }

        public void TransferLongIfTrue(Label label)
        {
            Gen.Emit(OpCodes.Brtrue, label);
        }

        public void TransferShort(Label label)
        {
            Gen.Emit(OpCodes.Br_S, label);
        }

        public void TransferShortIfFalse(Label label)
        {
            Gen.Emit(OpCodes.Brfalse_S, label);
        }

        public void TransferShortIfTrue(Label label)
        {
            Gen.Emit(OpCodes.Brtrue_S, label);
        }

        public void TransferIfNull(ILCodeVariable reference, Label label)
        {
            Var.Load(reference);
            LoadNull();
            CompareEquals();
            TransferLongIfTrue(label);
        }

        public void TransferIfNotNull(ILCodeVariable reference, Label label)
        {
            Var.Load(reference);
            LoadNull();
            CompareEquals();
            TransferLongIfFalse(label);
        }

        public Label Try()
        {
            return Gen.BeginExceptionBlock();
        }

        public void Catch(Type exceptionType)
        {
            Gen.BeginCatchBlock(exceptionType);
        }

        public void Finally()
        {
            Gen.BeginFinallyBlock();
        }

        public void EndTry()
        {
            Gen.EndExceptionBlock();
        }

        public void LoadNull()
        {
            Gen.Emit(OpCodes.Ldnull);
        }

        public void CompareEquals()
        {
            Gen.Emit(OpCodes.Ceq);
        }

        public void SetField(FieldInfo field, IILCode value)
        {
            LoadThis();
            value.Generate(this);
            Gen.Emit(OpCodes.Stfld, field);
        }

        public void SetFieldWithDefaultConstructor(FieldInfo field, ConstructorInfo constructor)
        {
            LoadThis();
            Gen.Emit(OpCodes.Newobj, constructor);
            Gen.Emit(OpCodes.Stfld, field);
        }

        public void LoadThis()
        {
            Gen.Emit(OpCodes.Ldarg_0);
        }

        public void Construct(ConstructorInfo constructor)
        {
            Gen.Emit(OpCodes.Newobj, constructor);
        }

        public void Constrained(Type type)
        {
            Gen.Emit(OpCodes.Constrained, type);
        }

        public void Throw()
        {
            Gen.Emit(OpCodes.Throw);
        }

        public void Generate(IILCode code)
        {
            code.Generate(this);
        }

        public void Negate()
        {
            LoadValue(0);
            CompareEquals();
        }

        public void LoadRef(Type type)
        {
            Gen.Emit(OpCodes.Ldtoken, type);
            Call(MethodGetTypeFromHandleToken);
        }

        public ILChainIfCondition IfEqual(ILCodeParameter left, ILCodeParameter right)
        {
            return new ILChainIfCondition(this, () => Snippets.AreEqual(left, right));
        }

        public ILChainIfCondition IfNotEqual(ILCodeParameter left, ILCodeParameter right)
        {
            return new ILChainIfCondition(this, () => {
                Snippets.AreEqual(left, right);
                Negate();
            });
        }
    }
}