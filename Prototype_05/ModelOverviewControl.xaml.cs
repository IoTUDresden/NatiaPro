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
using System.Collections.ObjectModel;
using Prototype_05.ModelData;
using System.Windows.Media.Animation;
using System.ComponentModel;
using System.Collections.Specialized;
using log4net;
using System.Reflection;

namespace Prototype_05
{
	/// <summary>
	/// Interaktionslogik für ModelOverviewControl.xaml
    /// Zeigt Übersicht über Prozessmodelle.
	/// </summary>
	public partial class ModelOverviewControl : UserControl
    {
        #region Deklarationen

        /// <summary>
        /// Globaler Logdienst
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Enum-Definitionen für aufrufbare Panels.
        /// </summary>
        public enum ChosenPanel { None, Add, Remove }
        protected ChosenPanel _chosenPanel = ChosenPanel.None;
        /// <summary>
        /// Aktuell gewähltes Panel, das angezeigt werden soll.
        /// </summary>
        public ChosenPanel SelectedPanel
        {
            get { return _chosenPanel; }
            set { _chosenPanel = value; }
        }

        /// <summary>
        /// Observable Collection, damit in Listboxen anzuzeigende Prozessmodelle automatisch aktualisiert werden
        /// </summary>
        private TrulyObservableCollection<ProcessModel> _cat0ProcessItems;
        private TrulyObservableCollection<ProcessModel> _cat1ProcessItems;
        private TrulyObservableCollection<ProcessModel> _cat2ProcessItems;
        private TrulyObservableCollection<ProcessModel> _cat3ProcessItems;
        private TrulyObservableCollection<ProcessModel> _cat4ProcessItems;

        /// <summary>
        /// Observable Collection, damit in Listboxen anzuzeigende Prozessmodelle automatisch aktualisiert werden
        /// </summary>
        public ObservableCollection<ProcessModel> Cat0ProcessItems
        {
            get
            {
                return _cat0ProcessItems;
            }
        }

        public ObservableCollection<ProcessModel> Cat1ProcessItems
        {
            get
            {
                return _cat1ProcessItems;
            }
        }

        public ObservableCollection<ProcessModel> Cat2ProcessItems
        {
            get
            {
                return _cat2ProcessItems;
            }
        }

        public ObservableCollection<ProcessModel> Cat3ProcessItems
        {
            get
            {
                return _cat3ProcessItems;
            }
        }

        public ObservableCollection<ProcessModel> Cat4ProcessItems
        {
            get
            {
                return _cat4ProcessItems;
            }
        }

        private MenuThreeOptions _attachedMenu;
        /// <summary>
        /// Zugehöriges Menü (für Listbox-Einträge).
        /// </summary>
        public MenuThreeOptions AttachedMenu
        {
            get { return _attachedMenu; }
            set { _attachedMenu = value; }
        }

        private ModelOverviewArea _boundArea = new ModelOverviewArea();
        /// <summary>
        /// zugehöriger Bereichsrahmen
        /// </summary>
        public ModelOverviewArea BoundArea
        {
            get { return _boundArea; }
            set { _boundArea = value; }
        }

        /// <summary>
        /// Aktuell gewählter Tab.
        /// </summary>
        private int _currentCategory = 1;
        private SurfaceListBox _currentListBox;
        /// <summary>
        /// Aktuell sichtbare ListBox.
        /// </summary>
        public SurfaceListBox CurrentListBox
        {
            get { return _currentListBox; }
            set { _currentListBox = value; }
        }
        /// <summary>
        /// Favoriten-ListBox
        /// </summary>
        public SurfaceListBox FavouritesListBox
        {
            get { return Cat0ListBox; }
        }
        /// <summary>
        /// Position des aktuelle ausgewählten Listbox-eintrages.
        /// Wichtig für Menüpositionierung
        /// </summary>
        private Point _currentSelectedItemPosition;

        /// <summary>
        /// Tocuhdevice zur Differenzierung mehrerer Finger auf Tabs.
        /// </summary>
        private TouchDevice CategoryTouchDevice;
        /// <summary>
        /// aktuell als Vorschau ausgewählter Navigationstab.
        /// </summary>
        private int previewCategory = -1;

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

        /// <summary>
        /// Tocuhdevice zur Differenzierung mehrerer Finger auf SafeSelect.
        /// </summary>
        protected TouchDevice OptionControlTouchDevice = null;
        /// <summary>
        /// Position des ersten TocuhDowns auf SafeSelect-Button.
        /// </summary>
        protected double TouchPointXOnButton = 0;

        /// <summary>
        /// SelectionChangedEvent, needed to pass event outside of ModelOverviewControl-Class
        /// </summary>
        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ModelOverviewControl));
        public event RoutedEventHandler SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

        /// <summary>
        /// MenuOptionSelectedEvent, needed to pass event outside of InstanceControlWidget-Class
        /// </summary>
        public static readonly RoutedEvent AddOrRemoveSelectedEvent = EventManager.RegisterRoutedEvent("AddOrRemoveSelected", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ModelOverviewControl));
        public event RoutedEventHandler AddOrRemoveSelected
        {
            add { AddHandler(AddOrRemoveSelectedEvent, value); }
            remove { RemoveHandler(AddOrRemoveSelectedEvent, value); }
        }
        #endregion

        /// <summary>
        /// Konstruktor
        /// </summary>
		public ModelOverviewControl()
		{
			this.InitializeComponent();

            _currentListBox = Cat1ListBox;
            _currentSelectedItemPosition = new Point(0, 0);
		}

        #region Methoden

        /// <summary>
        /// Fügt Prozessmodelle je nach Kategorie der/den entsprechenden Observable Collection hinzu.
        /// </summary>
        /// <param name="modelList"></param>
        public void LoadListContent(List<ProcessModel> modelList)
        {
            foreach (ProcessModel model in modelList)
            {
                foreach (Enums.Category category in model.Categories)
                {
                    if ((int)category == 0) _cat0ProcessItems.Add(model);
                    if ((int)category == 1) _cat1ProcessItems.Add(model);
                    if ((int)category == 2) _cat2ProcessItems.Add(model);
                    if ((int)category == 3) _cat3ProcessItems.Add(model);
                    if ((int)category == 4) _cat4ProcessItems.Add(model);
                }
            }
        }

        /// <summary>
        /// Aktualisiert Anzahl Modelle in einer Kategorie und fügt neues Prozessmodell hinzu.
        /// </summary>
        /// <param name="model"></param>
        public void UpdateListContent(ProcessModel model)
        {
            foreach (Enums.Category category in model.Categories)
            {
                if ((int)category == 0)
                {
                    _cat0ProcessItems.Add(model);
                    UpdateCategoryItemsAmount(Cat0ListBox);
                }
                if ((int)category == 1)
                {
                    _cat1ProcessItems.Add(model);
                    UpdateCategoryItemsAmount(Cat1ListBox);
                }
                if ((int)category == 2)
                {
                    _cat2ProcessItems.Add(model);
                    UpdateCategoryItemsAmount(Cat2ListBox);
                }
                if ((int)category == 3)
                {
                    _cat3ProcessItems.Add(model);
                    UpdateCategoryItemsAmount(Cat3ListBox);
                }
                if ((int)category == 4)
                {
                    _cat4ProcessItems.Add(model);
                    UpdateCategoryItemsAmount(Cat4ListBox);
                }
            }
        }

        /// <summary>
        /// Berechnet über welchem Tab Finger positioniert ist und veranlasst entsprechendes Feedback.
        /// </summary>
        /// <param name="touchPoint">Position Finger</param>
        private void UpdateCategoryPreview(Point touchPoint)
        {
            double cellHeight = CategoryGrid.Height / 5;
            if (touchPoint.Y >= 0 * cellHeight && touchPoint.Y < 1 * cellHeight) previewCategory = 0;
            if (touchPoint.Y >= 1 * cellHeight && touchPoint.Y < 2 * cellHeight) previewCategory = 1;
            if (touchPoint.Y >= 2 * cellHeight && touchPoint.Y < 3 * cellHeight) previewCategory = 2;
            if (touchPoint.Y >= 3 * cellHeight && touchPoint.Y < 4 * cellHeight) previewCategory = 3;
            if (touchPoint.Y >= 4 * cellHeight && touchPoint.Y < 5 * cellHeight) previewCategory = 4;

            if (previewCategory == 0 || _currentCategory == 0) Category0Rect.Fill = new SolidColorBrush((Color)FindResource("Category0ColorStrongNEW"));
            else Category0Rect.Fill = new SolidColorBrush((Color)FindResource("Category0ColorFadeNEW"));
            if (previewCategory == 1 || _currentCategory == 1) Category1Rect.Fill = new SolidColorBrush((Color)FindResource("Category1ColorStrongNEW"));
            else Category1Rect.Fill = new SolidColorBrush((Color)FindResource("Category1ColorFadeNEW"));
            if (previewCategory == 2 || _currentCategory == 2) Category2Rect.Fill = new SolidColorBrush((Color)FindResource("Category2ColorStrongNEW"));
            else Category2Rect.Fill = new SolidColorBrush((Color)FindResource("Category2ColorFadeNEW"));
            if (previewCategory == 3 || _currentCategory == 3) Category3Rect.Fill = new SolidColorBrush((Color)FindResource("Category3ColorStrongNEW"));
            else Category3Rect.Fill = new SolidColorBrush((Color)FindResource("Category3ColorFadeNEW"));
            if (previewCategory == 4 || _currentCategory == 4) Category4Rect.Fill = new SolidColorBrush((Color)FindResource("Category4ColorStrongNEW"));
            else Category4Rect.Fill = new SolidColorBrush((Color)FindResource("Category4ColorFadeNEW"));
        }

        /// <summary>
        /// Schaltet Inhalt um, je nach gewähltem Tab.
        /// </summary>
        /// <param name="newCategory"></param>
        private void SwitchCategory(int newCategory)
        {
            if (previewCategory == -1) return;

            _currentListBox.SelectedIndex = -1;

            switch (_currentCategory)
            {
                case 0:
                    Cat0ListBox.Visibility = System.Windows.Visibility.Hidden;
                    Category0Rect.Fill = new SolidColorBrush((Color)FindResource("Category0ColorFadeNEW"));
                    CreateReduceStoryBoard(Category0Rect, Category0Text, Category0Image);
                    break;
                case 1:
                    Cat1ListBox.Visibility = System.Windows.Visibility.Hidden;
                    Category1Rect.Fill = new SolidColorBrush((Color)FindResource("Category1ColorFadeNEW"));
                    CreateReduceStoryBoard(Category1Rect, Category1Text, Category1Image);
                    break;
                case 2:
                    Cat2ListBox.Visibility = System.Windows.Visibility.Hidden;
                    Category2Rect.Fill = new SolidColorBrush((Color)FindResource("Category2ColorFadeNEW"));
                    CreateReduceStoryBoard(Category2Rect, Category2Text, Category2Image);
                    break;
                case 3:
                    Cat3ListBox.Visibility = System.Windows.Visibility.Hidden;
                    Category3Rect.Fill = new SolidColorBrush((Color)FindResource("Category3ColorFadeNEW"));
                    CreateReduceStoryBoard(Category3Rect, Category3Text, Category3Image);
                    break;
                case 4:
                    Cat4ListBox.Visibility = System.Windows.Visibility.Hidden;
                    Category4Rect.Fill = new SolidColorBrush((Color)FindResource("Category4ColorFadeNEW"));
                    CreateReduceStoryBoard(Category4Rect, Category4Text, Category4Image);
                    break;
            }

            // Create a linear gradient brush with 2 stops 
            LinearGradientBrush linGB = new LinearGradientBrush();
            linGB.StartPoint = new Point(0.5, 0);
            linGB.EndPoint = new Point(0.5, 1);
            // Create and add Gradient stops
            GradientStop firstGS = new GradientStop();
            firstGS.Offset = 0.0;
            linGB.GradientStops.Add(firstGS);
            GradientStop secondGS = new GradientStop();
            secondGS.Offset = 0.9;
            linGB.GradientStops.Add(secondGS);
            // Set Fill property of rectangle
            BackgroundRectangleHeader.Fill = linGB;
            firstGS.Color = (Color)FindResource("Category" + newCategory + "ColorStrongNEW");
            secondGS.Color = (Color)FindResource("Category" + newCategory + "ColorFadeNEW");
            BackgroundRectangleListBox.Fill = new SolidColorBrush((Color)FindResource("Category" + newCategory + "ColorFadeNEW"));

            //Border.Stroke = new SolidColorBrush((Color)FindResource("Category" + newCategory + "ColorStrongNEW"));

            SurfaceListBox newListBox = Cat0ListBox;

            switch (newCategory)
            {
                case 0:
                    Cat0ListBox.Visibility = System.Windows.Visibility.Visible;
                    // Category0Line.Visibility = System.Windows.Visibility.Hidden;

                    CreateExpandStoryBoard(Category0Rect, Category0Text, Category0Image);

                    break;
                case 1:
                    Cat1ListBox.Visibility = System.Windows.Visibility.Visible;
                    // Category1Line.Visibility = System.Windows.Visibility.Hidden;

                    CreateExpandStoryBoard(Category1Rect, Category1Text, Category1Image);

                    newListBox = Cat1ListBox;

                    break;
                case 2:
                    Cat2ListBox.Visibility = System.Windows.Visibility.Visible;
                    // Category2Line.Visibility = System.Windows.Visibility.Hidden;

                    CreateExpandStoryBoard(Category2Rect, Category2Text, Category2Image);

                    newListBox = Cat2ListBox;

                    break;
                case 3:
                    Cat3ListBox.Visibility = System.Windows.Visibility.Visible;
                    // Category3Line.Visibility = System.Windows.Visibility.Hidden;

                    CreateExpandStoryBoard(Category3Rect, Category3Text, Category3Image);

                    newListBox = Cat3ListBox;

                    break;
                case 4:
                    Cat4ListBox.Visibility = System.Windows.Visibility.Visible;
                    // Category4Line.Visibility = System.Windows.Visibility.Hidden;

                    CreateExpandStoryBoard(Category4Rect, Category4Text, Category4Image);

                    newListBox = Cat4ListBox;

                    break;
            }

            if (newCategory == 0)
                _attachedMenu.SetOptions(_attachedMenu.OptionTexts[0].Text, _attachedMenu.OptionTexts[1].Text, "Remove from Favourites", "../Images/play_black.png", "../Images/lupe_black.png", "../Images/wherz_black_crossed.png");
            else
                _attachedMenu.SetOptions(_attachedMenu.OptionTexts[0].Text, _attachedMenu.OptionTexts[1].Text, "Add to Favourites", "../Images/play_black.png", "../Images/lupe_black.png", "../Images/wherz_black_crossed2.png");

            _currentCategory = newCategory;
            previewCategory = -1;

            if (_currentListBox.Height != newListBox.Height)
            {
                ResizeListBox(newListBox);
            }
            _currentListBox = newListBox;

        }

        /// <summary>
        /// Berechnet Größenanpassung der ListbOx an Anzahl enthaltener Modelle.
        /// </summary>
        /// <param name="listbox"></param>
        public void ResizeListBox(SurfaceListBox listbox)
        {
            int count = listbox.Items.Count;
            double newHeight;
            double totalHeight = 50 * count;

            if (count > 9) newHeight = 470;
            //if (totalHeight > 455) newHeight = 455;
            else if (totalHeight < 275) newHeight = 275;
            else newHeight = totalHeight;
            // previous values:
            //if (totalHeight > 705) newHeight = 705;
            //else if (totalHeight < 275) newHeight = 275;
            //else newHeight = totalHeight;

            listbox.Height = newHeight;

            if (((_currentCategory == 0) && (listbox.Equals(Cat0ListBox))) || ((_currentCategory == 1) && (listbox.Equals(Cat1ListBox)))
                || ((_currentCategory == 2) && (listbox.Equals(Cat2ListBox))) || ((_currentCategory == 3) && (listbox.Equals(Cat3ListBox)))
                || ((_currentCategory == 4) && (listbox.Equals(Cat4ListBox))))
            {
                ResizeGridHeightAndAdaptBorder(newHeight, listbox);
            }
        }

        /// <summary>
        /// Passt Größe des Controls an Listboxgröße an und ruft Anpassenmethode für Rahmenbereich auf.
        /// </summary>
        /// <param name="newHeight"></param>
        /// <param name="listbox"></param>
        private void ResizeGridHeightAndAdaptBorder(double newHeight, SurfaceListBox listbox)
        {
            MainGrid.RowDefinitions[2].Height = new GridLength(newHeight + listbox.Margin.Bottom);
            MainGrid.Height = MainGrid.RowDefinitions[0].Height.Value + MainGrid.RowDefinitions[1].Height.Value + MainGrid.RowDefinitions[2].Height.Value;
            this.Height = MainGrid.Height;
            BorderPoint1.Point = new Point(0, newHeight + MainGrid.RowDefinitions[1].ActualHeight + listbox.Margin.Bottom);
            BorderPoint2.Point = new Point(MainGrid.ActualWidth - CategoryGrid.ActualWidth, newHeight + MainGrid.RowDefinitions[1].ActualHeight + listbox.Margin.Bottom);

            // Logger.Debug("MainGrid.Height: " + MainGrid.Height);
            _boundArea.ResizeAndAdaptBorder(MainGrid);
        }

        /// <summary>
        /// Fügt Prozessmodell in Favoriten-Listbox ein.
        /// </summary>
        /// <param name="item"></param>
        public void AddModelToFavourites(ProcessModel item)
        {
            _cat0ProcessItems.Add(item);
            UpdateCategoryItemsAmount(Cat0ListBox);
        }

        /// <summary>
        /// Löscht Prozessmodell aus Favoriten-Listbox.
        /// </summary>
        /// <param name="item"></param>
        public void DeleteModelFromFavourites(ProcessModel item)
        {
            _cat0ProcessItems.Remove(item);
            UpdateCategoryItemsAmount(Cat0ListBox);
        }

        /// <summary>
        /// Aktualisiert Textbox für Anzeige Anzahl enthaltener Modelle in Kategorie.
        /// </summary>
        /// <param name="listbox"></param>
        private void UpdateCategoryItemsAmount(SurfaceListBox listbox)
        {
            if (listbox.Equals(Cat0ListBox))
            {
                Category0AmountText.Text = "(" + listbox.Items.Count + ")";
                if (_currentCategory == 0) ResizeListBox(Cat0ListBox);
            }
            else if (listbox.Equals(Cat1ListBox)) Category1AmountText.Text = "(" + listbox.Items.Count + ")";
            else if (listbox.Equals(Cat2ListBox)) Category2AmountText.Text = "(" + listbox.Items.Count + ")";
            else if (listbox.Equals(Cat3ListBox)) Category3AmountText.Text = "(" + listbox.Items.Count + ")";
            else if (listbox.Equals(Cat4ListBox)) Category4AmountText.Text = "(" + listbox.Items.Count + ")";
        }

        /// <summary>
        /// Erzeugt und beginnt Animation für Tab-Aktivierung.
        /// </summary>
        /// <param name="rect">zu verschiebendes Rechteck</param>
        /// <param name="textbox">zu verschiebende Textbox</param>
        /// <param name="image">zu verschiebendes Symbol</param>
        private void CreateExpandStoryBoard(Rectangle rect, TextBox textbox, Image image)
        {
            DoubleAnimation expandAnimation = new DoubleAnimation();
            expandAnimation.From = 155;
            expandAnimation.To = 170;
            expandAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            expandAnimation.AutoReverse = false;

            ThicknessAnimation moveAnimation = new ThicknessAnimation();
            moveAnimation.Duration = TimeSpan.FromSeconds(0.5);
            moveAnimation.From = new Thickness(42, 0, 0, 10);
            moveAnimation.To = new Thickness(27, 0, 0, 10);

            ThicknessAnimation moveSymbolAnimation = new ThicknessAnimation();
            moveSymbolAnimation.Duration = TimeSpan.FromSeconds(0.5);
            moveSymbolAnimation.From = new Thickness(23, 4, 0, 0);
            moveSymbolAnimation.To = new Thickness(8, 4, 0, 0);

            Storyboard expandStoryboard = new Storyboard();
            expandStoryboard.Children.Add(expandAnimation);
            expandStoryboard.Children.Add(moveAnimation);
            expandStoryboard.Children.Add(moveSymbolAnimation);
            Storyboard.SetTargetName(expandAnimation, rect.Name);
            Storyboard.SetTargetProperty(expandAnimation, new PropertyPath(Rectangle.WidthProperty));
            Storyboard.SetTargetName(moveAnimation, textbox.Name);
            Storyboard.SetTargetProperty(moveAnimation, new PropertyPath(TextBox.MarginProperty));
            Storyboard.SetTargetName(moveSymbolAnimation, image.Name);
            Storyboard.SetTargetProperty(moveSymbolAnimation, new PropertyPath(Image.MarginProperty));

            expandStoryboard.Begin(this);
        }

        /// <summary>
        /// Erzeugt und beginnt Animation für Tab-Deaktivierung.
        /// </summary>
        /// <param name="rect">zu verschiebendes Rechteck</param>
        /// <param name="textbox">zu verschiebende Textbox</param>
        /// <param name="image">zu verschiebendes Symbol</param>
        private void CreateReduceStoryBoard(Rectangle rect, TextBox textbox, Image image)
        {
            DoubleAnimation reduceAnimation = new DoubleAnimation();
            reduceAnimation.From = 170;
            reduceAnimation.To = 155;
            reduceAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            reduceAnimation.AutoReverse = false;

            ThicknessAnimation moveAnimation = new ThicknessAnimation();
            moveAnimation.Duration = TimeSpan.FromSeconds(0.5);
            // moveAnimation.FillBehavior = FillBehavior.HoldEnd;
            moveAnimation.From = new Thickness(27, 0, 0, 10);
            moveAnimation.To = new Thickness(42, 0, 0, 10);

            ThicknessAnimation moveSymbolAnimation = new ThicknessAnimation();
            moveSymbolAnimation.Duration = TimeSpan.FromSeconds(0.5);
            moveSymbolAnimation.From = new Thickness(8, 4, 0, 0);
            moveSymbolAnimation.To = new Thickness(23, 4, 0, 0);

            Storyboard reduceStoryboard = new Storyboard();
            reduceStoryboard.Children.Add(reduceAnimation);
            reduceStoryboard.Children.Add(moveAnimation);
            reduceStoryboard.Children.Add(moveSymbolAnimation);
            Storyboard.SetTargetName(reduceAnimation, rect.Name);
            Storyboard.SetTargetProperty(reduceAnimation, new PropertyPath(Rectangle.WidthProperty));
            Storyboard.SetTargetName(moveAnimation, textbox.Name);
            Storyboard.SetTargetProperty(moveAnimation, new PropertyPath(TextBox.MarginProperty));
            Storyboard.SetTargetName(moveSymbolAnimation, image.Name);
            Storyboard.SetTargetProperty(moveSymbolAnimation, new PropertyPath(Image.MarginProperty));

            reduceStoryboard.Begin(this);
        }
        /// <summary>
        /// ANimiert Zurückgleiten SafeSelect-Feld in Ausgangsposition.
        /// </summary>
        /// <param name="button"></param>
        private void ResetButton(SurfaceButton button)
        {
            ThicknessAnimation moveAnimation = new ThicknessAnimation();
            moveAnimation.Duration = TimeSpan.FromSeconds(0.1);
            moveAnimation.FillBehavior = FillBehavior.Stop;
            moveAnimation.To = new Thickness(226, button.Margin.Top, button.Margin.Right, button.Margin.Bottom);

            Storyboard resetStoryboard = new Storyboard();
            resetStoryboard.Children.Add(moveAnimation);
            Storyboard.SetTargetName(moveAnimation, button.Name);
            Storyboard.SetTargetProperty(moveAnimation, new PropertyPath(TextBox.MarginProperty));
            resetStoryboard.Completed += new EventHandler(resetStoryboard_Completed);

            resetStoryboard.Begin(this);
        }
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
            _hideMargin = _boundArea.HideMargin + 40;

        }
        /// <summary>
        /// Event wird aufgerufen, wenn Selektion in ListBox geändert wurde.
        /// Leitet Event an SurfaceWindow weiter.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void ModelSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CurrentListBox.SelectedIndex != -1)
            {
                if (((ProcessModel)CurrentListBox.SelectedItem).Categories.Contains(Enums.Category.Favourites))
                    _attachedMenu.SetOptions(_attachedMenu.OptionTexts[0].Text, _attachedMenu.OptionTexts[1].Text, "Remove from Favourites", "../Images/play_black.png", "../Images/lupe_black.png", "../Images/wherz_black_crossed5.png");

                else
                    _attachedMenu.SetOptions(_attachedMenu.OptionTexts[0].Text, _attachedMenu.OptionTexts[1].Text, "Add to Favourites", "../Images/play_black.png", "../Images/lupe_black.png", "../Images/wherz_black.png");
            }

            // pass event to SurfaceWindow1.xaml.cs
            RoutedEventArgs eventargs = new RoutedEventArgs(ModelOverviewControl.SelectionChangedEvent);
            RaiseEvent(eventargs);
        }

        /// <summary>
        /// Event wird aufgerufen, wenn XAML-Komponenten initialisiert wurden, damit Prozessmodelle hinzugefügt werden können
        /// </summary>
        /// <param name="e">Eventparameter</param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            DataContext = this;

            _cat0ProcessItems = new TrulyObservableCollection<ProcessModel>();
            _cat1ProcessItems = new TrulyObservableCollection<ProcessModel>();
            _cat2ProcessItems = new TrulyObservableCollection<ProcessModel>();
            _cat3ProcessItems = new TrulyObservableCollection<ProcessModel>();
            _cat4ProcessItems = new TrulyObservableCollection<ProcessModel>();

        }

        /// <summary>
        /// Event wird aufgerufen bei TouchDown auf Tabs.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void CategoryTouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            // Capture to the ScaleButton.  
            e.TouchDevice.Capture(this.CategoryGrid);

            // Remember this contact if a contact has not been remembered already.  
            // This contact is then used to move the ellipse around.
            if (CategoryTouchDevice == null)
            {
                CategoryTouchDevice = e.TouchDevice;
                UpdateCategoryPreview(CategoryTouchDevice.GetTouchPoint(this.CategoryGrid).Position);
            }
            // Mark this event as handled.  
            e.Handled = true;
        }


        /// <summary>
        /// Event wird aufgerufen bei TouchMove auf Tabs.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void CategoryTouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
            if (e.TouchDevice == CategoryTouchDevice)
            {
                UpdateCategoryPreview(CategoryTouchDevice.GetTouchPoint(this.CategoryGrid).Position);
            }
            // Mark this event as handled.  
            e.Handled = true;
        }


        /// <summary>
        /// Event wird aufgerufen bei TouchUp auf Tabs.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void CategoryTouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            // If this contact is the one that was remembered  
            if (e.TouchDevice == CategoryTouchDevice)
            {
                // Forget about this contact.
                CategoryTouchDevice = null;

                if (_currentCategory != previewCategory)
                {
                    SwitchCategory(previewCategory);
                }
            }
            // Mark this event as handled.  
            e.Handled = true;
        }

        /// <summary>
        /// Event wird aufgerufen wenn XAMLK-Komponenten geladen, damit Größenanpassung und Katgeorieanzahl-Aktualiserung erfolgen kann.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void WasLoaded(object sender, RoutedEventArgs e)
        {
            ResizeListBox((SurfaceListBox)sender);
            UpdateCategoryItemsAmount((SurfaceListBox)sender);
        }

        /// <summary>
        /// Event wird aufgerufen bei TouchDown auf SafeSelect.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OptionTouchDown(object sender, TouchEventArgs e)
        {
            // Remember this contact if a contact has not been remembered already.
            if (OptionControlTouchDevice == null)
            {
                OptionControlTouchDevice = e.TouchDevice;
                TouchPointXOnButton = OptionControlTouchDevice.GetTouchPoint((SurfaceButton)sender).Position.X;
            }
        }

        /// <summary>
        /// Event wird aufgerufen bei TouchMove auf SafeSelect.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OptionTouchMove(object sender, TouchEventArgs e)
        {
            // If this contact is the one that was remembered  
            if (e.TouchDevice == OptionControlTouchDevice)
            {
                double touchPointXOnGrid = OptionControlTouchDevice.GetTouchPoint(this.AddOrRemoveGrid).Position.X;
                touchPointXOnGrid = touchPointXOnGrid - TouchPointXOnButton;
                if (touchPointXOnGrid < 136)
                {
                    touchPointXOnGrid = 136;
                }
                else if (touchPointXOnGrid > 316)
                {
                    touchPointXOnGrid = 316;
                }
                ((SurfaceButton)sender).Margin = new Thickness(touchPointXOnGrid, ((SurfaceButton)sender).Margin.Top, ((SurfaceButton)sender).Margin.Right, ((SurfaceButton)sender).Margin.Bottom);
                // Console.WriteLine(touchPointXOnGrid);

                RemovingText.Foreground = new SolidColorBrush(Colors.White);
                AddingText.Foreground = new SolidColorBrush(Colors.White);
                ConfirmAddRectangle.Fill = new SolidColorBrush(Colors.White);
                ConfirmRemoveRectangle.Fill = new SolidColorBrush(Colors.White);
                _chosenPanel = ChosenPanel.None;
                if (touchPointXOnGrid < 231 && touchPointXOnGrid > 221)
                {
                    PlusMinusImage.Source = new BitmapImage(new Uri("../Images/plusminus.png", UriKind.Relative));
                }
                else if (touchPointXOnGrid < 146)
                {
                    AddingText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    ConfirmAddRectangle.Fill = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    PlusMinusImage.Source = new BitmapImage(new Uri("../Images/plusminusplus.png", UriKind.Relative));
                    _chosenPanel = ChosenPanel.Add;
                }
                else if (touchPointXOnGrid > 306)
                {
                    RemovingText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    ConfirmRemoveRectangle.Fill = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    PlusMinusImage.Source = new BitmapImage(new Uri("../Images/plusminusminus.png", UriKind.Relative));
                    _chosenPanel = ChosenPanel.Remove;
                }
                else if (touchPointXOnGrid <= 221)
                {
                    AddingText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    ConfirmAddRectangle.Fill = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    PlusMinusImage.Source = new BitmapImage(new Uri("../Images/plusminusplus.png", UriKind.Relative));
                    _chosenPanel = ChosenPanel.Remove;
                }
                else if (touchPointXOnGrid >= 231)
                {
                    RemovingText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    ConfirmRemoveRectangle.Fill = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    PlusMinusImage.Source = new BitmapImage(new Uri("../Images/plusminusminus.png", UriKind.Relative));
                    _chosenPanel = ChosenPanel.Remove;
                }
            }
        }

        /// <summary>
        /// Event wird aufgerufen bei TouchUp auf SafeSelect.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OptionTouchUp(object sender, TouchEventArgs e)
        {
            // If this contact is the one that was remembered  
            if (e.TouchDevice == OptionControlTouchDevice)
            {
                // Forget about this contact.
                OptionControlTouchDevice = null;

                SurfaceButton pressedButton = (SurfaceButton)sender;

                var bc = new BrushConverter();
                RemovingText.Foreground = new SolidColorBrush(Colors.White);
                AddingText.Foreground = new SolidColorBrush(Colors.White);
                ConfirmAddRectangle.Fill = (Brush)bc.ConvertFrom("#FFE2E2E2");
                ConfirmRemoveRectangle.Fill = (Brush)bc.ConvertFrom("#FFE2E2E2");

                if (pressedButton.Margin.Left <= 146 || pressedButton.Margin.Left >= 306)
                {
                    // pass event to SurfaceWindow1.xaml.cs
                    RoutedEventArgs eventargs = new RoutedEventArgs(ModelOverviewControl.AddOrRemoveSelectedEvent);
                    RaiseEvent(eventargs);
                }
                _chosenPanel = ChosenPanel.None;
                // Reset Button
                ResetButton(pressedButton);
            }
        }

        /// <summary>
        /// Stellt sicher, dass SafeSelect-Feld exakt an Ausgangsposition steht.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void resetStoryboard_Completed(object sender, EventArgs e)
        {
            ConfirmButton.Margin = new Thickness(226, ConfirmButton.Margin.Top, ConfirmButton.Margin.Right, ConfirmButton.Margin.Bottom);
            PlusMinusImage.Source = new BitmapImage(new Uri("../Images/plusminus.png", UriKind.Relative));
        }
        #endregion


    }

    /// <summary>
    /// eigene Klasse für Observable Collection, die auch bei Attributwertänderungen der observierten Items Aktualisierung ermöglicht.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TrulyObservableCollection<T> : ObservableCollection<T>
        where T : INotifyPropertyChanged
    {
        public TrulyObservableCollection()
            : base()
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(TrulyObservableCollection_CollectionChanged);
        }

        void TrulyObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Object item in e.NewItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                }
            }
            if (e.OldItems != null)
            {
                foreach (Object item in e.OldItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
                }
            }
        }

        /// <summary>
        /// Event wird aufgerufen, wenn Attribut eines observeirten Items geändert wurde.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs a = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(a);
        }
    }
}