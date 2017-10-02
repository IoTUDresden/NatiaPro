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
    /// Interaktionslogik für ModelStatisticProtocol.xaml
    /// Zeigt Fake-History für Prozessinstanzschritte an.
    /// </summary>
    public partial class InstanceStepProtocol : UserControl
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public InstanceStepProtocol()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Adaptiert Inhalt.
        /// </summary>
        /// <param name="stepname">Name des betreffenden Instanzschrittes</param>
        /// <param name="starttime">Startzeit des Instanzschrittes</param>
        /// <param name="endtime">Endzeit des Instanzschrittes</param>
        /// <param name="devicename">Ausführungsort des Instanzschrittes</param>
        /// <param name="parameterToShow">Anzahl von Daten-Parametern des Instanzschrittes</param>
        /// <param name="parameter1">Titel eines Parameters</param>
        /// <param name="value1">Wert eines Parameters</param>
        public void AdaptContent(String stepname, String starttime, String endtime, string devicename, bool parameterToShow, string parameter1, string value1)
        {
            StepNameText.Text = stepname;
            StartTimeText.Text = starttime;
            EndTimeText.Text = endtime;
            DeviceText.Text = devicename;
            if (parameterToShow)
            {
                Parameter1Text.Text = parameter1;
                Value1Text.Text = value1;
            }
            else
            {
                ParameterTitleText.Text = "";
                ValueTitleText.Text = "";
                Parameter1Text.Text = "";
                Value1Text.Text = "";
            }
        }
    }
}