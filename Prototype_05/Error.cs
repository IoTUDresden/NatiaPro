using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prototype_05.InstanceData;

namespace Prototype_05
{
    /// <summary>
    /// Entwurfsklasse für Fehlermanagement.
    /// Bisher nicht implementiert.
    /// </summary>
    class Error
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

        protected string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        protected string _startTime;
        public string StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        protected ProcessInstance _instance;
        public ProcessInstance FailedInstance
        {
            get { return _instance; }
            set { _instance = value; }
        }

        #endregion


        #region Constructor

        public Error() { }

        public Error(string name, string id)
        {
            _name = name;
            _id = id;
        }

        public Error(string name, string id, ProcessInstance instance)
        {
            _name = name;
            _id = id;
            _instance = instance;
        }

        #endregion
    }
}
