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
using Prototype_05.ModelData;

namespace Prototype_05
{
	/// <summary>
	/// Interaktionslogik für ModelRemovingPanel.xaml
    /// Bietet Möglichkeit zum Löschen von Prozessmodellden.
	/// </summary>
	public partial class ModelRemovingPanel : UserControl
    {
        #region Deklarationen
        /// <summary>
        /// TouchDown-Position relativ zu Ok-Button
        /// </summary>
        private Point TouchPointRelativeToButtonYes = new Point(0, 0);
        /// <summary>
        /// TouchDown-Position relativ zu Cancel-Button
        /// </summary>
        private Point TouchPointRelativeToButtonNo = new Point(0, 0);

        /// <summary>
        /// Gibt an ob Ok-Button zur Zeit vom Nutzer bewegt wird.
        /// </summary>
        private Boolean _yesButtonIsMoving = false;
        public Boolean YesButtonIsMoving
        {
            get { return _yesButtonIsMoving; }
            set
            {
                _yesButtonIsMoving = value;
                if (!value) ResetButton(OkButton);
            }
        }
        private Boolean _noButtonIsMoving = false;
        /// <summary>
        /// Gibt an ob Cancel-Button zur Zeit vom Nutzer bewegt wird.
        /// </summary>
        public Boolean NoButtonIsMoving
        {
            get { return _noButtonIsMoving; }
            set
            {
                _noButtonIsMoving = value;
                if (!value) ResetButton(CancelButton);
            }
        }

        Storyboard UpdateStoryboard;
        Rectangle AnimatedRectangle;

        public delegate void SendDeleteModelsEventHandler(object sender, RemoveModelsEventArgs e);
        public event SendDeleteModelsEventHandler ModelRemovingPanelClosed;
        #endregion

        /// <summary>
        /// Konstruktor
        /// </summary>
		public ModelRemovingPanel()
		{
            this.InitializeComponent();
            CreateUpdateAnimation();
        }

        #region Methoden

        /// <summary>
        /// Animiert Zurückgleiten des SafeSelect-Feldes
        /// </summary>
        /// <param name="button"></param>
        private void ResetButton(SurfaceButton button)
        {
            ThicknessAnimation moveAnimation = new ThicknessAnimation();
            moveAnimation.Duration = TimeSpan.FromSeconds(0.1);
            moveAnimation.FillBehavior = FillBehavior.Stop;
            moveAnimation.From = new Thickness(button.Margin.Left, button.Margin.Top, button.Margin.Right, button.Margin.Bottom);
            moveAnimation.To = new Thickness(10, button.Margin.Top, button.Margin.Right, button.Margin.Bottom);

            Storyboard resetStoryboard = new Storyboard();
            resetStoryboard.Children.Add(moveAnimation);
            Storyboard.SetTargetName(moveAnimation, button.Name);
            Storyboard.SetTargetProperty(moveAnimation, new PropertyPath(TextBox.MarginProperty));
            resetStoryboard.Completed += new EventHandler(resetStoryboard_Completed);

            resetStoryboard.Begin(this);
        }

        private void CreateUpdateAnimation()
        {
            //NameScope.SetNameScope(this, new NameScope());
            AnimatedRectangle = new Rectangle();
            AnimatedRectangle.Name = "AnimatedRectangle";
            this.RegisterName(AnimatedRectangle.Name, AnimatedRectangle);
            AnimatedRectangle.Width = this.Width;
            AnimatedRectangle.Height = 100;
            AnimatedRectangle.Margin = new Thickness(0, -40, 0, 0);
            AnimatedRectangle.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            AnimatedRectangle.Stroke = null;
            Grid.SetRowSpan(AnimatedRectangle, 4);
            MainGrid.Children.Add(AnimatedRectangle);
            AnimatedRectangle.Fill = (LinearGradientBrush)this.FindResource("AnimatedRectangleBrush");
            AnimatedRectangle.Visibility = System.Windows.Visibility.Hidden;

            ThicknessAnimation updateAnimation = new ThicknessAnimation();
            updateAnimation.Duration = TimeSpan.FromSeconds(1.3);
            updateAnimation.From = new Thickness(AnimatedRectangle.Margin.Left, AnimatedRectangle.Margin.Top, AnimatedRectangle.Margin.Right, AnimatedRectangle.Margin.Bottom);
            updateAnimation.To = new Thickness(AnimatedRectangle.Margin.Left, this.Height + 70, AnimatedRectangle.Margin.Right, AnimatedRectangle.Margin.Bottom);

            UpdateStoryboard = new Storyboard();
            UpdateStoryboard.Children.Add(updateAnimation);
            Storyboard.SetTargetName(updateAnimation, AnimatedRectangle.Name);
            Storyboard.SetTargetProperty(updateAnimation, new PropertyPath(Rectangle.MarginProperty));
        }

        public void HighlightPanel()
        {
            AnimatedRectangle.Visibility = System.Windows.Visibility.Visible;
            UpdateStoryboard.Begin(this);
        }

        #endregion

        #region Events

        /// <summary>
        /// Event wird aufgerufen bei TouchDown auf Grid.
        /// Berechnung ob Touchpunkt inenrhalb Ok- oder Cancel-Button liegt.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OptionTouchDown(object sender, TouchEventArgs e)
        {
            // as long as TouchPoint is within Button-Container, X and Y range from 0 to 38
            TouchPointRelativeToButtonYes = new Point(e.TouchDevice.GetTouchPoint(OkButton).Position.X, e.TouchDevice.GetTouchPoint(OkButton).Position.Y);
            TouchPointRelativeToButtonNo = new Point(e.TouchDevice.GetTouchPoint(CancelButton).Position.X, e.TouchDevice.GetTouchPoint(CancelButton).Position.Y);

            if (TouchPointRelativeToButtonYes.X >= 0 && TouchPointRelativeToButtonYes.X <= OkButton.Width && TouchPointRelativeToButtonYes.Y >= 0 && TouchPointRelativeToButtonYes.Y <= OkButton.Height)
            {
                ((ScatterViewItem)Parent).CanMove = !true;
                ((ScatterViewItem)Parent).CanRotate = !true;
                YesButtonIsMoving = true;
                NoButtonIsMoving = !true;
                e.Handled = true;
            }

            if (TouchPointRelativeToButtonNo.X >= 0 && TouchPointRelativeToButtonNo.X <= CancelButton.Width && TouchPointRelativeToButtonNo.Y >= 0 && TouchPointRelativeToButtonNo.Y <= CancelButton.Height)
            {
                ((ScatterViewItem)Parent).CanMove = !true;
                ((ScatterViewItem)Parent).CanRotate = !true;
                YesButtonIsMoving = !true;
                NoButtonIsMoving = true;
                e.Handled = true;
            }
            // Console.WriteLine("YOnButton " + e.TouchDevice.GetTouchPoint(ConfirmButton).Position.Y);

        }

        /// <summary>
        /// Event wird aufgerufen bei TouchMove auf Grid.
        /// Berechnung ob Touchpunkt innerhalb Ok- oder Cancel-Button liegt. 
        /// Falls ja mitbewegen des SafeSelect-Feldes mit Finger und Aktualisieren des Looks.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OptionTouchMove(object sender, TouchEventArgs e)
        {
            if (YesButtonIsMoving)
            {
                Point TempTouchPointRelativeToButton = new Point(e.TouchDevice.GetTouchPoint(OkButton).Position.X, e.TouchDevice.GetTouchPoint(OkButton).Position.Y);
                if (TempTouchPointRelativeToButton.Y >= 0 && TempTouchPointRelativeToButton.Y <= 38)
                {
                    double movingLength = TempTouchPointRelativeToButton.X - TouchPointRelativeToButtonYes.X;
                    double newLeftButtonMargin = OkButton.Margin.Left + movingLength;
                    if (newLeftButtonMargin < 10)
                    {
                        newLeftButtonMargin = 10;
                    }
                    else if (newLeftButtonMargin > 100)
                    {
                        newLeftButtonMargin = 100;
                    }
                    OkButton.Margin = new Thickness(newLeftButtonMargin, OkButton.Margin.Top, OkButton.Margin.Right, OkButton.Margin.Bottom);
                    // ResetTextLook();
                    if (newLeftButtonMargin < 90)
                    {
                        OkText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                        OkRectangle.Fill = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    }
                    else
                    {
                        OkText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                        OkRectangle.Fill = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    }
                }
            }
            else if (NoButtonIsMoving)
            {
                Point TempTouchPointRelativeToButton = new Point(e.TouchDevice.GetTouchPoint(CancelButton).Position.X, e.TouchDevice.GetTouchPoint(CancelButton).Position.Y);
                if (TempTouchPointRelativeToButton.Y >= 0 && TempTouchPointRelativeToButton.Y <= 38)
                {
                    double movingLength = TempTouchPointRelativeToButton.X - TouchPointRelativeToButtonNo.X;
                    double newLeftButtonMargin = CancelButton.Margin.Left + movingLength;
                    if (newLeftButtonMargin < 10)
                    {
                        newLeftButtonMargin = 10;
                    }
                    else if (newLeftButtonMargin > 100)
                    {
                        newLeftButtonMargin = 100;
                    }
                    CancelButton.Margin = new Thickness(newLeftButtonMargin, CancelButton.Margin.Top, CancelButton.Margin.Right, CancelButton.Margin.Bottom);
                    // ResetTextLook();
                    if (newLeftButtonMargin < 90)
                    {
                        CancelText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                        CancelRectangle.Fill = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                    }
                    else
                    {
                        CancelText.Foreground = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                        CancelRectangle.Fill = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                    }
                }
            }
        }

        /// <summary>
        /// Event wird aufgerufen bei TouchUp auf Grid.
        /// Berechnung ob Touchpunkt inenrhalb Ok- oder Cancel-Button liegt.
        /// Wenn SafeSelect-Feld weit genug gezogen wird entsprechende Aktzion ausgelöst durch Event feuern für SurfaceWindow.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void OptionTouchUp(object sender, TouchEventArgs e)
        {
            ((ScatterViewItem)Parent).CanMove = true;
            ((ScatterViewItem)Parent).CanRotate = true;

            if (YesButtonIsMoving)
            {
                var bc = new BrushConverter();
                OkText.Foreground = Brushes.White;
                OkRectangle.Fill = (Brush)bc.ConvertFrom("#FFE2E2E2");

                if (OkButton.Margin.Left >= 90)
                {
                    // ToDo: get the models ready for deleting
                    // pass event to SurfaceWindow1.xaml.cs
                    ModelRemovingPanelClosed(this, new RemoveModelsEventArgs(true, null));
                }
            }
            else if (NoButtonIsMoving)
            {
                var bc = new BrushConverter();
                CancelText.Foreground = Brushes.White;
                CancelRectangle.Fill = (Brush)bc.ConvertFrom("#FFE2E2E2");

                if (CancelButton.Margin.Left >= 90)
                {
                    // pass event to SurfaceWindow1.xaml.cs
                    ModelRemovingPanelClosed(this, new RemoveModelsEventArgs(true, null));
                }
            }

            YesButtonIsMoving = !true;
            NoButtonIsMoving = !true;
        }

        /// <summary>
        /// Wird nach Zurückgleiten-Animation aufgerufen.
        /// Stellt sicher, dass SafeSelect-Felder nach Animation exakt an Ausgangsposition stehen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        void resetStoryboard_Completed(object sender, EventArgs e)
        {
            OkButton.Margin = new Thickness(10, OkButton.Margin.Top, OkButton.Margin.Right, OkButton.Margin.Bottom);
            CancelButton.Margin = new Thickness(10, CancelButton.Margin.Top, CancelButton.Margin.Right, CancelButton.Margin.Bottom);
        }
        #endregion
	}

    /// <summary>
    /// Klasse für eigene Eventparameterübergabe wenn Modelle löschen bestätigt oder abgebrochen wurde.
    /// </summary>
    public class RemoveModelsEventArgs : EventArgs
    {
        public readonly bool RemoveModel;
        public readonly ProcessModel[] DeletableModelList;

        public RemoveModelsEventArgs(bool removeModel, ProcessModel[] deletableModelList)
        {
            RemoveModel = removeModel;
            DeletableModelList = deletableModelList;
        }
    }
}