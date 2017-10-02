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
	/// Interaktionslogik für StartInstancePanel.xaml
    /// Zeigt einzugebende Parameter an und fragt Bestätigung vor Instanzerzeugung ab.
	/// </summary>
	public partial class StartInstancePanel : UserControl
    {
        #region Deklarationen
        private ProcessModel BoundModel;
        /// <summary>
        /// Array, der angibt ob an entsprechender Stelle Parametereingabe erforderlich
        /// </summary>
        private bool[] ParameterToShow;

        /// <summary>
        /// TouchDown-Position relativ zu Ok-Button
        /// </summary>
        private Point TouchPointRelativeToButtonYes = new Point(0, 0);
        /// <summary>
        /// TouchDown-Position relativ zu Cancel-Button
        /// </summary>
        private Point TouchPointRelativeToButtonNo = new Point(0, 0);

        private Boolean _yesButtonIsMoving = false;
        /// <summary>
        /// Gibt an ob Ok-Button zur Zeit vom Nutzer bewegt wird.
        /// </summary>
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

        /// <summary>
        /// Erforderliche Gridreihenhöhe wenn Parameter an entsprechender Stelle abgefragt werden soll.
        /// </summary>
        private double[] GridHeights = new double[] { 85.5, 85.5, 150.5, 70.5, 70.5 };

        public delegate void ParameterChosenEventHandler(object sender, ParameterChosenEventArgs e);
        public event ParameterChosenEventHandler StartInstancePanelClosed;
        
        #endregion

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="parameterToShow">Array, der angibt ob an entsprechender Stelle Parametereingabe erforderlich</param>
        /// <param name="parameterTitles">Array, der an entsprechender Stelle Parameterbezeichnung speichert</param>
        /// <param name="model">Porzessmodell aus dem eine Instanz erzeugtw erden soll</param>
        /// <param name="parameterCounter">Anzahl erforderlicher Parameter</param>
		public StartInstancePanel(bool[] parameterToShow, string[] parameterTitles, ProcessModel model, int parameterCounter)
		{
            this.InitializeComponent();
            if (parameterCounter > 0)
            {
                OkText.Text = "Define all required Parameters first";
                OkButton.IsHitTestVisible = false;
                OkButton.Opacity = 0.5;
                OkText.Opacity = 0.5;
                OkRectangle.Opacity = 0.5;
                OkImage.Opacity = 0.5;

                if (model.Name == "Cook lunch for ...")
                {
                    Object0Text.Text = "Chicken Soup";
                    Object1Text.Text = "Spaghetti";
                    Object2Text.Text = "Salad";
                }
                else if (model.Name == "Call ...")
                {
                    Object0Text.Text = "Friend Lisa";
                    Object1Text.Text = "Friend Marc";
                    Object2Text.Text = "Parents";
                }
                else if (model.Name == "Wash all dirty clothes")
                {
                    Object0Text.Text = "30° Wool";
                    Object1Text.Text = "40° Coloureds";
                    Object2Text.Text = "60° Coloureds";
                }
                else if (model.Name == "Turn all electronic devices ...")
                {
                    Object0Text.Text = "on";
                    Object1Text.Text = "off";
                    ObjectComboListBox.Items.RemoveAt(2);
                }
                else if (model.Name == "Turn all lights ...")
                {
                    Object0Text.Text = "on";
                    Object1Text.Text = "off";
                    ObjectComboListBox.Items.RemoveAt(2);
                }
            }
            ParameterToShow = parameterToShow;
            List<TextBox> parameterTitelTextBoxList = new List<TextBox> { Numeric1Text, Numeric2Text, TimeGridText, LocationGridText, ObjectGridText };
            BoundModel = model;
            TitleHeader2.Text = model.Name;
            double height = 46 + 125 + 70;
            for (int i = 0; i < 5; i++)
            {
                parameterTitelTextBoxList[i].Text = parameterTitles[i];
                if (!parameterToShow[i]) MainGrid.RowDefinitions[i + 1].Height = new GridLength(0);
                else height += GridHeights[i];
            }
            this.Height = height;
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
        /// <summary>
        /// Macht OK-SafeSelect erst verfügbar wenn alle erforderlichen Parameter ausgewählt wurden.
        /// </summary>
        private void UpdateOkButtonOpacity()
        {

            if ((ParameterToShow[3] && AreaComboListBox.SelectedIndex == -1) || (ParameterToShow[3] && LocationComboListBox.SelectedIndex == -1) || (ParameterToShow[4] && ObjectComboListBox.SelectedIndex == -1))
            {
                OkText.Text = "Define all required Parameters first";
                OkButton.IsHitTestVisible = false;
                OkButton.Opacity = 0.5;
                OkText.Opacity = 0.5;
                OkRectangle.Opacity = 0.5;
                OkImage.Opacity = 0.5;
            }
            else
            {
                OkButton.IsHitTestVisible = true;
                OkButton.Opacity = 1;
                OkText.Opacity = 1;
                OkRectangle.Opacity = 1;
                OkImage.Opacity = 1;
                if (InstanceToWaitBox.IsChecked.Value) OkText.Text = "Create Instance";
                else OkText.Text = "Create and start Instance";
            }
        }
        #endregion

        #region Events

        /// <summary>
        /// Event wird aufgerufen wenn Slider für Double-Eingabe verschoben wurde.
        /// Aktualisiert nach Wertrundung Textbox die zum Slider gehört.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void NumericSliderValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            SliderValueText.Text = Math.Round(((SurfaceSlider)sender).Value, 1, MidpointRounding.ToEven).ToString();
        }

        /// <summary>
        /// Event wird aufgerufen wenn Slider für Int-Eingabe verschoben wurde.
        /// Aktualisiert nach Wertrundung Textbox die zum Slider gehört.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void NumericSlider2ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            SliderValueText2.Text = Math.Round(((SurfaceSlider)sender).Value, 0, MidpointRounding.ToEven).ToString();
        }

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
                if (!OkButton.IsHitTestVisible) return;
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
                    string[] chosenParameters = new string[] { "", "", "", "", "" };
                    if (ParameterToShow[0]) chosenParameters[0] = SliderValueText.Text;
                    if (ParameterToShow[1]) chosenParameters[1] = SliderValueText2.Text;
                    if (ParameterToShow[2]) chosenParameters[2] = "14.03";
                    if (ParameterToShow[3]) chosenParameters[3] = LocationTitleText.Text;
                    if (ParameterToShow[4]) chosenParameters[4] = ObjectTitleText.Text;

                    // pass event to SurfaceWindow1.xaml.cs
                    StartInstancePanelClosed(this, new ParameterChosenEventArgs(BoundModel, InstanceToWaitBox.IsChecked.Value, true, chosenParameters));
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
                    StartInstancePanelClosed(this, new ParameterChosenEventArgs(null, InstanceToWaitBox.IsChecked.Value, !true, null));
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
        private void resetStoryboard_Completed(object sender, EventArgs e)
        {
            OkButton.Margin = new Thickness(10, OkButton.Margin.Top, OkButton.Margin.Right, OkButton.Margin.Bottom);
            CancelButton.Margin = new Thickness(10, CancelButton.Margin.Top, CancelButton.Margin.Right, CancelButton.Margin.Bottom);
        }

        /// <summary>
        /// Event wird aufgerufen wenn Tap auf Object-ComboBox (eigtl SurfaceButton im Look einer ComboBox)
        /// Bewirkt Ein- oder Ausblenden der Listbox mit Objeect-Einträgen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void ObjectComboButton_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ObjectComboListBox.Visibility == System.Windows.Visibility.Visible)
            {
                ObjectComboListBox.Visibility = System.Windows.Visibility.Hidden;
                ObjectBoxPath0.LayoutTransform = new RotateTransform(0, 0.5, 0.5);
                ObjectBoxPath1.LayoutTransform = new RotateTransform(0, 0.5, 0.5);
            }
            else
            {
                ObjectComboListBox.Visibility = System.Windows.Visibility.Visible;
                ObjectBoxPath0.LayoutTransform = new RotateTransform(180, 0.5, 0.5);
                ObjectBoxPath1.LayoutTransform = new RotateTransform(180, 0.5, 0.5);
            }
        }

        /// <summary>
        /// Event wird aufgerufen wenn ListBox-Selektion geändert wurde.
        /// Aktualisiert text für ausgewähltes Element und blendet Listbox aus.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void ObjectComboListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ObjectComboListBox.SelectedIndex == 0) ObjectTitleText.Text = Object0Text.Text;
            else if (ObjectComboListBox.SelectedIndex == 1) ObjectTitleText.Text = Object1Text.Text;
            else if (ObjectComboListBox.SelectedIndex == 2) ObjectTitleText.Text = Object2Text.Text;
            ObjectComboListBox.Visibility = System.Windows.Visibility.Hidden;
            ObjectBoxPath0.LayoutTransform = new RotateTransform(0, 0.5, 0.5);
            ObjectBoxPath1.LayoutTransform = new RotateTransform(0, 0.5, 0.5);
            UpdateOkButtonOpacity();
        }

        /// <summary>
        /// Event wird aufgerufen wenn Tap auf Area-ComboBox (eigtl SurfaceButton im Look einer ComboBox)
        /// Bewirkt Ein- oder Ausblenden der Listbox mit Area-Einträgen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void AreaComboButton_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AreaComboListBox.Visibility == System.Windows.Visibility.Visible)
            {
                AreaComboListBox.Visibility = System.Windows.Visibility.Hidden;
                AreaBoxPath0.LayoutTransform = new RotateTransform(0, 0.5, 0.5);
                AreaBoxPath1.LayoutTransform = new RotateTransform(0, 0.5, 0.5);
            }
            else
            {
                AreaComboListBox.Visibility = System.Windows.Visibility.Visible;
                AreaBoxPath0.LayoutTransform = new RotateTransform(180, 0.5, 0.5);
                AreaBoxPath1.LayoutTransform = new RotateTransform(180, 0.5, 0.5);
            }
        }

        /// <summary>
        /// Event wird aufgerufen wenn ListBox-Selektion geändert wurde.
        /// Aktualisiert text für ausgewähltes Element und blendet Listbox aus.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void AreaComboListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (AreaComboListBox.SelectedIndex == 0) AreaTitleText.Text = Area0Text.Text;
            else if (AreaComboListBox.SelectedIndex == 1) AreaTitleText.Text = Area1Text.Text;
            else if (AreaComboListBox.SelectedIndex == 2) AreaTitleText.Text = Area2Text.Text;
            AreaComboListBox.Visibility = System.Windows.Visibility.Hidden;
            AreaBoxPath0.LayoutTransform = new RotateTransform(0, 0.5, 0.5);
            AreaBoxPath1.LayoutTransform = new RotateTransform(0, 0.5, 0.5);
            UpdateOkButtonOpacity();
        }

        /// <summary>
        /// Event wird aufgerufen wenn Tap auf Location-ComboBox (eigtl SurfaceButton im Look einer ComboBox)
        /// Bewirkt Ein- oder Ausblenden der Listbox mit Location-Einträgen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void LocationComboButton_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (LocationComboListBox.Visibility == System.Windows.Visibility.Visible)
            {
                LocationComboListBox.Visibility = System.Windows.Visibility.Hidden;
                LocationBoxPath0.LayoutTransform = new RotateTransform(0, 0.5, 0.5);
                LocationBoxPath1.LayoutTransform = new RotateTransform(0, 0.5, 0.5);
            }
            else
            {
                LocationComboListBox.Visibility = System.Windows.Visibility.Visible;
                LocationBoxPath0.LayoutTransform = new RotateTransform(180, 0.5, 0.5);
                LocationBoxPath1.LayoutTransform = new RotateTransform(180, 0.5, 0.5);
            }
        }

        /// <summary>
        /// Event wird aufgerufen wenn ListBox-Selektion geändert wurde.
        /// Aktualisiert text für ausgewähltes Element und blendet Listbox aus.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void LocationComboListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (LocationComboListBox.SelectedIndex == 0) LocationTitleText.Text = Location0Text.Text;
            else if (LocationComboListBox.SelectedIndex == 1) LocationTitleText.Text = Location1Text.Text;
            else if (LocationComboListBox.SelectedIndex == 2) LocationTitleText.Text = Location2Text.Text;
            LocationComboListBox.Visibility = System.Windows.Visibility.Hidden;
            LocationBoxPath0.LayoutTransform = new RotateTransform(0, 0.5, 0.5);
            LocationBoxPath1.LayoutTransform = new RotateTransform(0, 0.5, 0.5);
            UpdateOkButtonOpacity();
        }
        #endregion

	}

    /// <summary>
    /// Klasse für eigene Eventparameterübergabe wenn Instanzerzeugung bestätigt oder abgebrochen wurde.
    /// </summary>
    public class ParameterChosenEventArgs : EventArgs
    {
        //public readonly string[] FilePathList;
        public readonly ProcessModel Model;
        public readonly bool InstanceHasToWait;
        public readonly bool ConfirmedStarting;
        public readonly string[] ChosenParameters;

        public ParameterChosenEventArgs(ProcessModel model, bool instanceHasToWait, bool confirmedStarting, string[] chosenParameters)
        {
            //FilePathList = filePathList;
            Model = model;
            InstanceHasToWait = instanceHasToWait;
            ConfirmedStarting = confirmedStarting;
            ChosenParameters = chosenParameters;
        }
    }
}