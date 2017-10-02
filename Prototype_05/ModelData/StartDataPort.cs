using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_05.ModelData
{
    public class StartDataPort : DataPort
    {
        #region Constructor

        public StartDataPort()
            : base() { }

        public StartDataPort(string name, string id)
            : base(name, id) { }

        public StartDataPort(string name, string id, List<Transition> transitionList)
            : base(name, id, transitionList) { }

        public StartDataPort(string name, string id, List<Transition> transitionList, Enums.DataType inputData, string dataTitle)
            : base(name, id, transitionList)
        {
            _data = inputData;
            _dataTitle = dataTitle;
        }

        #endregion
    }
}
