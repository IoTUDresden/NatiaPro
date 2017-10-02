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

namespace Prototype_05
{
	/// <summary>
    /// Interaktionslogik für RunningInstancesArea.xaml
    /// Bildet optischen Rahmen für RunningInstancesControl.
	/// </summary>
	public partial class RunningInstancesArea : UserControl
    {
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

        private bool _isShown = true;
        /// <summary>
        /// <value>true</value> wenn Bereich eingeblendet ist.
        /// </summary>
        public bool IsShown
        {
            get { return _isShown; }
            set { _isShown = value; }
        }
        /// <summary>
        /// tocuhdevice zur Differnzierung mehrerer Finger auf ShowHideButton
        /// </summary>
        protected TouchDevice _moveTouchDevice;
        /// <summary>
        /// letzte Touchposition.
        /// Punkt kann auch innerhalb Panel liegen, dann keine Skalierung mehr.
        /// </summary>
        protected Point TouchDownPoint;
        //private bool MovingActivated = false;
        //private bool MovingSufficient = false;
        //private double _startPointX;
        //public double StartPointX
        //{ get { return _startPointX; } }

        //public delegate void SendHideTouchLengthEventHandler(object sender, MoveAreaAndControlEventArgs e);
        //public event SendHideTouchLengthEventHandler HideMoveEvent;
        //public delegate void SendShowTouchLengthEventHandler(object sender, MoveAreaAndControlEventArgs e);
        //public event SendShowTouchLengthEventHandler ShowMoveEvent;
        public delegate void SendAnimationCommandEventHandler(object sender, AnimateAreaAndControlEventArgs e);
        public event SendAnimationCommandEventHandler AnimateMarginEvent;

        /// <summary>
        /// ShowOrHideEvent, needed to pass event outside of this Class
        /// </summary>
        public static readonly RoutedEvent ShowOrHideEvent = EventManager.RegisterRoutedEvent("ShowOrHide", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(RunningInstancesArea));
        public event RoutedEventHandler ShowOrHide
        {
            add { AddHandler(ShowOrHideEvent, value); }
            remove { RemoveHandler(ShowOrHideEvent, value); }
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
		public RunningInstancesArea()
		{
			this.InitializeComponent();
            // ShowHideButton.Click -=new RoutedEventHandler(ShowHideButtonClicked);
        }

        ///// <summary>
        ///// Event wird aufgerufen wenn Tap auf SHow/Hide Schaltfläche.
        ///// Wird an SurfaceWindow-Klasse weitergeleitet.
        ///// </summary>
        ///// <param name="sender">Sender</param>
        ///// <param name="e">Eventparameter</param>
        //private void ShowHideButtonClicked(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    // pass event to SurfaceWindow1.xaml.cs
        //    RoutedEventArgs eventargs = new RoutedEventArgs(RunningInstancesArea.ShowOrHideEvent);
        //    RaiseEvent(eventargs);
        //    MovingSufficient = false;
        //    MovingActivated = false;
        //    ShowHideButton.Click -= new RoutedEventHandler(ShowHideButtonClicked);
        //    _moveTouchDevice = null;
        //}

        /// <summary>
        /// Event wird aufgerufen wenn XAML-Komponenten geladen wurden.
        /// Speichert bzw berechnet Margins für Ein- und Ausblenden.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void AreaLoaded(object sender, RoutedEventArgs e)
        {
            _showMargin = this.Margin.Left;
            _hideMargin = 1920 - 40 - 20;
        }

        private void ShowHideTouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            // Capture to the HideShowButton.  
            e.TouchDevice.Capture(this.ShowHideButton);
            // Remember this contact if a contact has not been remembered already.  
            if (_moveTouchDevice == null)
            {
                _moveTouchDevice = e.TouchDevice;

                // Remember where this contact took place.  
                TouchDownPoint = _moveTouchDevice.GetTouchPoint(this.MainGrid).Position;
            }           
            
            // Mark this event as handled.  
            e.Handled = true;
        }

        private void ShowHideTouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
        //    if (e.TouchDevice == _moveTouchDevice)
        //    {
        //        Point newTouchPoint = e.TouchDevice.GetTouchPoint(this.MainGrid).Position;

        //        if (_isShown)
        //        {
        //            if (newTouchPoint.X - TouchDownPoint.X > 0)
        //            {
        //                HideMoveEvent(this, new MoveAreaAndControlEventArgs(newTouchPoint.X - TouchDownPoint.X));
        //            }
        //        }
        //        else
        //            if (TouchDownPoint.X - newTouchPoint.X > 0)
        //            {
        //                ShowMoveEvent(this, new MoveAreaAndControlEventArgs(newTouchPoint.X - TouchDownPoint.X));
        //            }
        //    }
        }

        private void ShowHideTouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            if (e.TouchDevice == _moveTouchDevice)
            {
                Point newTouchPoint = e.TouchDevice.GetTouchPoint(this.MainGrid).Position;
                _moveTouchDevice = null;
                if (_isShown)
                {
                    if (newTouchPoint.X - TouchDownPoint.X > 50)
                    {
                        // pass event to SurfaceWindow1.xaml.cs
                        AnimateMarginEvent(this, new AnimateAreaAndControlEventArgs(false));
                        _isShown = false;
                    }
                }
                else
                    if (TouchDownPoint.X - newTouchPoint.X > 50)
                    {
                        // pass event to SurfaceWindow1.xaml.cs
                        AnimateMarginEvent(this, new AnimateAreaAndControlEventArgs(true));
                        _isShown = true;
                    }
            }
        }


        ///// <summary>
        ///// Klasse für eigene Eventparameterübergabe wenn Area versteckt werden soll.
        ///// </summary>
        //public class MoveAreaAndControlEventArgs : EventArgs
        //{
        //    public readonly double MoveLength;

        //    public MoveAreaAndControlEventArgs(double moveLength)
        //    {
        //        MoveLength = moveLength;
        //    }
        //}

        /// <summary>
        /// Klasse für eigene Eventparameterübergabe wenn Area versteckt werden soll.
        /// </summary>
        public class AnimateAreaAndControlEventArgs : EventArgs
        {
            public readonly bool ShowIt;

            public AnimateAreaAndControlEventArgs(bool showIt)
            {
                ShowIt = showIt;
            }
        }

	}
}