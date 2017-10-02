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
	/// Interaktionslogik für ErrorPanel.xaml
	/// </summary>
	public partial class ErrorPanel : UserControl
    {
        /// <summary>
        /// Enum-Definition für markierbare Optionen
        /// </summary>
        public enum ChosenControl { MarkAsSolved, Technician, Process1, Process2, Stop, Kill, None }
        protected ChosenControl _chosenControl = ChosenControl.None;
        /// <summary>
        /// aktuell markierte Option
        /// </summary>
        public ChosenControl SelectedControl
        {
            get { return _chosenControl; }
            set { _chosenControl = value; }
        }

        private ProcessInstance Instance;

        /// <summary>
        /// TocuhDownPosition relativ zu SafeSelectButton
        /// </summary>
        protected Point TouchPointRelativeToButton = new Point(0, 0);

        public delegate void OptionChosenEventHandler(object sender, OptionChosenEventArgs e);
        public event OptionChosenEventHandler ErrorPanelChosenOption;

        /// <summary>
        /// Konstruktor
        /// </summary>
		public ErrorPanel(ProcessInstance instance)
		{
			this.InitializeComponent();
            Instance = instance;
		}

        /// <summary>
        /// Event wird aufgerufen wenn andere option markiert wurde.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OptionValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (((SurfaceSlider)sender).Value == 49) _chosenControl = ChosenControl.None;
            else if (((SurfaceSlider)sender).Value == 41.5) _chosenControl = ChosenControl.MarkAsSolved;
            else if (((SurfaceSlider)sender).Value == 31.6) _chosenControl = ChosenControl.Technician;
            else if (((SurfaceSlider)sender).Value == 24.35) _chosenControl = ChosenControl.Process1;
            else if (((SurfaceSlider)sender).Value == 17.1) _chosenControl = ChosenControl.Process2;
            else if (((SurfaceSlider)sender).Value == 7.1) _chosenControl = ChosenControl.Stop;
            else if (((SurfaceSlider)sender).Value == 0) _chosenControl = ChosenControl.Kill;
            UpdateChosenMeasureText();
        }

        /// <summary>
        /// Aktualisiert Texte im Bestätigungsabschnitt entsprechend markierter Option
        /// </summary>
        private void UpdateChosenMeasureText()
        {
            if (_chosenControl.Equals(ChosenControl.None))
            {
                ConfirmMeasureImage.Source = new BitmapImage(new Uri(@"../Images/wnothing_black.png", UriKind.Relative));
                ChosenMeasureText.Text = DoNothingText.Text;
            }
            else if (_chosenControl.Equals(ChosenControl.MarkAsSolved))
            {
                ConfirmMeasureImage.Source = new BitmapImage(new Uri(@"../Images/wok_black.png", UriKind.Relative));
                ChosenMeasureText.Text = SolvedText.Text;
            }
            else if (_chosenControl.Equals(ChosenControl.Technician))
            {
                ConfirmMeasureImage.Source = new BitmapImage(new Uri(@"../Images/wschraubschl_black.png", UriKind.Relative));
                ChosenMeasureText.Text = TechnicianText.Text;
            }
            else if (_chosenControl.Equals(ChosenControl.Process1))
            {
                ConfirmMeasureImage.Source = new BitmapImage(new Uri(@"../Images/wgetriebe_black.png", UriKind.Relative));
                ChosenMeasureText.Text = Correct1Text.Text;
            }
            else if (_chosenControl.Equals(ChosenControl.Process2))
            {
                ConfirmMeasureImage.Source = new BitmapImage(new Uri(@"../Images/wgetriebe_black.png", UriKind.Relative));
                ChosenMeasureText.Text = Correct2Text.Text;
            }
            else if (_chosenControl.Equals(ChosenControl.Stop))
            {
                ConfirmMeasureImage.Source = new BitmapImage(new Uri(@"../Images/stop_black.png", UriKind.Relative));
                ChosenMeasureText.Text = StopText.Text;
            }
            else if (_chosenControl.Equals(ChosenControl.Kill))
            {
                ConfirmMeasureImage.Source = new BitmapImage(new Uri(@"../Images/kill_black.png", UriKind.Relative));
                ChosenMeasureText.Text = KillText.Text;
            }
        }

        /// <summary>
        /// Event wird aufgerufen wenn TouchDown auf Grid
        /// Dann Berechnung ob innerhalb von SafeSelect-Feld
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OptionTouchDown(object sender, TouchEventArgs e)
        {
            // as long as TouchPoint is within Button-Container, X and Y range from 0 to 38
            TouchPointRelativeToButton = new Point(e.TouchDevice.GetTouchPoint(ConfirmButton).Position.X, e.TouchDevice.GetTouchPoint(ConfirmButton).Position.Y);

            if (TouchPointRelativeToButton.X >= 0 && TouchPointRelativeToButton.X <= 38 && TouchPointRelativeToButton.Y >= 0 && TouchPointRelativeToButton.Y <= 38)
            {
                ((ScatterViewItem)Parent).CanMove = !true;
                ((ScatterViewItem)Parent).CanRotate = !true;
                e.Handled = true;
            }
            // Console.WriteLine("YOnButton " + e.TouchDevice.GetTouchPoint(ConfirmButton).Position.Y);

        }

        /// <summary>
        /// Event wird aufgerufen wenn TouchMove auf Grid
        /// Dann Berechnung ob innerhalb von SafeSelect-Feld.
        /// Falls ja entsprechendes visuelles Feedback und Mitbewegen des Safeselect-Feldes.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OptionTouchMove(object sender, TouchEventArgs e)
        {
            Point TempTouchPointRelativeToButton = new Point(e.TouchDevice.GetTouchPoint(ConfirmButton).Position.X, e.TouchDevice.GetTouchPoint(ConfirmButton).Position.Y);
            if (TempTouchPointRelativeToButton.Y >= 0 && TempTouchPointRelativeToButton.Y <= 38)
            {
                double movingLength = TempTouchPointRelativeToButton.X - TouchPointRelativeToButton.X;
                double newLeftButtonMargin = ConfirmButton.Margin.Left + movingLength;
                if (newLeftButtonMargin < 21)
                {
                    newLeftButtonMargin = 21;
                }
                else if (newLeftButtonMargin > 121)
                {
                    newLeftButtonMargin = 121;
                }
                ConfirmButton.Margin = new Thickness(newLeftButtonMargin, ConfirmButton.Margin.Top, ConfirmButton.Margin.Right, ConfirmButton.Margin.Bottom);
                ResetTextLook();
                if (newLeftButtonMargin < 110)
                {
                    ChosenMeasureText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    ConfirmRectangle.Fill = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));

                    if (_chosenControl.Equals(ChosenControl.None))
                    {
                        DoNothingText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    }
                    else if (_chosenControl.Equals(ChosenControl.MarkAsSolved))
                    {
                        SolvedText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    }
                    else if (_chosenControl.Equals(ChosenControl.Technician))
                    {
                        TechnicianText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    }
                    else if (_chosenControl.Equals(ChosenControl.Process1))
                    {
                        Correct1Text.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    }
                    else if (_chosenControl.Equals(ChosenControl.Process2))
                    {
                        Correct2Text.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    }
                    else if (_chosenControl.Equals(ChosenControl.Stop))
                    {
                        StopText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    }
                    else if (_chosenControl.Equals(ChosenControl.Kill))
                    {
                        KillText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    }
                }
                else
                {
                    ChosenMeasureText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    ConfirmRectangle.Fill = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    if (_chosenControl.Equals(ChosenControl.None))
                    {
                        DoNothingText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    }
                    else if (_chosenControl.Equals(ChosenControl.MarkAsSolved))
                    {
                        SolvedText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    }
                    else if (_chosenControl.Equals(ChosenControl.Technician))
                    {
                        TechnicianText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    }
                    else if (_chosenControl.Equals(ChosenControl.Process1))
                    {
                        Correct1Text.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    }
                    else if (_chosenControl.Equals(ChosenControl.Process2))
                    {
                        Correct2Text.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    }
                    else if (_chosenControl.Equals(ChosenControl.Stop))
                    {
                        StopText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    }
                    else if (_chosenControl.Equals(ChosenControl.Kill))
                    {
                        KillText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    }
                }
            }
        }

        /// <summary>
        /// Event wird aufgerufen wenn TouchUp auf Grid
        /// Wenn SafeSelect-Feld weit genug gezogen wurde dann Event weiterleiten an SurfaceWindow
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OptionTouchUp(object sender, TouchEventArgs e)
        {
            ((ScatterViewItem)Parent).CanMove = true;
            ((ScatterViewItem)Parent).CanRotate = true;

            ResetTextLook();
            var bc = new BrushConverter();
            ChosenMeasureText.Foreground = Brushes.White;
            ConfirmRectangle.Fill = (Brush)bc.ConvertFrom("#FFE2E2E2");

            if (ConfirmButton.Margin.Left >= 110)
            {
                // pressedButton.Margin = new Thickness(21, pressedButton.Margin.Top, pressedButton.Margin.Right, pressedButton.Margin.Bottom);
                // Begin action
                // ToDo: pass event to SurfaceWindow1.xaml.cs and RunningInstancesControl  

                // just as placeholder for now
                ResetButton(ConfirmButton);
                ErrorPanelChosenOption(this, new OptionChosenEventArgs(Instance,_chosenControl));
            }
            else
            {
                // Reset Button
                ResetButton(ConfirmButton);
            }
        }

        /// <summary>
        /// Animiert zurückgleitend es SafeSelect-Feldes in Ausgangsposition.
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
        /// Event wird anch ZUrückgleiten-Animation aufgerufen und stellt sicher, dass Feld an exakter Position steht.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void resetStoryboard_Completed(object sender, EventArgs e)
        {
            ConfirmButton.Margin = new Thickness(21, ConfirmButton.Margin.Top, ConfirmButton.Margin.Right, ConfirmButton.Margin.Bottom);
        }

        /// <summary>
        /// Setzt visuelles Feedback zurück.
        /// </summary>
        private void ResetTextLook()
        {
            DoNothingText.Foreground = new SolidColorBrush(Colors.White);
            SolvedText.Foreground = new SolidColorBrush(Colors.White);
            TechnicianText.Foreground = new SolidColorBrush(Colors.White);
            Correct1Text.Foreground = new SolidColorBrush(Colors.White);
            Correct2Text.Foreground = new SolidColorBrush(Colors.White);
            StopText.Foreground = new SolidColorBrush(Colors.White);
            KillText.Foreground = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// Klasse für eigene Eventparameterübergabe bzgl gewählter Option.
        /// </summary>
        public class OptionChosenEventArgs : EventArgs
        {
            public readonly ProcessInstance Instance;
            public readonly ChosenControl ChosenOption;

            public OptionChosenEventArgs(ProcessInstance instance, ChosenControl chosenOption)
            {
                Instance = instance;
                ChosenOption = chosenOption;
            }
        }
	}
}