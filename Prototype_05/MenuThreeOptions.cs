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
    /// Konkrete Klasse von Menü mit drei Einträgen.
    /// </summary>
    public class MenuThreeOptions : Menu
    {
        #region Deklaration
        /// <summary>
        /// Enum-Defintionen für Menüzustand.
        /// </summary>
        public enum ChosenOption { Option0, Option1, Option2, None }
        protected ChosenOption _chosenOption = ChosenOption.None;
        /// <summary>
        /// aktuell gewählter Menüzustand.
        /// </summary>
        public ChosenOption SelectedOption
        {
            get { return _chosenOption; }
            set { _chosenOption = value; }
        }

        public delegate void SendChosenOptionEventHandler(object sender, ChosenOptionEventArgs e);
        public event SendChosenOptionEventHandler OptionWasChosenEvent;
        #endregion

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="optionsAmount">Anzahl benötigter Menüeinträge, Wert = <value>3</value></param>
        /// <param name="openMenu"><value>true</value> wenn Menüeinträge sofort sichtbar sein sollen</param>
        public MenuThreeOptions(int optionsAmount, bool openMenu)
            : base(optionsAmount, openMenu) { }

        #region Methoden
        /// <summary>
        /// Events für Touch auf Menüschaltfläche registrieren.
        /// </summary>
        public override void RegisterEvents()
        {
            base.RegisterEvents();

            _chosenOption = ChosenOption.None;
        }

        /// <summary>
        /// Text und Symbole für Menüeinträge festlegen.
        /// </summary>
        /// <param name="option0Title">Text erster Eintrag</param>
        /// <param name="option1Title">Text zweiter Eintrag</param>
        /// <param name="option2Title">Text dritter Eintrag</param>
        /// <param name="option0ImgPath">Bild erster Eintrag</param>
        /// <param name="option1ImgPath">Bild zweiter Eintrag</param>
        /// <param name="option2ImgPath">Bild dritter Eintrag</param>
        public void SetOptions(String option0Title, String option1Title, String option2Title, String option0ImgPath, String option1ImgPath, String option2ImgPath)
        {
            _optionTexts[0].Text = option0Title;
            _optionTexts[1].Text = option1Title;
            _optionTexts[2].Text = option2Title;
            _optionImages[0].Source = new BitmapImage(new Uri(@option0ImgPath, UriKind.Relative));
            _optionImages[1].Source = new BitmapImage(new Uri(@option1ImgPath, UriKind.Relative));
            _optionImages[2].Source = new BitmapImage(new Uri(@option2ImgPath, UriKind.Relative));
        }

        /// <summary>
        /// Je nach Menüzustand umschalten zwischen Menüeinträge sichtbar und nicht sichtbar.
        /// </summary>
        public override void UpdateOptionsVisibility()
        {
            base.UpdateOptionsVisibility();
            _chosenOption = ChosenOption.None;
        }
#endregion

#region Events

        /// <summary>
        /// Event wird bei TouchDown auf Menüschaltfläche aufgerufen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        protected override void ButtonTouchDown(object sender, TouchEventArgs e)
        {
            base.ButtonTouchDown(sender, e);

            // hier werden nun die Subklassen-spezifischen Dinge getan
            // nix zu tun :)

            // Mark this event as handled.  
            //e.Handled = true;
        }

        /// <summary>
        /// Event wird bei TouchMove auf Menüschaltfläche aufgerufen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        protected override void ButtonTouchMove(object sender, TouchEventArgs e)
        {
            base.ButtonTouchMove(sender, e);

            // hier werden nun die Subklassen-spezifischen Dinge getan
            Point currentTouchPoint = e.TouchDevice.GetTouchPoint(this.MainGrid).Position;
            _touchLine.X2 = currentTouchPoint.X;
            _touchLine.Y2 = currentTouchPoint.Y;

            if ((currentTouchPoint.X >= ((230 - 155) + 25)) && (currentTouchPoint.X <= 230))
            {
                _touchLine.Stroke = new SolidColorBrush((Color)FindResource("TouchLineColor_Chosen"));
                _currentState = State.Hold_Option;

                if ((currentTouchPoint.Y >= 0) && (currentTouchPoint.Y < 50))
                {
                    _optionImgTxtGrids[0].Background = new SolidColorBrush((Color)FindResource("OptionColorChosen"));
                    _chosenOption = ChosenOption.Option0;
                }
                else
                { _optionImgTxtGrids[0].Background = new SolidColorBrush((Color)FindResource("Option0ColorStrong")); }

                if ((currentTouchPoint.Y >= 50) && (currentTouchPoint.Y <= 100))
                {
                    _optionImgTxtGrids[1].Background = new SolidColorBrush((Color)FindResource("OptionColorChosen"));
                    _chosenOption = ChosenOption.Option1;
                }
                else
                { _optionImgTxtGrids[1].Background = new SolidColorBrush((Color)FindResource("Option1ColorStrong")); }

                if ((currentTouchPoint.Y > 100) && (currentTouchPoint.Y <= 150))
                {
                    _optionImgTxtGrids[2].Background = new SolidColorBrush((Color)FindResource("OptionColorChosen"));
                    _chosenOption = ChosenOption.Option2;
                }
                else
                { _optionImgTxtGrids[2].Background = new SolidColorBrush((Color)FindResource("Option2ColorStrong")); }
            }
            else
            {
                _touchLine.Stroke = new SolidColorBrush((Color)FindResource("TouchLineColor_Base"));
                _currentState = State.Hold_Base;
                _chosenOption = ChosenOption.None;
                foreach (Grid grid in _optionImgTxtGrids)
                {
                    grid.Background = new SolidColorBrush((Color)FindResource("Option" + _optionImgTxtGrids.IndexOf(grid) + "ColorStrong"));
                }
            }

            // Mark this event as handled.  
            e.Handled = true;
        }

        /// <summary>
        /// Event wird bei TouchUp auf Menüschaltfläche aufgerufen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        protected override bool ButtonTouchUp(object sender, TouchEventArgs e)
        {
            // hier werden nun die Subklassen-spezifischen Dinge getan
            ChosenOption rememberChosenOption = _chosenOption;
            if (base.ButtonTouchUp(sender, e))
            {
                OptionWasChosenEvent(this, new ChosenOptionEventArgs(rememberChosenOption, e.TouchDevice.GetOrientation(null) + 90));
            }

            // Mark this event as handled.  
            e.Handled = true;
            return true;
        }


        /*
         * Einkommentieren und entsprechende Events registrieren wenn gewünscht, dass Menüeinträge auch einfach nur per Tap oder wie bei
         * Registernavigation ausgewählt werden kann.
        protected override void OptionTouchDown(object sender, TouchEventArgs e)
        {
            base.OptionTouchDown(sender, e);

            // hier werden nun die Subklassen-spezifischen Dinge getan
            if (sender.Equals(_optionImgTxtGrids[0]))
            {
                _chosenOption = ChosenOption.Option0;
                ((Grid)sender).Background = new SolidColorBrush((Color)FindResource("OptionColorChosen"));
            }
            else if (sender.Equals(_optionImgTxtGrids[1]))
            {
                _chosenOption = ChosenOption.Option1;
                ((Grid)sender).Background = new SolidColorBrush((Color)FindResource("OptionColorChosen"));
            }
            else if (sender.Equals(_optionImgTxtGrids[2]))
            {
                _chosenOption = ChosenOption.Option2;
                ((Grid)sender).Background = new SolidColorBrush((Color)FindResource("OptionColorChosen"));
            }

            // Mark this event as handled.  
            e.Handled = true;
        }

        protected override void OptionTouchMove(object sender, TouchEventArgs e)
        {
            if (e.TouchDevice == _optionControlTouchDevice)
            {
                base.OptionTouchMove(sender, e);

                if (sender.Equals(_optionImgTxtGrids[0]))
                {
                    // Console.WriteLine("option 0 bei move");
                    _chosenOption = ChosenOption.Option0;
                    _optionImgTxtGrids[0].Background = new SolidColorBrush((Color)FindResource("OptionColorChosen"));
                    _optionImgTxtGrids[1].Background = new SolidColorBrush((Color)FindResource("Option1ColorStrong"));
                    _optionImgTxtGrids[2].Background = new SolidColorBrush((Color)FindResource("Option2ColorStrong"));
                }
                else if (sender.Equals(_optionImgTxtGrids[1]))
                {
                    // Console.WriteLine("option 1 bei move");
                    _chosenOption = ChosenOption.Option1;
                    _optionImgTxtGrids[1].Background = new SolidColorBrush((Color)FindResource("OptionColorChosen"));
                    _optionImgTxtGrids[0].Background = new SolidColorBrush((Color)FindResource("Option0ColorStrong"));
                    _optionImgTxtGrids[2].Background = new SolidColorBrush((Color)FindResource("Option2ColorStrong"));
                }
                else if (sender.Equals(_optionImgTxtGrids[2]))
                {
                    // Console.WriteLine("option 2 bei move");
                    _chosenOption = ChosenOption.Option2;
                    _optionImgTxtGrids[2].Background = new SolidColorBrush((Color)FindResource("OptionColorChosen"));
                    _optionImgTxtGrids[0].Background = new SolidColorBrush((Color)FindResource("Option0ColorStrong"));
                    _optionImgTxtGrids[1].Background = new SolidColorBrush((Color)FindResource("Option1ColorStrong"));
                }
                else
                {
                    // Console.WriteLine("im elsezweig gelandet bei move");
                    _chosenOption = ChosenOption.None;
                    foreach (Grid grid in _optionImgTxtGrids)
                    {
                        grid.Background = new SolidColorBrush((Color)FindResource("Option" + _optionImgTxtGrids.IndexOf(grid) + "ColorStrong"));
                    }
                }
            }

            // Mark this event as handled.  
            e.Handled = true;
        }

        protected override void OptionTouchUp(object sender, TouchEventArgs e)
        {
            base.OptionTouchUp(sender, e);

            UpdateOptionsVisibility();

            // Mark this event as handled.  
            e.Handled = true;
        }

        protected override void DeadZoneTouchMoved(object sender, TouchEventArgs e)
        {
            if (e.TouchDevice == _optionControlTouchDevice)
            {
                base.DeadZoneTouchMoved(sender, e);

                //Console.WriteLine("in deadzonetouchmoved gelandet");
                _chosenOption = ChosenOption.None;
                foreach (Grid grid in _optionImgTxtGrids)
                {
                    grid.Background = new SolidColorBrush((Color)FindResource("Option" + _optionImgTxtGrids.IndexOf(grid) + "ColorStrong"));
                }

            }
            //else
            //    Console.WriteLine("touchdevice != optiontouchdevice in dead zone");

            // Mark this event as not handled.  
            e.Handled = false;
        }
         */

        #endregion

        /// <summary>
        /// Klasse für eigene Eventparameterübergabe bzgl gewählten Menüeintrages.
        /// </summary>
        public class ChosenOptionEventArgs : EventArgs
        {
            public readonly ChosenOption ChosenOption;
            public readonly double TouchOrientation;

            public ChosenOptionEventArgs(ChosenOption chosenOption, double touchOrientation)
            {
                ChosenOption = chosenOption;
                TouchOrientation = touchOrientation;
            }
        }
    }
}
