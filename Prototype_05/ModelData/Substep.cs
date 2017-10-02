using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_05.ModelData
{
    public class Substep : ProcessModel
    {
        protected Enum _processCharacter;
        public Enum ProcessCharacter
        {
            get { return _processCharacter; }
            set { _processCharacter = value; }
        }

        public Substep()
            : base()
        {
            _processCharacter = Enums.Character.Process;
        }
    }
}
