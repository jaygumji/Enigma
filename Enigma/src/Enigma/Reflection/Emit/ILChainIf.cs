using System;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class ILChainIf
    {
        private readonly ILGenerator _il;

        public ILChainIf(ILGenerator il)
        {
            _il = il;
        }

        public ILGenerationHandler Condition { get; set; }
        public ILGenerationHandler Body { get; set; }
        public ILGenerationHandler ElseBody { get; set; }

        public void End()
        {
            if (Condition == null) throw new InvalidOperationException("No condition set");
            if (Body == null) throw new InvalidOperationException("No body set");

            Condition.Invoke();

            var endLabel = _il.NewLabel();

            var elseLabel = default(ILLabel);
            if (ElseBody != null) {
                elseLabel = _il.NewLabel();
                elseLabel.TransferLongIfFalse();
            }
            else {
                endLabel.TransferLongIfFalse();
            }

            Body.Invoke();

            if (ElseBody != null) {
                endLabel.TransferLong();

                elseLabel.Mark();
                ElseBody.Invoke();
            }
            
            endLabel.Mark();
        }
    }
}