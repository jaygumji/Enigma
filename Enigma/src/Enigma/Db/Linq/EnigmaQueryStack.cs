using System.Collections.Generic;

namespace Enigma.Db.Linq
{
    public class EnigmaQueryStack
    {

        private readonly Stack<IEnigmaQueryInstruction> _instructions;

        public EnigmaQueryStack()
        {
            _instructions = new Stack<IEnigmaQueryInstruction>();
        }

    }
}