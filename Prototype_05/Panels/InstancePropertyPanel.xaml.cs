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
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using Prototype_05.ModelData;
using Prototype_05.InstanceData;
using System.Windows.Media.Animation;

namespace Prototype_05
{
	/// <summary>
	/// Interaktionslogik für InstancePropertyPanel.xaml
    /// Zeigt Details einer Instanz an.
	/// </summary>
	public partial class InstancePropertyPanel : UserControl
    {
        #region Deklarationen
        /// <summary>
        /// originale Höhe von DataInfoGrid. Wichtig wegen EIn- und Ausklappen von Abschnittten
        /// </summary>
        protected double _original_DataInfoGrid_Height = 0;
        /// <summary>
        /// originale Höhe von HistoryInfoGrid. Wichtig wegen EIn- und Ausklappen von Abschnittten
        /// </summary>
        protected double _original_HistoryInfoGrid_Height = 0;
        /// <summary>
        /// originale Höhe von ModelInfoGrid. Wichtig wegen EIn- und Ausklappen von Abschnittten
        /// </summary>
        protected double _original_ModelInfoGrid_Height = 0;
        /// <summary>
        /// originale Höhe von DataGrid. Wichtig wegen EIn- und Ausklappen von Abschnittten
        /// </summary>
        protected double _original_DataGrid_Height = 0;
        /// <summary>
        /// originale Höhe von HistoryGrid. Wichtig wegen EIn- und Ausklappen von Abschnittten
        /// </summary>
        protected double _original_HistoryGrid_Height = 0;
        /// <summary>
        /// originale Höhe von ModelGrid. Wichtig wegen EIn- und Ausklappen von Abschnittten
        /// </summary>
        protected double _original_ModelGrid_Height = 0;
        /// <summary>
        /// Gibt an ob Data-sektion ausgeklappt ist.
        /// </summary>
        protected bool _dataExpanded = true;
        /// <summary>
        /// Gibt an ob History-sektion ausgeklappt ist.
        /// </summary>
        protected bool _historyExpanded = true;
        /// <summary>
        /// Gibt an ob Model-sektion ausgeklappt ist.
        /// </summary>
        protected bool _modelExpanded = true;
        /// <summary>
        /// tocuhdevice zur Differnzierung mehrerer Finger auf ScaleGrid
        /// </summary>
        protected TouchDevice _scaleControlTouchDevice;
        /// <summary>
        /// letzte Touchposition.
        /// Punkt kann auch innerhalb Panel liegen, dann keine Skalierung mehr.
        /// </summary>
        protected Point _lastPoint;
        /// <summary>
        /// letzte für Skalierung gültige Tocuhposition.
        /// </summary>
        protected Point _lastValidScalePoint;
        /// <summary>
        /// Benötigt bei Berechnung der Skalierung.
        /// </summary>
        protected double _currentHeightRestriction;
        //protected  double _originalMainGridFirstColumnWidth;

        /// <summary>
        /// Aktuell aktiver Tab.
        /// </summary>
        private int CurrentNavigation;
        /// <summary>
        /// TocuhDevice auf Tabs zur Differenzierung von Fingern.
        /// </summary>
        private TouchDevice NavigationTouchDevice;
        /// <summary>
        /// Vorschau für aktuell demnächst aktiven Tab.
        /// </summary>
        private int PreviewNavigation = -1;

        protected ScatterViewItem _parentSVI = null;
        /// <summary>
        /// Eltern-ScatterviewItem
        /// kann eigtl auch über this.Parent erreicht werden, dann aber jedes mal Casten nötig
        /// </summary>
        public ScatterViewItem ParentSVI
        {
            get { return _parentSVI; }
            set { _parentSVI = value; }
        }

        protected Menu _attachedMenu;
        /// <summary>
        /// zugehöriges Menü
        /// </summary>
        public Menu AttachedMenu
        {
            get { return _attachedMenu; }
            set { _attachedMenu = value; }
        }

        protected ProcessInstance _processInstance;
        /// <summary>
        /// betreffende Prozesinstanz
        /// </summary>
        public ProcessInstance ProcessInstance
        {
            get { return _processInstance; }
            set { _processInstance = value; }
        }
        /// <summary>
        /// Liste der Rechtecke im Naviagtionsgrid.
        /// </summary>
        private List<Rectangle> NavigationRectangleList = new List<Rectangle>();
        /// <summary>
        /// Liste der Textboxen im Naviagtionsgrid.
        /// </summary>
        private List<TextBox> NavigationTextBoxList = new List<TextBox>();
        /// <summary>
        /// ID-Vergabe für dynamisch erzeugte Rechtecke im Naviagtionsgrid.
        /// </summary>
        private int UniqueRectangleID = 0;
        /// <summary>
        /// ID-Vergabe für dynamisch erzeugte Textboxen im Naviagtionsgrid.
        /// </summary>
        private int UniqueTextBoxID = 0;

        /// <summary>
        /// Letzter Zeotpunkt eiens Taps auf Modellname.
        /// Wichtig für Berechnung ob Double-Tap.
        /// </summary>
        protected DateTime ModelTouchDownTimeForDoubleTap;
        /// <summary>
        /// Letzter Zeotpunkt eiens Taps auf aktuellen Instanzschritt.
        /// Wichtig für Berechnung ob Double-Tap.
        /// </summary>
        protected DateTime CurrentStepTouchDownTimeForDoubleTap;
        /// <summary>
        /// Letzter Zeotpunkt eiens Taps auf nächsten Instanzschritt.
        /// Wichtig für Berechnung ob Double-Tap.
        /// </summary>
        protected DateTime NextStepTouchDownTimeForDoubleTap;
        public delegate void ShowModelPanelEventHandler(object sender, ShowModelPanelEventArgs e);
        public event ShowModelPanelEventHandler DoubleTapOnModelName;
        public delegate void ShowCurrentStepPanelEventHandler(object sender, ShowStepInstancePanelEventArgs e);
        public event ShowCurrentStepPanelEventHandler DoubleTapOnCurrentStep;
        public delegate void ShowNextStepPanelEventHandler(object sender, ShowStepInstancePanelEventArgs e);
        public event ShowNextStepPanelEventHandler DoubleTapOnNextStep;

        public delegate void SendInstanceAndChosenOptionEventHandler(object sender, InstanceAndChosenOptionEventArgs e);
        public event SendInstanceAndChosenOptionEventHandler OptionWasChosenEvent;

        /// <summary>
        /// MenuOptionSelectedEvent, needed to pass event outside of InstancePropertyPanel-Class
        /// </summary>
        public static readonly RoutedEvent AttachedMenuOptionSelectedEvent = EventManager.RegisterRoutedEvent("AttachedMenuOptionSelected", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(InstancePropertyPanel));
        public event RoutedEventHandler AttachedMenuOptionSelected
        {
            add { AddHandler(AttachedMenuOptionSelectedEvent, value); }
            remove { RemoveHandler(AttachedMenuOptionSelectedEvent, value); }
        }
        #endregion

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="instance">detailliert anzuzeigende Instanz</param>
		public InstancePropertyPanel(ProcessInstance instance)
		{
            this.InitializeComponent();
            _processInstance = instance;

            AddMenu();
            FillContent();
            for (int i = 0; i < _processInstance.BoundModel.InstanceList.Count; i++)
            {
                if (_processInstance.BoundModel.InstanceList[i].Equals(_processInstance))
                {
                    CurrentNavigation = i;
                }
            }
            UpdateNavigation();

            _original_DataGrid_Height = MainGrid.RowDefinitions[1].Height.Value;
            _original_HistoryGrid_Height = MainGrid.RowDefinitions[2].Height.Value;
            _original_ModelGrid_Height = MainGrid.RowDefinitions[3].Height.Value;
		}

        #region Methoden

        /// <summary>
        /// Speicher originale Gridhöhen
        /// </summary>
        public void SetInfoGridHeights()
        {
            _original_DataInfoGrid_Height = Data_InfoGrid.ActualHeight + Data_InfoGrid.Margin.Top + Data_InfoGrid.Margin.Bottom;
            _original_HistoryInfoGrid_Height = History_InfoGrid.ActualHeight;
            _original_ModelInfoGrid_Height = Model_InfoGrid.ActualHeight;

            _currentHeightRestriction = this.ActualHeight;
            // _originalMainGridFirstColumnWidth = MainGrid.ColumnDefinitions[0].ActualWidth;
        }

        /// <summary>
        /// Aktualisiere Inhalt des Panels
        /// </summary>
        private void FillContent()
        {
            TitleHeader.Text = _processInstance.Name;
            ModelNameText.Text = _processInstance.Name;
            ModelText.Text = _processInstance.BoundModel.Name;
            IdText.Text = _processInstance.Id;
            StateText.Text = _processInstance.State.ToString();
            CStepText_P.Text = _processInstance.SubstepList[0].Name;
            CStepText.Text = _processInstance.SubstepList[0].Name;
            NStepText_P.Text = _processInstance.SubstepList[1].Name;

            //StartTimeText.Text = _processInstance.BoundModel.Name;
            //EndTimeText.Text = _processInstance.Id;

            int parameterCounter = 0;
            for (int i = 0; i < _processInstance.PortList.Count; i++)
            {
                if (_processInstance.PortList[i].GetType().Equals(typeof(Prototype_05.InstanceData.InstanceStartDataPort)))
                {
                    Data_InfoGrid.RowDefinitions[9+parameterCounter].Height = new GridLength(23);
                    if (parameterCounter == 0)
                    {
                        ParameterTitle1.Text = ((InstanceStartDataPort)_processInstance.PortList[i]).DataTitle;
                        ParameterValue1.Text = ((InstanceStartDataPort)_processInstance.PortList[i]).DataValue;
                    }
                    else if (parameterCounter == 1)
                    {
                        ParameterTitle2.Text = ((InstanceStartDataPort)_processInstance.PortList[i]).DataTitle;
                        ParameterValue2.Text = ((InstanceStartDataPort)_processInstance.PortList[i]).DataValue;
                    }
                    else if (parameterCounter == 2)
                    {
                        ParameterTitle3.Text = ((InstanceStartDataPort)_processInstance.PortList[i]).DataTitle;
                        ParameterValue3.Text = ((InstanceStartDataPort)_processInstance.PortList[i]).DataValue;
                    }                    
                    parameterCounter++;
                }
            }
            if (parameterCounter != 0)
            {
                Data_InfoGrid.RowDefinitions[8].Height = new GridLength(20);
                double additionalHeight = 20 + parameterCounter * 23;
                this.Height = this.Height + additionalHeight;
                MainGrid.RowDefinitions[1].Height = new GridLength(254 + additionalHeight);
                BackgroundPathBottomLeftCorner.Point = new Point(BackgroundPathBottomLeftCorner.Point.X, BackgroundPathBottomLeftCorner.Point.Y + additionalHeight);
                BackgroundPathBottomRightCorner.Point = new Point(BackgroundPathBottomRightCorner.Point.X, BackgroundPathBottomRightCorner.Point.Y + additionalHeight);
            }

            GenerateHistoryContent();
        }

        /// <summary>
        /// Aktualisiere in Navigation angezeigte Instanzen
        /// </summary>
        public void UpdateNavigation()
        {
            int numberOfInstances = _processInstance.BoundModel.InstanceList.Count;
            CategoryGrid.Height = 40 * (numberOfInstances + 1);
            int initialRowAmount = CategoryGrid.RowDefinitions.Count;
            foreach (Rectangle rectangle in NavigationRectangleList)
            {
                CategoryGrid.Children.Remove(rectangle);
            }
            foreach (TextBox textbox in NavigationTextBoxList)
            {
                CategoryGrid.Children.Remove(textbox);
            }
            NavigationRectangleList.Clear();
            NavigationTextBoxList.Clear();

            for (int rows = 1; rows < initialRowAmount; rows++)
            {
                CategoryGrid.RowDefinitions.RemoveAt(1);
            }

            for (int i = 1; i <= numberOfInstances; i++)
            {
                ProcessInstance instance = _processInstance.BoundModel.InstanceList[i-1];
                CategoryGrid.RowDefinitions.Add(new RowDefinition());
                Rectangle rect = new Rectangle();
                
                Grid.SetRow(rect, i);
                CategoryGrid.Children.Add(rect);
                TextBox textbox = new TextBox();
                if (instance.Equals(_processInstance))
                {
                    textbox.Style = (Style)FindResource("TextBox_Instance_EqualInstance_Title");
                    rect.Style = (Style)FindResource("InstanceDetailNavi_EqualInstance_Rectangle");
                }
                else
                {
                    textbox.Style = (Style)FindResource("TextBox_InstanceTitle");
                    rect.Style = (Style)FindResource("InstanceDetailNavi_Rectangle");
                }
                textbox.Text = "Instance " + instance.Id;
                Grid.SetRow(textbox, i);
                CategoryGrid.Children.Add(textbox);
                NavigationRectangleList.Add(rect);
                NavigationTextBoxList.Add(textbox);

                rect.Name = "Rectangle" + UniqueRectangleID++;
                this.RegisterName(rect.Name, rect);
                textbox.Name = "TextBox" + UniqueTextBoxID++;
                this.RegisterName(textbox.Name, textbox);
            }
        }

        /// <summary>
        /// Platzhalter-Daten für Schritt-Historie
        /// </summary>
        private void GenerateHistoryContent()
        {
            ISP1.AdaptContent("Name of Previous Step", "12.09", "12.13", "Device XX", false, "", "");
            ISP2.AdaptContent("Name of PrePrevious Step", "11.06", "12.09", "Device XW", false, "", "");
            ISP3.AdaptContent("Name of PrePrePrevious Step", "11.00", "11.06", "Device XV", true, "(In) Parameter Name", "Assigned Value");
        }

        /// <summary>
        /// Erzeuge zugehöriges Menü
        /// </summary>
        /// <returns>erzeugtes Menü</returns>
        private Menu AddMenu()
        {
            MenuTwoOptions InstancePanelMenu = new MenuTwoOptions(2, false);

            InstancePanelMenu.SetOptions("Control this instance", "Close this Panel", "../Images/controlinst.png", "../Images/wloeschselect.png");
            InstancePanelMenu.AdaptMenuColorToInstanceDetail();
            // ModelItemMenu.MenuOptionSelected += new RoutedEventHandler(ModelItemMenuOptionSelected);
            InstancePanelMenu.HorizontalAlignment = HorizontalAlignment.Left;
            InstancePanelMenu.VerticalAlignment = VerticalAlignment.Top;
            InstancePanelMenu.Margin = new Thickness(390 - InstancePanelMenu.MenuButtonWidth, 0, 0, 0);
            _attachedMenu = InstancePanelMenu;
            Grid.SetRowSpan(InstancePanelMenu, 3);
            Grid.SetColumn(InstancePanelMenu, 1);
            Grid.SetColumnSpan(InstancePanelMenu, 3);
            MainGrid.Children.Add(InstancePanelMenu);

            // _attachedMenu.MenuOptionSelected += new RoutedEventHandler(AttachedMenu_MenuOptionSelected);
            ((MenuTwoOptions)_attachedMenu).OptionWasChosenEvent += new MenuTwoOptions.SendChosenOptionEventHandler(InstancePanel_OptionWasChosenEvent);

            return InstancePanelMenu;
        }

        /// <summary>
        /// Aktualisiert Vorschau auf aktuell als nächsten aktiven Tab (je nach aktueller Fingerposition).
        /// </summary>
        /// <param name="touchPoint">Fingerposition</param>
        private void UpdateNavigationPreview(Point touchPoint)
        {
            double cellHeight = CategoryGrid.Height / CategoryGrid.RowDefinitions.Count;
            PreviewNavigation = (int)Math.Floor((touchPoint.Y - 40) / cellHeight);
            FillNavigationRectangles();
        }

        /// <summary>
        /// Aktualisiert Look der Navigations-Rechtecke.
        /// </summary>
        private void FillNavigationRectangles()
        {
            for (int i = 0; i < _processInstance.BoundModel.InstanceList.Count; i++)
            {
                if (_processInstance.BoundModel.InstanceList[i].Equals(_processInstance) || i == PreviewNavigation)
                {
                    NavigationRectangleList[i].Fill = (SolidColorBrush)FindResource("InstanceDetailNavigation_EqualInstance_Brush");
                }
                else
                {
                    NavigationRectangleList[i].Fill = (SolidColorBrush)FindResource("InstanceDetailNavigation_Brush");
                }
            }
        }

        /// <summary>
        /// Wechselt Inhalt entsprechend neuen aktiven tabs.
        /// </summary>
        /// <param name="newNavigation">neuer aktiver Tab</param>
        private void SwitchNavigation(int newNavigation)
        {
            if (PreviewNavigation == -1) return;
            _processInstance = _processInstance.BoundModel.InstanceList[newNavigation];

            for (int i = 0; i < _processInstance.BoundModel.InstanceList.Count; i++)
            {
                if (_processInstance.BoundModel.InstanceList[i].Equals(_processInstance))
                {
                    NavigationRectangleList[i].Fill = (SolidColorBrush)FindResource("InstanceDetailNavigation_EqualInstance_Brush");
                }
                else
                {
                    NavigationRectangleList[i].Fill = (SolidColorBrush)FindResource("InstanceDetailNavigation_Brush");
                }
            }

            CreateReduceStoryBoard(NavigationRectangleList[CurrentNavigation], NavigationTextBoxList[CurrentNavigation]);
            CreateExpandStoryBoard(NavigationRectangleList[newNavigation], NavigationTextBoxList[newNavigation]);
            CurrentNavigation = newNavigation;
            PreviewNavigation = -1;
            FillContent();
        }

        /// <summary>
        /// Erzeugt Bewegungsanimation für Tab-Aktivierung.
        /// </summary>
        /// <param name="rect">zu bewegendes Rechteck</param>
        /// <param name="textbox">zu bewegende Textbox</param>
        private void CreateExpandStoryBoard(Rectangle rect, TextBox textbox)
        {
            DoubleAnimation expandAnimation = new DoubleAnimation();
            expandAnimation.From = 105;
            expandAnimation.To = 120;
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
        /// Erzeugt Bewegungsanimation für Tab-Deaktivierung.
        /// </summary>
        /// <param name="rect">zu bewegendes Rechteck</param>
        /// <param name="textbox">zu bewegende Textbox</param>
        private void CreateReduceStoryBoard(Rectangle rect, TextBox textbox)
        {
            DoubleAnimation reduceAnimation = new DoubleAnimation();
            reduceAnimation.From = 120;
            reduceAnimation.To = 105;
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

        /// <summary>
        /// Speichert Eltern-ScatterViewItem und Klappt einige Abschnitte ein.
        /// </summary>
        /// <param name="svi">Eltern.ScatterViewItem</param>
        public void SetParentSVI(ScatterViewItem svi)
        {
            _parentSVI = svi;
            SetInfoGridHeights();
            ReduceHistoryArea();
            ReduceModelArea();
        }

        /// <summary>
        /// Klappt Data-Abschnitt aus.
        /// </summary>
        public void ExpandDataArea()
        {
            Data_InfoGrid.Visibility = System.Windows.Visibility.Visible;
            DataGrid.RowDefinitions[1].Height = new GridLength(_original_DataInfoGrid_Height);
            Data_InfoGrid.Height = _original_DataInfoGrid_Height - Data_InfoGrid.Margin.Top - Data_InfoGrid.Margin.Bottom;
            DataGrid.Height = _original_DataGrid_Height;
            MainGrid.RowDefinitions[1].Height = new GridLength(_original_DataGrid_Height);

            this.Height = this.Height + _original_DataInfoGrid_Height;
            _parentSVI.Center = new Point(_parentSVI.Center.X, _parentSVI.Center.Y + ((this.Height - _parentSVI.Height) / 2));
            _parentSVI.Height = this.Height;
            _currentHeightRestriction = this.Height;

            TurnDataPaths(_dataExpanded);
            _dataExpanded = true;
        }

        /// <summary>
        /// Klappt Data-Abschnitt ein.
        /// </summary>
        public void ReduceDataArea()
        {
            Data_InfoGrid.Visibility = System.Windows.Visibility.Collapsed;
            DataGrid.RowDefinitions[1].Height = new GridLength(0);
            Data_InfoGrid.Height = 0;
            DataGrid.Height = _original_DataGrid_Height - _original_DataInfoGrid_Height;
            MainGrid.RowDefinitions[1].Height = new GridLength(_original_DataGrid_Height - _original_DataInfoGrid_Height);

            this.Height = this.Height - _original_DataInfoGrid_Height;
            _parentSVI.Center = new Point(_parentSVI.Center.X, _parentSVI.Center.Y - ((_parentSVI.Height - this.Height) / 2));
            _parentSVI.Height = this.Height;
            _currentHeightRestriction = this.Height;

            TurnDataPaths(_dataExpanded);
            _dataExpanded = false;
        }

        /// <summary>
        /// Klappt History-Abschnitt aus.
        /// </summary>
        public void ExpandHistoryArea()
        {
            History_InfoGrid.Visibility = System.Windows.Visibility.Visible;
            HistoryGrid.RowDefinitions[1].Height = new GridLength(_original_HistoryInfoGrid_Height);
            History_InfoGrid.Height = _original_HistoryInfoGrid_Height;
            HistoryGrid.Height = _original_HistoryGrid_Height;
            MainGrid.RowDefinitions[2].Height = new GridLength(_original_HistoryGrid_Height);

            this.Height = this.Height + _original_HistoryInfoGrid_Height;
            _parentSVI.Center = new Point(_parentSVI.Center.X, _parentSVI.Center.Y + ((this.Height - _parentSVI.Height) / 2));
            _parentSVI.Height = this.Height;
            _currentHeightRestriction = this.Height;

            TurnHistoryPaths(_historyExpanded);
            _historyExpanded = true;
        }

        /// <summary>
        /// Klappt History-Abschnitt ein.
        /// </summary>
        public void ReduceHistoryArea()
        {
            History_InfoGrid.Visibility = System.Windows.Visibility.Collapsed;
            HistoryGrid.RowDefinitions[1].Height = new GridLength(0);
            History_InfoGrid.Height = 0;
            HistoryGrid.Height = _original_HistoryGrid_Height - _original_HistoryInfoGrid_Height;
            MainGrid.RowDefinitions[2].Height = new GridLength(_original_HistoryGrid_Height - _original_HistoryInfoGrid_Height);

            this.Height = this.Height - _original_HistoryInfoGrid_Height;
            _parentSVI.Center = new Point(_parentSVI.Center.X, _parentSVI.Center.Y - ((_parentSVI.Height - this.Height) / 2));
            _parentSVI.Height = this.Height;
            _currentHeightRestriction = this.Height;

            TurnHistoryPaths(_historyExpanded);
            _historyExpanded = false;
        }

        /// <summary>
        /// Klappt Model-Abschnitt aus.
        /// </summary>
        public void ExpandModelArea()
        {
            Model_InfoGrid.Visibility = System.Windows.Visibility.Visible;
            ModelGrid.RowDefinitions[1].Height = new GridLength(_original_ModelInfoGrid_Height);
            Model_InfoGrid.Height = _original_ModelInfoGrid_Height;
            ScaleGrid.Height = _original_ModelGrid_Height;
            ModelGrid.Height = _original_ModelGrid_Height;
            MainGrid.RowDefinitions[3].Height = new GridLength(_original_ModelGrid_Height);
            ScaleButton.Visibility = System.Windows.Visibility.Visible;
            // ScaleText.Visibility = System.Windows.Visibility.Visible;
            ScaleOneWayImg.Visibility = System.Windows.Visibility.Visible;
            ScaleTwoWayImg.Visibility = System.Windows.Visibility.Hidden;
            var bc = new BrushConverter();
            ScaleGrid.Background = new SolidColorBrush((Color)FindResource("InstanceDetailBackgroundColor"));

            this.Height = this.Height + _original_ModelInfoGrid_Height;
            _parentSVI.Center = new Point(_parentSVI.Center.X, _parentSVI.Center.Y + ((this.Height - _parentSVI.Height) / 2));
            _parentSVI.Height = this.Height;
            _currentHeightRestriction = this.Height;

            TurnModelPaths(_modelExpanded);
            _modelExpanded = true;
        }

        /// <summary>
        /// Klappt Model-Abschnitt ein.
        /// </summary>
        public void ReduceModelArea()
        {
            Model_InfoGrid.Visibility = System.Windows.Visibility.Hidden;
            ModelGrid.RowDefinitions[1].Height = new GridLength(0);
            Model_InfoGrid.Height = 0;
            ScaleGrid.Height = _original_ModelGrid_Height - _original_ModelInfoGrid_Height;
            ModelGrid.Height = _original_ModelGrid_Height - _original_ModelInfoGrid_Height;
            MainGrid.RowDefinitions[3].Height = new GridLength(_original_ModelGrid_Height - _original_ModelInfoGrid_Height);
            ScaleButton.Visibility = System.Windows.Visibility.Hidden;
            ScaleGrid.Background = null;

            this.Width = 665;
            MainGrid.Width = this.Width;
            MainGrid.ColumnDefinitions[2].Width = new GridLength(30);
            MainGrid.ColumnDefinitions[1].Width = new GridLength(390);
            ScaleGrid.Width = MainGrid.ColumnDefinitions[1].Width.Value + MainGrid.ColumnDefinitions[2].Width.Value;
            ModelGrid.Width = MainGrid.ColumnDefinitions[1].Width.Value;
            Model_InfoGrid.Width = ModelGrid.Width - 30;
            // Grid.SetColumn(Model_ExpandReduceButton,0);

            this.Height = this.Height - _original_ModelInfoGrid_Height;
            _parentSVI.Center = new Point(_parentSVI.ActualCenter.X - ((_parentSVI.Width - this.Width) / 2), _parentSVI.ActualCenter.Y - ((_parentSVI.Height - this.Height) / 2));
            _parentSVI.Width = this.Width;
            _parentSVI.Height = this.Height;
            _currentHeightRestriction = this.Height;

            TurnModelPaths(_modelExpanded);
            _modelExpanded = false;
        }

        /// <summary>
        /// Dreht Pfeile je nachdem ob Abschnitt aus- oder eingeklappt.
        /// </summary>
        /// <param name="isExpanded"><value>true</value> wenn Abschnitt eingeklappt war.</param>
        public void TurnDataPaths(bool isExpanded)
        {
            if (isExpanded)
            {
                Data_TopArrow.RenderTransform = new RotateTransform(180, 0.5, 0.5);
                Data_BottomArrow.RenderTransform = new RotateTransform(180, 0.5, 0.5);
            }
            else
            {
                Data_TopArrow.RenderTransform = new RotateTransform(0, 0.5, 0.5);
                Data_BottomArrow.RenderTransform = new RotateTransform(0, 0.5, 0.5);
            }
        }

        /// <summary>
        /// Dreht Pfeile je nachdem ob Abschnitt aus- oder eingeklappt.
        /// </summary>
        /// <param name="isExpanded"><value>true</value> wenn Abschnitt eingeklappt war.</param>
        public void TurnHistoryPaths(bool isExpanded)
        {
            if (isExpanded)
            {
                History_TopArrow.RenderTransform = new RotateTransform(180, 0.5, 0.5);
                History_BottomArrow.RenderTransform = new RotateTransform(180, 0.5, 0.5);
            }
            else
            {
                History_TopArrow.RenderTransform = new RotateTransform(0, 0.5, 0.5);
                History_BottomArrow.RenderTransform = new RotateTransform(0, 0.5, 0.5);
            }
        }

        /// <summary>
        /// Dreht Pfeile je nachdem ob Abschnitt aus- oder eingeklappt.
        /// </summary>
        /// <param name="isExpanded"><value>true</value> wenn Abschnitt eingeklappt war.</param>
        public void TurnModelPaths(bool isExpanded)
        {
            if (isExpanded)
            {
                Model_TopArrow.RenderTransform = new RotateTransform(180, 0.5, 0.5);
                Model_BottomArrow.RenderTransform = new RotateTransform(180, 0.5, 0.5);
            }
            else
            {
                Model_TopArrow.RenderTransform = new RotateTransform(0, 0.5, 0.5);
                Model_BottomArrow.RenderTransform = new RotateTransform(0, 0.5, 0.5);
            }
        }

        /// <summary>
        /// Passt Spaltenbreite je nach Skalierung an.
        /// </summary>
        private void AdaptMainGridCol2Width()
        {
            double column2Width = 155 - MainGrid.ColumnDefinitions[2].Width.Value;
            if (column2Width < 0)
                MainGrid.ColumnDefinitions[3].Width = new GridLength(0);
            else
                MainGrid.ColumnDefinitions[3].Width = new GridLength(column2Width);
        }
        #endregion

        #region Events

        /// <summary>
        /// Event wird aufgerufen wenn Menüeintrag gewählt wurde.
        /// Feuert Event für SurfaceWindow inkl Fingerorientierung und gewählter Option.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void InstancePanel_OptionWasChosenEvent(object sender, MenuTwoOptions.ChosenOptionEventArgs e)
        {
            OptionWasChosenEvent(this, new InstanceAndChosenOptionEventArgs(e.ChosenOption, e.TouchOrientation, ProcessInstance));
        }

        /// <summary>    
        /// Event wird aufgerufen wenn Menüeintrag gewählt wurde.
        /// Leitet Event an SurfaceWindow weiter
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void AttachedMenu_MenuOptionSelected(object sender, RoutedEventArgs e)
        {
            // pass event to SurfaceWindow1.xaml.cs
            RoutedEventArgs eventargs = new RoutedEventArgs(InstancePropertyPanel.AttachedMenuOptionSelectedEvent);
            RaiseEvent(eventargs);
        }

        /// <summary>
        /// Event wird aufgerufen wenn Tap auf Schaltfläche zum ein- oder Ausklappen des Abschnittes.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void DataExpandReduceClick(object sender, RoutedEventArgs e)
        {
            if (_dataExpanded)
                ReduceDataArea();
            else
                ExpandDataArea();
        }

        /// <summary>
        /// Event wird aufgerufen wenn Tap auf Schaltfläche zum ein- oder Ausklappen des Abschnittes.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void HistoryExpandReduceClick(object sender, RoutedEventArgs e)
        {
            if (_historyExpanded)
                ReduceHistoryArea();
            else
                ExpandHistoryArea();
        }

        /// <summary>
        /// Event wird aufgerufen wenn Tap auf Schaltfläche zum ein- oder Ausklappen des Abschnittes.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void ModelExpandReduceClick(object sender, RoutedEventArgs e)
        {
            if (_modelExpanded)
                ReduceModelArea();
            else
                ExpandModelArea();
        }

        /// <summary>
        /// Event wird aufgerufen wenn TouchDown auf Skalieren-Schaltfläche.
        /// Speichert Touchpunkte.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void ScaleButtonDown(object sender, TouchEventArgs e)
        {
            // Capture to the ScaleButton.  
            e.TouchDevice.Capture(this.ScaleButton);

            // Remember this contact if a contact has not been remembered already.  
            // This contact is then used to move the ellipse around.
            if (_scaleControlTouchDevice == null)
            {
                _scaleControlTouchDevice = e.TouchDevice;

                // Remember where this contact took place.  
                _lastPoint = _scaleControlTouchDevice.GetTouchPoint(this.MainGrid).Position;
                _lastValidScalePoint = _lastPoint;
                // Console.WriteLine("first Point: (" + _lastPoint.X + ", " + _lastPoint.Y + " )");
            }

            _parentSVI.CanMove = false;

            // Mark this event as handled.  
            e.Handled = true;
        }

        /// <summary>
        /// Event wird aufgerufen wenn TouchMove auf Skalieren-Schaltfläche.
        /// Berechnet Skalierungsfaktor und speichert neue Touchpunkte.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void ScaleButtonMoved(object sender, TouchEventArgs e)
        {
            bool TwoWayScalePossibleX = true;
            bool TwoWayScalePossibleY = true;

            if (e.TouchDevice == _scaleControlTouchDevice)
            {
                // Get the current position of the contact.  
                Point currentTouchPoint = _scaleControlTouchDevice.GetCenterPosition(this.MainGrid);
                // Point currentTouchPoint = _scaleControlTouchDevice.GetTouchPoint(this.MainGrid).Position;

                // Get the change between the controlling contact point and
                // the changed contact point.  
                double deltaX = currentTouchPoint.X - _lastPoint.X;
                double deltaY = currentTouchPoint.Y - _lastPoint.Y;

                Point backupLastValidScalePoint = _lastValidScalePoint;


                // Width...

                UpdateLayout();
                double widthAndDeltaX = this.Width + deltaX;

                if (widthAndDeltaX > 665)
                {
                    if (!(_lastPoint.X < _lastValidScalePoint.X))
                    {
                        // 1
                        this.Width = widthAndDeltaX;
                        MainGrid.ColumnDefinitions[2].Width = new GridLength(MainGrid.ColumnDefinitions[2].ActualWidth + deltaX);
                        AdaptMainGridCol2Width();
                        _parentSVI.Center = new Point(_parentSVI.Center.X + (deltaX / 2), _parentSVI.Center.Y);

                        _lastValidScalePoint.X = currentTouchPoint.X;
                        // Console.WriteLine("Width Fall #1");
                    }
                    else if (deltaX > (_lastValidScalePoint.X - _lastPoint.X))
                    {
                        // 2
                        double diffDeltaXLastPoints = deltaX - (_lastValidScalePoint.X - _lastPoint.X);

                        this.Width = this.Width + diffDeltaXLastPoints;
                        MainGrid.ColumnDefinitions[2].Width = new GridLength(MainGrid.ColumnDefinitions[2].ActualWidth + diffDeltaXLastPoints);
                        AdaptMainGridCol2Width();
                        _parentSVI.Center = new Point(_parentSVI.Center.X + (diffDeltaXLastPoints / 2), _parentSVI.Center.Y);

                        _lastValidScalePoint.X = currentTouchPoint.X;
                        // Console.WriteLine("Width Fall #2");
                    }
                    else
                    {
                        // 3
                        this.Width = 665;
                        MainGrid.ColumnDefinitions[2].Width = new GridLength(30);
                        AdaptMainGridCol2Width();
                        _parentSVI.Center = new Point(_parentSVI.Center.X, _parentSVI.Center.Y);

                        _lastValidScalePoint.X = _lastValidScalePoint.X;
                        TwoWayScalePossibleX = false;
                        // Console.WriteLine("Width Fall #3");
                    }
                }
                else
                {
                    // 4
                    double diffDeltaXWidths = deltaX + (665 - widthAndDeltaX);

                    this.Width = 665;
                    MainGrid.ColumnDefinitions[2].Width = new GridLength(30);
                    AdaptMainGridCol2Width();
                    _parentSVI.Center = new Point(_parentSVI.Center.X + (diffDeltaXWidths / 2), _parentSVI.Center.Y);

                    _lastValidScalePoint.X = _lastValidScalePoint.X + diffDeltaXWidths;
                    TwoWayScalePossibleX = false;
                    // Console.WriteLine("Width Fall #4");
                }



                // Height...

                double heightAndDeltaY = this.Height + deltaY;

                if (heightAndDeltaY > _currentHeightRestriction)
                {
                    if (!(_lastPoint.Y < _lastValidScalePoint.Y))
                    {
                        // 1
                        this.Height = heightAndDeltaY;
                        MainGrid.RowDefinitions[3].Height = new GridLength(MainGrid.RowDefinitions[3].ActualHeight + deltaY);
                        _parentSVI.Center = new Point(_parentSVI.Center.X, _parentSVI.Center.Y + (deltaY / 2));

                        _lastValidScalePoint.Y = currentTouchPoint.Y;
                        // Console.WriteLine("Height Fall #1");
                    }
                    else if (deltaY > (_lastValidScalePoint.Y - _lastPoint.Y))
                    {
                        // 2
                        double diffDeltaYLastPoints = deltaY - (_lastValidScalePoint.Y - _lastPoint.Y);

                        this.Height = this.Height + diffDeltaYLastPoints;
                        MainGrid.RowDefinitions[3].Height = new GridLength(MainGrid.RowDefinitions[3].ActualHeight + diffDeltaYLastPoints);
                        _parentSVI.Center = new Point(_parentSVI.Center.X, _parentSVI.Center.Y + (diffDeltaYLastPoints / 2));

                        _lastValidScalePoint.Y = currentTouchPoint.Y;
                        // Console.WriteLine("Height Fall #2");
                    }
                    else
                    {
                        // 3
                        this.Height = _currentHeightRestriction;
                        MainGrid.RowDefinitions[3].Height = new GridLength(200);
                        _parentSVI.Center = new Point(_parentSVI.Center.X, _parentSVI.Center.Y);

                        _lastValidScalePoint.Y = _lastValidScalePoint.Y;
                        TwoWayScalePossibleY = false;
                        // Console.WriteLine("Height Fall #3");
                    }
                }
                else
                {
                    // 4
                    double diffDeltaYHeights = deltaY + (_currentHeightRestriction - heightAndDeltaY);

                    this.Height = _currentHeightRestriction;
                    MainGrid.RowDefinitions[3].Height = new GridLength(200);
                    _parentSVI.Center = new Point(_parentSVI.Center.X, _parentSVI.Center.Y + (diffDeltaYHeights / 2));

                    _lastValidScalePoint.Y = _lastValidScalePoint.Y + diffDeltaYHeights;
                    TwoWayScalePossibleY = false;
                    // Console.WriteLine("Height Fall #4");
                }
                // UpdateLayout();
                MainGrid.Width = this.Width;
                MainGrid.ColumnDefinitions[1].Width = new GridLength(390);
                ScaleGrid.Height = MainGrid.RowDefinitions[3].Height.Value;
                ModelGrid.Height = MainGrid.RowDefinitions[3].Height.Value;

                //ScaleGrid.Width = MainGrid.Width;
                //ModelGrid.Width = MainGrid.Width;
                ScaleGrid.Width = MainGrid.ColumnDefinitions[1].Width.Value + MainGrid.ColumnDefinitions[2].Width.Value;
                ModelGrid.Width = ScaleGrid.Width - 30;

                ModelGrid.RowDefinitions[1].Height = new GridLength(ModelGrid.Height - 60);
                Model_InfoGrid.Height = ModelGrid.Height - 60;

                Model_InfoGrid.Width = ModelGrid.Width - 30;

                if (Model_InfoGrid.Width > 570) ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2UpInstanceText.png", UriKind.Relative));
                else ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2UpInstanceBlank.png", UriKind.Relative));

                _parentSVI.Height = this.Height;
                _parentSVI.Width = this.Width;

                if (!TwoWayScalePossibleX && !TwoWayScalePossibleY)
                {
                    ScaleOneWayImg.Visibility = System.Windows.Visibility.Visible;
                    ScaleTwoWayImg.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    ScaleOneWayImg.Visibility = System.Windows.Visibility.Hidden;
                    ScaleTwoWayImg.Visibility = System.Windows.Visibility.Visible;
                }

                _lastPoint.X = currentTouchPoint.X;
                _lastPoint.Y = currentTouchPoint.Y;

                // Mark this event as handled.  
                e.Handled = true;
            }
        }

        /// <summary>
        /// Event wird aufgerufen wenn TouchUp auf Skalieren-Schaltfläche.
        /// Gibt Bewegung und Rotierung für Eltern-Svcatterviewitem wieder frei.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void ScaleButtonUp(object sender, TouchEventArgs e)
        {
            // If this contact is the one that was remembered  
            if (e.TouchDevice == _scaleControlTouchDevice)
            {
                // Forget about this contact.
                _scaleControlTouchDevice = null;

                _parentSVI.CanMove = true;
            }

            // Mark this event as handled.  
            e.Handled = true;
        }

        /// <summary>
        /// TouchDown auf Naviagtionsgrid.
        /// Überprüft Position. Bricht ab wenn Positionierung über All-Instances, da bisher Inhalt für aktivierten All-Tab nicht existiert.
        /// Gibt visuelles Feedback.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void NavigationTouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            // Capture to the ScaleButton.  
            e.TouchDevice.Capture(this.CategoryGrid);
            if (e.TouchDevice.GetTouchPoint(this.CategoryGrid).Position.Y < 40) return;

            // Remember this contact if a contact has not been remembered already.  
            // This contact is then used to move the ellipse around.
            if (NavigationTouchDevice == null)
            {
                NavigationTouchDevice = e.TouchDevice;
                UpdateNavigationPreview(NavigationTouchDevice.GetTouchPoint(this.CategoryGrid).Position);
            }
            // Mark this event as handled.  
            e.Handled = true;
        }

        /// <summary>
        /// TouchMOve auf Naviagtionsgrid.
        /// Gibt visuelles Feedback.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void NavigationTouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
            if (e.TouchDevice == NavigationTouchDevice)
            {
                UpdateNavigationPreview(NavigationTouchDevice.GetTouchPoint(this.CategoryGrid).Position);
            }
            // Mark this event as handled.  
            e.Handled = true;
        }

        /// <summary>
        /// TouchMOve auf Naviagtionsgrid.
        /// Wechselt aktiven Tab entsprechend Fingerpsoition.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void NavigationTouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            // If this contact is the one that was remembered  
            if (e.TouchDevice == NavigationTouchDevice)
            {
                // Forget about this contact.
                NavigationTouchDevice = null;

                if (CurrentNavigation != PreviewNavigation)
                {
                    SwitchNavigation(PreviewNavigation);
                }
            }
            // Mark this event as handled.  
            e.Handled = true;
        }

        /// <summary>
        /// Tap auf Modellname.
        /// Prüfen ob Double-Tap. Wenn ja Aufrufen des ModelPropertyPanel.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void BoundModelClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((DateTime.Now.Subtract(ModelTouchDownTimeForDoubleTap)).Seconds < 0.600)
            {
                // pass event to SurfaceWindow1.xaml.cs
                DoubleTapOnModelName(this, new ShowModelPanelEventArgs(_processInstance.BoundModel));
            }
            ModelTouchDownTimeForDoubleTap = DateTime.Now;
        }

        /// <summary>
        /// Tap auf Aktuellen Schritt.
        /// Prüfen ob Double-Tap. Wenn ja Aufrufen des InstancePropertyPanel.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void CurrentStepClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((DateTime.Now.Subtract(CurrentStepTouchDownTimeForDoubleTap)).Seconds < 0.600)
            {
                // pass event to SurfaceWindow1.xaml.cs
                try
                {
                    DoubleTapOnCurrentStep(this, new ShowStepInstancePanelEventArgs(_processInstance.SubstepList[0]));
                }
                catch (ArgumentOutOfRangeException)
                {
                    CurrentStep_Data_Button.IsHitTestVisible = false;
                    CurrentStep_Protocol_Button.IsHitTestVisible = false;
                    CStepText.TextDecorations = null;
                    CStepText_P.TextDecorations = null;
                }
            }
            CurrentStepTouchDownTimeForDoubleTap = DateTime.Now;
        }

        /// <summary>
        /// Tap auf nächsten Schritt.
        /// Prüfen ob Double-Tap. Wenn ja Aufrufen des InstancePropertyPanel.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void NextStepClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((DateTime.Now.Subtract(NextStepTouchDownTimeForDoubleTap)).Seconds < 0.600)
            {
                // pass event to SurfaceWindow1.xaml.cs
                try
                {
                    DoubleTapOnNextStep(this, new ShowStepInstancePanelEventArgs(_processInstance.SubstepList[1]));
                }
                catch (ArgumentOutOfRangeException)
                {
                    NextStep_Protocol_Button.IsHitTestVisible = false;
                    NStepText_P.TextDecorations = null;
                }
            }
            NextStepTouchDownTimeForDoubleTap = DateTime.Now;
        }
        #endregion


        /// <summary>
        /// Klasse für eigene Eventparameterübergabe inkl gewähltem MEnüeintrag und Fingerorientierung bei Auswahl.
        /// </summary>
        public class InstanceAndChosenOptionEventArgs : EventArgs
        {
            public readonly MenuTwoOptions.ChosenOption ChosenOption;
            public readonly double TouchOrientation;
            public readonly ProcessInstance Instance;

            public InstanceAndChosenOptionEventArgs(MenuTwoOptions.ChosenOption chosenOption, double touchOrientation, ProcessInstance instance)
            {
                ChosenOption = chosenOption;
                TouchOrientation = touchOrientation;
                Instance = instance;
            }
        }

        /// <summary>
        /// Klasse für eigene Eventparameterübergabe wenn Modelpropertypanel gezeigt werden soll.
        /// </summary>
        public class ShowModelPanelEventArgs : EventArgs
        {
            public readonly ProcessModel Model;

            public ShowModelPanelEventArgs(ProcessModel model)
            {
                Model = model;
            }
        }

        /// <summary>
        /// Klasse für eigene Eventparameterübergabe wenn Instanzpropertypanel gezeigt werden soll.
        /// </summary>
        public class ShowStepInstancePanelEventArgs : EventArgs
        {
            public readonly ProcessInstance Instance;

            public ShowStepInstancePanelEventArgs(ProcessInstance instance)
            {
                Instance = instance;
            }
        }
	}
}