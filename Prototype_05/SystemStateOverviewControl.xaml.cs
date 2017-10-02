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
using System.Windows.Media.Animation;

namespace Prototype_05
{
	/// <summary>
	/// Interaktionslogik für SystemStateOverviewControl.xaml
	/// </summary>
	public partial class SystemStateOverviewControl : UserControl
    {

        protected Menu _attachedLogMenu;
        /// <summary>
        /// Menü für selektierten Eintrag in Log-Tabelle.
        /// Wird derzeit nicht angezeigt.
        /// </summary>
        public Menu AttachedLogMenu
        {
            get { return _attachedLogMenu; }
            set { _attachedLogMenu = value; }
        }

        protected Menu _attachedErrorMenu;
        /// <summary>
        /// Menü für selektierten Eintrag in Error-Tabelle.
        /// Wird derzeit nicht angezeigt.
        /// </summary>
        public Menu AttachedErrorMenu
        {
            get { return _attachedErrorMenu; }
            set { _attachedErrorMenu = value; }
        }

        private SystemOverviewArea _boundArea = new SystemOverviewArea();
        /// <summary>
        /// zugehöriger Bereichsrahmen
        /// </summary>
        public SystemOverviewArea BoundArea
        {
            get { return _boundArea; }
            set { _boundArea = value; }
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

        Storyboard UpdateStoryboard;
        Rectangle AnimatedRectangle;

        /// <summary>
        /// MenuOptionSelectedEvent, needed to pass event outside of SystemStateOverviewControl-Class
        /// </summary>
        public static readonly RoutedEvent AttachedMenuOptionSelectedEvent = EventManager.RegisterRoutedEvent("AttachedMenuOptionSelected", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(SystemStateOverviewControl));
        public event RoutedEventHandler AttachedMenuOptionSelected
        {
            add { AddHandler(AttachedMenuOptionSelectedEvent, value); }
            remove { RemoveHandler(AttachedMenuOptionSelectedEvent, value); }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
		public SystemStateOverviewControl()
		{
            this.InitializeComponent();
            AddMenus();
            CreateUpdateAnimation();
        }

        /// <summary>
        /// Erzeugt Menü für Log- bzw Error-Tabelle
        /// </summary>
        private void AddMenus()
        {
            MenuThreeOptions LogMenu = new MenuThreeOptions(3, false);
            LogMenu.SetOptions("Show Details about Model", "Show Details about Instance", "Start this Process again", "Images/lupe_black.png", "Images/lupe_black.png", "Images/play_black.png");
            LogMenu.AdaptMenuColorToSystemStateOverview();
            LogMenu.HorizontalAlignment = HorizontalAlignment.Right;
            LogMenu.VerticalAlignment = VerticalAlignment.Top;
            _attachedLogMenu = LogMenu;
            Grid.SetRowSpan(LogMenu, 3);
            Grid.SetColumn(LogMenu, 1);
            Grid.SetColumnSpan(LogMenu, 2);
            MainGrid.Children.Add(LogMenu);
            _attachedLogMenu.MenuOptionSelected += new RoutedEventHandler(AttachedMenu_MenuOptionSelected);
            LogMenu.Visibility = System.Windows.Visibility.Hidden;

            MenuTwoOptions ErrorMenu = new MenuTwoOptions(2, false);
            ErrorMenu.SetOptions("Show Possibilites to solve this error", "Guess another option...?", "Images/lupe_black.png", "Images/maybe.png");
            ErrorMenu.RemoveButtonBackgroundColorAndSetTextWhite();
            ErrorMenu.HorizontalAlignment = HorizontalAlignment.Right;
            ErrorMenu.VerticalAlignment = VerticalAlignment.Top;
            _attachedErrorMenu = ErrorMenu;
            Grid.SetRowSpan(ErrorMenu, 3);
            Grid.SetColumn(ErrorMenu, 1);
            Grid.SetColumnSpan(ErrorMenu, 2);
            MainGrid.Children.Add(ErrorMenu);
            _attachedErrorMenu.MenuOptionSelected += new RoutedEventHandler(AttachedMenu_MenuOptionSelected);
            ErrorMenu.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Aktualisiert Textboxen, die Systeminformationen anzeigen
        /// </summary>
        /// <param name="allInstancesAmount">neue Gesamtanzahl Instanzen</param>
        /// <param name="executingInstancesAmount">neue Anzahl ausführender Instanzen</param>
        /// <param name="pausedWaitingInstancesAmount">neue Anzahl wartender oder pausierter Instanzen</param>
        /// <param name="faultyInstancesAmount">neue Anzahl fehlerhafter Instanzen</param>
        public void UpdateSystemFacts(int allInstancesAmount, int executingInstancesAmount, int pausedWaitingInstancesAmount, int faultyInstancesAmount)
        {
            RunningInstancesAmount.Text = allInstancesAmount.ToString();
            ExecutingInstancesAmount.Text = executingInstancesAmount.ToString();
            PausedInstancesAmount.Text = pausedWaitingInstancesAmount.ToString();
            FaultyInstancesAmount.Text = faultyInstancesAmount.ToString();
        }


        /// <summary>
        /// Fügt Log neuen Menüeintrag zu. Überschreibt ältesten Logeintrag.
        /// </summary>
        /// <param name="newLogType">Art der zu meldenden Aktion</param>
        /// <param name="newLogTime">Zeitpunkt des Eintretens der Aktion</param>
        /// <param name="newLogProcess">an Aktion beteiligtes Prozessmodell oder Prozessinstanz</param>
        public void UpdateLog(string newLogType, string newLogTime, string newLogProcess)
        {
            AnimatedRectangle.Visibility = System.Windows.Visibility.Visible;
            UpdateStoryboard.Begin(this);
            LogType2.Text = LogType1.Text;
            LogType1.Text = LogType0.Text;
            LogType0.Text = newLogType;
            LogTime2.Text = LogTime1.Text;
            LogTime1.Text = LogTime0.Text;
            LogTime0.Text = newLogTime;
            LogProcess2.Text = LogProcess1.Text;
            LogProcess1.Text = LogProcess0.Text;
            LogProcess0.Text = newLogProcess;
        }

        /// <summary>
        /// Event wird aufgerufen, wenn im zugehörigen Menü ein Eintrag ausgewählt wurde.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void AttachedMenu_MenuOptionSelected(object sender, RoutedEventArgs e)
        {
            // pass event to SurfaceWindow1.xaml.cs for showing DetailPanel
            //RoutedEventArgs eventargs = new RoutedEventArgs(InstanceControlWidget.AttachedMenuOptionSelectedEvent);
            //RaiseEvent(eventargs);
        }

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

        public void SwitchToErrorView(string errorType, string errorTime, string errorInstance)
        {
            ErrorText.Text = "Errors (1)";
            LogGrid.Visibility = System.Windows.Visibility.Hidden;
            ErrorGrid.Visibility = System.Windows.Visibility.Visible;
            ErrorTime0.Text = errorTime;
            ErrorType0.Text = errorType;
            ErrorProcess0.Text = errorInstance;
            CreateExpandStoryBoard(ErrorRect, ErrorText);
            CreateReduceStoryBoard(LastActions, LastActionsText);
            Console.WriteLine(ErrorType0.Visibility.ToString());
            MessageRectangle0.Stroke = (SolidColorBrush)FindResource("ErrorMessageBorderBrush");
            MessageRectangle0.StrokeThickness = 4;
        }

        public void SwitchToLogView()
        {
            ErrorText.Text = "Errors (-)";
            CreateReduceStoryBoard(ErrorRect, ErrorText);
            CreateExpandStoryBoard(LastActions, LastActionsText);
            LogGrid.Visibility = System.Windows.Visibility.Visible;
            ErrorGrid.Visibility = System.Windows.Visibility.Hidden;
            MessageRectangle0.Stroke = new SolidColorBrush(Colors.Gainsboro);
            MessageRectangle0.StrokeThickness = 1;
        }

        #region Animations
        /// <summary>
        /// Animiere Aktivieren eines Tabs.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="textbox"></param>
        private void CreateExpandStoryBoard(Rectangle rect, TextBox textbox)
        {
            DoubleAnimation expandAnimation = new DoubleAnimation();
            expandAnimation.From = 85;
            expandAnimation.To = 100;
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
            reduceAnimation.From = 100;
            reduceAnimation.To = 85;
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

        private void CreateUpdateAnimation()
        {
            NameScope.SetNameScope(this, new NameScope());
            AnimatedRectangle = new Rectangle();

            AnimatedRectangle.Name = "AnimatedRectangle";
            this.RegisterName(AnimatedRectangle.Name, AnimatedRectangle);

            ErrorRect.Name = "ErrorRectangle";
            this.RegisterName(ErrorRect.Name, ErrorRect);
            LastActions.Name = "LogRectangle";
            this.RegisterName(LastActions.Name, LastActions);
            ErrorText.Name = "ErrorText";
            this.RegisterName(ErrorText.Name, ErrorText);
            LastActionsText.Name = "LogText";
            this.RegisterName(LastActionsText.Name, LastActionsText);

            AnimatedRectangle.Width = LogGrid.Width;
            AnimatedRectangle.Height = 60;
            AnimatedRectangle.Margin = new Thickness(0, -30, 0, 0);
            AnimatedRectangle.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            AnimatedRectangle.Stroke = null;
            Grid.SetRow(AnimatedRectangle, 2);
            Grid.SetColumn(AnimatedRectangle, 1);
            MainGrid.Children.Add(AnimatedRectangle);
            AnimatedRectangle.Fill = (LinearGradientBrush)this.FindResource("AnimatedRectangleBrush");
            AnimatedRectangle.Visibility = System.Windows.Visibility.Hidden;

            ThicknessAnimation updateAnimation = new ThicknessAnimation();
            updateAnimation.Duration = TimeSpan.FromSeconds(1.2);
            updateAnimation.From = new Thickness(AnimatedRectangle.Margin.Left, AnimatedRectangle.Margin.Top, AnimatedRectangle.Margin.Right, AnimatedRectangle.Margin.Bottom);
            updateAnimation.To = new Thickness(AnimatedRectangle.Margin.Left, LogGrid.Height + 30, AnimatedRectangle.Margin.Right, AnimatedRectangle.Margin.Bottom);

            UpdateStoryboard = new Storyboard();
            UpdateStoryboard.Children.Add(updateAnimation);
            Storyboard.SetTargetName(updateAnimation, AnimatedRectangle.Name);
            Storyboard.SetTargetProperty(updateAnimation, new PropertyPath(Rectangle.MarginProperty));
        }
        #endregion
	}
}