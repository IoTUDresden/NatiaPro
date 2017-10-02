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

namespace Prototype_05
{
	/// <summary>
    /// Interaktionslogik für ModelPropertyPanel.xaml
    /// Zeigt Details eines Modells an.
	/// </summary>
    public partial class ModelPropertyPanel : UserControl
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
			get {return _attachedMenu;}
			set {_attachedMenu = value;}
        }

        protected ProcessModel _processModel;
        /// <summary>
        /// betreffendes Prozessmodell
        /// </summary>
        public ProcessModel ProcessModel
        {
            get { return _processModel; }
            set { _processModel = value; }
        }
        protected ProcessModel Hierarchy0Model;
        protected int Hierarchy1Counter = -1;

        /// <summary>
        /// Letzter Zeotpunkt eiens Taps auf Instanzanzahl.
        /// Wichtig für Berechnung ob Double-Tap.
        /// </summary>
        protected DateTime TouchDownTimeForDoubleTap;
        public delegate void ShowInstancePanelEventHandler(object sender, ShowInstancePanelEventArgs e);
        public event ShowInstancePanelEventHandler DoubleTapOnInstanceAmount;

        /// <summary>
        /// MenuOptionSelectedEvent, needed to pass event outside of ModelPropertyPanel-Class
        /// </summary>
        public static readonly RoutedEvent AttachedMenuOptionSelectedEvent = EventManager.RegisterRoutedEvent("AttachedMenuOptionSelected", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ModelPropertyPanel));
        public event RoutedEventHandler AttachedMenuOptionSelected
        {
            add { AddHandler(AttachedMenuOptionSelectedEvent, value); }
            remove { RemoveHandler(AttachedMenuOptionSelectedEvent, value); }
        }
        #endregion

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="processModel">detailliert anzuzeigendes Modell</param>
        public ModelPropertyPanel(ProcessModel processModel)
        {
            this.InitializeComponent();
            TouchDownTimeForDoubleTap = DateTime.Now;
            _processModel = processModel;
            Hierarchy0Model = processModel;

            AddMenu();
            UpdateFavouriteMenuEntry();
            FillContent();

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
        /// Aktualisiere Anzahl an Prozessinstanzen.
        /// </summary>
        public void UpdateRunningInstancesAmount()
        {
            Instances.Text = _processModel.CurrentlyExecutedInstances.ToString();
            if (_processModel.CurrentlyExecutedInstances == 0)
            {
                Instances.TextDecorations = null;
                InstancesAmountButton.IsHitTestVisible = false;
            }
            else
            {
                Instances.TextDecorations = TextDecorations.Underline;
                InstancesAmountButton.IsHitTestVisible = true;
            }
        }

        /// <summary>
        /// Aktualisiere Text und Symbol für Favoriten-Option im zugehörigen Menü.
        /// </summary>
        public void UpdateFavouriteMenuEntry()
        {
            if (_processModel.Categories.Contains(Enums.Category.Favourites))
                    ((MenuThreeOptions)_attachedMenu).SetOptions(_attachedMenu.OptionTexts[0].Text,
                        "Remove from Favourites", _attachedMenu.OptionTexts[2].Text, "../Images/play_black.png",
                        "../Images/wherz_black_crossed5.png", "../Images/wloeschselect.png");
            else
                ((MenuThreeOptions)_attachedMenu).SetOptions(_attachedMenu.OptionTexts[0].Text,
                        "Add to Favourites", _attachedMenu.OptionTexts[2].Text, "../Images/play_black.png",
                        "../Images/wherz_black.png", "../Images/wloeschselect.png");
        }
        
        /// <summary>
        /// Aktualisiere Inhalt des Panels
        /// </summary>
        private void FillContent()
        {
            TitleHeader.Text = _processModel.Name;
            TitleHeader2.Text = _processModel.Name;
            ModelNameText.Text = _processModel.Name;
            TitleData.Text = _processModel.Name;
            Id.Text = _processModel.Id;
            Type.Text = _processModel.Type;
            Random randomDurationHours = new Random();
            int hours = randomDurationHours.Next(0, 2);
            Random randomDurationMinutes = new Random();
            int minutes = randomDurationMinutes.Next(6, 54);
            Duration.Text = "0" + hours + "h " + minutes + "'";
            GenerateHistoryContent(hours, minutes);
            Instances.Text = _processModel.CurrentlyExecutedInstances.ToString();
            if (_processModel.CurrentlyExecutedInstances == 0)
            {
                Instances.TextDecorations = null;
                InstancesAmountButton.IsHitTestVisible = false;
            }           
            Categories.Text = "";
            int categories = _processModel.Categories.Count;
            for (int i=0; i<categories; i++)
            {
                if (i<categories-1)
                    Categories.Text += _processModel.Categories[i].ToString() + ", "; 
                else Categories.Text += _processModel.Categories[i].ToString();
            }

            int parameterCounter = 0;
            for (int i = 0; i < _processModel.PortList.Count; i++)
            {
                if (_processModel.PortList[i].GetType().Equals(typeof(Prototype_05.ModelData.StartDataPort)))
                {
                    Data_InfoGrid.RowDefinitions[9 + parameterCounter].Height = new GridLength(23);
                    if (parameterCounter == 0)
                    {
                        ParameterTitle1.Text = ((StartDataPort)_processModel.PortList[i]).DataTitle;
                        ParameterValue1.Text = ((StartDataPort)_processModel.PortList[i]).Data.ToString();
                    }
                    else if (parameterCounter == 1)
                    {
                        ParameterTitle2.Text = ((StartDataPort)_processModel.PortList[i]).DataTitle;
                        ParameterValue2.Text = ((StartDataPort)_processModel.PortList[i]).Data.ToString();
                    }
                    else if (parameterCounter == 2)
                    {
                        ParameterTitle3.Text = ((StartDataPort)_processModel.PortList[i]).DataTitle;
                        ParameterValue3.Text = ((StartDataPort)_processModel.PortList[i]).Data.ToString();
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

            if (_processModel.Name == "Open windows in ...")
            {
                Hierarchy0Button.Click += new RoutedEventHandler(Hierarchy0Button_Click);
                Hierarchy0Button.Visibility = System.Windows.Visibility.Visible;
                ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2MiddleModelBlank.png", UriKind.Relative));
            }
        }

        void Hierarchy0Button_Click(object sender, RoutedEventArgs e)
        {
            _processModel = Hierarchy0Model.SubstepList[2];
            Hierarchy0Button.Click -= new RoutedEventHandler(Hierarchy0Button_Click);
            Hierarchy0Button.Visibility = System.Windows.Visibility.Hidden;
            //Hierarchy1Button.Visibility = System.Windows.Visibility.Visible;
            PrevModelHierarchyButton.Visibility = System.Windows.Visibility.Visible;
            NextModelHierarchyButton.Visibility = System.Windows.Visibility.Visible;
            UpperModelHierarchyButton.Visibility = System.Windows.Visibility.Visible;
            PrevModelHierarchyButton.Click += new RoutedEventHandler(PrevModelHierarchyButton_Click);
            NextModelHierarchyButton.Click += new RoutedEventHandler(NextModelHierarchyButton_Click);
            UpperModelHierarchyButton.Click += new RoutedEventHandler(UpperModelHierarchyButton_Click);
            UpperModelHeaderButton.Click += new RoutedEventHandler(UpperModelHeaderButton_Click);
            UpperModelHierarchyText.Text = Hierarchy0Model.Name;
            UpperModelNameHeader.Text = Hierarchy0Model.Name;
            HeaderGrid.Visibility = System.Windows.Visibility.Hidden;
            HeaderGrid2.Visibility = System.Windows.Visibility.Visible;

            ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2DownModelBlank.png", UriKind.Relative));
            ModelNameText.Margin = new Thickness(40, 34, 0, 0);
            FillContent();
        }

        void UpperModelHeaderButton_Click(object sender, RoutedEventArgs e)
        {
            _processModel = Hierarchy0Model;
            ModelNameText.Margin = new Thickness(21, 5, 0, 0);
            Hierarchy0Button.Visibility = System.Windows.Visibility.Visible;
            PrevModelHierarchyButton.Visibility = System.Windows.Visibility.Hidden;
            NextModelHierarchyButton.Visibility = System.Windows.Visibility.Hidden;
            UpperModelHierarchyButton.Visibility = System.Windows.Visibility.Hidden;
            PrevModelHierarchyButton.Click -= new RoutedEventHandler(PrevModelHierarchyButton_Click);
            NextModelHierarchyButton.Click -= new RoutedEventHandler(NextModelHierarchyButton_Click);
            UpperModelHierarchyButton.Click -= new RoutedEventHandler(UpperModelHierarchyButton_Click);
            UpperModelHeaderButton.Click -= new RoutedEventHandler(UpperModelHeaderButton_Click);
            ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2MiddleModelBlank.png", UriKind.Relative));
            HeaderGrid.Visibility = System.Windows.Visibility.Visible;
            HeaderGrid2.Visibility = System.Windows.Visibility.Hidden;
            FillContent();
        }

        void UpperModelHierarchyButton_Click(object sender, RoutedEventArgs e)
        {
            _processModel = Hierarchy0Model;
            ModelNameText.Margin = new Thickness(21, 5, 0, 0);
            Hierarchy0Button.Visibility = System.Windows.Visibility.Visible;
            PrevModelHierarchyButton.Visibility = System.Windows.Visibility.Hidden;
            NextModelHierarchyButton.Visibility = System.Windows.Visibility.Hidden;
            UpperModelHierarchyButton.Visibility = System.Windows.Visibility.Hidden;
            PrevModelHierarchyButton.Click -= new RoutedEventHandler(PrevModelHierarchyButton_Click);
            NextModelHierarchyButton.Click -= new RoutedEventHandler(NextModelHierarchyButton_Click);
            UpperModelHierarchyButton.Click -= new RoutedEventHandler(UpperModelHierarchyButton_Click);
            UpperModelHeaderButton.Click -= new RoutedEventHandler(UpperModelHeaderButton_Click);
            ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2MiddleModelBlank.png", UriKind.Relative));
            HeaderGrid.Visibility = System.Windows.Visibility.Visible;
            HeaderGrid2.Visibility = System.Windows.Visibility.Hidden;
            FillContent();
        }

        void NextModelHierarchyButton_Click(object sender, RoutedEventArgs e)
        {
            ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2UpModelBlank.png", UriKind.Relative));
            PrevModelHierarchyButton.Visibility = System.Windows.Visibility.Visible;
            NextModelHierarchyButton.Visibility = System.Windows.Visibility.Visible;
            int hierarchy = 1;
            if (_processModel.Name == "DisableHeating")
            {
                hierarchy = 2;
                ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2DownModelBlank.png", UriKind.Relative));
            }
            else if (_processModel.Name == "CloseDoor")
            {
                hierarchy = 3;
                ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2DownModelBlank.png", UriKind.Relative));
            }
            else if (_processModel.Name == "DriveToWindow")
            {
                hierarchy = 4;
                ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2UpModelBlank.png", UriKind.Relative));
                NextModelHierarchyButton.Visibility = System.Windows.Visibility.Hidden;
            }
            // else ich bin im 0. Schritt, hierarchy muss also 1 sein

            _processModel = Hierarchy0Model.SubstepList[hierarchy];
            FillContent();
        }

        void PrevModelHierarchyButton_Click(object sender, RoutedEventArgs e)
        {
            ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2DownModelBlank.png", UriKind.Relative));
            PrevModelHierarchyButton.Visibility = System.Windows.Visibility.Visible;
            NextModelHierarchyButton.Visibility = System.Windows.Visibility.Visible;
            int hierarchy = 3;
            if (_processModel.Name == "DisableHeating")
            {
                hierarchy = 0;
                PrevModelHierarchyButton.Visibility = System.Windows.Visibility.Hidden;
                ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2UpModelBlank.png", UriKind.Relative));
            }
            else if (_processModel.Name == "CloseDoor")
            {
                hierarchy = 1;
                ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2DownModelBlank.png", UriKind.Relative));
            }
            else if (_processModel.Name == "DriveToWindow")
            {
                hierarchy = 2;
                ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2DownModelBlank.png", UriKind.Relative));
            }
            // else ich bin im 4. Schritt, hierarchy muss also 3 sein

            _processModel = Hierarchy0Model.SubstepList[hierarchy];
            FillContent();
        }
        
        /// <summary>
        /// Erzeuge Fake-History-Daten
        /// </summary>
        /// <param name="hours">durchschnittliche Ausführunsgdauer (stunden)</param>
        /// <param name="minutes">durchschnittliche Ausführunsgdauer (Minuten)</param>
        private void GenerateHistoryContent(int hours, int minutes)
        {
            //MHC1.AdaptContent("Mon, 18-Feb-13 - Sun, 24-Feb-13", "No Power Supply", hours, minutes);
            //MHC2.AdaptContent("Mon, 11-Feb-13 - Sun, 17-Feb-13", "Object blocked", hours, minutes);
            //MHC3.AdaptContent("Mon, 04-Feb-13 - Sun, 10-Feb-13", "No Water Supply", hours, minutes);
            MHC4.AdaptContent("Mon, 28-Jan-13 - Sun, 03-Feb-13", "Routing Problem", hours, minutes,
                MHC3.AdaptContent("Mon, 04-Feb-13 - Sun, 10-Feb-13", "No Water Supply", hours, minutes,
                    MHC2.AdaptContent("Mon, 11-Feb-13 - Sun, 17-Feb-13", "Object blocked", hours, minutes,
                        MHC1.AdaptContent("Mon, 18-Feb-13 - Sun, 24-Feb-13", "No Power Supply", hours, minutes, new int[2]{0,0}))));
        }

        /// <summary>
        /// Erzeuge zugehöriges Menü
        /// </summary>
        /// <returns>erzeugtes Menü</returns>
        private Menu AddMenu()
        {
            MenuThreeOptions ModelItemMenu = new MenuThreeOptions(3, false);
            
            // Make dependent from Favourite-State
            ModelItemMenu.SetOptions("Start Process", "Add to Favourites", "Close this Panel", "../Images/play_black.png", "../Images/wherz_black.png", "../Images/wloeschselect.png");
            ModelItemMenu.AdaptMenuColorToModelDetail();
            // ModelItemMenu.MenuOptionSelected += new RoutedEventHandler(ModelItemMenuOptionSelected);
            ModelItemMenu.HorizontalAlignment = HorizontalAlignment.Left;
            ModelItemMenu.VerticalAlignment = VerticalAlignment.Top;
            ModelItemMenu.Margin = new Thickness(390 - ModelItemMenu.MenuButtonWidth,0,0,0);
            _attachedMenu = ModelItemMenu;
            Grid.SetRowSpan(ModelItemMenu, 2);
            Grid.SetColumnSpan(ModelItemMenu, 3);
            MainGrid.Children.Add(ModelItemMenu);
            
            _attachedMenu.MenuOptionSelected += new RoutedEventHandler(AttachedMenu_MenuOptionSelected);
            ((MenuThreeOptions)_attachedMenu).OptionWasChosenEvent += new MenuThreeOptions.SendChosenOptionEventHandler(ModelItem_OptionWasChosenEvent);

            return ModelItemMenu;
        }

        /// <summary>
        /// Speichert Eltern-ScatterViewItem und Klappt einige Abschnitte ein.
        /// </summary>
        /// <param name="svi">Eltern.ScatterViewItem</param>
        public void SetParentSVIAndReduceAreas(ScatterViewItem svi)
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
            ScaleGrid.Background = (Brush)bc.ConvertFrom("#FF1E311C");

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

            this.Width = 545;
            MainGrid.Width = this.Width;
            MainGrid.ColumnDefinitions[1].Width = new GridLength(30);
            MainGrid.ColumnDefinitions[0].Width = new GridLength(390);
            ScaleGrid.Width = MainGrid.ColumnDefinitions[0].Width.Value + MainGrid.ColumnDefinitions[1].Width.Value;
            ModelGrid.Width = MainGrid.ColumnDefinitions[0].Width.Value;
            Model_InfoGrid.Width = ModelGrid.Width;// - 30;
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
            double column2Width = 155 - MainGrid.ColumnDefinitions[1].Width.Value;
            if (column2Width < 0)
                MainGrid.ColumnDefinitions[2].Width = new GridLength(0);
            else
                MainGrid.ColumnDefinitions[2].Width = new GridLength(column2Width);
        }
        #endregion

        #region Events
        /// <summary>
        /// Tap auf Instanzanzahl.
        /// Prüfen ob Double-Tap. Wenn ja Aufrufen des InstancePropertyPanel.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void InstanceAmountClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((DateTime.Now.Subtract(TouchDownTimeForDoubleTap)).Seconds < 0.600)
            {
                // pass event to SurfaceWindow1.xaml.cs
                DoubleTapOnInstanceAmount(this, new ShowInstancePanelEventArgs(_processModel.InstanceList[0]));
            }
            TouchDownTimeForDoubleTap = DateTime.Now;
        }

        /// <summary>
        /// Event wird aufgerufen wenn Menüeintrag gewählt wurde.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void ModelItem_OptionWasChosenEvent(object sender, MenuThreeOptions.ChosenOptionEventArgs e)
        {
            // do nothing here so long as processmodel or touchorientation are not of interest
        }

        /// <summary>
        /// Event wird aufgerufen wenn Menüeintrag gewählt wurde.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void AttachedMenu_MenuOptionSelected(object sender, RoutedEventArgs e)
        {
            // pass event to SurfaceWindow1.xaml.cs
            RoutedEventArgs eventargs = new RoutedEventArgs(ModelPropertyPanel.AttachedMenuOptionSelectedEvent);
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

                if (widthAndDeltaX > 545)
                {
                    if (!(_lastPoint.X < _lastValidScalePoint.X))
                    {
                        // 1
                        this.Width = widthAndDeltaX;
                        MainGrid.ColumnDefinitions[1].Width = new GridLength(MainGrid.ColumnDefinitions[1].ActualWidth + deltaX);
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
                        MainGrid.ColumnDefinitions[1].Width = new GridLength(MainGrid.ColumnDefinitions[1].ActualWidth + diffDeltaXLastPoints);
                        AdaptMainGridCol2Width();
                        _parentSVI.Center = new Point(_parentSVI.Center.X + (diffDeltaXLastPoints / 2), _parentSVI.Center.Y);

                        _lastValidScalePoint.X = currentTouchPoint.X;
                        // Console.WriteLine("Width Fall #2");
                    }
                    else
                    {
                        // 3
                        this.Width = 545;
                        MainGrid.ColumnDefinitions[1].Width = new GridLength(30);
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
                    double diffDeltaXWidths = deltaX + (545 - widthAndDeltaX);

                    this.Width = 545;
                    MainGrid.ColumnDefinitions[1].Width = new GridLength(30);
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
                MainGrid.ColumnDefinitions[0].Width = new GridLength(390);
                ScaleGrid.Height = MainGrid.RowDefinitions[3].Height.Value;
                ModelGrid.Height = MainGrid.RowDefinitions[3].Height.Value;

                ScaleGrid.Width = MainGrid.ColumnDefinitions[0].Width.Value + MainGrid.ColumnDefinitions[1].Width.Value;
                ModelGrid.Width = ScaleGrid.Width - 30;

                ModelGrid.RowDefinitions[1].Height = new GridLength(ModelGrid.Height - 60);
                Model_InfoGrid.Height = ModelGrid.Height - 60;

                Model_InfoGrid.Width = ModelGrid.Width; // -30;

                _parentSVI.Height = this.Height;
                _parentSVI.Width = this.Width;

                if (Hierarchy0Model.Name != "Open windows in ...")
                {
                    if (Model_InfoGrid.Width > 570) ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2UpModelText.png", UriKind.Relative));
                    else ModelImg.Source = new BitmapImage(new Uri(@"../Images/hierarchy2UpModelBlank.png", UriKind.Relative));
                }

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
        #endregion
		
	}

    /// <summary>
    /// Klasse für eigene Eventparameterübergabe wenn Instanzpropertypanel gezeigt werden soll.
    /// </summary>
    public class ShowInstancePanelEventArgs : EventArgs
    {
        public readonly ProcessInstance Instance;

        public ShowInstancePanelEventArgs(ProcessInstance instance)
        {
            Instance = instance;
        }
    }
}