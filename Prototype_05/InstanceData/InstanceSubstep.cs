using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prototype_05.ModelData;

namespace Prototype_05.InstanceData
{
    public class InstanceSubstep : ProcessInstance
    {
        protected Enum _processCharacter;
        public Enum ProcessCharacter
        {
            get { return _processCharacter; }
            set { _processCharacter = value; }
        }

        public InstanceSubstep()
        {
            _processCharacter = Enums.Character.Process;
        }

        public InstanceSubstep(ProcessModel model)
            : base(model)
        {
            _processCharacter = Enums.Character.Process;
        }
    }
}
