using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Xml;
using Prototype_05.ModelData;
using Prototype_05.InstanceData;
using System.IO;
using System.Windows.Media.Animation;

namespace Prototype_05
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        #region Declaration
        /// <summary>
        /// benötigt für Menüpositionierung über ModelOverview
        /// </summary>
        private Point CurrentSelectedItemPosition;
        private MenuThreeOptions ModelOverviewMenu;

        private List<ProcessModel> ModelList;
        private List<ProcessInstance> InstanceList;
        private XmlNamespaceManager NSManager;
        /// <summary>
        /// Scatterview nimmt alle ScatterViewItems auf.
        /// </summary>
        private ScatterView MainScatterView;
        private InstanceControlWidget InstanceControl;

        private bool RemoveModelPanelIsVisible = false;
        private bool AddModelPanelIsVisible = false;
        private ScatterViewItem ModelAddingSVI;
        private ScatterViewItem ModelRemoveSVI;

        /// <summary>
        /// Generiert einzigartige ID für InstancePropertyPanel damit einzigartiger Name für Animation vorhanden.
        /// </summary>
        private int UniqueInstancePropertyPanelID = 0;
        /// <summary>
        /// Generiert einzigartige ID für ModelPropertyPanel damit einzigartiger Name für Animation vorhanden.
        /// </summary>
        private int UniqueModelPropertyPanelID = 0;
        /// <summary>
        /// Generiert einzigartige ID für StartInstancePanel damit einzigartiger Name für Animation vorhanden.
        /// </summary>
        private int UniqueStartInstancePanelID = 0;
        private int UniqueErrorPanelID = 0;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();

            ModelList = new List<ProcessModel>();
            InstanceList = new List<ProcessInstance>();

            if (Properties.Settings.Default.LoadModelsFromInputPath)
            {
                LoadDefaultModels(Properties.Settings.Default.InputPath);
                InstanceList.Add(new ProcessInstance(ModelList[2]));
                ((InstanceStartDataPort)InstanceList[0].PortList[0]).DataValue = "Guestroom";
                InstanceList[0].Name = "Clean Guestroom";
                InstanceList.Add(new ProcessInstance(ModelList[4]));
                ((InstanceStartDataPort)InstanceList[1].PortList[0]).DataValue = "1";
                ((InstanceStartDataPort)InstanceList[1].PortList[1]).DataValue = "Pizza";
                ((InstanceStartDataPort)InstanceList[1].PortList[2]).DataValue = "12.45";
                InstanceList[1].Name = "Cook lunch (Pizza) for 1";
                InstanceList.Add(new ProcessInstance(ModelList[9]));
                InstanceList.Add(new ProcessInstance(ModelList[12]));
                ((InstanceStartDataPort)InstanceList[3].PortList[0]).DataValue = "60° Coloureds";
            }
            CurrentSelectedItemPosition = new Point(0, 0);
            // TouchOnModelItemCaptured = false;
            // Counter = 0;

            ModelOverviewMenu = new MenuThreeOptions(3, true);
            ModelOverviewMenu.SetOptions("Start Process", "Show more Information", "Add to Favourites", "Images/play_black.png", "Images/lupe_black.png", "Images/wherz_black.png");
            //ModelOverviewMenu.MenuOptionSelected += new RoutedEventHandler(ModelOverviewMenuOptionSelected);
            ModelOverviewMenu.HorizontalAlignment = HorizontalAlignment.Left;
            ModelOverviewMenu.VerticalAlignment = VerticalAlignment.Top;
            ModelOverviewMenu.Margin = new Thickness(403 + ModelOverview.Margin.Left, 40, 0, 0);
            ModelOverviewMenu.Visibility = System.Windows.Visibility.Hidden;
            ModelOverviewMenu.AdaptMenuColorToModelOverview();
            MainGrid.Children.Add(ModelOverviewMenu);
            ModelOverview.AttachedMenu = ModelOverviewMenu;

            ModelOverview.AddOrRemoveSelected += new RoutedEventHandler(ModelOverview_AddOrRemoveSelected);
            ModelOverview.LoadListContent(ModelList);

            InstanceControl = RunningInstancesControl.AttachedControl;
            // InstanceControlWidget.AttachedMenuOptionSelected += new RoutedEventHandler(InstanceControl_AttachedMenuOptionSelected);
            // InstanceControlWidget.ControlSelected += new RoutedEventHandler(InstanceControl_ControlSelected);
            InstanceControl.ControlInstanceChosenOption += new Prototype_05.InstanceControlWidget.OptionChosenEventHandler(InstanceControl_ControlInstanceChosenOption);
            InstanceControl.ShowInstancePanel += new RoutedEventHandler(InstanceControl_ShowInstancePanel);

            foreach (ProcessInstance instance in InstanceList)
            {
                instance.State = Enums.ProcessState.executing;
            }
            RunningInstancesControl.LoadBaseUI(InstanceList, Properties.Settings.Default.LoadModelsFromInputPath);

            MainScatterView = new ScatterView();
            Grid.SetRowSpan(MainScatterView, 2);
            Grid.SetColumnSpan(MainScatterView, 4);
            MainGrid.Children.Add(MainScatterView);

            ModelOverview.BoundArea = ModOvArea;
            //ModOvArea.
            RunningInstancesControl.BoundArea = RunningInstancesArea;
            RunningInstancesControl.FaultyInstanceClickedEvent += new Prototype_05.RunningInstancesControl.SendFaultyInstanceEventHandler(RunningInstancesControl_FaultyInstanceClickedEvent);

            RunningInstancesArea.AnimateMarginEvent += new Prototype_05.RunningInstancesArea.SendAnimationCommandEventHandler(RunningInstancesArea_AnimateMarginEvent);
            ModOvArea.AnimateMarginEvent += new ModelOverviewArea.SendAnimationCommandEventHandler(ModOvArea_AnimateMarginEvent);
            SystemStateOverviewArea.AnimateMarginEvent += new SystemOverviewArea.SendAnimationCommandEventHandler(SystemStateOverviewArea_AnimateMarginEvent);
            //SystemStateOverviewArea.ShowOrHide += new RoutedEventHandler(SystemStateOverviewArea_ShowOrHide);
            //ModOvArea.ShowOrHide += new RoutedEventHandler(ModOvArea_ShowOrHide);

            ////ModOvArea.ShowMoveEvent += new Prototype_05.RunningInstancesArea.SendShowTouchLengthEventHandler(RunningInstancesArea_ShowMoveEvent);
            //ModOvArea.ShowHideMoveEvent += new Prototype_05.ModelOverviewArea.SendTouchLengthEventHandler(ModelOverviewArea_ShowHideMoveEvent);
            //SystemStateOverviewArea.ShowHideMoveEvent += new Prototype_05.SystemOverviewArea.SendTouchLengthEventHandler(SystemStateArea_ShowHideMoveEvent);

            SystemStateOverview.BoundArea = SystemStateOverviewArea;

            ModelAddingPanel ModelAddingPanel = new ModelAddingPanel();
            ModelAddingSVI = new ScatterViewItem();
            ModelAddingSVI.Name = "AddSVI";
            this.RegisterName(ModelAddingSVI.Name, ModelAddingSVI);
            ModelAddingSVI.Width = 400;
            ModelAddingSVI.Height = 396;
            ModelAddingSVI.Orientation = 0;
            ModelAddingSVI.Style = (Style)this.FindResource("Main_SpecialSVIShape");
            ModelAddingSVI.Content = ModelAddingPanel;
            MainScatterView.Items.Add(ModelAddingSVI);
            ModelAddingPanel.ModelAddingPanelClosed += new Prototype_05.ModelAddingPanel.SendAddModelsEventHandler(ModelAddingPanel_Closed);
            ModelAddingPanel.FileNamesChosen += new Prototype_05.ModelAddingPanel.ParseFileTitleEventHandler(ModelAddingPanel_FileNamesChosen);
            AddModelPanelIsVisible = false;
            ModelAddingSVI.Visibility = System.Windows.Visibility.Hidden;

            ModelRemovingPanel ModelRemovePanel = new ModelRemovingPanel();
            ModelRemoveSVI = new ScatterViewItem();
            ModelRemoveSVI.Name = "RemoveSVI";
            this.RegisterName(ModelRemoveSVI.Name, ModelRemoveSVI);
            ModelRemoveSVI.Width = 420;
            ModelRemoveSVI.Height = 582;
            ModelRemoveSVI.Orientation = 0;
            ModelRemoveSVI.Style = (Style)this.FindResource("Main_SpecialSVIShape");
            ModelRemoveSVI.Content = ModelRemovePanel;
            MainScatterView.Items.Add(ModelRemoveSVI);
            ModelRemovePanel.ModelRemovingPanelClosed += new ModelRemovingPanel.SendDeleteModelsEventHandler(ModelRemovePanel_Closed);
            ModelRemoveSVI.Visibility = System.Windows.Visibility.Hidden;

            RunningInstancesControl.ShowInstanceEvent += new Prototype_05.RunningInstancesControl.SendInstanceEventHandler(RunningInstancesControl_ShowInstanceEvent);
            ModelOverviewMenu.OptionWasChosenEvent += new MenuThreeOptions.SendChosenOptionEventHandler(ModelOverviewMenu_OptionWasChosenEvent);
            UpdateSystemStateOverviewFacts();

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();

        }

        void RunningInstancesControl_FaultyInstanceClickedEvent(object sender, RunningInstancesControl.InstanceEventArgs e)
        {
            ErrorPanel ErrorPanel = new ErrorPanel(e.Instance);
            ErrorPanel.IdText.Text = e.Instance.Id;
            ErrorPanel.InstanceText.Text = e.Instance.Name;
            ErrorPanel.StepText.Text = e.Instance.SubstepList[0].Name;

            ScatterViewItem ErrorSVI = new ScatterViewItem();
            ErrorSVI.Name = "ErrorSVI" + UniqueErrorPanelID++;
            this.RegisterName(ErrorSVI.Name, ErrorSVI);
            //ErrorSVI.Width = 345;
            //ErrorSVI.Height = 720;
            ErrorSVI.Orientation = 0;
            ErrorSVI.Center = new Point(700, 540);
            ErrorSVI.Style = (Style)this.FindResource("Main_SpecialSVIShape");
            ErrorSVI.Content = ErrorPanel;
            MainScatterView.Items.Add(ErrorSVI);
            ErrorPanel.ErrorPanelChosenOption += new Prototype_05.ErrorPanel.OptionChosenEventHandler(ErrorPanel_ErrorPanelChosenOption);
        }

        void ErrorPanel_ErrorPanelChosenOption(object sender, ErrorPanel.OptionChosenEventArgs e)
        {
            if (!e.ChosenOption.Equals(ErrorPanel.ChosenControl.None))
            {
                e.Instance.State = Enums.ProcessState.executing;
                RunningInstancesControl.ReactOnErrorSolved(e.Instance);
                SystemStateOverview.UpdateLog("Error solved", "12:35", e.Instance.Name);
                SystemStateOverview.SwitchToLogView();
                //SystemStateOverviewArea.BackgroundBorderPath.Fill = (SolidColorBrush)FindResource("SystemStateBackgroundBrush");
            }

            ClosePanel((ScatterViewItem)((ErrorPanel)sender).Parent, true);
            UpdateSystemStateOverviewFacts();
        }
        #endregion

        #region Methoden

        private void RaiseError()
        {
            InstanceList[0].State = Enums.ProcessState.faulty;
            RunningInstancesControl.ReactOnFaultyInstance(InstanceList[0]);
            UpdateSystemStateOverviewFacts();
            SystemStateOverview.SwitchToErrorView("Device can't move", "12:35", InstanceList[0].Name);
            //SystemStateOverviewArea.BackgroundBorderPath.Fill = (SolidColorBrush)FindResource("ErrorSystemStateBackgroundBrush");
        }

        /// <summary>
        /// Kalkuliert aktuelle Instanzwerte und übermittelt sie an Systemübersicht
        /// </summary>
        private void UpdateSystemStateOverviewFacts()
        {
            int executingCounter = 0;
            int pausedWaitingCounter = 0;
            int faultyCounter = 0;
            foreach (ProcessInstance instance in InstanceList)
            {
                if (instance.State.Equals(Enums.ProcessState.executing)) executingCounter++;
                else if (instance.State.Equals(Enums.ProcessState.paused) || instance.State.Equals(Enums.ProcessState.waiting)) pausedWaitingCounter++;
                else if (instance.State.Equals(Enums.ProcessState.faulty)) faultyCounter++;
            }
            SystemStateOverview.UpdateSystemFacts(executingCounter + pausedWaitingCounter + faultyCounter, executingCounter, pausedWaitingCounter, faultyCounter);
        }

        /// <summary>
        /// Analysiert Parameter, die für Starten einer Instanz benötigt werden und generiert StartInstancePanel.
        /// </summary>
        /// <param name="model">Prozessmodell von dem Instanz erstellt werden soll</param>
        /// <param name="touchOrientation">Fingerorientierung bei Nutzereingabe</param>
        private void CheckModelParameters(ProcessModel model, double touchOrientation)
        {
            bool[] parameterToShow = new bool[] { false, false, false, false, false };
            string[] parameterTitles = new string[] { "", "", "", "", "" };
            int countParameters = 0;
            for (int i = 0; i < model.PortList.Count; i++)
            {
                if (model.PortList[i].GetType().Equals(typeof(Prototype_05.ModelData.StartDataPort)))
                {
                    int parameterPositionInEnum = (int)((StartDataPort)model.PortList[i]).Data;
                    parameterToShow[parameterPositionInEnum] = true;
                    parameterTitles[parameterPositionInEnum] = ((StartDataPort)model.PortList[i]).DataTitle;
                    countParameters++;
                }
            }
            StartInstancePanel StartInstancePanel = new StartInstancePanel(parameterToShow, parameterTitles, model, countParameters);
            ScatterViewItem StartInstanceSVI = new ScatterViewItem();
            StartInstanceSVI.Name = "StartInstanceSVI" + UniqueStartInstancePanelID++;
            this.RegisterName(StartInstanceSVI.Name, StartInstanceSVI);
            StartInstanceSVI.Width = 300;
            StartInstanceSVI.Height = StartInstancePanel.Height;
            StartInstanceSVI.Orientation = touchOrientation;
            StartInstanceSVI.Center = new Point(350, 550);
            StartInstanceSVI.Style = (Style)this.FindResource("Main_SpecialSVIShape");
            StartInstanceSVI.Content = StartInstancePanel;
            MainScatterView.Items.Add(StartInstanceSVI);
            StartInstancePanel.StartInstancePanelClosed += new Prototype_05.StartInstancePanel.ParameterChosenEventHandler(StartInstancePanel_StartInstancePanelClosed);
        }

        /// <summary>
        /// Erstellt und evtl startet eine Prozessinstanz.
        /// </summary>
        /// <param name="model">Prozessmodell, aus dem Instanz gestartet wird</param>
        /// <param name="isInstanceToBeStarted"><value>true</value> wenn nicht nur erzeugen sondern auch gleich Starten der Instanz</param>
        /// <param name="chosenParameters">Anzahl Parameter, die für Instanz spezifiziert wurden</param>
        private void CreateInstance(ProcessModel model, bool isInstanceToBeStarted, string[] chosenParameters)
        {
            ProcessInstance instance = new ProcessInstance(model);
            S_CreateInstance(instance, isInstanceToBeStarted);
            if (isInstanceToBeStarted) StartInstance(instance);
            if (chosenParameters != null)
            {
                for (int i = 0; i < instance.PortList.Count; i++)
                {
                    if (instance.PortList[i].GetType().Equals(typeof(Prototype_05.InstanceData.InstanceStartDataPort)))
                    {
                        for (int j = 0; j < chosenParameters.Length; j++)
                        {
                            if ((int)((InstanceStartDataPort)instance.PortList[i]).Data == j)
                            {
                                ((InstanceStartDataPort)instance.PortList[i]).DataValue = chosenParameters[j];
                            }
                        }
                    }
                }
                if (instance.Name == "Bring ...") instance.Name = "Bring " + chosenParameters[4] + " to " + chosenParameters[3];
                else if (instance.Name == "Cook lunch for ...") instance.Name = "Cook lunch (" + chosenParameters[4] + ") for " + chosenParameters[1];
                else if (instance.Name == "Call ...") instance.Name = "Call " + chosenParameters[4];
                else if (instance.Name == "Turn all electronic devices ...") instance.Name = "Turn all electronic devices " + chosenParameters[4];
                else if (instance.Name == "Turn all lights ...") instance.Name = "Turn all lights " + chosenParameters[4];
                else if (instance.Name == "Open windows in ...") instance.Name = "Open windows in " + chosenParameters[3];
                else if (instance.Name == "Clean Room ...") instance.Name = "Clean " + chosenParameters[3];
                else if (instance.Name == "Play Rock Music") instance.Name = "Play Rock Music in " + chosenParameters[3];
            }
            RunningInstancesControl.AddInstance(instance);
            InstanceList.Add(instance);
            //update SystemOverview-LogMessages
            if (isInstanceToBeStarted) SystemStateOverview.UpdateLog("Created and started", "12:35", instance.Name);
            else SystemStateOverview.UpdateLog("Created", "12:35", instance.Name);
            UpdateSystemStateOverviewFacts();
        }

        /// <summary>
        /// Setzt Status einer Prozessinstanz auf executing.
        /// </summary>
        /// <param name="instanceToStart"></param>
        private void StartInstance(ProcessInstance instanceToStart)
        {
            instanceToStart.State = Enums.ProcessState.executing;
        }

        /// <summary>
        /// Erzeugt ModelPropertyPanel.
        /// </summary>
        /// <param name="model">anzuzeigendes Prozessmodell</param>
        /// <param name="touchOrientation">Fingerorientierung bei Nutzereingabe</param>
        private void ShowModelDetailPanel(ProcessModel model, double touchOrientation)
        {
            ModelPropertyPanel ModelItem = new ModelPropertyPanel(model);
            ScatterViewItem ModelSVI = new ScatterViewItem();
            ModelSVI.Name = "ModelSVI" + UniqueModelPropertyPanelID++;
            this.RegisterName(ModelSVI.Name, ModelSVI);
            ModelSVI.Width = 545;
            ModelSVI.Height = 998;
            ModelSVI.Orientation = touchOrientation;
            ModelSVI.Style = (Style)this.FindResource("Main_SpecialSVIShape");
            ModelSVI.Content = ModelItem;
            MainScatterView.Items.Add(ModelSVI);
            // ModelSVI.Loaded += new RoutedEventHandler(ModelSVI_Loaded);

            ModelItem.AttachedMenuOptionSelected += new RoutedEventHandler(ModelItem_AttachedMenuOptionSelected);
            ModelItem.DoubleTapOnInstanceAmount += new Prototype_05.ModelPropertyPanel.ShowInstancePanelEventHandler(ModelItem_DoubleTapOnInstanceAmount);

            ModelItem.UpdateLayout();
            ModelItem.InvalidateVisual();
            MainScatterView.InvalidateVisual();
            ModelItem.SetParentSVIAndReduceAreas(ModelSVI);
            model.PanelList.Add(ModelItem);
        }

        /// <summary>
        /// Erzeugt InstancePropertyPanel.
        /// </summary>
        /// <param name="instance">anzuzeigende Prozessinstanz</param>
        /// <param name="touchOrientation">Fingerorientierung bei Nutzereingabe</param>
        private void ShowInstanceDetailPanel(ProcessInstance instance, double touchOrientation)
        {
            InstancePropertyPanel InstancePanel = new InstancePropertyPanel(instance);
            ScatterViewItem InstanceSVI = new ScatterViewItem();
            InstanceSVI.Name = "InstanceSVI" + UniqueInstancePropertyPanelID++;
            this.RegisterName(InstanceSVI.Name, InstanceSVI);
            InstanceSVI.Width = 665;
            InstanceSVI.Height = 1023;
            InstanceSVI.Orientation = touchOrientation;
            InstanceSVI.Style = (Style)this.FindResource("Main_SpecialSVIShape");
            InstanceSVI.Content = InstancePanel;
            MainScatterView.Items.Add(InstanceSVI);
            // ModelSVI.Loaded += new RoutedEventHandler(ModelSVI_Loaded);

            //InstancePropertyPanel.AttachedMenuOptionSelected += new RoutedEventHandler(InstancePanel_AttachedMenuOptionSelected);
            InstancePanel.OptionWasChosenEvent += new Prototype_05.InstancePropertyPanel.SendInstanceAndChosenOptionEventHandler(InstancePanel_OptionWasChosenEvent);
            InstancePanel.DoubleTapOnModelName += new Prototype_05.InstancePropertyPanel.ShowModelPanelEventHandler(InstancePanel_DoubleTapOnModelName);
            InstancePanel.DoubleTapOnCurrentStep += new Prototype_05.InstancePropertyPanel.ShowCurrentStepPanelEventHandler(InstancePanel_DoubleTapOnCurrentStep);
            InstancePanel.DoubleTapOnNextStep += new Prototype_05.InstancePropertyPanel.ShowNextStepPanelEventHandler(InstancePanel_DoubleTapOnNextStep);

            InstancePanel.UpdateLayout();
            InstancePanel.InvalidateVisual();
            MainScatterView.InvalidateVisual();
            InstancePanel.SetParentSVI(InstanceSVI);
            instance.PanelList.Add(InstancePanel);
        }

        /// <summary>
        /// Löscht bzw blendet ScatterViewItem (inkl Panel) aus.
        /// </summary>
        /// <param name="svi">betreffendes ScatterViewItem</param>
        /// <param name="deletePanel"><value>true</value> wenn ScatterViewItem gelöscht werden muss</param>
        private void ClosePanel(ScatterViewItem svi, bool deletePanel)
        {
            DoubleAnimation fadeOutAnimation = new DoubleAnimation();
            fadeOutAnimation.Duration = TimeSpan.FromSeconds(0.4);
            fadeOutAnimation.FillBehavior = FillBehavior.Stop;
            fadeOutAnimation.From = 1;
            fadeOutAnimation.To = 0;

            Storyboard fadeOutStoryboard = new Storyboard();
            fadeOutStoryboard.Children.Add(fadeOutAnimation);
            Storyboard.SetTargetName(fadeOutAnimation, svi.Name);
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath(ScatterViewItem.OpacityProperty));

            if (deletePanel) fadeOutStoryboard.Completed += delegate(System.Object o, System.EventArgs e)
            { MainScatterView.Items.Remove(svi); };

            fadeOutStoryboard.Begin(this);
            // MainScatterView.Items.Remove(svi);
        }

        /// <summary>
        /// Verwaltet Favoritenstatus, d.h. entfernt ihn wenn schon Favorit bzw umgekehrt.
        /// Aufgerufen durch Menü in SurfaceListBox.
        /// </summary>
        private void ManageFavouriteState_byModelList()
        {
            SurfaceListBox listbox = ModelOverview.CurrentListBox;
            SurfaceListBoxItem item = listbox.ItemContainerGenerator.ContainerFromItem(listbox.SelectedItem) as SurfaceListBoxItem;
            ProcessModel model = (ProcessModel)item.Content;
            if (listbox.Equals(ModelOverview.FavouritesListBox))
            {
                // Remove from Favourite-Category
                model.RemoveCategory(0);
                ModelOverview.DeleteModelFromFavourites(model);
            }
            else
            {
                // Add to Favourite-Category, no duplicates
                if (!model.Categories.Contains(0))
                {
                    model.AddCategory(0);
                    ModelOverview.AddModelToFavourites(model);
                }
            }
            ModelOverview.ResizeListBox(ModelOverview.FavouritesListBox);
        }

        /// <summary>
        /// Verwaltet Favoritenstatus, d.h. entfernt ihn wenn schon Favorit bzw umgekehrt.
        /// Aufgerufen durch Menü in ModelPropertyPanel.
        /// </summary>
        /// <param name="model">betreffendes Prozessmodell</param>
        private void ManageFavouriteState_byModelItem(ProcessModel model)
        {
            if (model.Categories.Contains(Enums.Category.Favourites))
            {
                // Remove from Favourite-Category
                model.RemoveCategory(0);
                ModelOverview.DeleteModelFromFavourites(model);
            }
            else
            {
                // Add to Favourite-Category, no duplicates
                if (!model.Categories.Contains(0))
                {
                    model.AddCategory(0);
                    ModelOverview.AddModelToFavourites(model);
                }
            }
            ModelOverview.ResizeListBox(ModelOverview.FavouritesListBox);
        }

        /// <summary>
        /// Blendet ModelAddingPanel ein.
        /// </summary>
        private void ShowAddModelPanel()
        {
            if (!AddModelPanelIsVisible)
            {
                ((ModelAddingPanel)ModelAddingSVI.Content).ClearContent();
                AddModelPanelIsVisible = true;
                ModelAddingSVI.Visibility = System.Windows.Visibility.Visible;
            }
            //highlight the open Panel
            else ((ModelAddingPanel)ModelAddingSVI.Content).HighlightPanel();
        }

        /// <summary>
        /// Blendet ModelRemovingPanel ein.
        /// </summary>
        private void ShowRemoveModelPanel()
        {
            if (!RemoveModelPanelIsVisible)
            {
                RemoveModelPanelIsVisible = true;
                ModelRemoveSVI.Visibility = System.Windows.Visibility.Visible;
            }
            //highlight the open Panel
            else ((ModelRemovingPanel)ModelRemoveSVI.Content).HighlightPanel();
        }

        /// <summary>
        /// Löscht angegebene Prozessinstanz.
        /// </summary>
        /// <param name="instance">zu löschende Prozessinstanz</param>
        /// <param name="stopped"><value>true</value> wenn gestoppt, <value>false</value> wenn gekillt</param>
        /// <param name="optionForAll"><value>true</value> wenn alle Instanzen des selben Prozessmodells zu löschen sind</param>
        private void DeleteInstance(ProcessInstance instance, bool stopped, bool optionForAll)
        {
            foreach (InstancePropertyPanel panel in instance.PanelList)
            {
                ClosePanel((ScatterViewItem)panel.Parent, true);
            }
            instance.BoundModel.InstanceList.Remove(instance);
            instance.BoundModel.CurrentlyExecutedInstances--;
            if (!optionForAll)
            {
                InstanceList.Remove(instance);
                if (stopped) SystemStateOverview.UpdateLog("Stopped", "12:35", instance.Name);
                else SystemStateOverview.UpdateLog("Killed", "12:35", instance.Name);
            }
        }

        #region XML-Parsing

        /// <summary>
        /// Bereitet Parsen des Namens einer Prozessbeschreibung vor.
        /// Wird zum Anzeigen des Modellnamens im <code>ModelAddingPanel</code> benötigt
        /// </summary>
        /// <param name="inputPath">Pfad zur einzulesenden sofia-Datei</param>
        /// <returns>Prozessmodellname</returns>
        private string ParseModelTitle(string inputPath)
        {
            XmlDocument doc = new XmlDocument();
            NSManager = new XmlNamespaceManager(doc.NameTable);
            NSManager.AddNamespace("sofia", "http://vicci.eu/sofia/1.0");
            doc.Load(inputPath);
            XmlNodeList modelNodes = doc.GetElementsByTagName("sofia:Process");
            XmlElement xmlElement = (XmlElement)modelNodes[0];
            return xmlElement.GetAttribute("name");
        }

        /// <summary>
        /// Leitet Einlesen aller sofia-Prozessbeschreibungen im angegebenen Verzeichnis ein.
        /// </summary>
        /// <param name="inputPath">Pfad zum Startverzeichnis laut config-Datei</param>
        private void LoadDefaultModels(string inputPath)
        {
            foreach (string file in Directory.EnumerateFiles(inputPath, "*.sofia"))
            {
                LoadXMLData(file);
            }
        }

        /// <summary>
        /// Bereitet Parsen der Prozessbeschreibung vor.
        /// Nutzt zum Aufbau der Prozessstruktur rekursive Methode <code>BuildSubelementStructure()</code>
        /// und zum Auflösen der Targetportreferenzen <code>SolveTargetPortReferences()</code>
        /// </summary>
        /// <param name="file">Pfad zur sofia-Datei</param>
        private ProcessModel LoadXMLData(string file)
        {
            XmlDocument doc = new XmlDocument();
            NSManager = new XmlNamespaceManager(doc.NameTable);
            NSManager.AddNamespace("sofia", "http://vicci.eu/sofia/1.0");
            doc.Load(file);

            XmlNodeList modelNodes = doc.GetElementsByTagName("sofia:Process");
            ProcessModel model = new ProcessModel();
            foreach (XmlNode processNode in modelNodes)
            {
                ModelList.Add(model);
                BuildSubelementStructure(model, processNode);
            }
            SolveTargetPortReferences(model, model);
            return model;
        }

        /// <summary>
        /// Arbeitet sich rekursiv durch Prozessbeschreibung und baut so Prozessstruktur auf.
        /// </summary>
        /// <param name="model">übergeordnetes Prozessmodell (kann auch Prozessschritt sein)</param>
        /// <param name="processNode">übergeordneter XML-Prozessknoten</param>
        private void BuildSubelementStructure(ProcessModel model, XmlNode processNode)
        {
            XmlElement xmlElement = (XmlElement)processNode;
            model.Id = xmlElement.GetAttribute("id");
            model.Name = xmlElement.GetAttribute("name");
            model.Type = xmlElement.GetAttribute("type");

            if (processNode.HasChildNodes)
            {
                XmlNode processChildNode = processNode.FirstChild;
                do
                {
                    if (processChildNode.Name.Equals("ports"))
                    {
                        XmlElement xmlPortElement = (XmlElement)processChildNode;
                        string portType = xmlPortElement.GetAttribute("xsi:type");

                        Port port = new ControlPort();
                        if (portType.Equals("sofia:StartDataPort"))
                            port = new StartDataPort();
                        else if (portType.Equals("sofia:EndDataPort"))
                            port = new EndDataPort();

                        model.PortList.Add(port);
                        port.Id = xmlPortElement.GetAttribute("id");
                        port.Name = xmlPortElement.GetAttribute("name");

                        if (processChildNode.HasChildNodes)
                        {
                            XmlNode portChildNode = processChildNode.FirstChild;
                            do
                            {
                                if (portChildNode.Name.Equals("outTransitions"))
                                {
                                    Transition transition = new Transition();
                                    port.TransitionList.Add(transition);
                                    XmlElement xmlTransitionElement = (XmlElement)portChildNode;
                                    transition.Id = xmlTransitionElement.GetAttribute("id");
                                    transition.Name = xmlTransitionElement.GetAttribute("name");
                                    // targetPort may be not known at this time so just store the string and handle correct reference later
                                    String targetPath = xmlTransitionElement.GetAttribute("targetPort");
                                    transition.TargetPortId = targetPath;
                                }
                                else if (portChildNode.Name.Equals("type"))
                                {
                                    XmlElement xmlTypeElement = (XmlElement)portChildNode;
                                    ((DataPort)port).Data = Enums.DataType.ObjectType;
                                    string datatype = xmlTypeElement.GetAttribute("xsi:type");
                                    if (datatype != "")
                                    {
                                        ((DataPort)port).DataTitle = xmlTypeElement.GetAttribute("name");
                                        if (datatype.Equals("sofia:DoubleType"))
                                            ((DataPort)port).Data = Enums.DataType.DoubleType;
                                        else if (datatype.Equals("sofia:IntType"))
                                            ((DataPort)port).Data = Enums.DataType.IntType;
                                        else if (datatype.Equals("sofia:TimeType"))
                                            ((DataPort)port).Data = Enums.DataType.TimeType;
                                        else if (datatype.Equals("sofia:LocationType"))
                                            ((DataPort)port).Data = Enums.DataType.LocationType;
                                        else if (datatype.Equals("sofia:ObjectType"))
                                            ((DataPort)port).Data = Enums.DataType.ObjectType;
                                    }
                                }

                                portChildNode = portChildNode.NextSibling;
                            }
                            while (portChildNode != null);
                        }
                    }
                    else if (processChildNode.Name.Equals("subSteps"))
                    {
                        Substep substep = new Substep();
                        model.SubstepList.Add(substep);
                        XmlElement xmlSubStepElement = (XmlElement)processChildNode;
                        substep.Id = xmlSubStepElement.GetAttribute("id");
                        substep.Name = xmlSubStepElement.GetAttribute("name");
                        string character = xmlSubStepElement.GetAttribute("xsi:type");
                        if (character != "")
                        {
                            if (character.Equals("sofia:Invoke"))
                                substep.ProcessCharacter = Enums.Character.Invoke;
                            else if (character.Equals("sofia:Or"))
                                substep.ProcessCharacter = Enums.Character.Or;
                            else if (character.Equals("sofia:If"))
                                substep.ProcessCharacter = Enums.Character.If;
                        }
                        BuildSubelementStructure(substep, processChildNode);
                    }
                    else if (processChildNode.Name.Equals("category"))
                    {
                        XmlElement xmlCategoryElement = (XmlElement)processChildNode;
                        try
                        {
                            int categoryId = Convert.ToInt32(xmlCategoryElement.GetAttribute("type"));
                            model.Categories.Add((Enums.Category)categoryId);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Category of model " + model.Name + " could not be resolved.");
                        }
                    }

                    processChildNode = processChildNode.NextSibling;
                }
                while (processChildNode != null);
            }
        }
        #endregion

        #region TargetPortIdResolving
        /// <summary>
        /// Löst Referenzen der Transitionen auf Targetport auf.
        /// </summary>
        /// <param name="model">zugehöriges Prozessmodell höchster Hierarchieebene</param>
        /// <param name="substep">direkt übergeordnetes Prozessmodell/ Prozessschritt</param>
        private void SolveTargetPortReferences(ProcessModel model, ProcessModel substep)
        {
            foreach (Port port in substep.PortList)
            {
                foreach (Transition transition in port.TransitionList)
                {
                    transition.TargetPortId = SearchForTargetPortId(ExtractTargetPortPath(transition.TargetPortId), model);
                    if (transition.TargetPortId.Equals("not set"))
                        Console.WriteLine("targetPortId of transition " + transition.Name + " in model " + model.Name + " could not be resolved.");
                }
            }
            foreach (Substep sub in substep.SubstepList)
            {
                SolveTargetPortReferences(model, sub);
            }
        }

        /// <summary>
        /// Formatiert Targetport-Pfad entsprechend anwendungsseitiger Prozessstruktur.
        /// </summary>
        /// <param name="invalidId">originale Zeichenfolge des Targetport-Attributes aus sofia-Datei</param>
        /// <returns>in Hierarchieebenen zerlegte Zeichenfolge</returns>
        private string[,] ExtractTargetPortPath(string invalidId)
        {
            invalidId = invalidId.Replace("/", string.Empty);
            invalidId = invalidId.Remove(0, 1);
            string[] parts = invalidId.Split(new char[] { '@', '.' });
            string[,] array = new string[parts.Length / 2, 2];

            for (int i = 0; i < parts.Length / 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    array[i, j] = parts[i * 2 + j];
                }
            }
            return array;
        }

        /// <summary>
        /// Versucht Targetport-Pfad entsprechend der formatierten Zeichenfolge zu ermitteln.
        /// </summary>
        /// <param name="idPath">in Hierarchieebenen zerlegte Pfad-Zeichenfolge</param>
        /// <param name="stepmodel">zugehöriges Prozessmodell höchster Hierarchieebene</param>
        /// <returns>ID des TargetPorts oder <value>not set</value> wenn am angegeben Zielpfad Port nicht gefunden wurde</returns>
        private string SearchForTargetPortId(string[,] idPath, ProcessModel stepmodel)
        {
            String targetPortId = ("not set");
            for (int i = 0; i < idPath.Length / 2; i++)
            {
                try
                {
                    if (idPath[i, 0].Equals("subSteps")) stepmodel = stepmodel.SubstepList[Convert.ToInt32(idPath[i, 1])];
                    else if (idPath[i, 0].Equals("ports"))
                    {
                        targetPortId = stepmodel.PortList[Convert.ToInt32(idPath[i, 1])].Id;
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception " + e.ToString() + " occured");
                }
            }
            return targetPortId;
        }

        #endregion
        #endregion

        #region Events
        #region MyEvents
        /// <summary>
        /// Event, das aufgerufen wird sobald Window geladen wurde.
        /// Ruft Adaptieren der WrapPanel-Breite in RunningInstancesArea auf
        /// und erstellt Listener für Selektionsänderungen der SurfaceListBoxen in ModelOverviewControl.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void WindowIsLoaded(object sender, RoutedEventArgs e)
        {
            RunningInstancesControl.AdaptWrapPanelWidth();
            ModelOverview.SelectionChanged += new RoutedEventHandler(ModelListSelectionChanged);
        }

        /// <summary>
        /// Event wird aufgerufen, wenn Selektion in einer der Listboxen in ModelOverview geändert wurde.
        /// Veranlasst Ein- bzw. Ausblenden des Menüs, da dieses als einziges (im Vergleich zu restlichen Menüs) im SurfaceWindow liegt
        /// statt im Panel oder Control selbst. Kann bei Bedarf in ModelOverviewControl verlagert werden.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void ModelListSelectionChanged(object sender, RoutedEventArgs e)
        {
            SurfaceListBox listbox = ((ModelOverviewControl)sender).CurrentListBox;

            if (listbox.SelectedIndex == -1)
            {
                CurrentSelectedItemPosition = new Point(0, 0);
                ModelOverviewMenu.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                SurfaceListBoxItem item = listbox.ItemContainerGenerator.ContainerFromItem(listbox.SelectedItem) as SurfaceListBoxItem;
                CurrentSelectedItemPosition = item.PointToScreen(new Point(0, 25));
                ModelOverviewMenu.Margin = new Thickness(ModelOverviewMenu.Margin.Left, CurrentSelectedItemPosition.Y - (ModelOverviewMenu.Height / 2), 0, 0);
                ModelOverviewMenu.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void TagContactDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            if (e.TouchDevice.GetTagData().Value == 5)
            {
                var bc = new BrushConverter();
                ((Canvas)sender).Background = (Brush)bc.ConvertFrom("#80FFFFFF");
            }
        }

        private void TagContactUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            if (e.TouchDevice.GetTagData().Value == 5)
            {
                var bc = new BrushConverter();
                ((Canvas)sender).Background = (Brush)bc.ConvertFrom("#19FFFFFF");
                RaiseError();
            }
        }

        #endregion

        #region Events Triggered by Others

        /// <summary>
        /// Event das aufgerufen wird, wenn in RunningInstancesControl Nutzereingabe für Aufrufen InstancePropertyPanel kam.
        /// Ruft entsprechende Methode auf.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void RunningInstancesControl_ShowInstanceEvent(object sender, RunningInstancesControl.InstanceEventArgs e)
        {
            ShowInstanceDetailPanel(e.Instance, 0);
        }

        /// <summary>
        /// Event das aufgerufen wird, wenn in InstancesControlWidget Nutzereingabe für Aufrufen InstancePropertyPanel kam.
        /// Ruft entsprechende Methode auf.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void InstanceControl_ShowInstancePanel(object sender, RoutedEventArgs e)
        {
            ShowInstanceDetailPanel(InstanceControl.Instance, 0);
        }

        //void RunningInstancesArea_HideMoveEvent(object sender, RunningInstancesArea.MoveAreaAndControlEventArgs e)
        //{
        //    RunningInstancesArea.Margin = new Thickness(
        //            RunningInstancesArea.ShowMargin + e.MoveLength,
        //            RunningInstancesArea.Margin.Top,
        //            RunningInstancesArea.Margin.Right,
        //            RunningInstancesArea.Margin.Bottom);
        //    RunningInstancesControl.Margin = new Thickness(
        //            RunningInstancesControl.ShowMargin + e.MoveLength,
        //            RunningInstancesControl.Margin.Top,
        //            RunningInstancesControl.Margin.Right,
        //            RunningInstancesControl.Margin.Bottom);
        //}

        //void RunningInstancesArea_ShowMoveEvent(object sender, RunningInstancesArea.MoveAreaAndControlEventArgs e)
        //{
        //    RunningInstancesArea.Margin = new Thickness(
        //            RunningInstancesArea.HideMargin + e.MoveLength,
        //            RunningInstancesArea.Margin.Top,
        //            RunningInstancesArea.Margin.Right,
        //            RunningInstancesArea.Margin.Bottom);
        //    RunningInstancesControl.Margin = new Thickness(
        //            RunningInstancesControl.HideMargin + e.MoveLength,
        //            RunningInstancesControl.Margin.Top,
        //            RunningInstancesControl.Margin.Right,
        //            RunningInstancesControl.Margin.Bottom);
        //}

        //void ModelOverviewArea_ShowHideMoveEvent(object sender, ModelOverviewArea.MoveAreaAndControlEventArgs e)
        //{
        //    ModOvArea.Margin = new Thickness(
        //            ModOvArea.ShowMargin - e.MoveLength,
        //            ModOvArea.Margin.Top,
        //            ModOvArea.Margin.Right,
        //            ModOvArea.Margin.Bottom);
        //    ModelOverview.Margin = new Thickness(
        //            ModelOverview.ShowMargin - e.MoveLength,
        //            ModelOverview.Margin.Top,
        //            ModelOverview.Margin.Right,
        //            ModelOverview.Margin.Bottom);
        //}

        //void SystemStateArea_ShowHideMoveEvent(object sender, SystemOverviewArea.MoveAreaAndControlEventArgs e)
        //{
        //    SystemStateOverviewArea.Margin = new Thickness(
        //            SystemStateOverviewArea.ShowMargin - e.MoveLength,
        //            SystemStateOverviewArea.Margin.Top,
        //            SystemStateOverviewArea.Margin.Right,
        //            SystemStateOverviewArea.Margin.Bottom);
        //    SystemStateOverview.Margin = new Thickness(
        //            SystemStateOverview.ShowMargin - e.MoveLength,
        //            SystemStateOverview.Margin.Top,
        //            SystemStateOverview.Margin.Right,
        //            SystemStateOverview.Margin.Bottom);
        //}

        /// <summary>
        /// Event wird aufgerufen, wenn Nutzer Button zum Ein- bzw Ausblenden des ModelOverview-Bereiches betätigt hat.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void ModOvArea_AnimateMarginEvent(object sender, ModelOverviewArea.AnimateAreaAndControlEventArgs e)
        {
            double areaToMargin = 0;
            double controlToMargin = 0;

            if (!e.ShowIt)
            {
                // hide it
                areaToMargin = ModOvArea.HideMargin;
                controlToMargin = ModelOverview.HideMargin;
                ModOvArea.IsShown = !true;
                ModOvArea.ShowOrHideText.Text = "Show Process Models";
                ModOvArea.ShowOrHideImage.RenderTransform = new RotateTransform(180, 0.5, 0.5);
                ModelOverviewMenu.CloseMenu();
                ModelOverviewMenu.Visibility = System.Windows.Visibility.Hidden;
                ModelOverview.CurrentListBox.SelectedIndex = -1;
            }
            else
            {
                // show it
                areaToMargin = ModOvArea.ShowMargin;
                controlToMargin = ModelOverview.ShowMargin;
                ModOvArea.IsShown = true;
                ModOvArea.ShowOrHideText.Text = "Hide Process Models";
                ModOvArea.ShowOrHideImage.RenderTransform = new RotateTransform(0, 0.5, 0.5);
            }

            ThicknessAnimation areaMoveAnimation = new ThicknessAnimation();
            areaMoveAnimation.Duration = TimeSpan.FromSeconds(0.5);
            areaMoveAnimation.From = new Thickness(ModOvArea.Margin.Left, ModOvArea.Margin.Top, ModOvArea.Margin.Right, ModOvArea.Margin.Bottom);
            areaMoveAnimation.To = new Thickness(areaToMargin, ModOvArea.Margin.Top, ModOvArea.Margin.Right, ModOvArea.Margin.Bottom);
            // updateAnimation.Completed += new EventHandler(areaMoveAnimation_Completed);

            ThicknessAnimation controlMoveAnimation = new ThicknessAnimation();
            controlMoveAnimation.Duration = TimeSpan.FromSeconds(0.5);
            controlMoveAnimation.From = new Thickness(ModelOverview.Margin.Left, ModelOverview.Margin.Top, ModelOverview.Margin.Right, ModelOverview.Margin.Bottom);
            controlMoveAnimation.To = new Thickness(controlToMargin, ModelOverview.Margin.Top, ModelOverview.Margin.Right, ModelOverview.Margin.Bottom);

            Storyboard moveStoryboard = new Storyboard();
            moveStoryboard.Children.Add(areaMoveAnimation);
            moveStoryboard.Children.Add(controlMoveAnimation);
            Storyboard.SetTargetName(areaMoveAnimation, ModOvArea.Name);
            Storyboard.SetTargetProperty(areaMoveAnimation, new PropertyPath(UserControl.MarginProperty));
            Storyboard.SetTargetName(controlMoveAnimation, ModelOverview.Name);
            Storyboard.SetTargetProperty(controlMoveAnimation, new PropertyPath(UserControl.MarginProperty));

            moveStoryboard.Begin(this);
        }
        
        /// <summary>
        /// Event wird aufgerufen, wenn Nutzer Button zum Ein- bzw Ausblenden des RunningInstances-Bereiches betätigt hat.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void RunningInstancesArea_AnimateMarginEvent(object sender, RunningInstancesArea.AnimateAreaAndControlEventArgs e)
        {
            double areaFromMargin = RunningInstancesArea.Margin.Left;
            double areaToMargin = 0;
            double controlFromMarginRunning = RunningInstancesControl.Margin.Left;
            double controlToMargin = 0;

            if (!e.ShowIt)
            {
                // hide it
                areaToMargin = RunningInstancesArea.HideMargin;
                controlToMargin = RunningInstancesControl.HideMargin;
                RunningInstancesArea.IsShown = !true;
                RunningInstancesArea.ShowOrHideText.Text = "Show Running Instances";
                RunningInstancesArea.ShowOrHideImage.RenderTransform = new RotateTransform(180, 0.5, 0.5);
            }
            else
            {
                // show it
                areaToMargin = RunningInstancesArea.ShowMargin;
                controlToMargin = RunningInstancesControl.ShowMargin;
                RunningInstancesArea.IsShown = true;
                RunningInstancesArea.ShowOrHideText.Text = "Hide Running Instances";
                RunningInstancesArea.ShowOrHideImage.RenderTransform = new RotateTransform(0, 0.5, 0.5);
            }

            ThicknessAnimation areaMoveAnimation = new ThicknessAnimation();
            areaMoveAnimation.Duration = TimeSpan.FromSeconds(0.5);
            areaMoveAnimation.From = new Thickness(RunningInstancesArea.Margin.Left, RunningInstancesArea.Margin.Top, RunningInstancesArea.Margin.Right, RunningInstancesArea.Margin.Bottom);
            areaMoveAnimation.To = new Thickness(areaToMargin, RunningInstancesArea.Margin.Top, RunningInstancesArea.Margin.Right, RunningInstancesArea.Margin.Bottom);
            // updateAnimation.Completed += new EventHandler(areaMoveAnimation_Completed);

            ThicknessAnimation controlMoveAnimation = new ThicknessAnimation();
            controlMoveAnimation.Duration = TimeSpan.FromSeconds(0.5);
            controlMoveAnimation.From = new Thickness(RunningInstancesControl.Margin.Left, RunningInstancesControl.Margin.Top, RunningInstancesControl.Margin.Right, RunningInstancesControl.Margin.Bottom);
            controlMoveAnimation.To = new Thickness(controlToMargin, RunningInstancesControl.Margin.Top, RunningInstancesControl.Margin.Right, RunningInstancesControl.Margin.Bottom);

            Storyboard moveStoryboard = new Storyboard();
            moveStoryboard.Children.Add(areaMoveAnimation);
            moveStoryboard.Children.Add(controlMoveAnimation);
            Storyboard.SetTargetName(areaMoveAnimation, RunningInstancesArea.Name);
            Storyboard.SetTargetProperty(areaMoveAnimation, new PropertyPath(UserControl.MarginProperty));
            Storyboard.SetTargetName(controlMoveAnimation, RunningInstancesControl.Name);
            Storyboard.SetTargetProperty(controlMoveAnimation, new PropertyPath(UserControl.MarginProperty));

            moveStoryboard.Begin(this);
        }
        
        /// <summary>
        /// Event wird aufgerufen, wenn Nutzer Button zum Ein- bzw Ausblenden des SystemState-Bereiches betätigt hat.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void SystemStateOverviewArea_AnimateMarginEvent(object sender, SystemOverviewArea.AnimateAreaAndControlEventArgs e)
        {
            double areaToMargin = 0;
            double controlToMargin = 0;

            if (!e.ShowIt)
            {
                // hide it
                areaToMargin = SystemStateOverviewArea.HideMargin;
                controlToMargin = SystemStateOverview.HideMargin;
                SystemStateOverviewArea.IsShown = !true;
                SystemStateOverviewArea.ShowOrHideText.Text = "Show System State";
                SystemStateOverviewArea.ShowOrHideImage.RenderTransform = new RotateTransform(180, 0.5, 0.5);
            }
            else
            {
                // show it
                areaToMargin = SystemStateOverviewArea.ShowMargin;
                controlToMargin = SystemStateOverview.ShowMargin;
                SystemStateOverviewArea.IsShown = true;
                SystemStateOverviewArea.ShowOrHideText.Text = "Hide System State";
                SystemStateOverviewArea.ShowOrHideImage.RenderTransform = new RotateTransform(0, 0.5, 0.5);
            }

            ThicknessAnimation areaMoveAnimation = new ThicknessAnimation();
            areaMoveAnimation.Duration = TimeSpan.FromSeconds(0.5);
            areaMoveAnimation.From = new Thickness(SystemStateOverviewArea.Margin.Left, SystemStateOverviewArea.Margin.Top, SystemStateOverviewArea.Margin.Right, SystemStateOverviewArea.Margin.Bottom);
            areaMoveAnimation.To = new Thickness(areaToMargin, SystemStateOverviewArea.Margin.Top, SystemStateOverviewArea.Margin.Right, SystemStateOverviewArea.Margin.Bottom);
            // updateAnimation.Completed += new EventHandler(areaMoveAnimation_Completed);

            ThicknessAnimation controlMoveAnimation = new ThicknessAnimation();
            controlMoveAnimation.Duration = TimeSpan.FromSeconds(0.5);
            controlMoveAnimation.From = new Thickness(SystemStateOverview.Margin.Left, SystemStateOverview.Margin.Top, SystemStateOverview.Margin.Right, SystemStateOverview.Margin.Bottom);
            controlMoveAnimation.To = new Thickness(controlToMargin, SystemStateOverview.Margin.Top, SystemStateOverview.Margin.Right, SystemStateOverview.Margin.Bottom);

            Storyboard moveStoryboard = new Storyboard();
            moveStoryboard.Children.Add(areaMoveAnimation);
            moveStoryboard.Children.Add(controlMoveAnimation);
            Storyboard.SetTargetName(areaMoveAnimation, SystemStateOverviewArea.Name);
            Storyboard.SetTargetProperty(areaMoveAnimation, new PropertyPath(UserControl.MarginProperty));
            Storyboard.SetTargetName(controlMoveAnimation, SystemStateOverview.Name);
            Storyboard.SetTargetProperty(controlMoveAnimation, new PropertyPath(UserControl.MarginProperty));

            moveStoryboard.Begin(this);
        }

        /// <summary>
        /// Event wird aufgerufen wenn im InstanceControlWidget eine Option ausgewählt wurde.
        /// Leitet entsprechende Massnahmen ein und ruft Methoden für Serverkommunikation auf.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void InstanceControl_ControlInstanceChosenOption(object sender, OptionChosenEventArgs e)
        {
            ProcessInstance instance = e.Instance;
            ProcessModel model = e.Instance.BoundModel;
            if (e.ChosenOption.Equals(InstanceControlWidget.ChosenControl.PausePlay) || e.ChosenOption.Equals(InstanceControlWidget.ChosenControl.Pause))
            {
                if (e.OptionForAll)
                {
                    if (e.ChosenOption.Equals(InstanceControlWidget.ChosenControl.Pause))
                    {
                        // Pause all instances with same model
                        foreach (ProcessInstance oneInstance in InstanceList)
                        {
                            if (oneInstance.BoundModel.Equals(instance.BoundModel))
                            {
                                oneInstance.State = Enums.ProcessState.paused;
                                S_PauseInstance(oneInstance);
                                RunningInstancesControl.UpdateIOCs(oneInstance);
                            }
                        }
                        SystemStateOverview.UpdateLog("Paused all Instances", "12:35", instance.BoundModel.Name);
                    }
                    else
                    {
                        // Continue all instances with same model
                        foreach (ProcessInstance oneInstance in InstanceList)
                        {
                            if (oneInstance.BoundModel.Equals(instance.BoundModel))
                            {
                                oneInstance.State = Enums.ProcessState.executing;
                                S_ContinueInstance(oneInstance);
                                RunningInstancesControl.UpdateIOCs(oneInstance);
                            }
                        }
                        SystemStateOverview.UpdateLog("Started all Instances", "12:35", instance.BoundModel.Name);
                    }
                }
                else
                {   // Play or Pause instance
                    if (instance.State.Equals(Enums.ProcessState.executing))
                    {
                        // Pause instance
                        instance.State = Enums.ProcessState.paused;
                        S_PauseInstance(instance);
                        RunningInstancesControl.UpdateIOCs(instance);
                        SystemStateOverview.UpdateLog("Paused", "12:35", instance.Name);
                    }
                    else
                    {
                        // Continue instance
                        instance.State = Enums.ProcessState.executing;
                        S_ContinueInstance(instance);
                        RunningInstancesControl.UpdateIOCs(instance);
                        SystemStateOverview.UpdateLog("Started", "12:35", instance.Name);
                    }
                }
            }
            else if (InstanceControl.SelectedControl.Equals(InstanceControlWidget.ChosenControl.Stop))
            {
                if (e.OptionForAll)
                {
                    List<ProcessInstance> instancesToDeleteList = new List<ProcessInstance>();
                    // Stop all instances with same model
                    foreach (ProcessInstance oneInstance in InstanceList)
                    {
                        if (oneInstance.BoundModel.Equals(model))
                        {
                            oneInstance.State = Enums.ProcessState.stopped;
                            RunningInstancesControl.DeleteInstanceIOCs(oneInstance);
                            DeleteInstance(oneInstance, true, e.OptionForAll);
                            S_StopInstance(oneInstance);
                            instancesToDeleteList.Add(oneInstance);
                        }
                    }
                    foreach (ProcessInstance instanceToDelete in instancesToDeleteList)
                    {
                        //try
                        //{
                        //    RunningInstancesControl.RunningInstances.Remove(instanceToDelete);
                        //}
                        //catch (Exception ex)
                        //{

                        //}
                        InstanceList.Remove(instanceToDelete);
                    }
                    SystemStateOverview.UpdateLog("Stopped all Instances", "12:35", instance.BoundModel.Name);
                }
                else
                {
                    // Stop instance
                    instance.State = Enums.ProcessState.stopped;
                    RunningInstancesControl.DeleteInstanceIOCs(instance);
                    DeleteInstance(instance, true, e.OptionForAll);
                    S_StopInstance(instance);
                }
            }
            else if (InstanceControl.SelectedControl.Equals(InstanceControlWidget.ChosenControl.Kill))
            {
                if (e.OptionForAll)
                {
                    List<ProcessInstance> instancesToDeleteList = new List<ProcessInstance>();
                    // Kill all instances with same model
                    foreach (ProcessInstance oneInstance in InstanceList)
                    {
                        if (oneInstance.BoundModel.Equals(instance.BoundModel))
                        {
                            oneInstance.State = Enums.ProcessState.killed;
                            RunningInstancesControl.DeleteInstanceIOCs(oneInstance);
                            DeleteInstance(oneInstance, false, e.OptionForAll);
                            S_KillInstance(oneInstance);
                            instancesToDeleteList.Add(oneInstance);
                        }
                    }
                    foreach (ProcessInstance instanceToDelete in instancesToDeleteList)
                    {
                        //try
                        //{
                        //    RunningInstancesControl.RunningInstances.Remove(instanceToDelete);
                        //}
                        //catch (Exception ex)
                        //{

                        //}
                        InstanceList.Remove(instanceToDelete);
                    }
                    SystemStateOverview.UpdateLog("Killed all Instances", "12:35", instance.BoundModel.Name);
                }
                else
                {
                    // Kill instance
                    instance.State = Enums.ProcessState.killed;
                    RunningInstancesControl.DeleteInstanceIOCs(instance);
                    DeleteInstance(instance, false, e.OptionForAll);
                    S_KillInstance(instance);
                }
            }
            UpdateSystemStateOverviewFacts();
        }

        /// <summary>
        /// Event wird aufgerufen, wenn Nutzer SafeSelect zum Aufrufen entweder von ModelAddingPanel oder von ModelRemovingPanel betätigt hat.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void ModelOverview_AddOrRemoveSelected(object sender, RoutedEventArgs e)
        {
            if (ModelOverview.SelectedPanel.Equals(ModelOverviewControl.ChosenPanel.Add))
                ShowAddModelPanel();
            else ShowRemoveModelPanel();
        }

        /// <summary>
        /// Event wird aufgerufen bei DoubleTap auf Instanzanzahllink.
        /// Ruft Methode zum Anzeigen InstancePropertyPanel auf.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void ModelItem_DoubleTapOnInstanceAmount(object sender, ShowInstancePanelEventArgs e)
        {
            ShowInstanceDetailPanel(e.Instance, ((ModelPropertyPanel)sender).ParentSVI.ActualOrientation);
        }

        /// <summary>
        /// Event wird aufgerufen bei DoubleTap auf Instanzlink.
        /// Ruft Methode zum Anzeigen InstancePropertyPanel auf.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void InstancePanel_DoubleTapOnNextStep(object sender, InstancePropertyPanel.ShowStepInstancePanelEventArgs e)
        {
            ShowInstanceDetailPanel(e.Instance, ((InstancePropertyPanel)sender).ParentSVI.ActualOrientation);
        }

        /// <summary>
        /// Event wird aufgerufen bei DoubleTap auf Instanzlink.
        /// Ruft Methode zum Anzeigen InstancePropertyPanel auf.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void InstancePanel_DoubleTapOnCurrentStep(object sender, InstancePropertyPanel.ShowStepInstancePanelEventArgs e)
        {
            ShowInstanceDetailPanel(e.Instance, ((InstancePropertyPanel)sender).ParentSVI.ActualOrientation);
        }

        /// <summary>
        /// Event wird aufgerufen bei DoubleTap auf Modelllink.
        /// Ruft Methode zum Anzeigen ModelPropertyPanel auf.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void InstancePanel_DoubleTapOnModelName(object sender, InstancePropertyPanel.ShowModelPanelEventArgs e)
        {
            ShowModelDetailPanel(e.Model, ((InstancePropertyPanel)sender).ParentSVI.ActualOrientation);
        }

        /// <summary>
        /// Event wird aufgerufen, wenn Nutzer im ModelAddingpanel über OpenFile-Dialog sofia-Dateien ausgewählt hat.
        /// Leitet Parsen des Modellnamens ein.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void ModelAddingPanel_FileNamesChosen(object sender, ModelAddingPanel.ParseFileTitleEventArgs e)
        {
            foreach (string file in e.FilePathList)
            {
                for (int i=0; i<3;i++)
                {
                    if (((ModelAddingPanel)sender).FileMap[i, 0].Equals(file))
                    {
                        ((ModelAddingPanel)sender).FileMap[i, 2] = ParseModelTitle(file);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Event wird aufgerufen, wenn Nutzer ModelAddingpanel geschlossen hat.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void ModelAddingPanel_Closed(object sender, ModelAddingPanel.AddModelsEventArgs e)
        {
            ClosePanel((ScatterViewItem)((ModelAddingPanel)sender).Parent, false);
            AddModelPanelIsVisible = false;
            ModelAddingSVI.Visibility = System.Windows.Visibility.Hidden;
            if (e.AddModel && e.FilePathList!=null)
            {
                // ToDo: load sofia-files, make sure e.FilePathList is not null, check whether SurfaceListBoxes are updating
                foreach (string file in e.FilePathList)
                {
                    ModelOverview.UpdateListContent(LoadXMLData(file));
                }
                SystemStateOverview.UpdateLog("Added", "12:35", e.FilePathList.Count + " Process Model(s)");
            }
        }

        /// <summary>
        /// Event wird aufgerufen, wenn Nutzer ModelRemovePanel geschlossen hat.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void ModelRemovePanel_Closed(object sender, RemoveModelsEventArgs e)
        {
            ClosePanel((ScatterViewItem)((ModelRemovingPanel)sender).Parent, false);
            ModelRemoveSVI.Visibility = System.Windows.Visibility.Hidden;
            RemoveModelPanelIsVisible = false;
            if (e.RemoveModel)
            {
                SystemStateOverview.UpdateLog("Removed", "12:35", "1 Process Model");
                // ToDo: make sure e.FilePathList is not null, delete models in e.DeletableModelList, check whether SurfaceListBoxes are updating             
            }
        }

        /// <summary>
        /// Event wird aufgerufen wenn in ModelOverview Menüeintrag gewählt wurde.
        /// Leitet entsprechende Massnahme ein.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void ModelOverviewMenu_OptionWasChosenEvent(object sender, MenuThreeOptions.ChosenOptionEventArgs e)
        {
            if (e.ChosenOption.Equals(Prototype_05.MenuThreeOptions.ChosenOption.Option0))
            {
                SurfaceListBox listbox = ModelOverview.CurrentListBox;
                SurfaceListBoxItem item = listbox.ItemContainerGenerator.ContainerFromItem(listbox.SelectedItem) as SurfaceListBoxItem;
                // CreateInstance((ProcessModel)item.Content, true);
                CheckModelParameters((ProcessModel)item.Content, e.TouchOrientation);
            }
            else if (e.ChosenOption.Equals(Prototype_05.MenuThreeOptions.ChosenOption.Option1))
            {
                SurfaceListBox listbox = ModelOverview.CurrentListBox;
                SurfaceListBoxItem item = listbox.ItemContainerGenerator.ContainerFromItem(listbox.SelectedItem) as SurfaceListBoxItem;
                ProcessModel model = (ProcessModel)item.Content;
                ShowModelDetailPanel(model, e.TouchOrientation);
            }
            else if (e.ChosenOption.Equals(Prototype_05.MenuThreeOptions.ChosenOption.Option2))
            {
                ManageFavouriteState_byModelList();
            }
            ModelOverview.CurrentListBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Event wird aufgerufen wenn StartInstancePanel geschlossen wurde.
        /// Erzeugt Instanz wenn gewünscht.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void StartInstancePanel_StartInstancePanelClosed(object sender, ParameterChosenEventArgs e)
        {
            if (e.ConfirmedStarting)
            {
                CreateInstance(e.Model, !e.InstanceHasToWait, e.ChosenParameters);
            }
            ClosePanel((ScatterViewItem)((StartInstancePanel)sender).Parent,true);
        }
        
        /// <summary>
        /// Event wird aufgerufen wenn Menüeintrag im ModelPropertyPanel gewählt wurde.
        /// Leitet entsprechende Massnahmen ein.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void ModelItem_AttachedMenuOptionSelected(object sender, RoutedEventArgs e)
        {
            ModelPropertyPanel item = (ModelPropertyPanel)sender;
            MenuThreeOptions menu = (MenuThreeOptions)item.AttachedMenu;
            if (menu.SelectedOption.Equals(Prototype_05.MenuThreeOptions.ChosenOption.Option0))
            {
                // CreateInstance(item.ProcessModel, true);
                CheckModelParameters(item.ProcessModel, 0);
            }
            else if (menu.SelectedOption.Equals(Prototype_05.MenuThreeOptions.ChosenOption.Option1))
            {
                ManageFavouriteState_byModelItem(item.ProcessModel);
            }
            else if (menu.SelectedOption.Equals(Prototype_05.MenuThreeOptions.ChosenOption.Option2))
            {
                item.ProcessModel.PanelList.Remove(item);
                ClosePanel(item.ParentSVI, true);
            }
        }

        /// <summary>
        /// Event wird aufgerufen wenn Menüeintrag im InstancePropertyPanel gewählt wurde.
        /// Leitet entsprechende Massnahmen ein.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void InstancePanel_OptionWasChosenEvent(object sender, InstancePropertyPanel.InstanceAndChosenOptionEventArgs e)
        {
            InstancePropertyPanel item = (InstancePropertyPanel)sender;
            if (e.ChosenOption.Equals(Prototype_05.MenuTwoOptions.ChosenOption.Option0))
            {
                // Get Control about Instance
                RunningInstancesControl.DisableMainGridButControl();
                RunningInstancesControl.AttachedControl.BindInstance(e.Instance);
            }
            else if (e.ChosenOption.Equals(Prototype_05.MenuTwoOptions.ChosenOption.Option1))
            {
                item.ProcessInstance.PanelList.Remove(item);
                // Close Panel
                ClosePanel(item.ParentSVI, true);
            }
        }

        #endregion
        #endregion

        #region ServerMethods

        /// <summary>
        /// Methode zur Serverkommunikation bei Instanz erzeugen.
        /// </summary>
        /// <param name="instanceToCreate">zu erzeugende Instanz</param>
        /// <param name="isInstanceToBeStarted"><value>true</value> wenn Instanz erzeugt und gestartet werden soll</param>
        private void S_CreateInstance(ProcessInstance instanceToCreate, bool isInstanceToBeStarted)
        {
            // handle creation on server
        }

        /// <summary>
        /// Methode zur Serverkommunikation bei Instanz pausieren.
        /// </summary>
        /// <param name="instanceToPause">zu pausierende Instanz</param>
        private void S_PauseInstance(ProcessInstance instanceToPause)
        {
            // handle pausing on server
        }

        /// <summary>
        /// Methode zur Serverkommunikation bei Instanz fortsetzen.
        /// </summary>
        /// <param name="instanceToContinue">fortzsetzende Instanz</param>
        private void S_ContinueInstance(ProcessInstance instanceToContinue)
        {
            // handle continue on server
        }

        /// <summary>
        /// Methode zur Serverkommunikation bei Instanz stoppen.
        /// </summary>
        /// <param name="instanceToStop">zu stoppende Instanz</param>
        private void S_StopInstance(ProcessInstance instanceToStop)
        {
            // handle stopping on server
        }

        /// <summary>
        /// Methode zur Serverkommunikation bei Instanz killen.
        /// </summary>
        /// <param name="instanceToKill">zu killende Instanz</param>
        private void S_KillInstance(ProcessInstance instanceToKill)
        {
            // handle killing on server
        }

        /// <summary>
        /// Methode sollte aufgerufen werden, wenn Server Erzeugen einer Instanz an Client gemeldet hat.
        /// </summary>
        /// <param name="modelToCreate">zu erzeugende Instanz</param>
        /// <param name="isInstanceToBeStarted"><value>true</value> wenn Instanz erzeugt und gestartet werden soll</param>
        private void C_CreateInstance(ProcessModel modelToCreate, bool isInstanceToBeStarted)
        {
            // handle creation on client
            ProcessInstance instance = new ProcessInstance(modelToCreate);
            if (isInstanceToBeStarted) StartInstance(instance);
            RunningInstancesControl.AddInstance(instance);
        }

        /// <summary>
        /// Methode sollte aufgerufen werden, wenn Server Pausieren einer Instanz an Client gemeldet hat.
        /// </summary>
        /// <param name="instanceToPause">zu pausierende Instanz</param>
        private void C_PauseInstance(ProcessInstance instanceToPause)
        {
            // handle pausing on client
            instanceToPause.State = Enums.ProcessState.paused;
            RunningInstancesControl.UpdateIOCs(instanceToPause);
        }

        /// <summary>
        /// Methode sollte aufgerufen werden, wenn Server Fortsetzen einer Instanz an Client gemeldet hat.
        /// </summary>
        /// <param name="instanceToContinue">fortzusetzende Instanz</param>
        private void C_ContinueInstance(ProcessInstance instanceToContinue)
        {
            // handle continue on client
            instanceToContinue.State = Enums.ProcessState.executing;
            RunningInstancesControl.UpdateIOCs(instanceToContinue);
        }

        /// <summary>
        /// Methode sollte aufgerufen werden, wenn Server Stoppen einer Instanz an Client gemeldet hat.
        /// </summary>
        /// <param name="instanceToStop">zu stoppende Instanz</param>
        private void C_StopInstance(ProcessInstance instanceToStop)
        {
            // handle stopping on client
            instanceToStop.State = Enums.ProcessState.faulty;
            RunningInstancesControl.UpdateIOCs(instanceToStop);
        }

        /// <summary>
        /// Methode sollte aufgerufen werden, wenn Server Killen einer Instanz an Client gemeldet hat.
        /// </summary>
        /// <param name="instanceToKill">zu killende Instanz</param>
        private void C_KillInstance(ProcessInstance instanceToKill)
        {
            // handle killing on client
            instanceToKill.State = Enums.ProcessState.failed;
            RunningInstancesControl.UpdateIOCs(instanceToKill);
        }
        #endregion

        #region temporär auskommentiertes
        /*
         * Einkommentieren und anpassen wenn sich Menü wieder bei Scrollen der aktiven SurfaceListBox
         * in ProcessModelOverview mitbewegen soll
         
        //private bool TouchOnModelItemCaptured;
        //private Line CurrentTouchVisLine;
        //private Point TouchStartPoint;
        //private int Counter;
         * 
        private void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (ListBox_Models.SelectedItem == null)
                return;

            if (ListBox_Models.IsLoaded)
            {
                double pointonscreenY = ModelMenu.PointToScreen(new Point(0, 0)).Y; // -100;
                double change = pointonscreenY - e.VerticalChange;
                //tb_scroll1.Text = tb_scroll1.Text + " " + change;
                //tb_scroll2.Text = tb_scroll2.Text + " " + pointonscreenY;
                //tb_scroll3.Text = tb_scroll3.Text + " " + e.VerticalChange;
                ModelMenuCanvas.Margin = new Thickness(0, (change), 0, 0);
                //ModelMenuCanvas.Margin = new Thickness(0, _currentSelectedItemPosition.Y - (ModelMenuCanvas.Height / 2), 0, 0);
            }
        }
         
        void SurfaceWindow1_ContainerManipulationDelta(object sender, ContainerManipulationDeltaEventArgs e)
        {
            UpdateModelItemsAttachedMenuPosition((ScatterViewItem)sender);
        }

        private void UpdateModelItemsAttachedMenuPosition(ScatterViewItem svi)
        {
            Menu menu = ((ModelPropertyPanel)svi.Content).AttachedMenu;
            menu.RenderTransform = new RotateTransform(svi.Orientation);

            ModelPropertyPanel item = (ModelPropertyPanel)svi.Content;
            Point modelSVIPosition = item.PointToScreen(new Point(item.MainGrid.ColumnDefinitions[0].Width.Value - menu.MenuButtonWidth, 0));
            menu.Margin = new Thickness(modelSVIPosition.X, modelSVIPosition.Y - (menu.Height - menu.MenuButtonHeight) / 2, 0, 0);
        }
        */
        #endregion

        #region WindowHandlers

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e">Eventparameter</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

        #endregion
    }
}