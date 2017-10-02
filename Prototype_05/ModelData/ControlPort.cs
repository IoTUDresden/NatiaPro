using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_05.ModelData
{
    public class ControlPort : Port
    {
        #region Constructor

        public ControlPort()
            : base() { }

        public ControlPort(string name, string id)
            : base(name, id) { }

        public ControlPort(string name, string id, List<Transition> transitionList)
            : base(name, id, transitionList) { }

        #endregion
    }
}
