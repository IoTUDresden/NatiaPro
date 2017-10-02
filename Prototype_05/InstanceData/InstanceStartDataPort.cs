using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_05.InstanceData
{
    public class InstanceStartDataPort : InstanceDataPort
    {
        #region Constructor

        public InstanceStartDataPort()
            : base() { }

        public InstanceStartDataPort(string name, string id)
            : base(name, id) { }

        public InstanceStartDataPort(string name, string id, List<InstanceTransition> transitionList)
            : base(name, id, transitionList) { }

        public InstanceStartDataPort(string name, string id, List<InstanceTransition> transitionList, Enums.DataType inputData, string dataTitle)
            : base(name, id, transitionList)
        {
            _data = inputData;
            _dataTitle = dataTitle;
        }

        #endregion
    }
}
