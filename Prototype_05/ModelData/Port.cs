using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_05.ModelData
{
    public abstract class Port
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

        protected List<Transition> _transitionList;
        public List<Transition> TransitionList
        {
            get
            {
                if (_transitionList == null)
                {
                    _transitionList = new List<Transition>();
                } 
                return _transitionList;
            }
            set { _transitionList = value; }
        }

        #endregion

        #region Constructor

        public Port() { }

        public Port(string name, string id)
        {
            _name = name;
            _id = id;
        }

        public Port(string name, string id, List<Transition> transitionList)
        {
            _name = name;
            _id = id;
            _transitionList = transitionList;
        }

        #endregion

        //public Substep Substep
        //{
        //    get
        //    {
        //    }
        //    set
        //    {
        //    }
        //}

    }
}
