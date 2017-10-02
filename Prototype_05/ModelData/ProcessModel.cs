using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prototype_05.InstanceData;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Reflection;

namespace Prototype_05.ModelData
{
    public class ProcessModel : INotifyPropertyChanged
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

        protected int _instanceAmount = 0;
        public int CurrentlyExecutedInstances
        {
            get { return _instanceAmount; }
            set
            {
                if (_instanceAmount != value)
                {
                    _instanceAmount = value;
                    OnPropertyChanged(MethodInfo.GetCurrentMethod());
                    NotifyPanelsAboutInstanceAmountChange();
                }
            }
        }

        protected int _nextUniqueInstanceId = 0;
        public int EverExecutedInstances
        {
            get { return _nextUniqueInstanceId; }
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

        protected List<Port> _portList;
        public List<Port> PortList
        {
            get
            {
                if (_portList == null)
                {
                    _portList = new List<Port>();
                }
                return _portList;
            }
            set { _portList = value; }
        }

        protected List<Substep> _substepList;
        public List<Substep> SubstepList
        {
            get
            {
                if (_substepList == null)
                {
                    _substepList = new List<Substep>();
                }
                return _substepList;
            }
            set { _substepList = value; }
        }

        protected List<ModelPropertyPanel> _panelList = new List<ModelPropertyPanel>();
        public List<ModelPropertyPanel> PanelList
        {
            get
            {
                if (_panelList == null)
                {
                    _panelList = new List<ModelPropertyPanel>();
                }
                return _panelList;
            }
            set { _panelList = value; }
        }

        protected List<ProcessInstance> _instanceList = new List<ProcessInstance>();
        public List<ProcessInstance> InstanceList
        {
            get
            {
                if (_instanceList == null)
                {
                    _instanceList = new List<ProcessInstance>();
                }
                return _instanceList;
            }
            set { _instanceList = value; }
        }
        #endregion
        
        #region INotifyPropertyChanged Member

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when property changed.
        /// </summary>
        /// <param name="name">The name.</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler tempHandler = PropertyChanged;
            if (tempHandler != null)
                tempHandler(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Called when property changed.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        protected void OnPropertyChanged(MethodBase methodBase)
        {
            OnPropertyChanged(methodBase.Name.Substring(4));
        }

        #endregion

        #region Constructor

        public ProcessModel() { }

        public ProcessModel(string name, string id)
        {
            _name = name;
            _id = id;
        }

        public ProcessModel(string name, string id, List<Port> portList, List<Substep> substepList)
        {
            _name = name;
            _id = id;
            _portList = portList;
            _substepList = substepList;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Füge Kategorie hinzu.
        /// Üblicherweise für Favoriten-Kategorie benutzt.
        /// </summary>
        /// <param name="category">hinzuzufügende Kategorie</param>
        public void AddCategory(int category)
        {
            _categoryList.Add((Enums.Category)category);
            foreach (ModelPropertyPanel modelpanel in _panelList)
            {
                modelpanel.UpdateFavouriteMenuEntry();
            }
        }

        /// <summary>
        /// Entferne Kategorie.
        /// Üblicherweise für Favoriten-Kategorie benutzt.
        /// </summary>
        /// <param name="category">hinzuzufügende Kategorie</param>
        public void RemoveCategory(int category)
        {
            _categoryList.Remove((Enums.Category)category);
            foreach (ModelPropertyPanel modelpanel in _panelList)
            {
                modelpanel.UpdateFavouriteMenuEntry();
            }
        }

        /// <summary>
        /// Speicher neue Instanz und generiere einzigartige ID.
        /// </summary>
        /// <param name="instance">Instanz</param>
        /// <returns>Instanz-ID</returns>
        public int RegisterNewInstance(ProcessInstance instance)
        {
            _instanceList.Add(instance);
            return _nextUniqueInstanceId++;
        }

        /// <summary>
        /// Ruft methoden auf zum Updaten der vorhandenen modellidentischen Instanzen.
        /// </summary>
        private void NotifyPanelsAboutInstanceAmountChange()
        {
            foreach (ModelPropertyPanel modelpanel in _panelList)
            {
                modelpanel.UpdateRunningInstancesAmount();
            }
            foreach (ProcessInstance instance in _instanceList)
            {
                foreach (InstancePropertyPanel instancePanel in instance.PanelList)
                {
                    instancePanel.UpdateNavigation();
                }
            }
        }

        #endregion
    }
}
