using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_05.InstanceData
{
    public abstract class InstancePort
    {
        #region Declaration
        protected string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        protected string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        protected List<InstanceTransition> _transitionList;
        public List<InstanceTransition> TransitionList
        {
            get
            {
                if (_transitionList == null)
                {
                    _transitionList = new List<InstanceTransition>();
                } 
                return _transitionList;
            }
            set { _transitionList = value; }
        }

        #endregion

        #region Constructor

        public InstancePort() { }

        public InstancePort(string name, string id)
        {
            _name = name;
            _id = id;
        }

        public InstancePort(string name, string id, List<InstanceTransition> transitionList)
        {
            _name = name;
            _id = id;
            _transitionList = transitionList;
        }

        #endregion

    }
}
