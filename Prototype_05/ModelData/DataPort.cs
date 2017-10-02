using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_05.ModelData
{
    public abstract class DataPort : Port
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

        #endregion

        #region Constructor

        public DataPort()
            : base() { }

        public DataPort(string name, string id)
            : base(name, id) { }

        public DataPort(string name, string id, List<Transition> transitionList)
            : base(name, id, transitionList) { }

        public DataPort(string name, string id, List<Transition> transitionList, Enums.DataType inputData, string dataTitle)
            : base(name, id, transitionList)
        {
            _data = inputData;
            _dataTitle = dataTitle;
        }

        #endregion
    }
}
