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
using Prototype_05.InstanceData;
using Prototype_05;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Windows.Media.Animation;

namespace Prototype_05
{
	/// <summary>
	/// Stellt detaillierte Instanzansicht dar.
	/// </summary>
	public partial class InstanceDetailWidget : UserControl
    {
        #region Deklaration
        /// <summary>
        /// Merkt sich originale Größe der zweiten Spalte (wegen Expansion bei Markierung)
        /// </summary>
        private double OriginalCol2Width;
        /// <summary>
        /// Merkt sich originale Größe des Widgets (wegen Expansion bei Markierung)
        /// </summary>
        private double OriginalMainGridWidth;
        /// <summary>
        /// Merkt sich verkleinerte Größe des Widgets (wegen Expansion bei Markierung)
        /// </summary>
        private double SmallMainGridWidth;

        /// <summary>
        /// Enum-Definitionen für Platzhalter-Gerätenamen
        /// </summary>
        public enum Devices { YouBot, R2D2, TurtleBot, Terminator, HAL9000, MachineX2, Heating, Ventilator };
        protected Devices CurrentDevice;

        private Random randomDevice;
        private Random randomStep;

        private ProcessInstance _instance;
        public ProcessInstance BoundInstance
        {
            get { return _instance; }
            set { _instance = value; }
        }

        private bool _isExpanded;
        /// <summary>
        /// <value>true</value> wenn markiert (expandiert)
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { _isExpanded = value; }
        }

        private bool _isRelative;
        /// <summary>
        /// <value>true</value> wenn verwandte Instanz (=selbes Modell) (expandiert)
        /// </summary>
        public bool IsRelative
        {
            get { return _isRelative; }
            set { _isRelative = value; }
        }

        public SurfaceButton RootButton
        {
            get { return RootSurfaceButton; }
            set { RootSurfaceButton = value; }
        }

        /// <summary>
        /// Farbbrush für Animation von verwandt zu normal
        /// </summary>
        SolidColorBrush animatedRelativeToTouchedBrush;
        SolidColorBrush animatedTouchedToRelativeBrush;
        SolidColorBrush animatedTouchedToNormalBrush;
        SolidColorBrush animatedNormalToTouchedBrush;
        /// <summary>
        /// Storyboard für Animation von normal zu ausgewählt (expandiert)
        /// </summary>
        Storyboard NormalToTouchedStoryboard;
        Storyboard NormalToRelativeStoryboard;
        Storyboard TouchedToNormalStoryboard;
        Storyboard RelativeToNormalStoryboard;
        Storyboard TouchedToRelativeStoryboard;
        Storyboard RelativeToTouchedStoryboard;
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Standard-Konstruktor
        /// </summary>
		public InstanceDetailWidget()
		{
            this.InitializeComponent();
            randomDevice = new Random();
            randomStep = new Random();
            RememberOriginalSize();
            CreateAnimations();
		}

        /// <summary>
        /// Konstruktor bei Aufruf zur Laufzeit.
        /// </summary>
        /// <param name="instance">gebundene Instanz</param>
        public InstanceDetailWidget(ProcessInstance instance)
        {
            this.InitializeComponent();
            randomDevice = new Random();
            randomStep = new Random();    
            _instance = instance;
            TitleText.Text = _instance.Name;
            ProgressBar.Value = 2;
            InstanceDurationText.Text = "0hr 01min";
            StepDurationText.Text = "0hr 01min";
            StartTimeText.Text = "today, 12:35";
            UpdateContent();
            RememberOriginalSize();
            CreateAnimations();
        }

        /// <summary>
        /// Konstruktor bei Aufruf mit bereits laufenden Instanzen bei Anwendungsstart.
        /// </summary>
        /// <param name="instance">gebundene Instanz</param>
        /// <param name="progress">Pseudo-Fortschritt</param>
        /// <param name="elapsedInstTime">Pseudo-verstrichene Zeit</param>
        /// <param name="elapsedPSTime">Pseudo-verstrichene Zeit aktueller Prozessschritt</param>
        /// <param name="starttime">Pseudo-Startzeit</param>
        public InstanceDetailWidget(ProcessInstance instance, double progress,string elapsedInstTime, string elapsedPSTime, string starttime)
        {
            this.InitializeComponent();
            randomDevice = new Random();
            randomStep = new Random();  
            _instance = instance;
            TitleText.Text = _instance.Name;
            ProgressBar.Value = progress;
            InstanceDurationText.Text = elapsedInstTime;
            StepDurationText.Text = elapsedPSTime;
            StartTimeText.Text = starttime;
            UpdateContent();
            RememberOriginalSize();
            CreateAnimations();
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Speichert originales Layout und reduziert Widget.
        /// </summary>
        private void RememberOriginalSize()
        {
            OriginalCol2Width = MainGrid.ColumnDefinitions[2].Width.Value;
            OriginalMainGridWidth = 385;
            SmallMainGridWidth = OriginalMainGridWidth - OriginalCol2Width;
            HideTwoTapButtons();
        }

        /// <summary>
        /// Kreiert Animationen für Statusübergänge (expandiert - verwandt - normal)
        /// </summary>
        private void CreateAnimations()
        {
            //NameScope.SetNameScope(this, new NameScope());
            CreateTouchedToNormalAnimation();
            CreateNormalToRelativeAnimation();
            CreateNormalToTouchedAnimation();
            CreateRelativeToNormalAnimation();
            CreateRelativeToTouchedAnimation();
            CreateTouchedToRelativeAnimation();
        }

        /// <summary>
        /// Reduziert Widget.
        /// </summary>
        private void HideTwoTapButtons()
        {
            MainGrid.ColumnDefinitions[2].Width = new GridLength(0);
            MainGrid.Width = MainGrid.Width - OriginalCol2Width;
            this.Width = this.Width - OriginalCol2Width;
            ChangeStateButton.Margin = new Thickness(0, 0, 15, 0);
            ShowInfosButton.Margin = new Thickness(0, 0, 15, 0);
            _isExpanded = false;
            _isRelative = false;
        }

        /// <summary>
        /// Animiert Widget-Reduktion.
        /// </summary>
        public void HideTwoTapButtons_Smooth()
        {
            _isExpanded = false;
            CreateReduceStoryBoard();
        }

        /// <summary>
        /// Animiert Widget-Expansion.
        /// </summary>
        public void ShowTwoTapButtons_Smooth()
        {
            _isExpanded = true;
            CreateExpandStoryBoard();
        }

        /// <summary>
        /// Animiert Übergang.
        /// </summary>
        public void FromNormalToTouched()
        {
            _isRelative = false;
            BackgroundRectangle.Stroke = new SolidColorBrush((Color)FindResource("InstanceWidgetBorderTouchedOrRelative"));
            BackgroundRectangle.Fill = animatedNormalToTouchedBrush;
            NormalToTouchedStoryboard.Begin(this);
        }

        /// <summary>
        /// Animiert Übergang.
        /// </summary>
        public void FromNormalToRelative()
        {
            _isRelative = true;
            BackgroundRectangle.Stroke = new SolidColorBrush((Color)FindResource("InstanceWidgetBorderTouchedOrRelative"));
            NormalToRelativeStoryboard.Begin(this);
        }

        /// <summary>
        /// Animiert Übergang.
        /// </summary>
        public void FromRelativeToNormal()
        {
            _isRelative = false;
            BackgroundRectangle.Stroke = new SolidColorBrush((Color)FindResource("InstanceWidgetBorderTouchedOrRelative"));
            RelativeToNormalStoryboard.Begin(this);
        }

        /// <summary>
        /// Animiert Übergang.
        /// </summary>
        public void FromTouchedToNormal()
        {
            _isRelative = false;
            BackgroundRectangle.Fill = animatedTouchedToNormalBrush;
            BackgroundRectangle.Stroke = new SolidColorBrush((Color)FindResource("InstanceWidgetBorderTouchedOrRelative"));
            TouchedToNormalStoryboard.Begin(this);
        }

        /// <summary>
        /// Animiert Übergang.
        /// </summary>
        public void FromTouchedToRelative()
        {
            _isRelative = true;
            BackgroundRectangle.Fill = animatedTouchedToRelativeBrush;
            TouchedToRelativeStoryboard.Begin(this);
        }

        /// <summary>
        /// Animiert Übergang.
        /// </summary>
        public void FromRelativeToTouched()
        {
            _isRelative = false;
            BackgroundRectangle.Fill = animatedRelativeToTouchedBrush;
            RelativeToTouchedStoryboard.Begin(this);
        }

        /// <summary>
        /// Aktualisiert Inhalt.
        /// </summary>
        public void UpdateContent()
        {
            IdText.Text = _instance.Id;
            StateText.Text = _instance.State.ToString();
            int rnddevice = randomDevice.Next(0, 7);
            CurrentDeviceText.Text = ((Devices)rnddevice).ToString();
            int rndstep = randomDevice.Next(0, _instance.SubstepList.Count);
            CurrentPSText.Text = _instance.SubstepList[rndstep].Name;
        }

        /// <summary>
        /// Aktualisiert nur Status.
        /// </summary>
        public void UpdateState()
        {
            if (_instance.State.Equals(Enums.ProcessState.waiting))
            {
                ProgressBar.Visibility = System.Windows.Visibility.Hidden;
                ProgressWarningText.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                ProgressBar.Visibility = System.Windows.Visibility.Visible;
                ProgressWarningText.Visibility = System.Windows.Visibility.Hidden;
            }
                        
            StateText.Text = _instance.State.ToString();
            if (_instance.State.Equals(Enums.ProcessState.executing))
            {
                ProgressBar.Foreground = new SolidColorBrush((Color)FindResource("ExecutingBarColor"));
                StateText.Foreground = new SolidColorBrush((Color)FindResource("ExecutingTextColor"));
            }
            else if (_instance.State.Equals(Enums.ProcessState.faulty))
            {
                ProgressBar.Foreground = new SolidColorBrush((Color)FindResource("ErrorBarColor"));
                StateText.Foreground = new SolidColorBrush((Color)FindResource("ErrorTextColor"));
                BackgroundRectangle.Fill = (SolidColorBrush)FindResource("ErrorWidgetBackgroundBrush");
                BackgroundRectangle.Stroke = (SolidColorBrush)FindResource("ErrorWidgetBorderBrush");
                BackgroundRectangle.StrokeThickness = 4;
            }
            else
            {
                ProgressBar.Foreground = new SolidColorBrush((Color)FindResource("WaitingBarColor"));
                StateText.Foreground = new SolidColorBrush((Color)FindResource("WaitingTextColor"));
            }
        }

        public void RemoveErrorLook()
        {
            BackgroundRectangle.Fill = new SolidColorBrush((Color)FindResource("InstanceBackgroundColor"));
            BackgroundRectangle.Stroke = new SolidColorBrush(Colors.Black);
            BackgroundRectangle.StrokeThickness = 1;
        }

        #endregion

        #region Animations
        /// <summary>
        /// Erzeugt Storyboard für Übergang normal zu expandiert.
        /// </summary>
        private void CreateNormalToTouchedAnimation()
        {
            animatedNormalToTouchedBrush = new SolidColorBrush((Color)FindResource("InstanceBackgroundColor"));
            this.RegisterName("AnimatedNormalToTouchedBrush", animatedNormalToTouchedBrush);
            ColorAnimation highlightAsTouchedAnimation = new ColorAnimation();
            highlightAsTouchedAnimation.To = (Color)FindResource("InstanceWidgetBackgroundTouched");
            highlightAsTouchedAnimation.Duration = TimeSpan.FromSeconds(0.4);

            //animatedBorderNormalToTouchedBrush = new SolidColorBrush(Colors.Black);
            //this.RegisterName("AnimatedBorderNormalToTouchedBrush", animatedBorderNormalToTouchedBrush);
            //ColorAnimation highlightBorderAsTouchedAnimation = new ColorAnimation();
            //highlightBorderAsTouchedAnimation.To = (Color)FindResource("InstanceWidgetBorderTouchedOrRelative");
            //highlightBorderAsTouchedAnimation.Duration = TimeSpan.FromSeconds(0.4);

            DoubleAnimation strokeAnimation = new DoubleAnimation();
            strokeAnimation.From = 1;
            strokeAnimation.To = 4;
            strokeAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.4));

            Storyboard.SetTargetName(highlightAsTouchedAnimation, "AnimatedNormalToTouchedBrush");
            Storyboard.SetTargetProperty(highlightAsTouchedAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            //Storyboard.SetTargetName(highlightBorderAsTouchedAnimation, "AnimatedBorderNormalToTouchedBrush");
            //Storyboard.SetTargetProperty(highlightBorderAsTouchedAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard.SetTargetName(strokeAnimation, BackgroundRectangle.Name);
            Storyboard.SetTargetProperty(strokeAnimation, new PropertyPath(Rectangle.StrokeThicknessProperty));
            NormalToTouchedStoryboard = new Storyboard();
            NormalToTouchedStoryboard.Children.Add(highlightAsTouchedAnimation);
            //NormalToTouchedStoryboard.Children.Add(highlightBorderAsTouchedAnimation);
            NormalToTouchedStoryboard.Children.Add(strokeAnimation);
        }

        /// <summary>
        /// Erzeugt Storyboard für Übergang.
        /// </summary>
        private void CreateRelativeToTouchedAnimation()
        {
            animatedRelativeToTouchedBrush = new SolidColorBrush((Color)FindResource("InstanceBackgroundColor"));
            this.RegisterName("AnimatedRelativeToTouchedBrush", animatedRelativeToTouchedBrush);
            ColorAnimation highlightAsTouchedAnimation = new ColorAnimation();
            highlightAsTouchedAnimation.To = (Color)FindResource("InstanceWidgetBackgroundTouched");
            highlightAsTouchedAnimation.Duration = TimeSpan.FromSeconds(0.4);

            Storyboard.SetTargetName(highlightAsTouchedAnimation, "AnimatedRelativeToTouchedBrush");
            Storyboard.SetTargetProperty(highlightAsTouchedAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            RelativeToTouchedStoryboard = new Storyboard();
            RelativeToTouchedStoryboard.Children.Add(highlightAsTouchedAnimation);
            //RelativeToTouchedStoryboard.Completed += delegate(System.Object o, System.EventArgs e)
            //    { _isRelative = false; };
        }

        /// <summary>
        /// Erzeugt Storyboard für Übergang.
        /// </summary>
        private void CreateNormalToRelativeAnimation()
        {
            //animatedBorderNormalToRelativeBrush = new SolidColorBrush(Colors.Black);
            //this.RegisterName("AnimatedBorderNormalToRelativeBrush", animatedBorderNormalToRelativeBrush);
            //ColorAnimation highlightBorderAsRelativeAnimation = new ColorAnimation();
            //highlightBorderAsRelativeAnimation.To = (Color)FindResource("InstanceWidgetBorderTouchedOrRelative");
            //highlightBorderAsRelativeAnimation.Duration = TimeSpan.FromSeconds(0.4);

            DoubleAnimation strokeAnimation = new DoubleAnimation();
            strokeAnimation.From = 1;
            strokeAnimation.To = 4;
            strokeAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.4));

            //Storyboard.SetTargetName(highlightBorderAsRelativeAnimation, "AnimatedBorderNormalToRelativeBrush");
            //Storyboard.SetTargetProperty(highlightBorderAsRelativeAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard.SetTargetName(strokeAnimation, BackgroundRectangle.Name);
            Storyboard.SetTargetProperty(strokeAnimation, new PropertyPath(Rectangle.StrokeThicknessProperty));
            NormalToRelativeStoryboard = new Storyboard();
            //NormalToRelativeStoryboard.Children.Add(highlightBorderAsRelativeAnimation);
            NormalToRelativeStoryboard.Children.Add(strokeAnimation);
            //NormalToRelativeStoryboard.Completed += delegate(System.Object o, System.EventArgs e)
            //{ _isRelative = true; };
        }

        /// <summary>
        /// Erzeugt Storyboard für Übergang.
        /// </summary>
        private void CreateTouchedToNormalAnimation()
        {
            animatedTouchedToNormalBrush = new SolidColorBrush((Color)FindResource("InstanceWidgetBackgroundTouched"));
            this.RegisterName("AnimatedTouchedToNormalBrush", animatedTouchedToNormalBrush);
            ColorAnimation AsNormalAnimation = new ColorAnimation();
            AsNormalAnimation.To = (Color)FindResource("InstanceBackgroundColor");
            AsNormalAnimation.Duration = TimeSpan.FromSeconds(0.4);

            DoubleAnimation strokeAnimation = new DoubleAnimation();
            strokeAnimation.From = 4;
            strokeAnimation.To = 1;
            strokeAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.4));

            Storyboard.SetTargetName(AsNormalAnimation, "AnimatedTouchedToNormalBrush");
            Storyboard.SetTargetProperty(AsNormalAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            //Storyboard.SetTargetName(BorderAsNormalAnimation, "AnimatedBorderTouchedToNormalBrush");
            //Storyboard.SetTargetProperty(BorderAsNormalAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard.SetTargetName(strokeAnimation, BackgroundRectangle.Name);
            Storyboard.SetTargetProperty(strokeAnimation, new PropertyPath(Rectangle.StrokeThicknessProperty));
            TouchedToNormalStoryboard = new Storyboard();
            TouchedToNormalStoryboard.Children.Add(AsNormalAnimation);
            //TouchedToNormalStoryboard.Children.Add(BorderAsNormalAnimation);
            TouchedToNormalStoryboard.Children.Add(strokeAnimation);
            TouchedToNormalStoryboard.Completed += delegate(System.Object o, System.EventArgs e)
            {
                BackgroundRectangle.Stroke = new SolidColorBrush(Colors.Black);
            };
        }

        /// <summary>
        /// Erzeugt Storyboard für Übergang.
        /// </summary>
        private void CreateTouchedToRelativeAnimation()
        {
            animatedTouchedToRelativeBrush = new SolidColorBrush((Color)FindResource("InstanceWidgetBackgroundTouched"));
            this.RegisterName("AnimatedTouchedToRelativeBrush", animatedTouchedToRelativeBrush);
            ColorAnimation AsNormalAnimation = new ColorAnimation();
            AsNormalAnimation.To = (Color)FindResource("InstanceBackgroundColor");
            AsNormalAnimation.Duration = TimeSpan.FromSeconds(0.4);

            Storyboard.SetTargetName(AsNormalAnimation, "AnimatedTouchedToRelativeBrush");
            Storyboard.SetTargetProperty(AsNormalAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            TouchedToRelativeStoryboard = new Storyboard();
            TouchedToRelativeStoryboard.Children.Add(AsNormalAnimation);
            //TouchedToRelativeStoryboard.Completed += delegate(System.Object o, System.EventArgs e)
            //    { _isRelative = true; };
        }

        /// <summary>
        /// Erzeugt Storyboard für Übergang.
        /// </summary>
        private void CreateRelativeToNormalAnimation()
        {
            DoubleAnimation strokeAnimation = new DoubleAnimation();
            strokeAnimation.From = 4;
            strokeAnimation.To = 1;
            strokeAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.4));

            //Storyboard.SetTargetName(BorderAsNormalAnimation, "AnimatedBorderRelativeToNormalBrush");
            //Storyboard.SetTargetProperty(BorderAsNormalAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard.SetTargetName(strokeAnimation, BackgroundRectangle.Name);
            Storyboard.SetTargetProperty(strokeAnimation, new PropertyPath(Rectangle.StrokeThicknessProperty));
            RelativeToNormalStoryboard = new Storyboard();
            //RelativeToNormalStoryboard.Children.Add(BorderAsNormalAnimation);
            RelativeToNormalStoryboard.Children.Add(strokeAnimation);
            RelativeToNormalStoryboard.Completed += delegate(System.Object o, System.EventArgs e)
            {
                BackgroundRectangle.Stroke = new SolidColorBrush(Colors.Black);
            };
        }

        /// <summary>
        /// Erzeugt und startet Storyboard für Expansion.
        /// </summary>
        private void CreateExpandStoryBoard()
        {
            GridLengthAnimation column2Expansion = new GridLengthAnimation();
            column2Expansion.From = MainGrid.ColumnDefinitions[2].Width;
            column2Expansion.To = new GridLength(OriginalCol2Width);
            column2Expansion.Duration = TimeSpan.FromSeconds(0.4);


            DoubleAnimation mainGridWidthExpansion = new DoubleAnimation();
            mainGridWidthExpansion.From = MainGrid.Width;
            mainGridWidthExpansion.To = OriginalMainGridWidth;
            mainGridWidthExpansion.Duration = new Duration(TimeSpan.FromSeconds(0.4));
            
            DoubleAnimation mainGridWidthExpansion2 = new DoubleAnimation();
            mainGridWidthExpansion2.From = MainGrid.Width;
            mainGridWidthExpansion2.To = OriginalMainGridWidth;
            mainGridWidthExpansion2.Duration = new Duration(TimeSpan.FromSeconds(0.4));

            ThicknessAnimation moveAnimation = new ThicknessAnimation();
            moveAnimation.Duration = TimeSpan.FromSeconds(0.2);
            moveAnimation.From = new Thickness(0, 0, 15, 0);
            moveAnimation.To = new Thickness(0, 0, 0, 0);

            ThicknessAnimation moveAnimation2 = new ThicknessAnimation();
            moveAnimation2.Duration = TimeSpan.FromSeconds(0.2);
            moveAnimation2.From = new Thickness(0, 0, 15, 0);
            moveAnimation2.To = new Thickness(0, 0, 0, 0);

            Storyboard expandStoryboard = new Storyboard();

            expandStoryboard.Children.Add(column2Expansion);
            expandStoryboard.Children.Add(mainGridWidthExpansion);
            expandStoryboard.Children.Add(mainGridWidthExpansion2);
            expandStoryboard.Children.Add(moveAnimation);
            expandStoryboard.Children.Add(moveAnimation2);

            Storyboard.SetTargetName(column2Expansion, MainGrid.ColumnDefinitions[2].Name);
            Storyboard.SetTargetProperty(column2Expansion, new PropertyPath(ColumnDefinition.WidthProperty));

            Storyboard.SetTargetName(mainGridWidthExpansion2, MainGrid.Name);
            Storyboard.SetTargetProperty(mainGridWidthExpansion2, new PropertyPath(Grid.WidthProperty));

            Storyboard.SetTargetName(mainGridWidthExpansion, this.Name);
            Storyboard.SetTargetProperty(mainGridWidthExpansion, new PropertyPath(System.Windows.Controls.UserControl.WidthProperty));

            Storyboard.SetTargetName(moveAnimation, ChangeStateButton.Name);
            Storyboard.SetTargetName(moveAnimation2, ShowInfosButton.Name);
            Storyboard.SetTargetProperty(moveAnimation, new PropertyPath(SurfaceButton.MarginProperty));
            Storyboard.SetTargetProperty(moveAnimation2, new PropertyPath(SurfaceButton.MarginProperty));

            //expandStoryboard.Completed += new EventHandler(expandStoryboard_Completed);
            expandStoryboard.Begin(this);
        }

        /// <summary>
        /// Erzeugt und startet Storyboard für Reduktion.
        /// </summary>
        private void CreateReduceStoryBoard()
        {
            GridLengthAnimation column2Reduction = new GridLengthAnimation();
            column2Reduction.From =  MainGrid.ColumnDefinitions[2].Width;
            column2Reduction.To = new GridLength(0);
            column2Reduction.Duration = TimeSpan.FromSeconds(0.4);


            DoubleAnimation mainGridWidthReduction = new DoubleAnimation();
            mainGridWidthReduction.From = MainGrid.Width;
            mainGridWidthReduction.To = SmallMainGridWidth;
            mainGridWidthReduction.Duration = new Duration(TimeSpan.FromSeconds(0.4));

            DoubleAnimation mainGridWidthReduction2 = new DoubleAnimation();
            mainGridWidthReduction2.From = MainGrid.Width;
            mainGridWidthReduction2.To = SmallMainGridWidth;
            mainGridWidthReduction2.Duration = new Duration(TimeSpan.FromSeconds(0.4));

            ThicknessAnimation moveAnimation = new ThicknessAnimation();
            moveAnimation.Duration = TimeSpan.FromSeconds(0.2);
            moveAnimation.From = new Thickness(0, 0, 0, 0);
            moveAnimation.To = new Thickness(0, 0, 15, 0);

            ThicknessAnimation moveAnimation2 = new ThicknessAnimation();
            moveAnimation2.Duration = TimeSpan.FromSeconds(0.2);
            moveAnimation2.From = new Thickness(0, 0, 0, 0);
            moveAnimation2.To = new Thickness(0, 0, 15, 0);

            Storyboard reduceStoryboard = new Storyboard();

            reduceStoryboard.Children.Add(column2Reduction);
            reduceStoryboard.Children.Add(mainGridWidthReduction);
            reduceStoryboard.Children.Add(mainGridWidthReduction2);
            reduceStoryboard.Children.Add(moveAnimation);
            reduceStoryboard.Children.Add(moveAnimation2);

            Storyboard.SetTargetName(column2Reduction, MainGrid.ColumnDefinitions[2].Name);
            Storyboard.SetTargetProperty(column2Reduction, new PropertyPath(ColumnDefinition.WidthProperty));

            Storyboard.SetTargetName(mainGridWidthReduction2, MainGrid.Name);
            Storyboard.SetTargetProperty(mainGridWidthReduction2, new PropertyPath(Grid.WidthProperty));

            Storyboard.SetTargetName(mainGridWidthReduction, this.Name);
            Storyboard.SetTargetProperty(mainGridWidthReduction, new PropertyPath(System.Windows.Controls.UserControl.WidthProperty));

            Storyboard.SetTargetName(moveAnimation, ChangeStateButton.Name);
            Storyboard.SetTargetName(moveAnimation2, ShowInfosButton.Name);
            Storyboard.SetTargetProperty(moveAnimation, new PropertyPath(SurfaceButton.MarginProperty));
            Storyboard.SetTargetProperty(moveAnimation2, new PropertyPath(SurfaceButton.MarginProperty));

            // reduceStoryboard.Completed += new EventHandler(reduceStoryboard_Completed);
            reduceStoryboard.Begin(this);
        }

        /// <summary>
        /// eigene Animationsklasse um das Animieren von Spaltenbreiten zu ermöglichen.
        /// </summary>
        internal class GridLengthAnimation : AnimationTimeline
        {
            public override Type TargetPropertyType
            {
                get
                {
                    return typeof(GridLength);
                }
            }
            protected override System.Windows.Freezable CreateInstanceCore()
            {
                return new GridLengthAnimation();
            }
            static GridLengthAnimation()
            {
                FromProperty = DependencyProperty.Register("From", typeof(GridLength),
                    typeof(GridLengthAnimation));

                ToProperty = DependencyProperty.Register("To", typeof(GridLength),
                    typeof(GridLengthAnimation));
            }
            public static readonly DependencyProperty FromProperty;
            public GridLength From
            {
                get
                {
                    return (GridLength)GetValue(GridLengthAnimation.FromProperty);
                }
                set
                {
                    SetValue(GridLengthAnimation.FromProperty, value);
                }
            }
            public static readonly DependencyProperty ToProperty;
            public GridLength To
            {
                get
                {
                    return (GridLength)GetValue(GridLengthAnimation.ToProperty);
                }
                set
                {
                    SetValue(GridLengthAnimation.ToProperty, value);
                }
            }
            public override object GetCurrentValue(object defaultOriginValue,
               object defaultDestinationValue, AnimationClock animationClock)
            {
                double fromVal = ((GridLength)GetValue(GridLengthAnimation.FromProperty)).Value;
                double toVal = ((GridLength)GetValue(GridLengthAnimation.ToProperty)).Value;

                if (fromVal > toVal)
                {
                    return new GridLength((1 - animationClock.CurrentProgress.Value) *
                        (fromVal - toVal) + toVal, GridUnitType.Pixel);
                }
                else
                {
                    return new GridLength(animationClock.CurrentProgress.Value *
                        (toVal - fromVal) + fromVal, GridUnitType.Pixel);
                }
            }
        }
        #endregion

	}
}