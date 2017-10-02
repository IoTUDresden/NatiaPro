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
using Microsoft.Win32;
using System.Collections.Specialized;

namespace Prototype_05
{
	/// <summary>
	/// Interaktionslogik für ModelAddingPanel.xaml
	/// </summary>
	public partial class ModelAddingPanel : UserControl
    {
        #region Deklarationen
        private int _fileAmount;
        /// <summary>
        /// Anzahl aktuell zum Hinzufügen ausgewählte Modelle.
        /// Achtung! Anzahl anzeigbarer Dateien derzeit auf 3 begrenzt! 
        /// </summary>
        public int FileAmount
        {
            get { return _fileAmount; }
            set
            {
                _fileAmount = value;
                UpdateGrid();
            }
        }

        private string[,] _fileMap;
        /// <summary>
        /// Zuordnung von Datei, Dateipfad und Modellname
        /// </summary>
        public string[,] FileMap
        {
            get { return _fileMap; }
            set { _fileMap = value; }
        }

        /// <summary>
        /// Höhe des Panels wenn fileamount=0
        /// </summary>
        private double BaseHeight = 270;
        /// <summary>
        /// Höhe einer Reihe im Grid für Anzeige einer Datei
        /// </summary>
        private double ContentRowHeight = 42;

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

        public delegate void SendAddModelsEventHandler(object sender, AddModelsEventArgs e);
        public event SendAddModelsEventHandler ModelAddingPanelClosed;
        public delegate void ParseFileTitleEventHandler(object sender, ParseFileTitleEventArgs e);
        public event ParseFileTitleEventHandler FileNamesChosen;
        #endregion

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ModelAddingPanel()
		{
			this.InitializeComponent();
            _fileMap = new string[3, 3];
            ClearContent();
            CreateUpdateAnimation();
        }

        #region Methoden

        /// <summary>
        /// Alle alten daten aus Panel löschen.
        /// </summary>
        public void ClearContent()
        {
            ContentGridRow.Height = new GridLength(BaseHeight - 156);
            ContentRow1.Height = new GridLength(0);
            ContentRow2.Height = new GridLength(0);
            ContentRow3.Height = new GridLength(0);
            this.Height = BaseHeight;
            FileAmount = 0;
            NotNullLine.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Aktualisiere Modellnamen.
        /// </summary>
        private void UpdateFileNames()
        {
            File0.Text = _fileMap[0, 1];
            Model00.Text = _fileMap[0, 2];
            File1.Text = _fileMap[1, 1];
            Model10.Text = _fileMap[1, 2];
            File2.Text = _fileMap[2, 1];
            Model11.Text = _fileMap[2, 2];
        }

        /// <summary>
        /// Anpassen der Panelhöhe entsprechend anzahl anzuzeigender Dateien
        /// </summary>
        private void UpdateGrid()
        {
            ContentGridRow.Height = new GridLength(BaseHeight - 156 + FileAmount * ContentRowHeight);
            this.Height = BaseHeight + FileAmount * ContentRowHeight;
            if (FileAmount > 0)
            {
                for (int i = 1; i <= FileAmount; i++)
                {
                    ContentGrid.RowDefinitions[i].Height = new GridLength(ContentRowHeight);
                }
            }

            ModelAmount.Text = _fileAmount.ToString();
            if (_fileAmount == 0)
            {
                NotNullLine.Visibility = System.Windows.Visibility.Hidden;
                OkText.Text = "Select Model(s) via 'Load File'-Button";
                OkButton.IsHitTestVisible = false;
                OkButton.Opacity = 0.5;
                OkText.Opacity = 0.5;
                OkRectangle.Opacity = 0.5;
                OkImage.Opacity = 0.5;
            }
            else
            {
                NotNullLine.Visibility = System.Windows.Visibility.Visible;
                OkButton.IsHitTestVisible = true;
                OkButton.Opacity = 1;
                OkText.Opacity = 1;
                OkRectangle.Opacity = 1;
                OkImage.Opacity = 1;
                if (_fileAmount == 1) OkText.Text = "Add 1 Model";
                else OkText.Text = "Add " + _fileAmount + " Models";
            }
        }

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
            Grid.SetRowSpan(AnimatedRectangle, 3);
            MainGrid.Children.Add(AnimatedRectangle);
            AnimatedRectangle.Fill = (LinearGradientBrush)this.FindResource("AnimatedRectangleBrush");
            AnimatedRectangle.Visibility = System.Windows.Visibility.Hidden;

            ThicknessAnimation updateAnimation = new ThicknessAnimation();
            updateAnimation.Duration = TimeSpan.FromSeconds(1);
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

        #region
        /// <summary>
        /// Event wird aufgerufen wenn Tap auf Schaltfläche Load file
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void LoadFile_Clicked(object sender, RoutedEventArgs e)
        {
            // Open Dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "sofia files (*.sofia)|*.sofia*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.Multiselect = true;
            // Show open file dialog box 
            Nullable<bool> result = openFileDialog.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                string[] filenames = openFileDialog.FileNames;

                int newFilesCounter = 0;
                for (int i = FileAmount; i < FileAmount + filenames.Length; i++)
                {
                    string[] filenamesplit = filenames[newFilesCounter].Split('\\');
                    _fileMap[i, 0] = filenames[newFilesCounter];
                    _fileMap[i, 1] = filenamesplit[filenamesplit.Length - 1];
                    newFilesCounter++;
                }
                FileAmount = FileAmount + filenames.Length;
                // pass event to SurfaceWindow1.xaml.cs
                FileNamesChosen(this, new ParseFileTitleEventArgs(filenames));
                UpdateFileNames();
            }            
            //this.openFileDialog1.Title = "My Image Browser";
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

        /// <summary>
        /// Event wird aufgerufen wenn ein Prozessmodell wieder per Schaltfläche von Dateiliste entfernt werden soll.
        /// VErschiebt restliche Dateinamen entsprechend um Lücke aufzufüllen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Eventparameter</param>
        private void RemoveButtonClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender.Equals(RemoveButton1))
            {
                _fileMap[0, 0] = _fileMap[1, 0];
                _fileMap[0, 1] = _fileMap[1, 1];
                _fileMap[0, 2] = _fileMap[1, 2];
                _fileMap[1, 0] = _fileMap[2, 0];
                _fileMap[1, 1] = _fileMap[2, 1];
                _fileMap[1, 2] = _fileMap[2, 2];
                _fileMap[2, 0] = null;
                _fileMap[2, 1] = null;
                FileMap[2, 2] = null;
            }
            else if (sender.Equals(RemoveButton2))
            {
                _fileMap[1, 0] = _fileMap[2, 0];
                _fileMap[1, 1] = _fileMap[2, 1];
                _fileMap[1, 2] = _fileMap[2, 2];
                _fileMap[2, 0] = null;
                _fileMap[2, 1] = null;
                FileMap[2, 2] = null;
            }
            else if (sender.Equals(RemoveButton3))
            {
                _fileMap[2, 0] = null;
                _fileMap[2, 1] = null;
                FileMap[2, 2] = null;
            }
            UpdateFileNames();
            ContentGrid.RowDefinitions[_fileAmount].Height = new GridLength(0);
            FileAmount--;
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
                    List<string> filePathList = new List<string>(_fileAmount);
                    for (int i = 0; i < 3; i++)
                    {
                        if (_fileMap[i, 0] != "" && _fileMap[i, 0] != null) filePathList.Add(_fileMap[i, 0]);
                    }
                    // ToDo: GetFilePath
                    // pass event to SurfaceWindow1.xaml.cs
                    ModelAddingPanelClosed(this, new AddModelsEventArgs(true, filePathList));
                }
            }
            else if (NoButtonIsMoving)
            {
                var bc = new BrushConverter();
                CancelText.Foreground = Brushes.White;
                CancelRectangle.Fill = (Brush)bc.ConvertFrom("#FFE2E2E2");

                if (CancelButton.Margin.Left >= 90)
                {
                    // ToDo: 
                    // pass event to SurfaceWindow1.xaml.cs  
                    ModelAddingPanelClosed(this, new AddModelsEventArgs(!true, null));
                }
            }

            YesButtonIsMoving = !true;
            NoButtonIsMoving = !true;
        }
        #endregion

        /// <summary>
        /// Klasse für eigene Eventparameterübergabe wenn Modelle hinzufügen bestätigt oder abgebrochen wurde.
        /// </summary>
        public class AddModelsEventArgs : EventArgs
        {
            public readonly bool AddModel;
            public readonly List<string> FilePathList;

            public AddModelsEventArgs(bool addModel, List<string> filePathList)
            {
                AddModel = addModel;
                FilePathList = filePathList;
            }
        }

        /// <summary>
        /// Klasse für eigene Eventparameterübergabe wenn Datei(en) ausgewählt wurde(n).
        /// </summary>
        public class ParseFileTitleEventArgs : EventArgs
        {
            public readonly string[] FilePathList;

            public ParseFileTitleEventArgs(string[] filePathList)
            {
                FilePathList = filePathList;
            }
        }
	}
}