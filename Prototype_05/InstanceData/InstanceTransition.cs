﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype_05.InstanceData
{
    public class InstanceTransition
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

        protected string _targetPortId;
        public string TargetPortId
        {
            get { return _targetPortId; }
            set { _targetPortId = value; }
        }

        #endregion

        #region Constructor

        public InstanceTransition() { }

        public InstanceTransition(string name, string id)
        {
            _name = name;
            _id = id;
        }

        public InstanceTransition(string name, string id, string targetPort)
        {
            _name = name;
            _id = id;
            _targetPortId = targetPort;
        }

        #endregion
    }
}
