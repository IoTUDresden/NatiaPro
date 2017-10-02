using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prototype_05.ModelData;

namespace Prototype_05.InstanceData
{
    public class ProcessInstance
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

        protected ProcessModel _model;
        public ProcessModel BoundModel
        {
            get { return _model; }
            set { _model = value; }
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

        protected string _executionLocation;
        public string ExecutionLocation
        {
            get { return _executionLocation; }
            set { _executionLocation = value; }
        }

        protected Enums.ProcessState _state = Enums.ProcessState.waiting;
        public Enums.ProcessState State
        {
            get { return _state; }
            set { _state = value; }
        }

        protected List<Enums.Category> _categoryList;
        public List<Enums.Category> Categories
        {
            get
            {
                if (_categoryList == null)
                {
                    _categoryList = new List<Enums.Category>();
                }
                return _categoryList;
            }
            set { _categoryList = value; }
        }

        protected List<InstancePort> _portList;
        public List<InstancePort> PortList
        {
            get
            {
                if (_portList == null)
                {
                    _portList = new List<InstancePort>();
                }
                return _portList;
            }
            set { _portList = value; }
        }

        protected List<InstanceSubstep> _substepList;
        public List<InstanceSubstep> SubstepList
        {
            get
            {
                if (_substepList == null)
                {
                    _substepList = new List<InstanceSubstep>();
                }
                return _substepList;
            }
            set { _substepList = value; }
        }

        protected List<InstancePropertyPanel> _panelList = new List<InstancePropertyPanel>();
        public List<InstancePropertyPanel> PanelList
        {
            get
            {
                if (_panelList == null)
                {
                    _panelList = new List<InstancePropertyPanel>();
                }
                return _panelList;
            }
            set { _panelList = value; }
        }

        #endregion

        #region Constructor

        public ProcessInstance() { }

        public ProcessInstance(ProcessModel model)
        {
            LoadDataFromModel(model);
        }
        
        #endregion

        /// <summary>
        /// Geht rekursiv durch Prozessstruktur und baut Pendant mit Instanzklassen auf.
        /// </summary>
        /// <param name="model">Prozessmodell, dessen Instanzpendant erzeugt wird.</param>
        public void LoadDataFromModel(ProcessModel model)
        {
            _id = model.Id + "-" + model.RegisterNewInstance(this).ToString();
            model.CurrentlyExecutedInstances++;
            
            _name = model.Name;
            _type = model.Type;
            _categoryList = model.Categories;

            _portList = new List<InstancePort>(model.PortList.Count);
            foreach (Port port in model.PortList)
            {
                List<InstanceTransition> transitionList = new List<InstanceTransition>(port.TransitionList.Count);
                foreach (Transition transition in port.TransitionList)
                {
                    transitionList.Add(new InstanceTransition(transition.Name, transition.Id, transition.TargetPortId));
                }

                InstancePort instancePort = new InstanceControlPort();
                if (port is ModelData.StartDataPort)
                {
                    instancePort = new InstanceStartDataPort();
                    ((InstanceStartDataPort)instancePort).Data = ((StartDataPort)port).Data;
                    ((InstanceStartDataPort)instancePort).DataTitle = ((StartDataPort)port).DataTitle;
                }
                else if (port is ModelData.EndDataPort)
                {
                    instancePort = new InstanceEndDataPort();
                    ((InstanceEndDataPort)instancePort).Data = ((EndDataPort)port).Data;
                    ((InstanceEndDataPort)instancePort).DataTitle = ((EndDataPort)port).DataTitle;
                }
                instancePort.Name = port.Name;
                instancePort.Id = port.Id;
                instancePort.TransitionList = transitionList;

                _portList.Add(instancePort);
            }

            _substepList = new List<InstanceSubstep>(model.SubstepList.Count);
            foreach (Substep substep in model.SubstepList)
            {
                InstanceSubstep instanceSubstep = new InstanceSubstep();
                instanceSubstep.Name = substep.Name;
                instanceSubstep.Id = substep.Id;
                instanceSubstep.ProcessCharacter = substep.ProcessCharacter;
                
                instanceSubstep.LoadDataFromModel(substep);
                _substepList.Add(instanceSubstep);
            }

            //_substepList = _model.SubstepList;
            _model = model;

        }

    }
}
