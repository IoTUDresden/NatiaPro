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
using System.Windows.Media.Animation;
using Prototype_05.InstanceData;

namespace Prototype_05
{
    /// <summary>
    /// UserControl zur Steuerung von Instanzzustand.
    /// </summary>
    public partial class InstanceControlWidget : UserControl
    {
        #region Deklaration
        /// <summary>
        /// Enum-Defintionen für angebotene Optionen
        /// </summary>
        public enum ChosenControl { PausePlay, Pause, Stop, Kill, None }
        protected ChosenControl _chosenControl = ChosenControl.None;
        /// <summary>
        /// Aktuell ausgewählte Option.
        /// </summary>
        public ChosenControl SelectedControl
        {
            get { return _chosenControl; }
            set { _chosenControl = value; }
        }

        /// <summary>
        /// Aktuell gewählter Navigationstab.
        /// </summary>
        private int CurrentNavigation = 0;
        /// <summary>
        /// Touchgerät zur Differenzierung von mehreren Fingern auf Tabs.
        /// </summary>
        private TouchDevice NavigationTouchDevice;
        /// <summary>
        /// aktuell als Vorschau ausgewählter Navigationstab.
        /// </summary>
        private int PreviewNavigation = -1;

        protected ProcessInstance _instance;
        public ProcessInstance Instance
        {
            get { return _instance; }
            set { _instance = value; }
        }

        /// <summary>
        /// Touchgerät zur Differenzierung von mehreren Fingern auf SafeSelect.
        /// </summary>
        protected TouchDevice OptionControlTouchDevice;
        /// <summary>
        /// Position Finger relativ zu SafeSelect-Button
        /// </summary>
        protected double TouchPointXOnButton = 0;

        /// <summary>
        /// letzter Zeitpunkt eines Taps auf Instanzname.
        /// </summary>
        protected DateTime TouchDownTimeForDoubleTap;


        public delegate void OptionChosenEventHandler(object sender, OptionChosenEventArgs e);
        public event OptionChosenEventHandler ControlInstanceChosenOption;
        
        /// <summary>
        /// MenuOptionSelectedEvent, needed to pass event outside of InstanceControlWidget-Class
        /// </summary>
        public static readonly RoutedEvent ShowInstancePanelEvent = EventManager.RegisterRoutedEvent("ShowInstancePanel", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(InstanceControlWidget));
        public event RoutedEventHandler ShowInstancePanel
        {
            add { AddHandler(ShowInstancePanelEvent, value); }
            remove { RemoveHandler(ShowInstancePanelEvent, value); }
        }
        #endregion

        /// <summary>
        /// Konstruktor
        /// </summary>
        public InstanceControlWidget()
        {
            this.InitializeComponent();
            TouchDownTimeForDoubleTap = DateTime.Now;
        }

        #region Methoden
        /// <summary>
        /// Bindet Instanz an ControlWidget.
        /// </summary>
        /// <param name="instance">wenn <value>null</value> dann Widget zur Zeit ausgeblendet</param>
        public void BindInstance(ProcessInstance instance) //, UserControl ioc
        {
            _chosenControl = ChosenControl.None;
            OptionSlider.Value = 49;
            _instance = instance;
            //_startedByIOC = ioc;
            if (instance == null)
            {
                this.Visibility = System.Windows.Visibility.Hidden;
                CurrentNavigation = 0;
                CreateExpandStoryBoard(InstanceRect, InstanceText);
                CreateReduceStoryBoard(AllRect, AllText);
                var bc = new BrushConverter();
                InstanceRect.Fill = (Brush)bc.ConvertFrom("#FFAF7648");
                AllRect.Fill = new SolidColorBrush((Color)FindResource("InstanceColorFade1"));
                SwitchSlider();
            }
            else
            {
                if (!instance.State.Equals(Enums.ProcessState.executing))
                {
                    PausePlayText.Text = "Start this instance";
                    PausePlayImage.Source = new BitmapImage(new Uri(@"Images/play_white.png", UriKind.Relative));
                }
                else
                {
                    PausePlayText.Text = "Pause this instance";
                    PausePlayImage.Source = new BitmapImage(new Uri(@"Images/pause_white.png", UriKind.Relative));
                }
                TitleText.Text = instance.Name + " - ID " + instance.Id;
                InstanceText.Text = "Instance " + instance.Id;
                StopText.Text = "Stop this instance";
                KillText.Text = "Kill this instance";
                this.Visibility = System.Windows.Visibility.Visible;
            }
        }

        /// <summary>
        /// Animiert Zurückgleiten des SafeSelect-Buttons.
        /// </summary>
        /// <param name="button"></param>
        private void ResetButton(SurfaceButton button)
        {
            ThicknessAnimation moveAnimation = new ThicknessAnimation();
            moveAnimation.Duration = TimeSpan.FromSeconds(0.1);
            moveAnimation.FillBehavior = FillBehavior.Stop;
            moveAnimation.From = new Thickness(button.Margin.Left, button.Margin.Top, button.Margin.Right, button.Margin.Bottom);
            moveAnimation.To = new Thickness(21, button.Margin.Top, button.Margin.Right, button.Margin.Bottom);

            Storyboard resetStoryboard = new Storyboard();
            resetStoryboard.Children.Add(moveAnimation);
            Storyboard.SetTargetName(moveAnimation, button.Name);
            Storyboard.SetTargetProperty(moveAnimation, new PropertyPath(TextBox.MarginProperty));
            resetStoryboard.Completed += new EventHandler(resetStoryboard_Completed);

            resetStoryboard.Begin(this);
        }

        /// <summary>
        /// Gibt visuelles Feedback welcher Tap bei aktueller Fingerposition als nächstes aktiv wäre.
        /// </summary>
        /// <param name="touchPoint"></param>
        private void UpdateNavigationPreview(Point touchPoint)
        {
            double cellHeight = NavigationGrid.Height / 2;
            if (touchPoint.Y >= 0 * cellHeight && touchPoint.Y < 1 * cellHeight) PreviewNavigation = 0;
            if (touchPoint.Y >= 1 * cellHeight && touchPoint.Y < 2 * cellHeight) PreviewNavigation = 1;

            var bc = new BrushConverter();
            if (PreviewNavigation == 0 || CurrentNavigation == 0) InstanceRect.Fill = (Brush)bc.ConvertFrom("#FFAF7648");
            else InstanceRect.Fill = (Brush)bc.ConvertFrom("#7EAF7648");
            if (PreviewNavigation == 1 || CurrentNavigation == 1) AllRect.Fill = new SolidColorBrush((Color)FindResource("InstanceColor1"));
            else AllRect.Fill = new SolidColorBrush((Color)FindResource("InstanceColorFade1"));
        }

        /// <summary>
        /// Wechselt Inhalt je nach neuem Tab.
        /// </summary>
        /// <param name="newNavigation"></param>
        private void SwitchNavigation(int newNavigation)
        {
            if (PreviewNavigation == -1) return;

            switch (CurrentNavigation)
            {
                case 0:
                    InstanceRect.Fill = new SolidColorBrush((Color)FindResource("Category0ColorFade"));
                    CreateReduceStoryBoard(InstanceRect, InstanceText);
                    break;
                case 1:
                    AllRect.Fill = new SolidColorBrush((Color)FindResource("Category0ColorFade"));
                    CreateReduceStoryBoard(AllRect, AllText);
                    break;
            }

            switch (newNavigation)
            {
                case 0:
                    if (!Instance.State.Equals(Enums.ProcessState.executing))
                    {
                        PausePlayText.Text = "Pause this instance";
                    }
                    else
                    {
                        PausePlayText.Text = "Start this instance";
                    }
                    DoNothingText.Text = "Don't change Instance's state";
                    StopText.Text = "Stop this instance";
                    KillText.Text = "Kill this instance";
                    TitleText.Text = Instance.Name + " - ID " + Instance.Id;
                    CreateExpandStoryBoard(InstanceRect, InstanceText);
                    break;
                case 1:
                    DoNothingText.Text = "Don't change any Instance's state";
                    PausePlayText.Text = "Start all instances";
                    StopText.Text = "Stop all instances";
                    KillText.Text = "Kill all instances";
                    TitleText.Text = "All instances of '" + Instance.Name + "' (" + Instance.BoundModel.CurrentlyExecutedInstances + " Instances)";
                    CreateExpandStoryBoard(AllRect, AllText);
                    break;
            }
            CurrentNavigation = newNavigation;
            PreviewNavigation = -1;
            UpdateChosenMeasureText();
            SwitchSlider();
        }

        /// <summary>
        /// Schaltet zwischen Instance- und AllInstances-Slider um.
        /// </summary>
        private void SwitchSlider()
        {
            OptionSlider.Value = 49;
            OptionSlider2.Value = 49;
            if (CurrentNavigation == 0)
            {
                this.Height = 315;
                SolvingInfoGrid.RowDefinitions[1].Height = new GridLength(124);
                Solve2Grid.RowDefinitions[1].Height = new GridLength(0);
                OptionSlider.Visibility = System.Windows.Visibility.Visible;
                OptionSlider2.Visibility = System.Windows.Visibility.Hidden;
                if (Instance != null)
                {
                    if (Instance.State.Equals(Enums.ProcessState.executing)) PausePlayImage.Source = new BitmapImage(new Uri(@"Images/play_white.png", UriKind.Relative));

                }
                else PausePlayImage.Source = new BitmapImage(new Uri(@"Images/pause_white.png", UriKind.Relative));
            }
            else
            {
                this.Height = 341;
                SolvingInfoGrid.RowDefinitions[1].Height = new GridLength(150);
                Solve2Grid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
                OptionSlider2.Visibility = System.Windows.Visibility.Visible;
                OptionSlider.Visibility = System.Windows.Visibility.Hidden;
                PausePlayImage.Source = new BitmapImage(new Uri(@"Images/play_white.png", UriKind.Relative));
            }
        }

        /// <summary>
        /// Aktualisiert Text der aktuell gewählte Option anzeigt.
        /// </summary>
        private void UpdateChosenMeasureText()
        {
            if (_chosenControl.Equals(ChosenControl.None))
            {
                ConfirmMeasureImage.Source = new BitmapImage(new Uri(@"Images/wnothing_black.png", UriKind.Relative));
                ChosenMeasureText.Text = DoNothingText.Text;
            }
            else if (_chosenControl.Equals(ChosenControl.PausePlay))
            {
                if (CurrentNavigation == 0)
                {
                    if (Instance.State.Equals(Enums.ProcessState.executing)) ConfirmMeasureImage.Source = new BitmapImage(new Uri(@"Images/pause_black.png", UriKind.Relative));
                    else ConfirmMeasureImage.Source = new BitmapImage(new Uri(@"Images/play_black.png", UriKind.Relative));
                }
                else ConfirmMeasureImage.Source = new BitmapImage(new Uri(@"Images/play_black.png", UriKind.Relative));
                ChosenMeasureText.Text = PausePlayText.Text;
            }
            else if (_chosenControl.Equals(ChosenControl.Pause))
            {
                ConfirmMeasureImage.Source = new BitmapImage(new Uri(@"Images/pause_black.png", UriKind.Relative));
                ChosenMeasureText.Text = PauseText.Text;
            }
            else if (_chosenControl.Equals(ChosenControl.Stop))
            {
                ConfirmMeasureImage.Source = new BitmapImage(new Uri(@"Images/stop_black.png", UriKind.Relative));
                ChosenMeasureText.Text = StopText.Text;
            }
            else if (_chosenControl.Equals(ChosenControl.Kill))
            {
                ConfirmMeasureImage.Source = new BitmapImage(new Uri(@"Images/kill_black.png", UriKind.Relative));
                ChosenMeasureText.Text = KillText.Text;
            }
        }

        /// <summary>
        /// Animiert Aktivierung eines Tab.
        /// </summary>
        /// <param name="rect">zu verschiebendes Tab-Rechteck</param>
        /// <param name="textbox">zu verschiebende Tab-TextBox</param>
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
        /// Animiert Deaktivierung eines Tab.
        /// </summary>
        /// <param name="rect">zu verschiebendes Tab-Rechteck</param>
        /// <param name="textbox">zu verschiebende Tab-TextBox</param>
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
        #endregion

        #region Events

        /// <summary>
        /// Event wird aufgerufen wenn TouchDown auf SafeSelect.
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
        /// Event wird aufgerufen wenn TouchMove auf SafeSelect.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OptionTouchMove(object sender, TouchEventArgs e)
        {
            // If this contact is the one that was remembered  
            if (e.TouchDevice == OptionControlTouchDevice)
            {
                double touchPointXOnGrid = OptionControlTouchDevice.GetTouchPoint(this.SolvingGrid).Position.X;
                touchPointXOnGrid = touchPointXOnGrid - TouchPointXOnButton;
                if (touchPointXOnGrid < 21)
                {
                    touchPointXOnGrid = 21;
                }
                else if (touchPointXOnGrid > 121)
                {
                    touchPointXOnGrid = 121;
                }
                ((SurfaceButton)sender).Margin = new Thickness(touchPointXOnGrid, ((SurfaceButton)sender).Margin.Top, ((SurfaceButton)sender).Margin.Right, ((SurfaceButton)sender).Margin.Bottom);

                DoNothingText.Foreground = new SolidColorBrush(Colors.White);
                PausePlayText.Foreground = new SolidColorBrush(Colors.White);
                StopText.Foreground = new SolidColorBrush(Colors.White);
                KillText.Foreground = new SolidColorBrush(Colors.White);
                if (touchPointXOnGrid < 100)
                {
                    ChosenMeasureText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    ConfirmRectangle.Fill = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    if (_chosenControl.Equals(ChosenControl.None))
                        DoNothingText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    else if (_chosenControl.Equals(ChosenControl.PausePlay))
                        PausePlayText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    else if (_chosenControl.Equals(ChosenControl.Stop))
                        StopText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    else if (_chosenControl.Equals(ChosenControl.Kill))
                        KillText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                }
                else
                {
                    ChosenMeasureText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    ConfirmRectangle.Fill = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    if (_chosenControl.Equals(ChosenControl.None))
                        DoNothingText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    else if (_chosenControl.Equals(ChosenControl.PausePlay))
                        PausePlayText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    else if (_chosenControl.Equals(ChosenControl.Stop))
                        StopText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    else if (_chosenControl.Equals(ChosenControl.Kill))
                        KillText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                }
            }
        }

        /// <summary>
        /// Event wird aufgerufen wenn TouchUp auf SafeSelect.
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
                DoNothingText.Foreground = new SolidColorBrush(Colors.White);
                PausePlayText.Foreground = new SolidColorBrush(Colors.White);
                StopText.Foreground = new SolidColorBrush(Colors.White);
                KillText.Foreground = new SolidColorBrush(Colors.White);
                ChosenMeasureText.Foreground = Brushes.White;
                ConfirmRectangle.Fill = (Brush)bc.ConvertFrom("#FFE2E2E2");

                if (pressedButton.Margin.Left >= 100)
                {
                    pressedButton.Margin = new Thickness(21, pressedButton.Margin.Top, pressedButton.Margin.Right, pressedButton.Margin.Bottom);
                    // pass event to SurfaceWindow1.xaml.cs
                    if (CurrentNavigation == 0)
                        ControlInstanceChosenOption(this, new OptionChosenEventArgs(_instance, false, _chosenControl));
                    else
                        ControlInstanceChosenOption(this, new OptionChosenEventArgs(_instance, true, _chosenControl));
                    BindInstance(null); //, null
                }
                else
                {
                    // Reset Button
                    ResetButton(pressedButton);
                }
            }
        }

        /// <summary>
        /// Event wird aufgerufen wenn Zurückgleiten-Animation beendet.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void resetStoryboard_Completed(object sender, EventArgs e)
        {
            ConfirmButton.Margin = new Thickness(21, ConfirmButton.Margin.Top, ConfirmButton.Margin.Right, ConfirmButton.Margin.Bottom);
        }

        /// <summary>
        /// Event wird aufgerufen wenn TouchDown auf Tab.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void NavigationTouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            // Capture to the ScaleButton.  
            e.TouchDevice.Capture(this.NavigationGrid);

            // Remember this contact if a contact has not been remembered already.  
            // This contact is then used to move the ellipse around.
            if (NavigationTouchDevice == null)
            {
                NavigationTouchDevice = e.TouchDevice;
                UpdateNavigationPreview(NavigationTouchDevice.GetTouchPoint(this.NavigationGrid).Position);
            }
            // Mark this event as handled.  
            e.Handled = true;
        }

        /// <summary>
        /// Event wird aufgerufen wenn TouchMove auf Tab.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void NavigationTouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
            if (e.TouchDevice == NavigationTouchDevice)
            {
                UpdateNavigationPreview(NavigationTouchDevice.GetTouchPoint(this.NavigationGrid).Position);
            }
            // Mark this event as handled.  
            e.Handled = true;
        }

        /// <summary>
        /// Event wird aufgerufen wenn TouchUp auf Tab.
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
        /// Event wird aufgerufen wenn Nutzer einen der Slider bewegt hat.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OptionValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (((SurfaceSlider)sender).Value == 49) _chosenControl = ChosenControl.None;
            else if (((SurfaceSlider)sender).Value == 29 || ((SurfaceSlider)sender).Value == 33.6) _chosenControl = ChosenControl.PausePlay;
            else if (((SurfaceSlider)sender).Value == 22.4) _chosenControl = ChosenControl.Pause;
            else if (((SurfaceSlider)sender).Value == 14.5 || ((SurfaceSlider)sender).Value == 11.2) _chosenControl = ChosenControl.Stop;
            else if (((SurfaceSlider)sender).Value == 0) _chosenControl = ChosenControl.Kill;
            UpdateChosenMeasureText();
        }

        /// <summary>
        /// Event wird aufgerufen wenn Tap auf Instanzname erfolgt ist.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void InstanceNameClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            TimeSpan timeSpanBetweeenClicks = DateTime.Now.Subtract(TouchDownTimeForDoubleTap);

            if ((DateTime.Now.Subtract(TouchDownTimeForDoubleTap)).Milliseconds < 600)
            {
                // pass event to SurfaceWindow1.xaml.cs
                RoutedEventArgs eventargs = new RoutedEventArgs(InstanceControlWidget.ShowInstancePanelEvent);
                RaiseEvent(eventargs);
            }

            TouchDownTimeForDoubleTap = DateTime.Now;
        }
        #endregion
    }

    /// <summary>
    /// Klasse für eigene Eventparameterübergabe bzgl gewählter Option.
    /// </summary>
    public class OptionChosenEventArgs : EventArgs
    {
        //public readonly string[] FilePathList;
        public readonly ProcessInstance Instance;
        public readonly bool OptionForAll;
        public readonly InstanceControlWidget.ChosenControl ChosenOption;

        public OptionChosenEventArgs(ProcessInstance instance, bool optionForAll, InstanceControlWidget.ChosenControl chosenOption)
        {
            //FilePathList = filePathList;
            Instance = instance;
            OptionForAll = optionForAll;
            ChosenOption = chosenOption;
        }
    }
}