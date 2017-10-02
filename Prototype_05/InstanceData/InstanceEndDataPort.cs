using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_05.InstanceData
{
    public class InstanceEndDataPort : InstanceDataPort
    {
        #region Declaration

        #endregion

        #region Constructor

        public InstanceEndDataPort()
            : base() { }

        public InstanceEndDataPort(string name, string id)
            : base(name, id) { }

        public InstanceEndDataPort(string name, string id, List<InstanceTransition> transitionList)
            : base(name, id, transitionList) { }

        public InstanceEndDataPort(string name, string id, List<InstanceTransition> transitionList, Enums.DataType inputData, string dataTitle)
            : base(name, id, transitionList)
        {
            _data = inputData;
            _dataTitle = dataTitle;
        }

        #endregion
    }
}
