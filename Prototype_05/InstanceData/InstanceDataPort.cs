using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_05.InstanceData
{
    public abstract class InstanceDataPort : InstancePort
    {
        #region Declaration

        protected Enums.DataType _data;
        public Enums.DataType Data
        {
            get { return _data; }
            set { _data = value; }
        }

        protected string _dataTitle;
        public string DataTitle
        {
            get { return _dataTitle; }
            set { _dataTitle = value; }
        }

        protected string _dataValue;
        public string DataValue
        {
            get { return _dataValue; }
            set { _dataValue = value; }
        }

        #endregion

        #region Constructor

        public InstanceDataPort()
            : base() { }

        public InstanceDataPort(string name, string id)
            : base(name, id) { }

        public InstanceDataPort(string name, string id, List<InstanceTransition> transitionList)
            : base(name, id, transitionList) { }

        public InstanceDataPort(string name, string id, List<InstanceTransition> transitionList, Enums.DataType inputData, string dataTitle)
            : base(name, id, transitionList)
        {
            _data = inputData;
            _dataTitle = dataTitle;
        }

        #endregion
    }
}
