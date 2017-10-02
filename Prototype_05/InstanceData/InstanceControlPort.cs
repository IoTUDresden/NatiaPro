using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_05.InstanceData
{
    public class InstanceControlPort : InstancePort
    {
        #region Constructor

        public InstanceControlPort()
            : base() { }

        public InstanceControlPort(string name, string id)
            : base(name, id) { }

        public InstanceControlPort(string name, string id, List<InstanceTransition> transitionList)
            : base(name, id, transitionList) { }

        #endregion
    }
}
