using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Prototype_05
{
    /// <summary>
    /// Abstrakte Menü-Klasse. Erstellt Aussehen.
    /// Arbeitet Eventhandling ab, soweit möglich (alles außer direkten Bezug auf aktuell ausgewählten Menüeintrag).
    /// </summary>
    public abstract class Menu : UserControl
    {
        #region Deklaration

        #region WPF-Elements
        protected Grid MainGrid;
        /// <summary>
        /// Schaltfläche zum Ein- und Ausblenden der Menüeinträge sowie Startfläche für Selektion
        /// </summary>
        protected SurfaceButton MenuButton;
        protected TextBox MenuText;
        protected Grid OptionsGrid;
        protected List<TextBox> _optionTexts;
        /// <summary>
        /// Liste der Textboxen für Menüeinträge
        /// </summary>
        public List<TextBox> OptionTexts
        {
            get { return _optionTexts; }
            set { _optionTexts = value; }
        }
        /// <summary>
        /// Liste der Bilder für Menüeintrag-Symbole
        /// </summary>
        protected List<Image> _optionImages;
        /// <summary>
        /// Liste der Grids für Menüeinträge
        /// </summary>
        protected List<Grid> _optionImgTxtGrids;

        /// <summary>
        /// Liste der Pfade von Menüeinträgen
        /// </summary>
        protected List<Path> _optionOuterPaths;
        #endregion

        #region UI-Values
        /// <summary>
        /// Breite des ausgeklappten Menüs
        /// </summary>
        protected double _controlWidth = 230;
        protected double _menuButtonHeight = 46;
        /// <summary>
        /// Höhe eines Menüeintrages
        /// </summary>
        public double MenuButtonHeight
        { get { return _menuButtonHeight; } }
        protected double _menuButtonWidth = 230 - 155;
        /// <summary>
        /// Breite der Menüschaltfläche
        /// </summary>
        public double MenuButtonWidth
        { get { return _menuButtonWidth; } }
        /// <summary>
        /// Anzahl Menüeinträge
        /// </summary>
        protected int _optionsAmount;
        #endregion

        #region Other Values
        /// <summary>
        /// aktueller Zustand des Menüs
        /// </summary>
        protected State _currentState = State.Idle;
        /// <summary>
        /// Startposition der Fingerbewegung auf Schaltfläche
        /// </summary>
        protected Point _moveStartPosition;
        /// <summary>
        /// Markierungslinie die Fingerbweegung visualisiert
        /// </summary>
        protected Line _touchLine;
        /// <summary>
        /// Touchdevice zur Differenzierung mehrerer Finger.
        /// </summary>
        protected TouchDevice _optionControlTouchDevice;
        /// <summary>
        /// Zeitpunkt vom ersten Tocuhdown auf Menü-Schaltfläche.
        /// Wichtig zur Erkennung ob Tap oder Drag bei zwischenzeitlich vom Tabletop erkannten TouchMoves.
        /// </summary>
        protected DateTime TouchDownTime;
        /// <summary>
        /// <value>true</value> wenn Menüeinträge sichtbar.
        /// </summary>
        protected bool OptionsVisible = true;
        /// <summary>
        /// vorhergehender Status des Menüs.
        /// </summary>
        protected State PreviousState;

        #endregion

        #region Enums
        /// <summary>
        /// Enum-Defintionen für Menüzustand.
        /// </summary>
        protected enum State { Idle, Clicked, Uncertain, Hold_Base, Hold_Mid, Hold_Option }
        #endregion

        /// <summary>
        /// MenuOptionSelectedEvent, needed to pass event outside of Menu-Class
        /// </summary>
        public static readonly RoutedEvent MenuOptionSelectedEvent = EventManager.RegisterRoutedEvent("MenuOptionSelected", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(Menu));
        public event RoutedEventHandler MenuOptionSelected
        {
            add { AddHandler(MenuOptionSelectedEvent, value); }
            remove { RemoveHandler(MenuOptionSelectedEvent, value); }
        }
        #endregion

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="optionsAmount">Anzahl benötigter Menüeinträge</param>
        /// <param name="openMenu"><value>true</value> wenn Menüeinträge sofort sichtbar sein sollen</param>
        public Menu(int optionsAmount, bool openMenu)
        {
            _optionsAmount = optionsAmount;
            this.InitializeBaseComponents();
            this.InitializeDependentComponents();
            this.RegisterEvents();

            PreviousState = State.Clicked;
            if (openMenu) OpenMenu();

        }

        #region UI-Initialization
         
        /// <summary>
        /// UI-Erstellung sowit unabhängig von Anzahl Menüeinträgen.
        /// </summary>
        private void InitializeBaseComponents()
        {
            this.Width = _controlWidth;
            this.Height = _optionsAmount * 50;
            MainGrid = new Grid();
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            MainGrid.ColumnDefinitions[1].Width = new GridLength(155);

            MenuButton = new SurfaceButton();
            MenuButton.Height = _menuButtonHeight;
            MenuButton.BorderThickness = new Thickness(0);
            MenuButton.Margin = new Thickness(0);
            MenuButton.Padding = new Thickness(0);
            Grid.SetColumn(MenuButton, 0);
            MainGrid.Children.Add(MenuButton);

            MenuText = new TextBox();
            MenuText.Text = "Menu >>";
            MenuText.FontSize = 12;
            MenuText.HorizontalAlignment = HorizontalAlignment.Center;
            MenuText.VerticalAlignment = VerticalAlignment.Center;
            MenuText.HorizontalContentAlignment = HorizontalAlignment.Center;
            MenuText.VerticalContentAlignment = VerticalAlignment.Center;
            MenuText.Background = ((LinearGradientBrush)FindResource("ListBoxItem_MenuBrush"));
            MenuText.Height = _menuButtonHeight;
            MenuText.Width = _menuButtonWidth;
            MenuText.BorderThickness = new Thickness(0);
            MenuText.Padding = new Thickness(1, 1, 5, 5);
            MenuText.Style = (Style)FindResource("TextBox_SimpleBlack");
            MenuButton.Content = MenuText;

            OptionsGrid = new Grid();
            OptionsGrid.VerticalAlignment = VerticalAlignment.Center;
            OptionsGrid.Width = 155;
            OptionsGrid.Visibility = System.Windows.Visibility.Hidden;
            Grid.SetColumn(OptionsGrid, 1);
            MainGrid.Children.Add(OptionsGrid);

            this.Content = MainGrid;

            _moveStartPosition = new Point(0, 0);

            _touchLine = new Line();
            _touchLine.X1 = 0;
            _touchLine.Y1 = 0;
            _touchLine.X2 = 1;
            _touchLine.Y2 = 0;
            _touchLine.StrokeThickness = 3;
            _touchLine.StrokeStartLineCap = PenLineCap.Round;
            _touchLine.StrokeEndLineCap = PenLineCap.Round;
            _touchLine.Stroke = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
            _touchLine.Visibility = System.Windows.Visibility.Hidden;
            _touchLine.RenderTransformOrigin = new Point(0, 0.5);
            Grid.SetColumnSpan(_touchLine, 2);
            MainGrid.Children.Add(_touchLine);
        }

        /// <summary>
        /// UI-Erstellung abhängig von Anzahl Menüeinträgen.
        /// </summary>
        private void InitializeDependentComponents()
        {
            OptionsGrid.Height = _optionsAmount * 50;
            _optionTexts = new List<TextBox>(_optionsAmount);
            _optionImages = new List<Image>(_optionsAmount);
            _optionImgTxtGrids = new List<Grid>(_optionsAmount);
            _optionOuterPaths = new List<Path>(_optionsAmount);
            double outerPathLength = _menuButtonHeight / _optionsAmount;
            double startouterPathsY = (this.Height - _menuButtonHeight) / 2;

            for (int i = 0; i < _optionsAmount; i++)
            {
                PathGeometry pathGeometry = new PathGeometry();
                pathGeometry.FillRule = FillRule.Nonzero;
                PathFigure pathFigure = new PathFigure();
                pathFigure.StartPoint = new Point(25, i * 50);
                pathFigure.IsClosed = true;
                pathGeometry.Figures.Add(pathFigure);
                LineSegment lineSegment1 = new LineSegment();
                lineSegment1.Point = new Point(0, startouterPathsY + i * outerPathLength);
                pathFigure.Segments.Add(lineSegment1);
                LineSegment lineSegment2 = new LineSegment();
                lineSegment2.Point = new Point(0, startouterPathsY + ((i + 1) * outerPathLength));
                pathFigure.Segments.Add(lineSegment2);
                LineSegment lineSegment3 = new LineSegment();
                lineSegment3.Point = new Point(25, (i + 1) * 50);
                pathFigure.Segments.Add(lineSegment3);
                Path pathLight = new Path();
                pathLight.Fill = new SolidColorBrush((Color)FindResource("Option" + i + "ColorLight"));
                pathLight.Data = pathGeometry;

                _optionOuterPaths.Add(pathLight);
                OptionsGrid.Children.Add(pathLight);
            }

            for (int j = 0; j < _optionsAmount; j++)
            {
                Grid textImageGrid = new Grid();
                textImageGrid.ColumnDefinitions.Add(new ColumnDefinition());
                textImageGrid.ColumnDefinitions.Add(new ColumnDefinition());
                textImageGrid.ColumnDefinitions[0].Width = new GridLength(30);
                textImageGrid.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                textImageGrid.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                textImageGrid.Background = new SolidColorBrush((Color)FindResource("Option" + j + "ColorStrong"));
                textImageGrid.Height = 50;
                textImageGrid.Width = 130;
                textImageGrid.Margin = new Thickness(0, j * 50, 0, 0);

                Image optionImage = new Image();
                optionImage.Width = 15;
                optionImage.Height = 15;
                optionImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

                TextBox textBox = new TextBox();
                textBox.HorizontalAlignment = HorizontalAlignment.Left;
                textBox.Padding = new Thickness(10, 0, 10, 0);
                textBox.MaxWidth = 100;
                textBox.Style = (Style)(FindResource("MenuOptionText"));

                Grid.SetColumn(optionImage, 0);
                Grid.SetColumn(textBox, 1);
                textImageGrid.Children.Add(optionImage);
                textImageGrid.Children.Add(textBox);

                _optionTexts.Add(textBox);
                _optionImages.Add(optionImage);
                _optionImgTxtGrids.Add(textImageGrid);
                OptionsGrid.Children.Add(textImageGrid);
            }

            PathGeometry borderPathGeometry = new PathGeometry();
            borderPathGeometry.FillRule = FillRule.Nonzero;
            PathFigure borderPathFigure = new PathFigure();
            borderPathFigure.StartPoint = new Point(0, startouterPathsY);
            borderPathFigure.IsClosed = true;
            borderPathGeometry.Figures.Add(borderPathFigure);
            LineSegment borderLineSegment1 = new LineSegment();
            borderLineSegment1.Point = new Point(25, 0);
            borderPathFigure.Segments.Add(borderLineSegment1);
            LineSegment borderLineSegment2 = new LineSegment();
            borderLineSegment2.Point = new Point(155, 0);
            borderPathFigure.Segments.Add(borderLineSegment2);
            LineSegment borderLineSegment3 = new LineSegment();
            borderLineSegment3.Point = new Point(155, _optionsAmount * 50);
            borderPathFigure.Segments.Add(borderLineSegment3);
            LineSegment borderLineSegment4 = new LineSegment();
            borderLineSegment4.Point = new Point(25, _optionsAmount * 50);
            borderPathFigure.Segments.Add(borderLineSegment4);
            LineSegment borderLineSegment5 = new LineSegment();
            borderLineSegment5.Point = new Point(0, startouterPathsY + _menuButtonHeight);
            borderPathFigure.Segments.Add(borderLineSegment5);
            Path borderPath = new Path();
            borderPath.Stroke = Brushes.Black;
            borderPath.StrokeThickness = 1;
            borderPath.Data = borderPathGeometry;
            OptionsGrid.Children.Add(borderPath);

            for (int k = 1; k < _optionsAmount; k++)
            {
                Line innerLineLeft = new Line();
                innerLineLeft.X1 = 0;
                innerLineLeft.Y1 = startouterPathsY + k * outerPathLength;
                innerLineLeft.X2 = 25;
                innerLineLeft.Y2 = k * 50;
                innerLineLeft.Stroke = Brushes.Black;
                innerLineLeft.StrokeThickness = 1;
                OptionsGrid.Children.Add(innerLineLeft);

                Line innerLineRight = new Line();
                innerLineRight.X1 = 25;
                innerLineRight.Y1 = k * 50;
                innerLineRight.X2 = 155;
                innerLineRight.Y2 = k * 50;
                innerLineRight.Stroke = Brushes.Black;
                innerLineRight.StrokeThickness = 1;
                OptionsGrid.Children.Add(innerLineRight);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Anpassung des Menüaussehens an Kontext.
        /// </summary>
        public void RemoveButtonBackgroundColorAndSetTextWhite()
        {
            MenuText.Background = null;
            MenuButton.Background = null;
            MenuText.Foreground = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// Anpassung des Menüaussehens an SytemStateOverview.
        /// Derzeit ist Anzeige von Menüeinträgen in Log- bzw. Error-Tabelle deaktiviert.
        /// </summary>
        public void AdaptMenuColorToSystemStateOverview()
        {
            MenuText.Background = new SolidColorBrush((Color)FindResource("SystemState_MenuColor"));
            MenuButton.Background = null;
            MenuText.Foreground = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// Anpassung des Menüaussehens an InstanceControlWidget.
        /// </summary>
        public void AdaptMenuColorToInstanceControl()
        {
            MenuText.Background = new SolidColorBrush((Color)FindResource("InstanceControl_MenuColor"));
            MenuButton.Background = null;
            MenuText.Foreground = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// Anpassung des Menüaussehens an ModelPropertyPanel.
        /// </summary>
        public void AdaptMenuColorToModelDetail()
        {
            MenuText.Background = new SolidColorBrush((Color)FindResource("ModelItem_TouchColor"));
            MenuButton.Background = null;
            MenuText.Foreground = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// Anpassung des Menüaussehens an InstancePropertyPanel.
        /// </summary>
        public void AdaptMenuColorToInstanceDetail()
        {
            MenuText.Background = new SolidColorBrush((Color)FindResource("InstanceItem_TouchColor"));
            MenuButton.Background = null;
            MenuText.Foreground = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// Anpassung des Menüaussehens an ModelOverview (ListBox).
        /// </summary>
        public void AdaptMenuColorToModelOverview()
        {
            MenuText.Background = null;
            //MenuButton.Background = new SolidColorBrush((Color)FindResource("ModelItem_TouchColor"));
            MenuText.Foreground = new SolidColorBrush(Colors.White);
            MenuButton.Template = (ControlTemplate)(FindResource("ModelMenu_ButtonControlTemplate"));
        }

        /// <summary>
        /// Je nach Menüzustand umschalten zwischen Menüeinträge sichtbar und nicht sichtbar.
        /// </summary>
        public virtual void UpdateOptionsVisibility()
        {
            if (_currentState.Equals(State.Idle))
            {
                // _chosenOption = ChosenOption.None; --> muss in Subklasse gemacht werden
                foreach (Grid grid in _optionImgTxtGrids)
                {
                    grid.Background = new SolidColorBrush((Color)FindResource("Option" + _optionImgTxtGrids.IndexOf(grid) + "ColorStrong"));
                }
                OptionsGrid.Visibility = System.Windows.Visibility.Hidden;
            }
            else OptionsGrid.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Events für Touch auf Menüschaltfläche registrieren.
        /// </summary>
        public virtual void RegisterEvents()
        {
            MenuButton.PreviewTouchDown += OnButtonTouchDown;
            MenuButton.PreviewTouchMove += OnButtonTouchMove;
            MenuButton.PreviewTouchUp += OnButtonTouchUp;
        }

        /// <summary>
        /// Menü öffnen.
        /// </summary>
        public void OpenMenu()
        {
            _currentState = State.Clicked;
            OptionsVisible = true;
            UpdateOptionsVisibility();
        }

        /// <summary>
        /// Menü schließen.
        /// </summary>
        public void CloseMenu()
        {
            _currentState = State.Idle;
            OptionsVisible = false;
            UpdateOptionsVisibility();
        }

        #endregion

        #region Events

        /// <summary>
        /// Event wird bei TouchDown auf Menüschaltfläche aufgerufen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OnButtonTouchDown(object sender, TouchEventArgs e)
        {
            ButtonTouchDown(sender, e);
        }

        /// <summary>
        /// Event wird bei TouchMove über Menü aufgerufen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OnButtonTouchMove(object sender, TouchEventArgs e)
        {
            ButtonTouchMove(sender, e);
        }

        /// <summary>
        /// Event wird bei TouchUp über Menü aufgerufen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OnButtonTouchUp(object sender, TouchEventArgs e)
        {
            ButtonTouchUp(sender, e);
        }

        /// <summary>
        /// Virtuelles TouchDown-EVent.
        /// Merkt sich Startposition der Fingereingabe, ändert Zustände und blendet Markierungslinie ein.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        protected virtual void ButtonTouchDown(object sender, TouchEventArgs e)
        {
            TouchDownTime = DateTime.Now;
            PreviousState = _currentState;

            // allgemeine behandlung (subkklassen-unabhängig)
            if (_currentState.Equals(State.Idle))
            {
                _currentState = State.Clicked;
            }
            else if (_currentState.Equals(State.Clicked))
            {
                _currentState = State.Uncertain;
            }
            UpdateOptionsVisibility();

            _moveStartPosition = e.TouchDevice.GetTouchPoint(this.MainGrid).Position;
            Point _moveStartPositionOnScreen = this.PointToScreen(_moveStartPosition);

            _touchLine.X1 = _moveStartPosition.X;
            _touchLine.Y1 = _moveStartPosition.Y;
            _touchLine.X2 = _moveStartPosition.X;
            _touchLine.Y2 = _moveStartPosition.Y;

            _touchLine.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Virtuelles TouchMove Event.
        /// Ändert ggf. Menüzustand.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        protected virtual void ButtonTouchMove(object sender, TouchEventArgs e)
        {
            // allgemeine behandlung (subkklassen-unabhängig)

            if (_currentState.Equals(State.Clicked))
            {
                _currentState = State.Hold_Base;
            }
            else if (_currentState.Equals(State.Uncertain))
            {
                _currentState = State.Hold_Base;
            }
            UpdateOptionsVisibility();

            // Rest wird in Subklasse erledigt
        }

        /// <summary>
        /// Virtuelles TouchUp Event.
        /// Entscheidet ob Drag oder Tap ausgeführt, passt Menüzustand an.
        /// Feuert bei ausgewählter Menüoption Event zum Abhören durch Controls, an die das Menü gebunden ist.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        protected virtual bool ButtonTouchUp(object sender, TouchEventArgs e)
        {
            // allgemeine behandlung (subkklassen-unabhängig)
            bool raiseEvent = false;

            DateTime touchUpTime = DateTime.Now;
            TimeSpan touchDuration = touchUpTime.Subtract(TouchDownTime);
            Point touchUpPoint = e.TouchDevice.GetTouchPoint(this.MainGrid).Position;
            Point touchDownUpDifference = new Point(Math.Abs(touchUpPoint.X-_moveStartPosition.X), Math.Abs(touchUpPoint.Y-_moveStartPosition.Y));

            if (_currentState.Equals(State.Hold_Base))
            {
                //Console.WriteLine("_countButtonMoves: " + _countButtonMoves);
                //if (_countButtonMoves > 7 && OptionsGrid.Visibility.Equals(System.Windows.Visibility.Visible))
                _currentState = State.Clicked;
                //else
                //    _currentState = State.Idle;
            }
            else if (_currentState.Equals(State.Uncertain))
            {
                _currentState = State.Idle;
            }
            else if (_currentState.Equals(State.Hold_Option))
            {
                _currentState = State.Idle;
                // pass event to SurfaceWindow1.xaml.cs
                RoutedEventArgs eventargs = new RoutedEventArgs(Menu.MenuOptionSelectedEvent);
                RaiseEvent(eventargs);
                raiseEvent = true;
            }

            // Console.WriteLine(touchDownUpDifference.ToString());
            
            UpdateOptionsVisibility();
            if (touchDuration.Milliseconds <= 300 && touchDownUpDifference.X <= 5 && touchDownUpDifference.Y <= 5)
            {
                if (PreviousState.Equals(State.Clicked))
                    _currentState = State.Idle;
                else
                    _currentState = State.Clicked;
            }
            UpdateOptionsVisibility();
            if (OptionsGrid.Visibility.Equals(System.Windows.Visibility.Hidden))
            {
                OptionsVisible = true;
            }
            else OptionsVisible = !true;
            _touchLine.Visibility = System.Windows.Visibility.Hidden;

            return raiseEvent;
        }

        /*
         * Einkommentieren und entsprechende Events registrieren wenn gewünscht, dass Menüeinträge auch einfach nur per Tap oder wie bei
         * Registernavigation ausgewählt werden kann.
        protected virtual void OptionTouchDown(object sender, TouchEventArgs e)
        {
            // currentState should be "Clicked"
            if (_currentState.Equals(State.Clicked))
            {
                _currentState = State.Uncertain;
            }

            // Capture to the ScaleButton.  
            e.TouchDevice.Capture(this.OptionsGrid, CaptureMode.SubTree);

            // Remember this contact if a contact has not been remembered already.
            if (_optionControlTouchDevice == null)
            {
                _optionControlTouchDevice = e.TouchDevice;
            }
        }

        protected virtual void OptionTouchMove(object sender, TouchEventArgs e)
        {
            // currentState should be "Uncertain" or "Hold_Option"
            if (_currentState.Equals(State.Uncertain))
            {
                _currentState = State.Hold_Option;
            }
            //else
            //    Console.WriteLine("touchdevice != optiontouchdevice");
        }

        protected virtual void OptionTouchUp(object sender, TouchEventArgs e)
        {
            _currentState = State.Idle;

            // pass event to SurfaceWindow1.xaml.cs
            RoutedEventArgs eventargs = new RoutedEventArgs(Menu.MenuOptionSelectedEvent);
            RaiseEvent(eventargs);

            // If this contact is the one that was remembered  
            if (e.TouchDevice == _optionControlTouchDevice)
            {
                // Forget about this contact.
                _optionControlTouchDevice = null;
            }
        }

        protected virtual void DeadZoneTouchMoved(object sender, TouchEventArgs e)
        {
            // currentState should be "Uncertain" or "Hold_Option"
            _currentState = State.Hold_Mid;
        }
        
        */

        #endregion


    }
}

