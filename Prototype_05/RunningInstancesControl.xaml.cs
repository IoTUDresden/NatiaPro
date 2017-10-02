using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prototype_05.ModelData;
using Prototype_05.InstanceData;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Windows.Media.Animation;
using System.Reflection;
using log4net;

namespace Prototype_05
{
	/// <summary>
	/// Interaktionslogik für RunningInstancesControl.xaml
	/// </summary>
	public partial class RunningInstancesControl : UserControl
    {
        #region Declaration

        /// <summary>
        /// Globaler Logdienst
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected InstanceControlWidget _attachedControl;
        /// <summary>
        /// zugehöriges InstanceControlWidget
        /// </summary>
        public InstanceControlWidget AttachedControl
        {
            get { return _attachedControl; }
            set { _attachedControl = value; }
        }

        /// <summary>
        /// aktuell ausgewählter Tab
        /// </summary>
        private int CurrentGroup = 2;
        /// <summary>
        /// Tocuhdevice zur Differenzierung mehrerer Finger
        /// </summary>
        private TouchDevice GroupTouchDevice;

        /// <summary>
        /// aktuell als Vorschau ausgewählter Navigationstab.
        /// </summary>
        private int PreviewGroup = -2;

        /// <summary>
        /// Gibt Startwerte für Detail- oder Miniview in Kategorie-Tab an.
        /// <value>true</value> wenn Mini-View aktiv sein soll
        /// </summary>
        private bool[] MiniView_Category = new bool[4] { true, true, true, true };
        /// <summary>
        /// Gibt Startwerte für Detail- oder Miniview in State-Tab an.
        /// <value>true</value> wenn Mini-View aktiv sein soll
        /// </summary>
        private bool[] MiniView_State = new bool[4] {true, !true, !true, !true};

        /// <summary>
        /// Mapping jeder Instanz mit ihren zugehörigen InstanceDetailWidgets, da es bei Kategorieview ja mehr als eins geben kann
        /// </summary>
        private Dictionary<ProcessInstance, List<InstanceDetailWidget>> MapInstanceToOverview
            = new Dictionary<ProcessInstance, List<InstanceDetailWidget>>();
        /// <summary>
        /// Mapping jeder Instanz mit ihren zugehörigen InstanceMiniWidgets, da es bei Kategorieview ja mehr als eins geben kann
        /// </summary>
        private Dictionary<ProcessInstance, List<InstanceMiniWidget>> MapInstanceToMini
            = new Dictionary<ProcessInstance, List<InstanceMiniWidget>>();

        /// <summary>
        /// Liste aller InstanceDetailWidgets
        /// </summary>
        private List<InstanceDetailWidget> MaxiInstanceControlList = new List<InstanceDetailWidget>();
        /// <summary>
        /// Liste aller InstanceMiniWidgets
        /// </summary>
        private List<InstanceMiniWidget> MiniInstanceControlList = new List<InstanceMiniWidget>();

        // private InstanceDetailWidget 

        private List<ProcessInstance> _runningInstances;
        /// <summary>
        /// Liste aller laufenden Instanzen (dazu zählen auch pausierte, etc)
        /// </summary>
        public List<ProcessInstance> RunningInstances
        {
            get { return _runningInstances; }
            set { _runningInstances = value; }
        }

        private double _showMargin;
        /// <summary>
        /// Margin links wenn Bereich eingeblendet.
        /// </summary>
        public double ShowMargin
        { get { return _showMargin; } }

        private double _hideMargin;
        /// <summary>
        /// Margin links wenn Bereich ausgeblendet.
        /// </summary>
        public double HideMargin
        { get { return _hideMargin; } }

        private RunningInstancesArea _boundArea = new RunningInstancesArea();
        /// <summary>
        /// zugehöriger Bereichsrahmen
        /// </summary>
        public RunningInstancesArea BoundArea
        {
            get { return _boundArea; }
            set { _boundArea = value; }
        }

        public delegate void SendInstanceEventHandler(object sender, InstanceEventArgs e);
        public event SendInstanceEventHandler ShowInstanceEvent;

        public delegate void SendFaultyInstanceEventHandler(object sender, InstanceEventArgs e);
        public event SendFaultyInstanceEventHandler FaultyInstanceClickedEvent;
        #endregion        
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        public RunningInstancesControl()
		{
            this.InitializeComponent();
            AddInstanceControl();
		}

        #region Initialization
        /// <summary>
        /// Lädt Startansicht mit aktivem State-Tab
        /// </summary>
        /// <param name="instanceList">Liste aller zu Beginn laufenden Instanzen</param>
        public void LoadBaseUI(List<ProcessInstance> instanceList, bool loadInstanceStartScene)
        {
            RunningInstances = instanceList;
            CurrentGroup = 1;
            Section1Text.Text = "executing Instances";
            Section2Text.Text = "paused Instances";
            Section3Text.Text = "waiting Instances";
            Section4Text.Text = "faulty Instances";

            if (loadInstanceStartScene) LoadInstances_StateGrouping(instanceList);

            UpdateGroupContentAmount();
            UpdateToggleButtons();
        }

        /// <summary>
        /// Lädt Instanzen aus Liste bei aktiviertem State-Tab
        /// </summary>
        /// <param name="instanceList">Liste aller zu Beginn laufenden Instanzen</param>
        private void LoadInstances_StateGrouping(List<ProcessInstance> instanceList)
        {
            #region GenerateValuesForStart

            ProcessInstance instance = instanceList[0];
            InstanceDetailWidget ioc = new InstanceDetailWidget(instance, 67, "1hr 41min", "0hr 2min", "today, 10:54");
            InstanceMiniWidget iocmini = new InstanceMiniWidget(instance, 67);
            StoreInstanceControls_StateGrouping(instance, ioc, iocmini);
            AddToStackPanel_StateGrouping(ioc, iocmini);

            ProcessInstance instance1 = instanceList[1];
            InstanceDetailWidget ioc1 = new InstanceDetailWidget(instance1, 93, "1hr 27min", "0hr 12min", "today, 11:08");
            InstanceMiniWidget iocmini1 = new InstanceMiniWidget(instance1, 93);
            StoreInstanceControls_StateGrouping(instance1, ioc1, iocmini1);
            AddToStackPanel_StateGrouping(ioc1, iocmini1);

            ProcessInstance instance2 = instanceList[3];
            InstanceDetailWidget ioc2 = new InstanceDetailWidget(instance2, 52, "0hr 54min", "0hr 23min", "today, 11:41");
            InstanceMiniWidget iocmini2 = new InstanceMiniWidget(instance2, 52);
            StoreInstanceControls_StateGrouping(instance2, ioc2, iocmini2);
            AddToStackPanel_StateGrouping(ioc2, iocmini2);

            ProcessInstance instance3 = instanceList[2];
            InstanceDetailWidget ioc3 = new InstanceDetailWidget(instance3, 13, "0hr 51min", "0hr 05min", "today, 11:44");
            InstanceMiniWidget iocmini3 = new InstanceMiniWidget(instance3, 13);
            StoreInstanceControls_StateGrouping(instance3, ioc3, iocmini3);
            AddToStackPanel_StateGrouping(ioc3, iocmini3);

            #endregion
            RegisterEvents();
        }
        
        /// <summary>
        /// Events für Widgets registrieren.
        /// </summary>
        private void RegisterEvents()
        {
            foreach (InstanceDetailWidget ioc in MaxiInstanceControlList)
            {
                ioc.RootButton.Click += new RoutedEventHandler(RootButtonOverview_Click);
                ioc.ChangeStateButton.Click += new RoutedEventHandler(Detail_ChangeStateButton_Click);
                ioc.ShowInfosButton.Click += new RoutedEventHandler(Detail_ShowInfosButton_Click);
            }
            foreach (InstanceMiniWidget iocmini in MiniInstanceControlList)
            {
                iocmini.RootButton.Click += new RoutedEventHandler(RootButtonMini_Click);
                iocmini.ChangeStateButton.Click += new RoutedEventHandler(Mini_ChangeStateButton_Click);
                iocmini.ShowInfosButton.Click += new RoutedEventHandler(Mini_ShowInfosButton_Click);
            }
        }

        /// <summary>
        /// InstanceControlWidget erzeugen.
        /// </summary>
        private void AddInstanceControl()
        {
            _attachedControl = new InstanceControlWidget();
            _attachedControl.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            _attachedControl.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            //_attachedControl.Margin = new Thickness(-300, -250, 0, 0);
            _attachedControl.Visibility = System.Windows.Visibility.Hidden;
            //_attachedControl.AttachedMenuOptionSelected += new RoutedEventHandler(_attachedControl_AttachedMenuOptionSelected);
            Grid.SetColumnSpan(_attachedControl, 2);
            Grid.SetRowSpan(_attachedControl, 2);
            MainGrid.Children.Add(_attachedControl);
            //_attachedControl.ControlSelected += new RoutedEventHandler(_attachedControl_ControlSelected);
            _attachedControl.ControlInstanceChosenOption += new InstanceControlWidget.OptionChosenEventHandler(_attachedControl_ControlInstanceChosenOption);
        }

        #endregion

        #region Methods

        #region Content Updates

        /// <summary>
        /// Aktualisiere look der TogglebUttons je nach aktiver Mini oder detail-View
        /// </summary>
        public void UpdateToggleButtons()
        {
            if (CurrentGroup == 0)
            {
                Section1MiniText.IsEnabled = MiniView_Category[0];
                Section1DetailText.IsEnabled = !MiniView_Category[0];
                Section2MiniText.IsEnabled = MiniView_Category[1];
                Section2DetailText.IsEnabled = !MiniView_Category[1];
                Section3MiniText.IsEnabled = MiniView_Category[2];
                Section3DetailText.IsEnabled = !MiniView_Category[2];
                Section4MiniText.IsEnabled = MiniView_Category[3];
                Section4DetailText.IsEnabled = !MiniView_Category[3];

                if (MiniView_Category[0]) ToggleButton1.Background = (LinearGradientBrush)FindResource("MiniBrush");
                else ToggleButton1.Background = (LinearGradientBrush)FindResource("DetailBrush");
                if (MiniView_Category[1]) ToggleButton2.Background = (LinearGradientBrush)FindResource("MiniBrush");
                else ToggleButton2.Background = (LinearGradientBrush)FindResource("DetailBrush");
                if (MiniView_Category[2]) ToggleButton3.Background = (LinearGradientBrush)FindResource("MiniBrush");
                else ToggleButton3.Background = (LinearGradientBrush)FindResource("DetailBrush");
                if (MiniView_Category[3]) ToggleButton4.Background = (LinearGradientBrush)FindResource("MiniBrush");
                else ToggleButton4.Background = (LinearGradientBrush)FindResource("DetailBrush");
            }
            else if (CurrentGroup == 1)
            {
                Section1MiniText.IsEnabled = MiniView_State[0];
                Section1DetailText.IsEnabled = !MiniView_State[0];
                Section2MiniText.IsEnabled = MiniView_State[1];
                Section2DetailText.IsEnabled = !MiniView_State[1];
                Section3MiniText.IsEnabled = MiniView_State[2];
                Section3DetailText.IsEnabled = !MiniView_State[2];
                Section4MiniText.IsEnabled = MiniView_State[3];
                Section4DetailText.IsEnabled = !MiniView_State[3];

                if (MiniView_State[0]) ToggleButton1.Background = (LinearGradientBrush)FindResource("MiniBrush");
                else ToggleButton1.Background = (LinearGradientBrush)FindResource("DetailBrush");
                if (MiniView_State[1]) ToggleButton2.Background = (LinearGradientBrush)FindResource("MiniBrush");
                else ToggleButton2.Background = (LinearGradientBrush)FindResource("DetailBrush");
                if (MiniView_State[2]) ToggleButton3.Background = (LinearGradientBrush)FindResource("MiniBrush");
                else ToggleButton3.Background = (LinearGradientBrush)FindResource("DetailBrush");
                if (MiniView_State[3]) ToggleButton4.Background = (LinearGradientBrush)FindResource("MiniBrush");
                else ToggleButton4.Background = (LinearGradientBrush)FindResource("DetailBrush");
            }
        }

        /// <summary>
        /// Aktualisiere Anzahl enthaltener Instanzen
        /// </summary>
        public void UpdateGroupContentAmount()
        {
            Section1AmountText.Text = GroupStackPanel1.Children.Count + " Instances";
            Section2AmountText.Text = GroupStackPanel2.Children.Count + " Instances";
            Section3AmountText.Text = GroupStackPanel3.Children.Count + " Instances";
            Section4AmountText.Text = GroupStackPanel4.Children.Count + " Instances";
        }

        /// <summary>
        /// Füge neue Instanz hinzu.
        /// </summary>
        /// <param name="instance">neue Instanz</param>
        public void AddInstance(ProcessInstance instance)
        {
            switch (CurrentGroup)
            {
                case 0:
                    AddInstance_CategoryGrouping(instance);
                    break;
                case 1:
                    InstanceDetailWidget ioc = new InstanceDetailWidget(instance);
                    ioc.RootButton.Click += new RoutedEventHandler(RootButtonOverview_Click);
                    ioc.ChangeStateButton.Click += new RoutedEventHandler(Detail_ChangeStateButton_Click);
                    ioc.ShowInfosButton.Click += new RoutedEventHandler(Detail_ShowInfosButton_Click);
                    ioc.UpdateState();
                    InstanceMiniWidget iocmini = new InstanceMiniWidget(instance);
                    iocmini.RootButton.Click += new RoutedEventHandler(RootButtonMini_Click);
                    iocmini.ChangeStateButton.Click += new RoutedEventHandler(Mini_ChangeStateButton_Click);
                    iocmini.ShowInfosButton.Click += new RoutedEventHandler(Mini_ShowInfosButton_Click);
                    StoreInstanceControls_StateGrouping(instance, ioc, iocmini);
                    AddToStackPanel_StateGrouping(ioc, iocmini);
                    iocmini.UpdateState();
                    CheckForRelatives(ioc);
                    CheckForRelatives(iocmini);
                    break;
            }

            UpdateGroupContentAmount();
        }

        /// <summary>
        /// Speicher Widgets einer Isntanz in zugehörigen Listen / Dictionaries.
        /// </summary>
        /// <param name="instance">betreffende Instanz</param>
        /// <param name="ioc">zugehöriges InstanceDetailWidget</param>
        /// <param name="iocmini">zugehöriges InstanceMiniWidget</param>
        private void StoreInstanceControls_StateGrouping(ProcessInstance instance, InstanceDetailWidget ioc, InstanceMiniWidget iocmini)
        {
            MaxiInstanceControlList.Add(ioc);
            MiniInstanceControlList.Add(iocmini);
            MapInstanceToOverview.Add(instance, new List<InstanceDetailWidget> { ioc });
            MapInstanceToMini.Add(instance, new List<InstanceMiniWidget> { iocmini });
        }

        /// <summary>
        /// Füge Widget hinzu wenn State-Tab aktiv.
        /// </summary>
        /// <param name="ioc">evtl hinzuzufügendes DetailWidget</param>
        /// <param name="iocmini">evtl hinzuzufügendes MiniWidget</param>
        private void AddToStackPanel_StateGrouping(InstanceDetailWidget ioc, InstanceMiniWidget iocmini)
        {
            if (ioc.BoundInstance.State.Equals(Enums.ProcessState.executing))
            {
                if (MiniView_State[0]) GroupStackPanel1.Children.Add(iocmini);
                else GroupStackPanel1.Children.Add(ioc);
            }
            else if (ioc.BoundInstance.State.Equals(Enums.ProcessState.paused))
            {
                if (MiniView_State[1]) GroupStackPanel2.Children.Add(iocmini);
                else GroupStackPanel2.Children.Add(ioc);
            }
            else if (ioc.BoundInstance.State.Equals(Enums.ProcessState.waiting))
            {
                if (MiniView_State[2]) GroupStackPanel3.Children.Add(iocmini);
                else GroupStackPanel3.Children.Add(ioc);
            }
            else if (ioc.BoundInstance.State.Equals(Enums.ProcessState.faulty))
            {
                if (MiniView_State[3]) GroupStackPanel4.Children.Add(iocmini);
                else GroupStackPanel4.Children.Add(ioc);
            }
        }

        /// <summary>
        /// bei Statusänderungen eienr Instanz verschiebe sie in richtigen Widgetscrollviewer
        /// </summary>
        /// <param name="iocList">Liste aller betreffenden DetailWidgets</param>
        /// <param name="iocMiniList">Liste aller betreffenden MiniWidgets</param>
        private void MoveIocToRightStackPanel_StateGrouping(List<InstanceDetailWidget> iocList, List<InstanceMiniWidget> iocMiniList)
        {
            // Delete ioc or iocmini from previous StackPanel first
            foreach (InstanceMiniWidget iocmini in iocMiniList)
            {
                if (GroupStackPanel1.Children.Contains(iocmini) && MiniView_State[0]) GroupStackPanel1.Children.Remove(iocmini);
                if (GroupStackPanel2.Children.Contains(iocmini) && MiniView_State[1]) GroupStackPanel2.Children.Remove(iocmini);
                if (GroupStackPanel3.Children.Contains(iocmini) && MiniView_State[2]) GroupStackPanel3.Children.Remove(iocmini);
                if (GroupStackPanel4.Children.Contains(iocmini) && MiniView_State[3]) GroupStackPanel4.Children.Remove(iocmini);
            }
            foreach (InstanceDetailWidget ioc in iocList)
            {
                if (GroupStackPanel1.Children.Contains(ioc) && !MiniView_State[0]) GroupStackPanel1.Children.Remove(ioc);
                if (GroupStackPanel2.Children.Contains(ioc) && !MiniView_State[1]) GroupStackPanel2.Children.Remove(ioc);
                if (GroupStackPanel3.Children.Contains(ioc) && !MiniView_State[2]) GroupStackPanel3.Children.Remove(ioc);
                if (GroupStackPanel4.Children.Contains(ioc) && !MiniView_State[3]) GroupStackPanel4.Children.Remove(ioc);
            }

            // Now add ioc or iocmini to new StackPanel
            for (int i = 0; i < iocList.Count; i++)
            {
                AddToStackPanel_StateGrouping(iocList[i], iocMiniList[i]);
            }
        }

        /// <summary>
        /// Aktualisiere Daten von Widgets
        /// </summary>
        /// <param name="instance">betreffende Instanz</param>
        public void UpdateIOCs(ProcessInstance instance)
        {
            // Enable this again
            InstancesSV.IsEnabled = true;
            GroupGrid.IsEnabled = true;
            List<InstanceDetailWidget> updatedIOCs = new List<InstanceDetailWidget>();
            List<InstanceMiniWidget> updatedMiniIOCs = new List<InstanceMiniWidget>();
                        
            foreach (InstanceDetailWidget iocToUpdate in MapInstanceToOverview[instance])
            {
                iocToUpdate.UpdateState();
                updatedIOCs.Add(iocToUpdate);
            }
            foreach (InstanceMiniWidget iocMiniToUpdate in MapInstanceToMini[instance])
            {
                iocMiniToUpdate.UpdateState();
                updatedMiniIOCs.Add(iocMiniToUpdate);
            }

            if (CurrentGroup == 1)
                MoveIocToRightStackPanel_StateGrouping(updatedIOCs, updatedMiniIOCs);

            AdaptWrapPanelWidth();
            Cat2SV.Width = Cat2SV.Width;
            InstancesSV.Width = InstancesSV.Width;
            UpdateGroupContentAmount();
        }

        /// <summary>
        /// Lösche beendete Instanz aus Übersicht.
        /// </summary>
        /// <param name="instance">zu löschende Instanz</param>
        public void DeleteInstanceIOCs(ProcessInstance instance)
        {
            // Enable this again
            InstancesSV.IsEnabled = true;
            GroupGrid.IsEnabled = true;

            List<InstanceDetailWidget> updatedIOCs = new List<InstanceDetailWidget>();
            List<InstanceMiniWidget> updatedMiniIOCs = new List<InstanceMiniWidget>();
            foreach (InstanceDetailWidget iocToUpdate in MapInstanceToOverview[instance])
            {
                MaxiInstanceControlList.Remove(iocToUpdate);
                updatedIOCs.Add(iocToUpdate);
            }
            foreach (InstanceMiniWidget iocMiniToUpdate in MapInstanceToMini[instance])
            {
                MiniInstanceControlList.Remove(iocMiniToUpdate);
                updatedMiniIOCs.Add(iocMiniToUpdate);
            }

            foreach (InstanceMiniWidget iocmini in updatedMiniIOCs)
            {
                if (GroupStackPanel1.Children.Contains(iocmini)) GroupStackPanel1.Children.Remove(iocmini);
                if (GroupStackPanel2.Children.Contains(iocmini)) GroupStackPanel2.Children.Remove(iocmini);
                if (GroupStackPanel3.Children.Contains(iocmini)) GroupStackPanel3.Children.Remove(iocmini);
                if (GroupStackPanel4.Children.Contains(iocmini)) GroupStackPanel4.Children.Remove(iocmini);
            }
            foreach (InstanceDetailWidget ioc in updatedIOCs)
            {
                if (GroupStackPanel1.Children.Contains(ioc)) GroupStackPanel1.Children.Remove(ioc);
                if (GroupStackPanel2.Children.Contains(ioc)) GroupStackPanel2.Children.Remove(ioc);
                if (GroupStackPanel3.Children.Contains(ioc)) GroupStackPanel3.Children.Remove(ioc);
                if (GroupStackPanel4.Children.Contains(ioc)) GroupStackPanel4.Children.Remove(ioc);
            }

            MapInstanceToOverview.Remove(instance);
            MapInstanceToMini.Remove(instance);

            //if (CurrentGroup == 1)
            //    MoveIocToRightStackPanel_StateGrouping(updatedIOCs, updatedMiniIOCs);

            AdaptWrapPanelWidth();
            Cat2SV.Width = Cat2SV.Width;
            InstancesSV.Width = InstancesSV.Width;
            UpdateGroupContentAmount();
        }

        /// <summary>
        /// Füge Instanz hinzu bei aktivem Kategorie-Tab.
        /// </summary>
        /// <param name="instance">neue Instanz</param>
        private void AddInstance_CategoryGrouping(ProcessInstance instance)
        {
            MapInstanceToOverview.Add(instance, new List<InstanceDetailWidget>());
            MapInstanceToMini.Add(instance, new List<InstanceMiniWidget>());
            List<Enums.Category> categories = instance.Categories;                     
            foreach (Enums.Category c in categories)
            {
                InstanceDetailWidget ioc = new InstanceDetailWidget(instance);
                ioc.RootButton.Click += new RoutedEventHandler(RootButtonOverview_Click);
                ioc.ChangeStateButton.Click += new RoutedEventHandler(Detail_ChangeStateButton_Click);
                ioc.ShowInfosButton.Click += new RoutedEventHandler(Detail_ShowInfosButton_Click);
                ioc.UpdateState();
                InstanceMiniWidget iocmini = new InstanceMiniWidget(instance);
                iocmini.RootButton.Click += new RoutedEventHandler(RootButtonMini_Click);
                iocmini.ChangeStateButton.Click += new RoutedEventHandler(Mini_ChangeStateButton_Click);
                iocmini.ShowInfosButton.Click += new RoutedEventHandler(Mini_ShowInfosButton_Click);
                MaxiInstanceControlList.Add(ioc);
                MiniInstanceControlList.Add(iocmini);
                iocmini.UpdateState();
                MapInstanceToOverview[instance].Add(ioc);
                MapInstanceToMini[instance].Add(iocmini);
                switch ((int)c)
                {
                    case 1:
                        if (MiniView_Category[0])
                            GroupStackPanel1.Children.Add(iocmini);
                        else GroupStackPanel1.Children.Add(ioc);
                        break;
                    case 2:
                        if (MiniView_Category[1])
                            GroupStackPanel2.Children.Add(iocmini);
                        else GroupStackPanel2.Children.Add(ioc);
                        break;
                    case 3:
                        if (MiniView_Category[2])
                            GroupStackPanel3.Children.Add(iocmini);
                        else GroupStackPanel3.Children.Add(ioc);
                        break;
                    case 4:
                        if (MiniView_Category[3])
                            GroupStackPanel4.Children.Add(iocmini);
                        else GroupStackPanel4.Children.Add(ioc);
                        break;
                }
                CheckForRelatives(ioc);
                CheckForRelatives(iocmini);
            }
        }

        /// <summary>
        /// Suche bei neu erzeugtem DetailWidget nach markierten Verwandten (modellidentische Instanzen), damit neues ebenfalls markiert werden kann
        /// </summary>
        /// <param name="newIOC">neues DetailWidget</param>
        private void CheckForRelatives(InstanceDetailWidget newIOC)
        {
            foreach (InstanceDetailWidget ioc in MaxiInstanceControlList)
            {
                if (ioc.BoundInstance.BoundModel.Equals(newIOC.BoundInstance.BoundModel) && (ioc.IsExpanded || ioc.IsRelative))
                {
                    newIOC.FromNormalToRelative();
                    return;
                }
            }
        }

        /// <summary>
        /// Suche bei neu erzeugtem MiniWidget nach markierten Verwandten (modellidentische Instanzen), damit neues ebenfalls markiert werden kann
        /// </summary>
        /// <param name="newIMC">neues MiniWidget</param>
        private void CheckForRelatives(InstanceMiniWidget newIMC)
        {
            foreach (InstanceMiniWidget imc in MiniInstanceControlList)
            {
                if (imc.BoundInstance.BoundModel.Equals(newIMC.BoundInstance.BoundModel) && (imc.IsExpanded || imc.IsRelative))
                {
                    newIMC.FromNormalToRelative();
                    return;
                }
            }
        }

        /// <summary>
        /// Berechnet Vorschau-Tab anhand aktueller Fingerposition.
        /// </summary>
        /// <param name="touchPoint">Position Finger</param>
        private void UpdateGroupPreview(Point touchPoint)
        {
            double cellHeight = GroupGrid.Height / 5;
            if (touchPoint.Y >= 0 * cellHeight && touchPoint.Y < 1 * cellHeight) PreviewGroup = -1;
            if (touchPoint.Y >= 1 * cellHeight && touchPoint.Y < 2 * cellHeight) PreviewGroup = 0;
            if (touchPoint.Y >= 2 * cellHeight && touchPoint.Y < 3 * cellHeight) PreviewGroup = 1;
            if (touchPoint.Y >= 3 * cellHeight && touchPoint.Y < 4 * cellHeight) PreviewGroup = 2;
            if (touchPoint.Y >= 4 * cellHeight && touchPoint.Y < 5 * cellHeight) PreviewGroup = 3;

            if (PreviewGroup == -1 || CurrentGroup == -1) AllRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColorAll"));
            else AllRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColorFadeAll"));
            if (PreviewGroup == 0 || CurrentGroup == 0) CategoryRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColor0"));
            else CategoryRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColorFade0"));
            if (PreviewGroup == 1 || CurrentGroup == 1) StateRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColor1"));
            else StateRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColorFade1"));
            if (PreviewGroup == 2 || CurrentGroup == 2) ProcessTypeRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColor2"));
            else ProcessTypeRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColorFade2"));
            if (PreviewGroup == 3 || CurrentGroup == 3) StartTimeRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColor3"));
            else StartTimeRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColorFade3"));
        }

        /// <summary>
        /// Schaltet Inhalt um entsprechend neuem aktiven Tab.
        /// </summary>
        /// <param name="newGroup"></param>
        private void SwitchGroup(int newGroup)
        {
            //InstancesSV

            if (PreviewGroup == -2) return;

            switch (CurrentGroup)
            {
                case -1:
                    AllRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColorFadeAll"));
                    CreateReduceStoryBoard(AllRectangle, AllText);
                    break;
                case 0:
                    CategoryRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColorFade" + CurrentGroup));
                    CreateReduceStoryBoard(CategoryRectangle, CategoryText);
                    break;
                case 1:
                    StateRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColorFade" + CurrentGroup));
                    CreateReduceStoryBoard(StateRectangle, StateText);
                    break;
                case 2:
                    ProcessTypeRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColorFade" + CurrentGroup));
                    CreateReduceStoryBoard(ProcessTypeRectangle, ProcessTypeText);
                    break;
                case 3:
                    StartTimeRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceColorFade" + CurrentGroup));
                    CreateReduceStoryBoard(StartTimeRectangle, StartTimeText);
                    break;
            }

            ClearStackPanels();

            switch (newGroup)
            {
                case -1:
                    DeleteUnnecessaryCategoryInstanceControls();
                    CreateExpandStoryBoard(AllRectangle, AllText);
                    break;
                case 0:
                    CreateExpandStoryBoard(CategoryRectangle, CategoryText);
                    FillStackPanels_CategoryGrouping();
                    Section1Text.Text = Enums.Category.Safety.ToString();
                    Section2Text.Text = Enums.Category.Housekeeping.ToString();
                    Section3Text.Text = Enums.Category.Convenience.ToString();
                    Section4Text.Text = Enums.Category.Entertainment.ToString();
                    break;
                case 1:
                    CreateExpandStoryBoard(StateRectangle, StateText);
                    DeleteUnnecessaryCategoryInstanceControls();
                    FillStackPanels_StateGrouping();
                    Section1Text.Text = "executing Instances";
                    Section2Text.Text = "paused Instances";
                    Section3Text.Text = "waiting Instances";
                    Section4Text.Text = "faulty Instances";
                    break;
                case 2:
                    DeleteUnnecessaryCategoryInstanceControls();
                    CreateExpandStoryBoard(ProcessTypeRectangle, ProcessTypeText);
                    Section1Text.Text = "Safety-Process";
                    Section2Text.Text = "Climate-Process";
                    Section3Text.Text = "Bring-Process";
                    Section4Text.Text = "Activate-Process";
                    break;
                case 3:
                    DeleteUnnecessaryCategoryInstanceControls();
                    CreateExpandStoryBoard(StartTimeRectangle, StartTimeText);
                    break;
            }

            if (newGroup != -1) InstancesSV.Background = (LinearGradientBrush)FindResource("InstanceBrush" + newGroup);
            else InstancesSV.Background = (LinearGradientBrush)FindResource("InstanceBrushAll");

            CurrentGroup = newGroup;
            PreviewGroup = -2;
            AdaptWrapPanelWidth();
            UpdateGroupContentAmount();
            UpdateToggleButtons();
        }

        public void ReactOnFaultyInstance(ProcessInstance instance)
        {            
            UpdateIOCs(instance);
        }

        public void ReactOnErrorSolved(ProcessInstance instance)
        {

            foreach (InstanceDetailWidget iocToUpdate in MapInstanceToOverview[instance])
            {
                iocToUpdate.RemoveErrorLook();
            }
            foreach (InstanceMiniWidget iocMiniToUpdate in MapInstanceToMini[instance])
            {
                iocMiniToUpdate.RemoveErrorLook();
            }
            UpdateIOCs(instance);
        }

        /// <summary>
        /// Löscht alle Inhalte der WidgetStackPanels, damit danach neu einsortiert werden kann entsprechend neuem Tab.
        /// </summary>
        private void ClearStackPanels()
        {
            GroupStackPanel1.Children.Clear();
            GroupStackPanel2.Children.Clear();
            GroupStackPanel3.Children.Clear();
            GroupStackPanel4.Children.Clear();
        }

        /// <summary>
        /// Löscht überflüssige InstanceWidgets bei Wechsel von kategroie-Tab zu anderem.
        /// </summary>
        private void DeleteUnnecessaryCategoryInstanceControls()
        {
            foreach (ProcessInstance instance in RunningInstances)
            {
                int iocs = MapInstanceToOverview[instance].Count;
                for (int i = iocs - 1; i > 0; i--)
                {
                    MapInstanceToOverview[instance].RemoveAt(i);
                }

                int minis = MapInstanceToMini[instance].Count;
                for (int j = minis - 1; j > 0; j--)
                {
                    MapInstanceToMini[instance].RemoveAt(j);
                }
            }
        }

        /// <summary>
        /// Füge Widgets hinzu bei aktivem State-Tab.
        /// </summary>
        private void FillStackPanels_StateGrouping()
        {
            foreach (ProcessInstance instance in RunningInstances)
            {
                try
                {
                    AddToStackPanel_StateGrouping(MapInstanceToOverview[instance][0], MapInstanceToMini[instance][0]);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.ToString() + " when adding instance " +  instance.Name + " zu StackpanelStateGrouping");
                }
            }
        }

        /// <summary>
        /// Füge Widgets hinzu bei aktivem Kategroie-Tab.
        /// </summary>
        private void FillStackPanels_CategoryGrouping()
        {
            foreach (ProcessInstance instance in RunningInstances)
            {
                List<Enums.Category> categories = instance.Categories;

                for (int i = 0; i < categories.Count; i++)
                {
                    if (i != 0)
                    {
                        InstanceDetailWidget forDuplicatingInfos = MapInstanceToOverview[instance][0];
                        InstanceDetailWidget ioc = new InstanceDetailWidget(instance, forDuplicatingInfos.ProgressBar.Value,
                            forDuplicatingInfos.InstanceDurationText.Text, forDuplicatingInfos.StepDurationText.Text,
                            forDuplicatingInfos.StartTimeText.Text);
                        ioc.RootButton.Click += new RoutedEventHandler(RootButtonOverview_Click);
                        ioc.ChangeStateButton.Click += new RoutedEventHandler(Detail_ChangeStateButton_Click);
                        ioc.ShowInfosButton.Click += new RoutedEventHandler(Detail_ShowInfosButton_Click);
                        InstanceMiniWidget iocmini = new InstanceMiniWidget(instance, forDuplicatingInfos.ProgressBar.Value);
                        iocmini.RootButton.Click += new RoutedEventHandler(RootButtonMini_Click);
                        iocmini.ChangeStateButton.Click += new RoutedEventHandler(Mini_ChangeStateButton_Click);
                        iocmini.ShowInfosButton.Click += new RoutedEventHandler(Mini_ShowInfosButton_Click);
                        MaxiInstanceControlList.Add(ioc);
                        MiniInstanceControlList.Add(iocmini);
                        MapInstanceToOverview[instance].Add(ioc);
                        MapInstanceToMini[instance].Add(iocmini);
                    }

                    if (categories[i].Equals(Enums.Category.Safety))
                    {
                        if (MiniView_Category[0])
                            GroupStackPanel1.Children.Add(MapInstanceToMini[instance][i]);
                        else GroupStackPanel1.Children.Add(MapInstanceToOverview[instance][i]);
                    }
                    else if (categories[i].Equals(Enums.Category.Housekeeping))
                    {
                        if (MiniView_Category[1])
                            GroupStackPanel2.Children.Add(MapInstanceToMini[instance][i]);
                        else GroupStackPanel2.Children.Add(MapInstanceToOverview[instance][i]);
                    }
                    else if (categories[i].Equals(Enums.Category.Convenience))
                    {
                        if (MiniView_Category[2])
                            GroupStackPanel3.Children.Add(MapInstanceToMini[instance][i]);
                        else GroupStackPanel3.Children.Add(MapInstanceToOverview[instance][i]);
                    }
                    else if (categories[i].Equals(Enums.Category.Entertainment))
                    {
                        if (MiniView_Category[3])
                            GroupStackPanel4.Children.Add(MapInstanceToMini[instance][i]);
                        else GroupStackPanel4.Children.Add(MapInstanceToOverview[instance][i]);
                    }
                }
            }
        }

        /// <summary>
        /// Passe Breite des WrapPanels an, je nach Tab und Mini- oder detailview (Detailview: horizontal scrollen, Mini ohne Scrollen rasterförmig anzeigen)
        /// </summary>
        public void AdaptWrapPanelWidth()
        {
            if (CurrentGroup == 0)
            {
                if (MiniView_Category[0]) GroupStackPanel1.Width = Section1Rectangle.ActualWidth;
                else GroupStackPanel1.Width = Double.NaN;
                if (MiniView_Category[1]) GroupStackPanel2.Width = Section2Rectangle.ActualWidth;
                else GroupStackPanel2.Width = Double.NaN;
                if (MiniView_Category[2]) GroupStackPanel3.Width = Section3Rectangle.ActualWidth;
                else GroupStackPanel3.Width = Double.NaN;
                if (MiniView_Category[3]) GroupStackPanel4.Width = Section4Rectangle.ActualWidth;
                else GroupStackPanel4.Width = Double.NaN;
            }
            else
            {
                if (MiniView_State[0]) GroupStackPanel1.Width = Section1Rectangle.ActualWidth;
                else GroupStackPanel1.Width = Double.NaN;
                if (MiniView_State[1]) GroupStackPanel2.Width = Section2Rectangle.ActualWidth;
                else GroupStackPanel2.Width = Double.NaN;
                if (MiniView_State[2]) GroupStackPanel3.Width = Section3Rectangle.ActualWidth;
                else GroupStackPanel3.Width = Double.NaN;
                if (MiniView_State[3]) GroupStackPanel4.Width = Section4Rectangle.ActualWidth;
                else GroupStackPanel4.Width = Double.NaN;
            }
            Cat1SV.Width = GroupStackPanel1.Width;
            Cat2SV.Width = GroupStackPanel2.Width;
            Cat3SV.Width = GroupStackPanel3.Width;
            Cat4SV.Width = GroupStackPanel4.Width;
        }

        /// <summary>
        /// Versperre Sicht auf Überblick, damit Fokus InstanceControlWidget
        /// </summary>
        public void DisableMainGridButControl()
        {
            InstancesSV.IsEnabled = false;
            GroupGrid.IsEnabled = false;
            GroupLabelText.IsEnabled = false;

            GroupLabelText.Opacity = 0.3;
            GroupGrid.Opacity = 0.3;
            InstancesSV.Opacity = 0.3;
        }

        /// <summary>
        /// Gib Sicht auf Übersciht wieder frei.
        /// </summary>
        public void EnableMainGrid()
        {
            InstancesSV.IsEnabled = true;
            GroupGrid.IsEnabled = true;
            GroupLabelText.IsEnabled = true;

            GroupLabelText.Opacity = 1;
            GroupGrid.Opacity = 1;
            InstancesSV.Opacity = 1;
        }

        #endregion

        #endregion

        #region Events

        /// <summary>
        /// Event wird aufgerufen wenn XAML-Komponenten geladen wurden.
        /// Speichert bzw berechnet Margins für Ein- und Ausblenden.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void ContentLoaded(object sender, RoutedEventArgs e)
        {
            _showMargin = this.Margin.Left;
            _hideMargin = _boundArea.HideMargin + 80;
        }

        /// <summary>
        /// Event wird aufgerufen wenn IstanceControlWidget wieder geschlossen wurde.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void _attachedControl_ControlInstanceChosenOption(object sender, OptionChosenEventArgs e)
        {
            EnableMainGrid();
        }

        /// <summary>
        /// Event wird aufgerufen bei Tap auf Toggle-Button.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void ToggleMiniDetailClick(object sender, System.Windows.RoutedEventArgs e)
        {
            int sectionNumber = 0;
            if (sender.Equals(ToggleButton1)) sectionNumber = 0;
            if (sender.Equals(ToggleButton2)) sectionNumber = 1;
            if (sender.Equals(ToggleButton3)) sectionNumber = 2;
            if (sender.Equals(ToggleButton4)) sectionNumber = 3;

            if (CurrentGroup == 0)
            {
                MiniView_Category[sectionNumber] = !MiniView_Category[sectionNumber];
                ClearStackPanels();
                FillStackPanels_CategoryGrouping();
                AdaptWrapPanelWidth();
            }
            else if (CurrentGroup == 1)
            {
                MiniView_State[sectionNumber] = !MiniView_State[sectionNumber];
                ClearStackPanels();
                FillStackPanels_StateGrouping();
                AdaptWrapPanelWidth();
            }
            UpdateToggleButtons();
        }

        /// <summary>
        /// Event wird aufgerufen bei Tap auf InstanceMiniWidget.
        /// Sucht nach Verwandten und steuert Markierungen / Expansion / Reduktion.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void RootButtonMini_Click(object sender, RoutedEventArgs e)
        {
            //foreach (InstanceMiniWidget imc in MiniInstanceControlList)
            //{
            //    if (imc.IsExpanded) imc.HideTwoTapButtons_Smooth();
            //    if (imc.RootButton.Equals(sender) && !imc.IsExpanded) imc.ShowTwoTapButtons_Smooth();
            //}

            if (((InstanceMiniWidget)((SurfaceButton)sender).Parent).BoundInstance.State.Equals(Enums.ProcessState.faulty))
            {
                FaultyInstanceClickedEvent(this, new InstanceEventArgs(((InstanceMiniWidget)((SurfaceButton)sender).Parent).BoundInstance));
                return;
            }
            

            InstanceMiniWidget senderIMC = (InstanceMiniWidget)((SurfaceButton)sender).Parent;
            foreach (InstanceMiniWidget imc in MiniInstanceControlList)
            {

                if (imc.IsRelative)
                {
                    // new imc, old imc and this one were relatives
                    if (senderIMC.BoundInstance.BoundModel.Equals(imc.BoundInstance.BoundModel))
                    {
                        // this one is the sender
                        if (imc.BoundInstance.Equals(senderIMC.BoundInstance))
                            imc.FromRelativeToTouched();
                        // the sender is old imc, so it has to become normal again, thats why relative must switch to normal too
                        if (senderIMC.IsExpanded)
                            imc.FromRelativeToNormal();
                        // else: this imc is not new or old imc, so it stays as it is as relative
                    }
                    // was relative to former imc
                    else if (!senderIMC.BoundInstance.BoundModel.Equals(imc.BoundInstance.BoundModel)) imc.FromRelativeToNormal();
                }
                else
                {
                    // is old imc
                    if (imc.IsExpanded)
                    {
                        // and this one is the sender too, so it has to become normal again
                        if (imc.BoundInstance.Equals(senderIMC.BoundInstance))
                            imc.FromTouchedToNormal();
                        // and relative to new imc
                        else if (senderIMC.BoundInstance.BoundModel.Equals(imc.BoundInstance.BoundModel))
                            imc.FromTouchedToRelative();
                        else // and not relative of new imc 
                            imc.FromTouchedToNormal();
                    }
                    // was normal before
                    else
                    {
                        // this one is the sender
                        if (imc.BoundInstance.Equals(senderIMC.BoundInstance))
                            imc.FromNormalToTouched();
                        // and is now relative to new imc
                        else if (senderIMC.BoundInstance.BoundModel.Equals(imc.BoundInstance.BoundModel))
                            imc.FromNormalToRelative();
                        // else it stays normal, so no animation needed
                    }
                }
            }

            foreach (InstanceMiniWidget imc in MiniInstanceControlList)
            {
                if (imc.IsExpanded)
                {
                    imc.HideTwoTapButtons_Smooth();
                }
                else if (imc.BoundInstance.Equals(senderIMC.BoundInstance) && !imc.IsExpanded) imc.ShowTwoTapButtons_Smooth();
            }

            foreach (InstanceDetailWidget ioc in MaxiInstanceControlList)
            {

                if (ioc.IsRelative)
                {
                    // new IOC, old IOC and this one were relatives
                    if (senderIMC.BoundInstance.BoundModel.Equals(ioc.BoundInstance.BoundModel))
                    {
                        // this one is the sender
                        if (ioc.BoundInstance.Equals(senderIMC.BoundInstance))
                            ioc.FromRelativeToTouched();
                        // the sender is old IOC, so it has to become normal again, thats why relative must switch to normal too
                        if (senderIMC.IsExpanded)
                            ioc.FromRelativeToNormal();
                        // else: this ioc is not new or old IOC, so it stays as it is as relative
                    }
                    // was relative to former IOC
                    else if (!senderIMC.BoundInstance.BoundModel.Equals(ioc.BoundInstance.BoundModel)) ioc.FromRelativeToNormal();
                }
                else
                {
                    // is old IOC
                    if (ioc.IsExpanded)
                    {
                        // and this one is the sender too, so it has to become normal again
                        if (ioc.BoundInstance.Equals(senderIMC.BoundInstance))
                            ioc.FromTouchedToNormal();
                        // and relative to new IOC
                        else if (senderIMC.BoundInstance.BoundModel.Equals(ioc.BoundInstance.BoundModel))
                            ioc.FromTouchedToRelative();
                        else // and not relative of new IOC 
                            ioc.FromTouchedToNormal();
                    }
                    // was normal before
                    else
                    {
                        // this one is the sender
                        if (ioc.BoundInstance.Equals(senderIMC.BoundInstance))
                            ioc.FromNormalToTouched();
                        // and is now relative to new IOC
                        else if (senderIMC.BoundInstance.BoundModel.Equals(ioc.BoundInstance.BoundModel))
                            ioc.FromNormalToRelative();
                        // else it stays normal, so no animation needed
                    }
                }
            }


            foreach (InstanceDetailWidget ioc in MaxiInstanceControlList)
            {
                if (ioc.IsExpanded)
                {
                    ioc.HideTwoTapButtons_Smooth();
                }
                else if (ioc.BoundInstance.Equals(senderIMC.BoundInstance) && !ioc.IsExpanded) ioc.ShowTwoTapButtons_Smooth();
            }
        }

        /// <summary>
        /// Event wird aufgerufen bei Tap auf InstanceDetailWidget.
        /// Sucht nach Verwandten und steuert Markierungen / Expansion / Reduktion.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void RootButtonOverview_Click(object sender, RoutedEventArgs e)
        {            
            InstanceDetailWidget senderIOC = (InstanceDetailWidget)((SurfaceButton)sender).Parent;


            if (senderIOC.BoundInstance.State.Equals(Enums.ProcessState.faulty))
            {
                FaultyInstanceClickedEvent(this, new InstanceEventArgs(senderIOC.BoundInstance));
                return;
            }

            foreach (InstanceDetailWidget ioc in MaxiInstanceControlList)
            {

                if (ioc.IsRelative)
                {
                    // new IOC, old IOC and this one were relatives
                    if (senderIOC.BoundInstance.BoundModel.Equals(ioc.BoundInstance.BoundModel))
                    {
                        // this one is the sender
                        if (ioc.BoundInstance.Equals(senderIOC.BoundInstance))
                            ioc.FromRelativeToTouched();
                        // the sender is old IOC, so it has to become normal again, thats why relative must switch to normal too
                        if (senderIOC.IsExpanded)
                            ioc.FromRelativeToNormal();
                        // else: this ioc is not new or old IOC, so it stays as it is as relative
                    }
                    // was relative to former IOC
                    else if (!senderIOC.BoundInstance.BoundModel.Equals(ioc.BoundInstance.BoundModel)) ioc.FromRelativeToNormal();
                }
                else
                {
                    // is old IOC
                    if (ioc.IsExpanded)
                    {
                        // and this one is the sender too, so it has to become normal again
                        if (ioc.BoundInstance.Equals(senderIOC.BoundInstance))
                            ioc.FromTouchedToNormal();
                        // and relative to new IOC
                        else if (senderIOC.BoundInstance.BoundModel.Equals(ioc.BoundInstance.BoundModel))
                            ioc.FromTouchedToRelative();
                        else // and not relative of new IOC 
                            ioc.FromTouchedToNormal();
                    }
                    // was normal before
                    else
                    {
                        // this one is the sender
                        if (ioc.BoundInstance.Equals(senderIOC.BoundInstance))
                            ioc.FromNormalToTouched();
                        // and is now relative to new IOC
                        else if (senderIOC.BoundInstance.BoundModel.Equals(ioc.BoundInstance.BoundModel))
                            ioc.FromNormalToRelative();
                        // else it stays normal, so no animation needed
                    }
                }
            }

                
            foreach (InstanceDetailWidget ioc in MaxiInstanceControlList)
            {
                if (ioc.IsExpanded)
                {
                    ioc.HideTwoTapButtons_Smooth();
                }
                else if (ioc.BoundInstance.Equals(senderIOC.BoundInstance) && !ioc.IsExpanded) ioc.ShowTwoTapButtons_Smooth();
            }

            foreach (InstanceMiniWidget imc in MiniInstanceControlList)
            {

                if (imc.IsRelative)
                {
                    // new imc, old imc and this one were relatives
                    if (senderIOC.BoundInstance.BoundModel.Equals(imc.BoundInstance.BoundModel))
                    {
                        // this one is the sender
                        if (imc.BoundInstance.Equals(senderIOC.BoundInstance))
                            imc.FromRelativeToTouched();
                        // the sender is old imc, so it has to become normal again, thats why relative must switch to normal too
                        if (senderIOC.IsExpanded)
                            imc.FromRelativeToNormal();
                        // else: this imc is not new or old imc, so it stays as it is as relative
                    }
                    // was relative to former imc
                    else if (!senderIOC.BoundInstance.BoundModel.Equals(imc.BoundInstance.BoundModel)) imc.FromRelativeToNormal();
                }
                else
                {
                    // is old imc
                    if (imc.IsExpanded)
                    {
                        // and this one is the sender too, so it has to become normal again
                        if (imc.BoundInstance.Equals(senderIOC.BoundInstance))
                            imc.FromTouchedToNormal();
                        // and relative to new imc
                        else if (senderIOC.BoundInstance.BoundModel.Equals(imc.BoundInstance.BoundModel))
                            imc.FromTouchedToRelative();
                        else // and not relative of new imc 
                            imc.FromTouchedToNormal();
                    }
                    // was normal before
                    else
                    {
                        // this one is the sender
                        if (imc.BoundInstance.Equals(senderIOC.BoundInstance))
                            imc.FromNormalToTouched();
                        // and is now relative to new imc
                        else if (senderIOC.BoundInstance.BoundModel.Equals(imc.BoundInstance.BoundModel))
                            imc.FromNormalToRelative();
                        // else it stays normal, so no animation needed
                    }
                }
            }


            foreach (InstanceMiniWidget imc in MiniInstanceControlList)
            {
                if (imc.IsExpanded)
                {
                    imc.HideTwoTapButtons_Smooth();
                }
                else if (imc.BoundInstance.Equals(senderIOC.BoundInstance) && !imc.IsExpanded) imc.ShowTwoTapButtons_Smooth();
            }
        }

        /// <summary>
        /// Event wird aufgerufen bei Tap auf Schaltfläche für InstancePropertyPanel.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void Mini_ShowInfosButton_Click(object sender, RoutedEventArgs e)
        {
            //// pass event to SurfaceWindow1.xaml.cs
            ShowInstanceEvent(this, new InstanceEventArgs( ((InstanceMiniWidget)((SurfaceButton)((Grid)((SurfaceButton)sender).Parent).Parent).Parent).BoundInstance) );
        }

        /// <summary>
        /// Event wird aufgerufen bei Tap auf Schaltfläche für InstanceControlWidget.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void Mini_ChangeStateButton_Click(object sender, RoutedEventArgs e)
        {
            DisableMainGridButControl();
            _attachedControl.BindInstance( ((InstanceMiniWidget)((SurfaceButton)((Grid)((SurfaceButton)sender).Parent).Parent).Parent).BoundInstance );
        }

        /// <summary>
        /// Event wird aufgerufen bei Tap auf Schaltfläche für InstancePropertyPanel.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void Detail_ShowInfosButton_Click(object sender, RoutedEventArgs e)
        {
            //// pass event to SurfaceWindow1.xaml.cs
            ShowInstanceEvent(this, new InstanceEventArgs(((InstanceDetailWidget)((SurfaceButton)((Grid)((SurfaceButton)sender).Parent).Parent).Parent).BoundInstance));
        }

        /// <summary>
        /// Event wird aufgerufen bei Tap auf Schaltfläche für InstanceControlWidget.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void Detail_ChangeStateButton_Click(object sender, RoutedEventArgs e)
        {
            DisableMainGridButControl();
            _attachedControl.BindInstance(((InstanceDetailWidget)((SurfaceButton)((Grid)((SurfaceButton)sender).Parent).Parent).Parent).BoundInstance);
        }
        
        /// <summary>
        /// Event wird aufgerufen bei TouchDown auf Tab.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void GroupTouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            // Capture to the ScaleButton.  
            e.TouchDevice.Capture(this.GroupGrid);

            // Remember this contact if a contact has not been remembered already.  
            // This contact is then used to move the ellipse around.
            if (GroupTouchDevice == null)
            {
                GroupTouchDevice = e.TouchDevice;
                UpdateGroupPreview(GroupTouchDevice.GetTouchPoint(this.GroupGrid).Position);
            }
            // Mark this event as handled.  
            e.Handled = true;
        }

        /// <summary>
        /// Event wird aufgerufen bei TouchMove auf Tab.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void GroupTouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
            if (e.TouchDevice == GroupTouchDevice)
            {
                UpdateGroupPreview(GroupTouchDevice.GetTouchPoint(this.GroupGrid).Position);
            }
            // Mark this event as handled.  
            e.Handled = true;
        }

        /// <summary>
        /// Event wird aufgerufen bei TouchUp auf Tab.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void GroupTouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            // If this contact is the one that was remembered  
            if (e.TouchDevice == GroupTouchDevice)
            {
                // Forget about this contact.
                GroupTouchDevice = null;

                if (CurrentGroup != PreviewGroup)
                {
                    SwitchGroup(PreviewGroup);
                }
            }
            // Mark this event as handled.  
            e.Handled = true;
        }

        #endregion

        #region Animations
        /// <summary>
        /// Animiere Aktivieren eines Tabs.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="textbox"></param>
        private void CreateExpandStoryBoard(Rectangle rect, TextBox textbox)
        {
            DoubleAnimation expandAnimation = new DoubleAnimation();
            expandAnimation.From = 125;
            expandAnimation.To = 140;
            expandAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            expandAnimation.AutoReverse = false;

            ThicknessAnimation moveAnimation = new ThicknessAnimation();
            moveAnimation.Duration = TimeSpan.FromSeconds(0.5);
            moveAnimation.From = new Thickness(20, 0, 0, 10);
            moveAnimation.To = new Thickness(5, 0, 0, 10);

            Storyboard expandStoryboard = new Storyboard();
            expandStoryboard.Children.Add(expandAnimation);
            expandStoryboard.Children.Add(moveAnimation);
            Storyboard.SetTargetName(expandAnimation, rect.Name);
            Storyboard.SetTargetProperty(expandAnimation, new PropertyPath(Rectangle.WidthProperty));
            Storyboard.SetTargetName(moveAnimation, textbox.Name);
            Storyboard.SetTargetProperty(moveAnimation, new PropertyPath(TextBox.MarginProperty));

            expandStoryboard.Begin(this);
        }

        /// <summary>
        /// Animiere Deaktivieren eines Tabs.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="textbox"></param>
        private void CreateReduceStoryBoard(Rectangle rect, TextBox textbox)
        {
            DoubleAnimation reduceAnimation = new DoubleAnimation();
            reduceAnimation.From = 140;
            reduceAnimation.To = 125;
            reduceAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            reduceAnimation.AutoReverse = false;

            ThicknessAnimation moveAnimation = new ThicknessAnimation();
            moveAnimation.Duration = TimeSpan.FromSeconds(0.5);
            moveAnimation.From = new Thickness(5, 0, 0, 10);
            moveAnimation.To = new Thickness(20, 0, 0, 10);

            Storyboard reduceStoryboard = new Storyboard();
            reduceStoryboard.Children.Add(reduceAnimation);
            reduceStoryboard.Children.Add(moveAnimation);
            Storyboard.SetTargetName(reduceAnimation, rect.Name);
            Storyboard.SetTargetProperty(reduceAnimation, new PropertyPath(Rectangle.WidthProperty));
            Storyboard.SetTargetName(moveAnimation, textbox.Name);
            Storyboard.SetTargetProperty(moveAnimation, new PropertyPath(TextBox.MarginProperty));

            reduceStoryboard.Begin(this);
        }

        #endregion

        /// <summary>
        /// Klasse für eigene Eventparameterübergabe bzgl gewählter Instanz für Aufrufen InstancePropertyPanel.
        /// </summary>
        public class InstanceEventArgs : EventArgs
        {
            public readonly ProcessInstance Instance;

            public InstanceEventArgs(ProcessInstance instance)
            {
                Instance = instance;
            }
        }

	}
}